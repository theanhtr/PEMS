<template>
  <div class="Predict-content">
    <div class="page__filter">
      <h1 class="page__filter-title">Bộ lọc</h1>
      <ttanh-separation-line 
            style="
              width: 98%;
              border-top: 2px solid var(--border-color-default);
              margin-bottom: 4px;
            " />
      <div class="page__filter-group page__filter-group-1">
        <ttanh-combobox
          v-model="dataFilter.provinceId"
          ref="provinceId"
          type="single-row"
          labelText="Tỉnh/Thành phố"
          :inputRequired="false"
          @show-combobox="getProvinces"
          :rowsData="dataAddress.provinces"
          class="w1/4"
          tabindex="1"
        />
        <ttanh-combobox
          v-model="dataFilter.districtId"
          ref="districtId"
          type="single-row"
          labelText="Quận/Huyện"
          :inputRequired="false"
          @show-combobox="getDistricts"
          :rowsData="dataAddress.districts"
          class="w1/4"
          tabindex="2"
        />
        <ttanh-combobox
          v-model="dataFilter.wardId"
          ref="wardId"
          type="single-row"
          labelText="Phường/Xã"
          :inputRequired="false"
          @show-combobox="dataAddress.getWards"
          :rowsData="wards"
          class="w1/4"
          tabindex="3"
        />
      </div>
      <div class="page__filter-group page__filter-group-2">
        <VueDatePicker
          v-model="dataFilter.dateRange"
          :placeholder="$store.state.formatDate"
          position="right"
          :clearable="false"
          :enable-time-picker="false"
          :format="$store.state.formatDate"
          text-input
          auto-apply
          range
          timezone="Asia/Novosibirsk"
          class="w1/4"
        ></VueDatePicker>
        <ttanh-combobox
          v-model="dataFilter.cropStateId"
          ref="cropStateId"
          type="single-row"
          labelText="Giai đoạn cây trồng"
          :inputRequired="false"
          :rowsData="[]"
          class="w1/4"
          tabindex="2"
        />
        <ttanh-combobox
          v-model="dataFilter.pestLevelId"
          ref="pestLevelId"
          type="single-row"
          labelText="Mức độ sâu bệnh"
          :inputRequired="false"
          :rowsData="[]"
          class="w1/4"
          tabindex="3"
        />
      </div>
      <div class="page__filter-group page__filter-group-3">
        <ttanh-button
          width="90px"
          type="main"
          borderRadius="var(--border-radius-default)"
          :border="batchExecutionDisable ? '' : '2px solid black'"
          :tabindex="-1"
          @clickItem="clickItemBatchExecution"
          >Tìm kiếm</ttanh-button
        >
        <ttanh-button
          width="80px"
          type="main"
          borderRadius="var(--border-radius-default)"
          :border="batchExecutionDisable ? '' : '2px solid black'"
          :tabindex="-1"
          @clickItem="clickItemBatchExecution"
          >Xóa lọc</ttanh-button
        >
      </div>
    </div>
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
            $t("PredictSubsystem.PredictContent.batchExecution")
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
            $t('PredictSubsystem.PredictContent.searchPlaceHolder')
          "
          :tooltip="$t('PredictSubsystem.PredictContent.searchInputTooltip')"
        />
        <ttanh-icon
          :icon="
            'page__reload--' +
            (pageButtonHover['page__reload'] ? 'black' : 'grey')
          "
          :tooltip="$t('PredictSubsystem.PredictContent.reloadTooltip')"
          @mouseenter="pageButtonHover['page__reload'] = true"
          @mouseleave="pageButtonHover['page__reload'] = false"
          @click="reloadDataWithSelectedRows"
        />
        <ttanh-icon
          :icon="
            'page__excel--' +
            (pageButtonHover['page__excel'] ? 'black' : 'grey')
          "
          :tooltip="$t('PredictSubsystem.PredictContent.exportExcelTooltip')"
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
            $t('PredictSubsystem.PredictContent.layoutSettingTooltip')
          "
          @mouseenter="pageButtonHover['page__setting'] = true"
          @mouseleave="pageButtonHover['page__setting'] = false"
          @click="isShowLayoutSetting = true"
        />
        <ttanh-button
          type="combo"
          @clickBtnContainer="showAddPredictPopup"
          :dataDropdown="addDataDropdown"
          :tabindex="-1"
          :tooltip="$t('PredictSubsystem.PredictContent.insertTooltip')"
          @clickItem="handleDropdownInsertButton"
          >{{ $t("common.button.add") }}</ttanh-button
        >
      </div>
    </div>
    <div class="page__table">
      <ttanh-table
        ref="ttanhTable"
        :columnsInfo="PredictColumnsInfo"
        :rowsData="computedPredicts"
        :selectedRows="computedSelectedPredicts"
        :noData="computedNoData"
        @checked-all="checkedAllRow"
        @unchecked-all="uncheckedAllRow"
        @checked-row="checkedRow"
        @unchecked-row="uncheckedRow"
        @doubleClickRow="openFormUpdate"
        @clickFixBtn="openFormUpdate"
        @clickContextDeleteBtn="openConfirmDeletePopup"
        @clickContextDuplicateBtn="openFormDuplicate"
        @resizeColumn="resizePredictColumn"
      />
    </div>
    <div class="page__footer">
      <ttanh-paging
        v-if="!this.noData"
        v-model="pagingData"
        @reloadData="reloadData"
      />
    </div>

    <AddPredictPopup
      v-if="isShowAddPredictPopup"
      :dataUpdate="dataUpdate"
      @clickCancelBtn="isShowAddPredictPopup = false"
      @reloadData="reloadData"
      ref="addPredictPopup"
    />

    <ttanh-delete-popup
      :titleText="computedDeletePopupText"
      v-if="isShowConfirmDeletePopup || isShowConfirmDeleteMultiplePopup"
      @no-click="
        isShowConfirmDeletePopup
          ? noDeleteBtnClick()
          : noDeleteMultiplePredict()
      "
      @yes-click="
        isShowConfirmDeletePopup
          ? yesDeleteBtnClick()
          : yesDeleteMultiplePredict()
      "
    />

    <ttanh-loading-spinner v-if="isLoading" size="large" />
  </div>
</template>

<script>
import VueDatePicker from "@vuepic/vue-datepicker";
import PredictService from "@/service/PredictService.js";
import AddressService from "@/service/AddressService.js";
import AddPredictPopup from "./AddPredictPopup.vue";
import { CommonErrorHandle } from "@/helper/error-handle";
import { findIndexByAttribute, sortArrayByAttribute } from "@/helper/common.js";
import { formatToNumber } from "@/helper/textfield-format-helper.js";
import { debounce } from "@/helper/debounce.js";
import { isProxy, toRaw } from "vue";

export default {
  name: "PredictContent",
  components: {
    AddPredictPopup,
    VueDatePicker
  },
  data() {
    return {
      Predicts: [],

      dataAddress: {
        provinces: [],
        districts: [],
        wards: [],
      },

      /* lưu dữ id các nhân viên đã được chọn */
      selectedPredicts: [],

      PredictColumnsInfo: [],

      /* thông tin cột thuần được gửi từ api đã sắp xếp */
      PredictColumnsInfoRaw: [],

      isLoading: false,
      /* các biến để xác định trạng thái trên page_action */
      tableSearchFocus: false,
      pageButtonHover: {
        page__setting: false,
        page__reload: false,
        page__reload: false,
      },

      /* biến xác định nút "Thực hiện hàng loạt" có disable hay không */
      batchExecutionDisable: true,

      /* các hành động cho nút "Thực hiện hàng loạt" ở page action */
      batchExecutionDataDropdown: [
        {
          id: "delete",
          title: this.$t(
            "PredictSubsystem.PredictContent.batchExecutionData.delete"
          ),
        },
      ],

      /*== các biến sử dụng cho add-Predict-popup ==*/
      isShowAddPredictPopup: false,

      dataUpdate: null,

      /* biến sử dụng cho việc xác nhận xóa */
      isShowConfirmDeletePopup: false,
      PredictCodeDelete: "",
      PredictIdDelete: "",

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

      dataFilter: {
        provinceId: -1,
        districtId: -1,
        wardId: -1,
        dateRange: "",
        cropStateId: -1,
        pestLevelId: -1
      },
    };
  },

  created() {
    // lấy dữ liệu phân trang được lưu trong local storage
    this.pagingData.pageNumber =
      formatToNumber(localStorage.getItem("pageNumber")) ?? 1;
    this.pagingData.pageSize =
      formatToNumber(localStorage.getItem("pageSize")) ?? 10;

    window.addEventListener("keydown", this.handleKeydown);

    //lấy dữ liệu nhân viên
    this.getPredicts();
  },

  updated() {
    //nếu add popup đang mở thì bỏ sự kiện keydown đi
    if (this.isShowAddPredictPopup) {
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
    sortPredictColumnsInfo(columnsInfoTemp) {
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
          "🚀 ~ file: PredictContent.vue:263 ~ sortPredictColumnsInfo ~ error:",
          error
        );
      }
    },

    /**
     * hàm thực hiện mở thêm nhân viên
     * @author: TTANH (11/07/2024)
     */
    showAddPredictPopup() {
      this.isShowAddPredictPopup = true;
      this.dataUpdate = null;
    },

    /**
     * thực hiện get dữ liệu nhân viên khi component được render
     * @author: TTANH (30/06/2024)
     */
    async getPredicts() {
      try {
        const res = await PredictService.filter({
          pageSize: this.pagingData.pageSize,
          pageNumber: this.pagingData.pageNumber,
          searchText: this.searchText.trim(),
        });

        if (res.success) {
          if (res.data.Data.length != 0) {
            this.Predicts = res.data.Data;
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
          "🚀 ~ file: PredictList.vue:116 ~ getPredicts ~ error:",
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
    noDeleteMultiplePredict() {
      this.isShowConfirmDeleteMultiplePopup = false;
    },

    /**
     * xóa nhiều nhân viên
     * @author: TTANH (17/07/2024)
     */
    async yesDeleteMultiplePredict() {
      var dataSendApi = null;

      if (isProxy(this.selectedPredicts)) {
        dataSendApi = toRaw(this.selectedPredicts);
      } else {
        dataSendApi = this.selectedPredicts;
      }

      this.isLoading = true;

      const res = await PredictService.deleteMultiple(dataSendApi);

      this.isLoading = false;

      if (res.success) {
        this.$store.commit("addToast", {
          type: "success",
          text: this.$t("successHandle.PredictSubsystem.deleteMultiple", {
            count: res.data,
          }),
        });

        this.selectedPredicts = [];
        this.isShowConfirmDeleteMultiplePopup = false;

        this.reloadData();
      } else {
        CommonErrorHandle();
      }
    },

    /**
     * cập nhật lại Predicts mới
     * @author: TTANH (03/07/2024)
     */
    reloadData() {
      try {
        this.getPredictColumnsInfo();

        this.previouslySelectedIndex = -1;
        this.Predicts = [];
        this.getPredicts();
      } catch (error) {
        console.log(
          "🚀 ~ file: PredictContent.vue:465 ~ reloadData ~ error:",
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
        this.selectedPredicts = [];
        this.reloadData();
      } catch (error) {
        console.log(
          "🚀 ~ file: PredictContent.vue:282 ~ reloadDataWithSelectedRows ~ error:",
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
        this.$router.push("/app/Predict/import");
      }
    },

    /**
     * thêm một id vào mảng dòng đã chọn
     * @author: TTANH (11/07/2024)
     * @param {string} rowId id của dòng được chọn
     */
    addSelectedRow(rowId) {
      let index = findIndexByAttribute(this.selectedPredicts, "", rowId);

      if (index === -1) {
        this.selectedPredicts.push(rowId);
      }
    },

    /**
     * xóa một id vào mảng dòng đã chọn
     * @author: TTANH (11/07/2024)
     * @param {string} rowId id của dòng được chọn
     */
    deleteSelectedRow(rowId) {
      let index = findIndexByAttribute(this.selectedPredicts, "", rowId);

      if (index !== -1) {
        this.selectedPredicts.splice(index, 1);
      }
    },

    /**
     * xử lý khi chọn checkbox ở header
     * @author: TTANH (27/06/2024)
     */
    checkedAllRow() {
      try {
        this.Predicts.forEach((Predict) => {
          this.addSelectedRow(Predict.PredictId);
        });
      } catch (error) {
        console.log(
          "🚀 ~ file: PredictList.vue:463 ~ checkedAllRow ~ error:",
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
        this.Predicts.forEach((Predict) => {
          this.deleteSelectedRow(Predict.PredictId);
        });
      } catch (error) {
        console.log(
          "🚀 ~ file: PredictList.vue:475 ~ uncheckedAllRow ~ error:",
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
          this.Predicts,
          "PredictId",
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
                const Predict = this.Predicts[index];

                this.addSelectedRow(Predict.PredictId);
              }
            } else if (this.previouslySelectedIndex < indexNewChecked) {
              for (
                let index = this.previouslySelectedIndex;
                index <= indexNewChecked;
                index++
              ) {
                const Predict = this.Predicts[index];

                this.addSelectedRow(Predict.PredictId);
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
          "🚀 ~ file: PredictList.vue:492 ~ uncheckedAllRow ~ error:",
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
          this.Predicts,
          "PredictId",
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
                const Predict = this.Predicts[index];

                this.deleteSelectedRow(Predict.PredictId);
              }
            } else if (this.previouslySelectedIndex < indexNewChecked) {
              for (
                let index = this.previouslySelectedIndex;
                index <= indexNewChecked;
                index++
              ) {
                const Predict = this.Predicts[index];

                this.deleteSelectedRow(Predict.PredictId);
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
          "🚀 ~ file: PredictList.vue:492 ~ uncheckedAllRow ~ error:",
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
          this.Predicts,
          "PredictId",
          rowId
        );

        this.isShowAddPredictPopup = true;
        this.dataUpdate = this.Predicts[indexRow];
      } catch (error) {
        console.log(
          "🚀 ~ file: PredictContent.vue:529 ~ openFormUpdate ~ error:",
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
        let index = findIndexByAttribute(this.Predicts, "PredictId", id);

        if (index !== -1) {
          this.PredictCodeDelete = this.Predicts[index].PredictCode;
          this.PredictIdDelete = id;
          this.isShowConfirmDeletePopup = true;
        } else {
          this.$store.commit("addToast", {
            type: "error",
            text: this.$t("errorHandle.PredictSubsystem.notFoundPredict"),
          });

          this.reloadData();
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: PredictContent.vue:351 ~ openConfirmDeletePopup ~ error:",
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
          this.Predicts,
          "PredictId",
          rowId
        );

        this.isShowAddPredictPopup = true;
        this.dataUpdate = this.Predicts[indexRow];

        this.$nextTick(() => {
          // thay đổi trạng thái form thành thêm mới
          this.$refs.addPredictPopup.changeFormModeToAdd();

          // lấy mã code mới
          this.$refs.addPredictPopup.getNewPredictCode();
        });
      } catch (error) {
        console.log(
          "🚀 ~ file: PredictContent.vue:529 ~ openFormUpdate ~ error:",
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
        this.PredictCodeDelete = "";
        this.PredictIdDelete = "";
        this.isShowConfirmDeletePopup = false;
      } catch (error) {
        console.log(
          "🚀 ~ file: PredictContent.vue:386 ~ closeConfirmDeletePopup ~ error:",
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
          "🚀 ~ file: PredictContent.vue:401 ~ noDeleteBtnClick ~ error:",
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
        this.deleteSelectedRow(this.PredictIdDelete);
        this.deleteRecord();
        this.closeConfirmDeletePopup();
      } catch (error) {
        console.log(
          "🚀 ~ file: PredictContent.vue:416 ~ yesDeleteBtnClick ~ error:",
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
        const PredictCode = this.PredictCodeDelete;
        const res = await PredictService.delete(this.PredictIdDelete);

        if (res.success) {
          this.$store.commit("addToast", {
            type: "success",
            text: this.$t("successHandle.PredictSubsystem.delete", {
              code: PredictCode,
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
          "🚀 ~ file: PredictContent.vue:582 ~ deleteRecord ~ error:",
          error
        );
      }
    },

    /**
     * thực hiện thay đổi kích thước của cột
     * @author: TTANH (04/07/2024)
     */
    resizePredictColumn(index, resizeWidth) {
      try {
        this.isUpdateColumnsInfo = true;
        this.PredictColumnsInfo[index].size = resizeWidth;
      } catch (error) {
        console.log(
          "🚀 ~ file: PredictContent.vue:524 ~ resizePredictColumn ~ error:",
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
        this.$router.push("/app/Predict/import");
      } else if (event.keyCode === this.$_TTANHEnum.KEY_CODE.INSERT) {
        this.showAddPredictPopup();
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
        this.PredictColumnsInfoRaw,
        "ServerColumnName",
        codeData.id
      );

      let PredictColumnInfoRaw = this.PredictColumnsInfoRaw[indexInRaw];

      let tempMap = {};

      tempMap.PredictLayoutId = PredictColumnInfoRaw.PredictLayoutId;
      tempMap.viClientColumnName = PredictColumnInfoRaw.viClientColumnName;
      tempMap.enClientColumnName = PredictColumnInfoRaw.enClientColumnName;
      tempMap.OrderNumber = PredictColumnInfoRaw.OrderNumber;

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

      const res = await PredictLayoutService.updateMultiple(datasUpdate);

      if (res.success) {
      } else {
        CommonErrorHandle();
      }
    },

    async getProvinces() {
      AddressService.province().then((res) => {
        if (res.statusCode === 200) {
          this.dataAddress.provinces = res.result;
        } else {
          this.dataAddress.provinces = [];
        }
      });
    },

    async getDistricts() {
      AddressService.district(this.dataFilter.provinceId).then((res) => {
        if (res.statusCode === 200) {
          this.dataAddress.districts = res.result;
        } else {
          this.dataAddress.districts = [];
        }
      });
    },

    async getWards() {
      AddressService.ward(this.dataFilter.districtId).then((res) => {
        if (res.statusCode === 200) {
          this.dataAddress.wards = res.result;
        } else {
          this.dataAddress.wards = [];
        }
      });
    },
  },
  computed: {
    /* thêm id để phân biệt các phần tử với nhau */
    computedPredicts() {
      try {
        let haveIdPredicts = [];

        this.Predicts.forEach((Predict, index) => {
          let id = Predict.PredictId;
          haveIdPredicts.push({
            id,
            ...Predict,
          });
        });

        return haveIdPredicts;
      } catch (error) {
        console.log(
          "🚀 ~ file: PredictList.vue:457 ~ computedPredicts ~ error:",
          error
        );
      }
    },

    computedSelectedPredicts() {
      if (this.selectedPredicts.length <= 1) {
        this.batchExecutionDisable = true;
      } else {
        this.batchExecutionDisable = false;
      }
      return this.selectedPredicts;
    },

    computedNoData() {
      return this.noData;
    },

    computedDeletePopupText() {
      if (this.isShowConfirmDeletePopup) {
        return this.$t("PredictSubsystem.PredictContent.deletePopupTitle", {
          code: this.PredictCodeDelete,
        });
      } else if (this.isShowConfirmDeleteMultiplePopup) {
        return this.$t(
          "PredictSubsystem.PredictContent.deleteMultiplePopupTitle",
          { count: this.selectedPredicts.length }
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

    PredictColumnsInfoRaw(newValue) {
      let tempMapArray = this.mapColumnsInfoFromRawToCode(newValue);
      /**
       * do là việc lấy dữ liệu gây ra thay đổi cho PredictColumnsInfo
       * nên không gọi đến hàm cập nhật
       */
      this.isUpdateColumnsInfo = false;

      this.PredictColumnsInfo = tempMapArray;
    },

    PredictColumnsInfo: {
      handler: debounce(function () {
        if (this.isUpdateColumnsInfo) {
          this.updateColumnsInfoToDB(this.PredictColumnsInfo);
        }
      }, 500),

      deep: true,
    },
  },
};
</script>

<style scoped>
@import url(./predict-content.css);
</style>
