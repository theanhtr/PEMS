<template>
  <div class="employee-content">
    <div class="page__action">
      <div class="page__action-left">
        <ttanh-button
          type="dropdown"
          width="180px"
          borderRadius="var(--border-radius-default)"
          :dataDropdown="batchExecutionDataDropdown"
          :border="batchExecutionDisable ? '' : '2px solid black'"
          :disable="batchExecutionDisable"
          :tabindex="-1"
          @clickItem="clickItemBatchExecution"
          >{{
            $t("employeeSubsystem.employeeContent.batchExecution")
          }}</ttanh-button
        >
      </div>
      <div class="page__action-right">
        <ttanh-search-input
          :width="tableSearchFocus ? '200px' : '100px'"
          @input-focus="tableSearchFocus = true"
          @input-blur="tableSearchFocus = false"
          :class="{ animated: tableSearchFocus }"
          v-model="searchText"
          ref="searchTextTable"
          :placeholder="
            $t('employeeSubsystem.employeeContent.searchPlaceHolder')
          "
          :tooltip="$t('employeeSubsystem.employeeContent.searchInputTooltip')"
        />
        <ttanh-icon
          :icon="
            'page__reload--' +
            (pageButtonHover['page__reload'] ? 'black' : 'grey')
          "
          :tooltip="$t('employeeSubsystem.employeeContent.reloadTooltip')"
          @mouseenter="pageButtonHover['page__reload'] = true"
          @mouseleave="pageButtonHover['page__reload'] = false"
          @click="reloadDataWithSelectedRows"
        />
        <ttanh-icon
          :icon="
            'page__excel--' +
            (pageButtonHover['page__excel'] ? 'black' : 'grey')
          "
          :tooltip="$t('employeeSubsystem.employeeContent.exportExcelTooltip')"
          @mouseenter="pageButtonHover['page__excel'] = true"
          @mouseleave="pageButtonHover['page__excel'] = false"
          @click="exportToExcelWithSearchText"
        />
        <ttanh-icon
          :icon="
            'page__setting--' +
            (pageButtonHover['page__setting'] ? 'black' : 'grey')
          "
          :tooltip="
            $t('employeeSubsystem.employeeContent.layoutSettingTooltip')
          "
          @mouseenter="pageButtonHover['page__setting'] = true"
          @mouseleave="pageButtonHover['page__setting'] = false"
          @click="isShowLayoutSetting = true"
        />
        <ttanh-button
          type="combo"
          @clickBtnContainer="showAddEmployeePopup"
          :dataDropdown="addDataDropdown"
          :tabindex="-1"
          :tooltip="$t('employeeSubsystem.employeeContent.insertTooltip')"
          @clickItem="handleDropdownInsertButton"
          >{{ $t("common.button.add") }}</ttanh-button
        >
      </div>
    </div>
    <div class="page__table">
      <ttanh-table
        ref="ttanhTable"
        :columnsInfo="employeeColumnsInfo"
        :rowsData="computedEmployees"
        :selectedRows="computedSelectedEmployees"
        :noData="computedNoData"
        @checked-all="checkedAllRow"
        @unchecked-all="uncheckedAllRow"
        @checked-row="checkedRow"
        @unchecked-row="uncheckedRow"
        @doubleClickRow="openFormUpdate"
        @clickFixBtn="openFormUpdate"
        @clickContextDeleteBtn="openConfirmDeletePopup"
        @clickContextDuplicateBtn="openFormDuplicate"
        @resizeColumn="resizeEmployeeColumn"
      />
    </div>
    <div class="page__footer">
      <ttanh-paging
        v-if="!this.noData"
        v-model="pagingData"
        @reloadData="reloadData"
      />
    </div>

    <AddEmployeePopup
      v-if="isShowAddEmployeePopup"
      :dataUpdate="dataUpdate"
      @clickCancelBtn="isShowAddEmployeePopup = false"
      @reloadData="reloadData"
      ref="addEmployeePopup"
    />

    <EmployeeLayoutSetting
      v-if="isShowLayoutSetting"
      ref="employeeLayoutSetting"
      :employeeColumnsInfoProp="employeeColumnsInfo"
      @close-click="isShowLayoutSetting = false"
      @set-default-click="SetDefaultLayout"
      @store-click="UpdateLayout"
    />

    <ttanh-delete-popup
      :titleText="computedDeletePopupText"
      v-if="isShowConfirmDeletePopup || isShowConfirmDeleteMultiplePopup"
      @no-click="
        isShowConfirmDeletePopup
          ? noDeleteBtnClick()
          : noDeleteMultipleEmployee()
      "
      @yes-click="
        isShowConfirmDeletePopup
          ? yesDeleteBtnClick()
          : yesDeleteMultipleEmployee()
      "
    />

    <ttanh-loading-spinner v-if="isLoading" size="large" />
  </div>
</template>

<script>
import EmployeeService from "@/service/EmployeeService.js";
import EmployeeLayoutService from "@/service/EmployeeLayoutService.js";
import EmployeeLayoutSetting from "./employee-layout-setting/EmployeeLayoutSetting.vue";
import AddEmployeePopup from "./AddEmployeePopup.vue";
import { CommonErrorHandle } from "@/helper/error-handle";
import { findIndexByAttribute, sortArrayByAttribute } from "@/helper/common.js";
import { formatToNumber } from "@/helper/textfield-format-helper.js";
import { debounce } from "@/helper/debounce.js";
import { isProxy, toRaw } from "vue";

export default {
  name: "EmployeeContent",
  components: {
    AddEmployeePopup,
    EmployeeLayoutSetting,
  },
  data() {
    return {
      employees: [],

      /* lưu dữ id các nhân viên đã được chọn */
      selectedEmployees: [],

      employeeColumnsInfo: [],

      /* thông tin cột thuần được gửi từ api đã sắp xếp */
      employeeColumnsInfoRaw: [],

      isLoading: false,
      /* các biến để xác định trạng thái trên page_action */
      tableSearchFocus: false,
      pageButtonHover: {
        page__setting: false,
        page__reload: false
      },

      /* biến xác định nút "Thực hiện hàng loạt" có disable hay không */
      batchExecutionDisable: true,

      /* các hành động cho nút "Thực hiện hàng loạt" ở page action */
      batchExecutionDataDropdown: [
        {
          id: "delete",
          title: this.$t(
            "employeeSubsystem.employeeContent.batchExecutionData.delete"
          ),
        },
      ],

      /* các hành động cho combo btn "Thêm" ở page action */
      addDataDropdown: [
        {
          id: "excel",
          title: this.$t(
            "employeeSubsystem.employeeContent.addDataDropdown.importExcel"
          ),
        },
      ],

      /*== các biến sử dụng cho add-employee-popup ==*/
      isShowAddEmployeePopup: false,

      dataUpdate: null,

      /* biến sử dụng cho việc xác nhận xóa */
      isShowConfirmDeletePopup: false,
      employeeCodeDelete: "",
      employeeIdDelete: "",

      isShowConfirmDeleteMultiplePopup: false,

      searchText: "",

      /* biến sử dụng cho phân trang */
      pagingData: {
        pageSize: 10,
        pageNumber: 1,
        totalPage: 0,
        totalRecord: 0,
      },

      // xử lý khi không có dữ liệu trả về
      noData: false,

      //biến sử dụng cho việc thao tác giữ shift khi chọn
      previouslySelectedIndex: -1,

      //biến dùng để phân biệt việc có update thông tin cột khi sử dụng watch không
      isUpdateColumnsInfo: false,

      //biến dùng cho việc tùy chỉnh giao diện
      isShowLayoutSetting: false,
    };
  },

  created() {
    // lấy dữ liệu phân trang được lưu trong local storage
    this.pagingData.pageNumber =
      formatToNumber(localStorage.getItem("pageNumber")) ?? 1;
    this.pagingData.pageSize =
      formatToNumber(localStorage.getItem("pageSize")) ?? 10;

    window.addEventListener("keydown", this.handleKeydown);

    // lấy thông tin cột
    this.getEmployeeColumnsInfo();

    //lấy dữ liệu nhân viên
    this.getEmployees();
  },

  updated() {
    //nếu add popup đang mở thì bỏ sự kiện keydown đi
    if (this.isShowAddEmployeePopup) {
      window.removeEventListener("keydown", this.handleKeydown);
    } else {
      window.addEventListener("keydown", this.handleKeydown);
    }
  },

  unmounted() {
    window.removeEventListener("keydown", this.handleKeydown);
  },

  methods: {
    /**
     * Sắp xếp theo ordernumber và isPin để hiển thị đúng
     * @author: TTANH (04/08/2024)
     */
    sortEmployeeColumnsInfo(columnsInfoTemp) {
      try {
        // sắp xếp theo thứ tự
        columnsInfoTemp = sortArrayByAttribute(
          columnsInfoTemp,
          "OrderNumber",
          false
        );

        //đưa những cột được ghim lên đầu
        columnsInfoTemp = sortArrayByAttribute(columnsInfoTemp, "ColumnIsPin");

        return columnsInfoTemp;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:263 ~ sortEmployeeColumnsInfo ~ error:",
          error
        );
      }
    },

    /**
     * Hàm lấy thông tin cột từ server
     * @author: TTANH (03/08/2024)
     */
    async getEmployeeColumnsInfo() {
      try {
        const res = await EmployeeLayoutService.get();

        if (res.success) {
          let columnsInfoTemp = res.data;

          columnsInfoTemp = this.sortEmployeeColumnsInfo(columnsInfoTemp);

          this.employeeColumnsInfoRaw = columnsInfoTemp;
        } else {
          CommonErrorHandle();
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:241 ~ getEmployeeColumnsInfo ~ error:",
          error
        );
      }
    },

    /**
     * Thực hiện cập nhật layout
     * @author: TTANH (04/08/2024)
     */
    async UpdateLayout() {
      this.isShowLayoutSetting = false;

      await this.updateColumnsInfoToDB(
        this.$refs.employeeLayoutSetting.getEmployeeColumnsInfoUpdate()
      );

      this.reloadData();
    },

    /**
     * Thực hiện lấy lại mẫu mặc định của layout
     * @author: TTANH (04/08/2024)
     */
    async SetDefaultLayout() {
      this.isShowLayoutSetting = false;

      const res = await EmployeeLayoutService.setDefaultLayout();

      if (res) {
      } else {
        CommonErrorHandle();
      }

      this.reloadData();
    },

    /**
     * hàm thực hiện mở thêm nhân viên
     * @author: TTANH (11/07/2024)
     */
    showAddEmployeePopup() {
      this.isShowAddEmployeePopup = true;
      this.dataUpdate = null;
    },

    /**
     * thực hiện get dữ liệu nhân viên khi component được render
     * @author: TTANH (30/06/2024)
     */
    async getEmployees() {
      try {
        const res = await EmployeeService.filter({
          pageSize: this.pagingData.pageSize,
          pageNumber: this.pagingData.pageNumber,
          searchText: this.searchText.trim(),
        });

        if (res.success) {
          if (res.data.Data.length != 0) {
            this.employees = res.data.Data;
            this.pagingData.totalPage = res.data.TotalPage;
            this.pagingData.totalRecord = res.data.TotalRecord;
            this.pagingData.pageNumber = res.data.CurrentPage;
            this.noData = false;
          } else {
            this.noData = true;
          }
        } else {
          CommonErrorHandle();
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:116 ~ getEmployees ~ error:",
          error
        );
      }
    },

    /**
     * sự kiện click vào các item con của "Thực hiện hàng loạt":
     * { id: "delete", title: "Xóa" },
     * { id: "merge", title: "Gộp" },
     * @author: TTANH (17/07/2024)
     * @param {string} idItem id của nút được click
     */
    clickItemBatchExecution(idItem) {
      if (idItem === "delete") {
        this.isShowConfirmDeleteMultiplePopup = true;
      }
    },

    /**
     * bỏ lệnh xóa nhiều nhân viên
     * @author: TTANH (31/07/2024)
     */
    noDeleteMultipleEmployee() {
      this.isShowConfirmDeleteMultiplePopup = false;
    },

    /**
     * xóa nhiều nhân viên
     * @author: TTANH (17/07/2024)
     */
    async yesDeleteMultipleEmployee() {
      var dataSendApi = null;

      if (isProxy(this.selectedEmployees)) {
        dataSendApi = toRaw(this.selectedEmployees);
      } else {
        dataSendApi = this.selectedEmployees;
      }

      this.isLoading = true;

      const res = await EmployeeService.deleteMultiple(dataSendApi);

      this.isLoading = false;

      if (res.success) {
        this.$store.commit("addToast", {
          type: "success",
          text: this.$t("successHandle.employeeSubsystem.deleteMultiple", {
            count: res.data,
          }),
        });

        this.selectedEmployees = [];
        this.isShowConfirmDeleteMultiplePopup = false;

        this.reloadData();
      } else {
        CommonErrorHandle();
      }
    },

    /**
     * cập nhật lại employees mới
     * @author: TTANH (03/07/2024)
     */
    reloadData() {
      try {
        this.getEmployeeColumnsInfo();

        this.previouslySelectedIndex = -1;
        this.employees = [];
        this.getEmployees();
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:465 ~ reloadData ~ error:",
          error
        );
      }
    },

    /**
     * bỏ hết dữ liệu đã chọn khi ấn vào nút "Lấy lại dữ liệu"
     * @author: TTANH (03/07/2024)
     */
    reloadDataWithSelectedRows() {
      try {
        this.selectedEmployees = [];
        this.reloadData();
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:282 ~ reloadDataWithSelectedRows ~ error:",
          error
        );
      }
    },

    /**
     * xuất dữ liệu đang tìm kiếm ra file excel
     * @author: TTANH (16/07/2024)
     */
    async exportToExcelWithSearchText() {
      try {
        this.isLoading = true;

        const res = await EmployeeService.getExcel({
          searchText: this.searchText,
        });

        this.isLoading = false;

        if (res.success) {
          this.$store.commit("addToast", {
            type: "success",
            text: this.$t("successHandle.employeeSubsystem.exportExcel"),
          });
        } else {
          CommonErrorHandle();
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:346 ~ exportToExcelWithSearchText ~ error:",
          error
        );
      }
    },

    /**
     * hàm xử lý việc ấn vào item của dropdown nút "Thêm"
     * @author: TTANH (19/07/2024)
     * @param {string} id id của item chọn
     */
    handleDropdownInsertButton(id) {
      if (id === "excel") {
        this.$router.push("/app/employee/import");
      }
    },

    /**
     * thêm một id vào mảng dòng đã chọn
     * @author: TTANH (11/07/2024)
     * @param {string} rowId id của dòng được chọn
     */
    addSelectedRow(rowId) {
      let index = findIndexByAttribute(this.selectedEmployees, "", rowId);

      if (index === -1) {
        this.selectedEmployees.push(rowId);
      }
    },

    /**
     * xóa một id vào mảng dòng đã chọn
     * @author: TTANH (11/07/2024)
     * @param {string} rowId id của dòng được chọn
     */
    deleteSelectedRow(rowId) {
      let index = findIndexByAttribute(this.selectedEmployees, "", rowId);

      if (index !== -1) {
        this.selectedEmployees.splice(index, 1);
      }
    },

    /**
     * xử lý khi chọn checkbox ở header
     * @author: TTANH (27/06/2024)
     */
    checkedAllRow() {
      try {
        this.employees.forEach((employee) => {
          this.addSelectedRow(employee.EmployeeId);
        });
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:463 ~ checkedAllRow ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi bỏ chọn checkbox ở header
     * @author: TTANH (27/06/2024)
     */
    uncheckedAllRow() {
      try {
        this.employees.forEach((employee) => {
          this.deleteSelectedRow(employee.EmployeeId);
        });
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:475 ~ uncheckedAllRow ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi chọn checkbox ở 1 row
     * @author: TTANH (27/06/2024)
     * @param {string} rowId: id của record được chọn
     */
    checkedRow(rowId) {
      try {
        let indexNewChecked = findIndexByAttribute(
          this.employees,
          "EmployeeId",
          rowId
        );

        if (event.shiftKey) {
          event.preventDefault();

          if (this.previouslySelectedIndex === -1) {
            this.addSelectedRow(rowId);
          } else {
            if (this.previouslySelectedIndex > indexNewChecked) {
              for (
                let index = indexNewChecked;
                index <= this.previouslySelectedIndex;
                index++
              ) {
                const employee = this.employees[index];

                this.addSelectedRow(employee.EmployeeId);
              }
            } else if (this.previouslySelectedIndex < indexNewChecked) {
              for (
                let index = this.previouslySelectedIndex;
                index <= indexNewChecked;
                index++
              ) {
                const employee = this.employees[index];

                this.addSelectedRow(employee.EmployeeId);
              }
            } else {
            }
          }
        } else {
          this.addSelectedRow(rowId);
        }

        this.previouslySelectedIndex = indexNewChecked;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:492 ~ uncheckedAllRow ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi bỏ chọn checkbox ở 1 row
     * @author: TTANH (27/06/2024)
     * @param {string} rowId: id của record được bỏ chọn
     */
    uncheckedRow(rowId) {
      try {
        let indexNewChecked = findIndexByAttribute(
          this.employees,
          "EmployeeId",
          rowId
        );

        if (event.shiftKey) {
          event.preventDefault();

          if (this.previouslySelectedIndex === -1) {
            this.deleteSelectedRow(rowId);
          } else {
            if (this.previouslySelectedIndex > indexNewChecked) {
              for (
                let index = indexNewChecked;
                index <= this.previouslySelectedIndex;
                index++
              ) {
                const employee = this.employees[index];

                this.deleteSelectedRow(employee.EmployeeId);
              }
            } else if (this.previouslySelectedIndex < indexNewChecked) {
              for (
                let index = this.previouslySelectedIndex;
                index <= indexNewChecked;
                index++
              ) {
                const employee = this.employees[index];

                this.deleteSelectedRow(employee.EmployeeId);
              }
            } else {
            }
          }
        } else {
          this.deleteSelectedRow(rowId);
        }

        this.previouslySelectedIndex = indexNewChecked;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:492 ~ uncheckedAllRow ~ error:",
          error
        );
      }
    },

    /**
     * mở form update
     * @param {string} rowId id của đối tượng muốn update
     */
    openFormUpdate(rowId) {
      try {
        let indexRow = findIndexByAttribute(
          this.employees,
          "EmployeeId",
          rowId
        );

        this.isShowAddEmployeePopup = true;
        this.dataUpdate = this.employees[indexRow];
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:529 ~ openFormUpdate ~ error:",
          error
        );
      }
    },

    /**
     * mở form xác nhận xóa
     * @author: TTANH (01/07/2024)
     * @param {string} id id của bản ghi cần xóa
     */
    openConfirmDeletePopup(id) {
      try {
        let index = findIndexByAttribute(this.employees, "EmployeeId", id);

        if (index !== -1) {
          this.employeeCodeDelete = this.employees[index].EmployeeCode;
          this.employeeIdDelete = id;
          this.isShowConfirmDeletePopup = true;
        } else {
          this.$store.commit("addToast", {
            type: "error",
            text: this.$t("errorHandle.employeeSubsystem.notFoundEmployee"),
          });

          this.reloadData();
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:351 ~ openConfirmDeletePopup ~ error:",
          error
        );
      }
    },

    /**
     * mở form nhân bản
     * @param {string} rowId id của đối tượng muốn nhân bản
     */
    openFormDuplicate(rowId) {
      try {
        let indexRow = findIndexByAttribute(
          this.employees,
          "EmployeeId",
          rowId
        );

        this.isShowAddEmployeePopup = true;
        this.dataUpdate = this.employees[indexRow];

        this.$nextTick(() => {
          // thay đổi trạng thái form thành thêm mới
          this.$refs.addEmployeePopup.changeFormModeToAdd();

          // lấy mã code mới
          this.$refs.addEmployeePopup.getNewEmployeeCode();
        });
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:529 ~ openFormUpdate ~ error:",
          error
        );
      }
    },

    /**
     * đóng form xác nhận xóa
     * @author: TTANH (01/07/2024)
     */
    closeConfirmDeletePopup() {
      try {
        this.employeeCodeDelete = "";
        this.employeeIdDelete = "";
        this.isShowConfirmDeletePopup = false;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:386 ~ closeConfirmDeletePopup ~ error:",
          error
        );
      }
    },

    /**
     * hủy xóa
     * @author: TTANH (01/07/2024)
     */
    noDeleteBtnClick() {
      try {
        this.closeConfirmDeletePopup();
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:401 ~ noDeleteBtnClick ~ error:",
          error
        );
      }
    },

    /**
     * xác nhận xóa
     * @author: TTANH (01/07/2024)
     */
    yesDeleteBtnClick() {
      try {
        this.deleteSelectedRow(this.employeeIdDelete);
        this.deleteRecord();
        this.closeConfirmDeletePopup();
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:416 ~ yesDeleteBtnClick ~ error:",
          error
        );
      }
    },

    /**
     * thực hiện xóa 1 bản ghi
     * @author: TTANH (01/07/2024)
     */
    async deleteRecord() {
      try {
        this.isLoading = true;
        const employeeCode = this.employeeCodeDelete;
        const res = await EmployeeService.delete(this.employeeIdDelete);

        if (res.success) {
          this.$store.commit("addToast", {
            type: "success",
            text: this.$t("successHandle.employeeSubsystem.delete", {
              code: employeeCode,
            }),
          });

          this.reloadData();
        } else {
          if (res.errorCode === this.$_TTANHEnum.ERROR_CODE.NOT_FOUND_DATA) {
            this.$store.commit("addToast", {
              type: "error",
              text: res.userMsg,
            });
          } else {
            CommonErrorHandle();
          }
        }

        this.isLoading = false;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:582 ~ deleteRecord ~ error:",
          error
        );
      }
    },

    /**
     * thực hiện thay đổi kích thước của cột
     * @author: TTANH (04/07/2024)
     */
    resizeEmployeeColumn(index, resizeWidth) {
      try {
        this.isUpdateColumnsInfo = true;
        this.employeeColumnsInfo[index].size = resizeWidth;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:524 ~ resizeEmployeeColumn ~ error:",
          error
        );
      }
    },

    /**
     * xử lý các phím tắt
     * @author: TTANH (11/07/2024)
     */
    handleKeydown(event) {
      if (event.keyCode === this.$_TTANHEnum.KEY_CODE.INSERT && event.ctrlKey) {
        this.$router.push("/app/employee/import");
      } else if (event.keyCode === this.$_TTANHEnum.KEY_CODE.INSERT) {
        this.showAddEmployeePopup();
      } else if (
        event.keyCode === this.$_TTANHEnum.KEY_CODE.F &&
        event.shiftKey &&
        event.ctrlKey
      ) {
        this.$refs.searchTextTable.focus();
      }
    },

    /**
     * chuyển đổi 1 cột thành dạng ttanh table có thể hiểu
     * @author: TTANH (03/08/2024)
     * @param {Object} rawData dữ liệu thông tin cột raw
     * @returns 1 object đã được chuyển đổi
     */
    mapColumnInfoFromRawToCode(rawData) {
      let langCode = this.$store.state.langCode;

      let tempMap = {};

      tempMap.id = rawData.ServerColumnName;
      tempMap.name = rawData[`${langCode}ClientColumnName`];
      tempMap.size = rawData.ColumnWidth;
      tempMap.textAlign = rawData.ColumnTextAlign;
      tempMap.format = rawData.ColumnFormat;
      tempMap.isShow = rawData.ColumnIsShow;
      tempMap.isPin = rawData.ColumnIsPin;
      tempMap.tooltip = rawData[`${langCode}Tooltip`];
      tempMap.clientColumnNameDefault =
        rawData[`${langCode}ClientColumnNameDefault`];
      tempMap.orderNumber = rawData.OrderNumber;

      return tempMap;
    },

    /**
     * chuyển đổi mảng cột thành dạng ttanh table có thể hiểu
     * @author: TTANH (03/08/2024)
     * @param {Array} rawsData dữ liệu thông tin cột raw
     * @returns 1 mảng object đã được chuyển đổi
     */
    mapColumnsInfoFromRawToCode(rawsData) {
      let tempMapArray = [];

      rawsData.forEach((e) => {
        let tempMap = this.mapColumnInfoFromRawToCode(e);

        tempMapArray.push(tempMap);
      });

      return tempMapArray;
    },

    /**
     * chuyển đổi dữ liệu cập nhật sang dữ liệu truyền cho api để cập nhật
     * @author: TTANH (03/08/2024)
     * @param {Object} codeData dữ liệu thông tin cột
     * @returns 1 object đã được chuyển đổi
     */
    mapColumnInfoFromCodeToRawForUpdate(codeData) {
      let langCode = this.$store.state.langCode;

      let indexInRaw = findIndexByAttribute(
        this.employeeColumnsInfoRaw,
        "ServerColumnName",
        codeData.id
      );

      let employeeColumnInfoRaw = this.employeeColumnsInfoRaw[indexInRaw];

      let tempMap = {};

      tempMap.EmployeeLayoutId = employeeColumnInfoRaw.EmployeeLayoutId;
      tempMap.viClientColumnName = employeeColumnInfoRaw.viClientColumnName;
      tempMap.enClientColumnName = employeeColumnInfoRaw.enClientColumnName;
      tempMap.OrderNumber = employeeColumnInfoRaw.OrderNumber;

      tempMap[`${langCode}ClientColumnName`] = codeData.name;
      tempMap.ColumnWidth = codeData.size;
      tempMap.ColumnIsShow = codeData.isShow;
      tempMap.ColumnIsPin = codeData.isPin;
      return tempMap;
    },

    /**
     * chuyển đổi dữ liệu cập nhật sang dữ liệu truyền cho api để cập nhật
     * @author: TTANH (03/08/2024)
     * @param {Array} codesData dữ liệu thông tin cột
     * @returns 1 mảng object đã được chuyển đổi
     */
    mapColumnsInfoFromCodeToRawForUpdate(codesData) {
      let tempMapArray = [];

      codesData.forEach((e) => {
        let tempMap = this.mapColumnInfoFromCodeToRawForUpdate(e);

        tempMapArray.push(tempMap);
      });

      return tempMapArray;
    },

    /**
     * thực hiện cập nhật thông tin cột trên db
     */
    async updateColumnsInfoToDB(newData) {
      let datasUpdate = this.mapColumnsInfoFromCodeToRawForUpdate(newData);

      const res = await EmployeeLayoutService.updateMultiple(datasUpdate);

      if (res.success) {
      } else {
        CommonErrorHandle();
      }
    },
  },
  computed: {
    /* thêm id để phân biệt các phần tử với nhau */
    computedEmployees() {
      try {
        let haveIdEmployees = [];

        this.employees.forEach((employee, index) => {
          let id = employee.EmployeeId;
          haveIdEmployees.push({
            id,
            ...employee,
          });
        });

        return haveIdEmployees;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:457 ~ computedEmployees ~ error:",
          error
        );
      }
    },

    computedSelectedEmployees() {
      if (this.selectedEmployees.length <= 1) {
        this.batchExecutionDisable = true;
      } else {
        this.batchExecutionDisable = false;
      }
      return this.selectedEmployees;
    },

    computedNoData() {
      return this.noData;
    },

    computedDeletePopupText() {
      if (this.isShowConfirmDeletePopup) {
        return this.$t("employeeSubsystem.employeeContent.deletePopupTitle", {
          code: this.employeeCodeDelete,
        });
      } else if (this.isShowConfirmDeleteMultiplePopup) {
        return this.$t(
          "employeeSubsystem.employeeContent.deleteMultiplePopupTitle",
          { count: this.selectedEmployees.length }
        );
      } else {
        return "";
      }
    },
  },
  watch: {
    searchText: debounce(function () {
      this.pagingData.pageNumber = 1;
      this.reloadData();
    }, 500),

    pagingData: {
      handler: function (newValue) {
        localStorage.setItem("pageNumber", newValue.pageNumber);
        localStorage.setItem("pageSize", newValue.pageSize);
      },

      deep: true,
    },

    employeeColumnsInfoRaw(newValue) {
      let tempMapArray = this.mapColumnsInfoFromRawToCode(newValue);
      /**
       * do là việc lấy dữ liệu gây ra thay đổi cho employeeColumnsInfo
       * nên không gọi đến hàm cập nhật
       */
      this.isUpdateColumnsInfo = false;

      this.employeeColumnsInfo = tempMapArray;
    },

    employeeColumnsInfo: {
      handler: debounce(function () {
        if (this.isUpdateColumnsInfo) {
          this.updateColumnsInfoToDB(this.employeeColumnsInfo);
        }
      }, 500),

      deep: true,
    },
  },
};
</script>

<style scoped>
@import url(./employee-content.css);
</style>
