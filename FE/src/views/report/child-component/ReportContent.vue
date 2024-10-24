<template>
  <div class="Report-content">
    <div class="page__filter">
      <h1 class="page__filter-title">Bộ lọc</h1>
      <ttanh-separation-line
        style="width: 98%; border-top: 2px solid var(--border-color-default); margin-bottom: 4px"
      />
      <div class="page__filter-group page__filter-group-1">
        <ttanh-combobox
          v-model="dataFilter.provinceId"
          ref="provinceId"
          type="single-row"
          labelText="Tỉnh/Thành phố"
          :inputRequired="false"
          @show-combobox="getProvinces"
          :rowsData="computedProvinces"
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
          :rowsData="computedDistricts"
          class="w1/4"
          tabindex="2"
        />
        <ttanh-combobox
          v-model="dataFilter.wardId"
          ref="wardId"
          type="single-row"
          labelText="Phường/Xã"
          :inputRequired="false"
          @show-combobox="getWards"
          :rowsData="computedWards"
          class="w1/4"
          tabindex="3"
        />
      </div>
      <div class="page__filter-group page__filter-group-2">
        <ttanh-textfield
          v-model="dataFilter.reportName"
          ref="reportNameFilter"
          idInput="reportNameFilter"
          labelText="Người báo cáo"
          placeholder="Người báo cáo"
          class="w1/4"
        />
        <div class="w1/4">
          <label class="label-input"> Khoảng thời gian báo cáo </label>
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
            class="w1"
          ></VueDatePicker>
        </div>
      </div>
      <div class="page__filter-group page__filter-group-3">
        <ttanh-button
          width="90px"
          type="main"
          borderRadius="var(--border-radius-default)"
          :border="batchExecutionDisable ? '' : '2px solid black'"
          :tabindex="-1"
          @clickButton="getReports"
          >Tìm kiếm</ttanh-button
        >
        <ttanh-button
          width="80px"
          type="sub"
          borderRadius="var(--border-radius-default)"
          :border="batchExecutionDisable ? '' : '2px solid black'"
          :tabindex="-1"
          @clickButton="clearFilter"
          >Xóa lọc</ttanh-button
        >
      </div>
    </div>
    <div class="page__action">
      <div class="page__action-left">
      </div>
      <div class="page__action-right">
        <ttanh-icon
          :icon="'page__reload--' + (pageButtonHover['page__reload'] ? 'black' : 'grey')"
          :tooltip="$t('ReportSubsystem.ReportContent.reloadTooltip')"
          @mouseenter="pageButtonHover['page__reload'] = true"
          @mouseleave="pageButtonHover['page__reload'] = false"
          @click="reloadDataWithSelectedRows"
        />
        <ttanh-button
          width="85px"
          type="main"
          borderRadius="var(--border-radius-default)"
          :border="batchExecutionDisable ? '' : '2px solid black'"
          :tabindex="-1"
          @clickButton="showAddReportPopup"
          >Tạo mới</ttanh-button
        >
      </div>
    </div>
    <div class="page__table">
      <ttanh-table
        ref="ttanhTable"
        :columnsInfo="ReportColumnsInfo"
        :rowsData="computedReports"
        :selectedRows="computedSelectedReports"
        :noData="computedNoData"
        :oneRowSelect="true"
        @checked-all="checkedAllRow"
        @unchecked-all="uncheckedAllRow"
        @checked-row="checkedRow"
        @unchecked-row="uncheckedRow"
        @doubleClickRow="openFormUpdate"
        @clickFixBtn="openFormUpdate"
        @clickContextDeleteBtn="openConfirmDeletePopup"
        @resizeColumn="resizeReportColumn"
      />
    </div>
    <div class="page__footer">
      <ttanh-paging v-if="!this.noData" v-model="pagingData" @reloadData="reloadData" />
    </div>

    <AddReportPopup
      v-if="isShowAddReportPopup"
      :dataUpdate="dataUpdate"
      @clickCancelBtn="isShowAddReportPopup = false"
      @reloadData="reloadData"
      ref="addReportPopup"
    />

    <ttanh-delete-popup
      :titleText="computedDeletePopupText"
      v-if="isShowConfirmDeletePopup || isShowConfirmDeleteMultiplePopup"
      @no-click="isShowConfirmDeletePopup ? noDeleteBtnClick() : noDeleteMultipleReport()"
      @yes-click="isShowConfirmDeletePopup ? yesDeleteBtnClick() : yesDeleteMultipleReport()"
    />

    <ttanh-loading-spinner v-if="isLoading" size="large" />
  </div>
</template>

<script>
import VueDatePicker from '@vuepic/vue-datepicker'
import ReportService from '@/service/ReportService.js'
import AddressService from '@/service/AddressService.js'
import AddReportPopup from './AddReportPopup.vue'
import { CommonErrorHandle } from '@/helper/error-handle'
import { findIndexByAttribute, sortArrayByAttribute } from '@/helper/common.js'
import { formatToNumber } from '@/helper/textfield-format-helper.js'
import { debounce } from '@/helper/debounce.js'
import { isProxy, toRaw } from 'vue'
import { pestLevels } from '../../../data_combobox/pestLevel'
import { cropStates } from '../../../data_combobox/cropState'

export default {
  name: 'ReportContent',
  components: {
    AddReportPopup,
    VueDatePicker
  },
  data() {
    return {
      reports: [],

      cropStates: cropStates,

      pestLevels: pestLevels,

      dataAddress: {
        provinces: [],
        districts: [],
        wards: []
      },

      /* lưu dữ id các báo cáo đã được chọn */
      selectedReports: [],

      ReportColumnsInfo: [
        {
          id: 'ProvinceName',
          name: 'TỈNH/THÀNH PHỐ',
          size: '150px',
          textAlign: 'left',
          format: 'text',
          isShow: true,
          isPin: false
        },
        {
          id: 'DistrictName',
          name: 'QUẬN/HUYỆN',
          size: '150px',
          textAlign: 'left',
          format: 'text',
          isShow: true,
          isPin: false
        },
        {
          id: 'WardName',
          name: 'PHƯỜNG/XÃ',
          size: '150px',
          textAlign: 'left',
          format: 'text',
          isShow: true,
          isPin: false
        },
        {
          id: 'ReportName',
          name: 'NGƯỜI BÁO CÁO',
          size: '150px',
          textAlign: 'center',
          format: 'text',
          isShow: true,
          isPin: false
        },
        {
          id: 'ModifiedDate',
          name: 'THỜI ĐIỂM CẬP NHẬT',
          size: '150px',
          textAlign: 'center',
          format: 'date',
          isShow: true,
          isPin: false
        }
      ],

      /* thông tin cột thuần được gửi từ api đã sắp xếp */
      ReportColumnsInfoRaw: [],

      isLoading: false,
      /* các biến để xác định trạng thái trên page_action */
      tableSearchFocus: false,
      pageButtonHover: {
        page__setting: false,
        page__reload: false
      },

      /* biến xác định nút "Thực hiện hàng loạt" có disable hay không */
      batchExecutionDisable: true,

      /*== các biến sử dụng cho add-Report-popup ==*/
      isShowAddReportPopup: false,

      dataUpdate: null,

      /* biến sử dụng cho việc xác nhận xóa */
      isShowConfirmDeletePopup: false,
      ReportCodeDelete: '',
      ReportIdDelete: '',

      isShowConfirmDeleteMultiplePopup: false,

      searchText: '',

      /* biến sử dụng cho phân trang */
      pagingData: {
        pageSize: 10,
        pageNumber: 1,
        totalPage: 0,
        totalRecord: 0
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
        dateRange: null,
        reportName: '',
      }
    }
  },

  created() {
    // lấy dữ liệu phân trang được lưu trong local storage
    this.pagingData.pageNumber = formatToNumber(localStorage.getItem('pageNumber')) ?? 1
    this.pagingData.pageSize = formatToNumber(localStorage.getItem('pageSize')) ?? 10

    //lấy dữ liệu báo cáo
    this.getReports()
  },

  methods: {
    clearFilter() {
      this.dataFilter = {
        provinceId: -1,
        districtId: -1,
        wardId: -1,
        dateRange: null,
        reportName: '',
      }

      this.$refs.provinceId.$refs.inputSearch.value = ''
      this.$refs.districtId.$refs.inputSearch.value = ''
      this.$refs.wardId.$refs.inputSearch.value = ''

      this.getReports()
    },
    /**
     * Sắp xếp theo ordernumber và isPin để hiển thị đúng
     * @author: TTANH (04/08/2024)
     */
    sortReportColumnsInfo(columnsInfoTemp) {
      try {
        // sắp xếp theo thứ tự
        columnsInfoTemp = sortArrayByAttribute(columnsInfoTemp, 'OrderNumber', false)

        //đưa những cột được ghim lên đầu
        columnsInfoTemp = sortArrayByAttribute(columnsInfoTemp, 'ColumnIsPin')

        return columnsInfoTemp
      } catch (error) {
        console.log('🚀 ~ file: ReportContent.vue:263 ~ sortReportColumnsInfo ~ error:', error)
      }
    },

    /**
     * hàm thực hiện mở thêm báo cáo
     * @author: TTANH (11/07/2024)
     */
    showAddReportPopup() {
      this.isShowAddReportPopup = true
      this.dataUpdate = null
    },

    /**
     * thực hiện get dữ liệu báo cáo khi component được render
     * @author: TTANH (30/06/2024)
     */
    async getReports() {
      try {
        let startDate = null
        let endDate = null

        if (this.dataFilter.dateRange) {
          startDate = this.dataFilter.dateRange[0]
          endDate = this.dataFilter.dateRange[1]
        }

        let dataFilter = {
          ProvinceId: this.dataFilter.provinceId,
          DistrictId: this.dataFilter.districtId,
          WardId: this.dataFilter.wardId,
          ReportStartDate: startDate,
          ReportEndDate: endDate,
          ReportName: this.dataFilter.reportName,
          PageSize: this.pagingData.pageSize,
          PageNumber: this.pagingData.pageNumber
        }

        const res = await ReportService.filterAdvanced('Report', dataFilter)

        if (res.success) {
          if (res.data.Data.length != 0) {
            this.reports = res.data.Data
            this.pagingData.totalPage = res.data.TotalPage
            this.pagingData.totalRecord = res.data.TotalRecord
            this.pagingData.pageNumber = res.data.CurrentPage
            this.noData = false
          } else {
            this.noData = true
          }
        } else {
          CommonErrorHandle()
        }
      } catch (error) {
        console.log('🚀 ~ file: ReportList.vue:116 ~ getReports ~ error:', error)
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
      if (idItem === 'delete') {
        this.isShowConfirmDeleteMultiplePopup = true
      }
    },

    /**
     * bỏ lệnh xóa nhiều báo cáo
     * @author: TTANH (31/07/2024)
     */
    noDeleteMultipleReport() {
      this.isShowConfirmDeleteMultiplePopup = false
    },

    /**
     * xóa nhiều báo cáo
     * @author: TTANH (17/07/2024)
     */
    async yesDeleteMultipleReport() {
      var dataSendApi = null

      if (isProxy(this.selectedReports)) {
        dataSendApi = toRaw(this.selectedReports)
      } else {
        dataSendApi = this.selectedReports
      }

      this.isLoading = true

      const res = await ReportService.deleteMultiple(dataSendApi)

      this.isLoading = false

      if (res.success) {
        this.$store.commit('addToast', {
          type: 'success',
          text: this.$t('successHandle.ReportSubsystem.deleteMultiple', {
            count: res.data
          })
        })

        this.selectedReports = []
        this.isShowConfirmDeleteMultiplePopup = false

        this.reloadData()
      } else {
        CommonErrorHandle()
      }
    },

    /**
     * cập nhật lại reports mới
     * @author: TTANH (03/07/2024)
     */
    reloadData() {
      try {
        this.previouslySelectedIndex = -1
        this.reports = []
        this.getReports()
      } catch (error) {
        console.log('🚀 ~ file: ReportContent.vue:465 ~ reloadData ~ error:', error)
      }
    },

    /**
     * bỏ hết dữ liệu đã chọn khi ấn vào nút "Lấy lại dữ liệu"
     * @author: TTANH (03/07/2024)
     */
    reloadDataWithSelectedRows() {
      try {
        this.selectedReports = []
        this.reloadData()
      } catch (error) {
        console.log('🚀 ~ file: ReportContent.vue:282 ~ reloadDataWithSelectedRows ~ error:', error)
      }
    },

    /**
     * thêm một id vào mảng dòng đã chọn
     * @author: TTANH (11/07/2024)
     * @param {string} rowId id của dòng được chọn
     */
    addSelectedRow(rowId) {
      let index = findIndexByAttribute(this.selectedReports, '', rowId)

      if (index === -1) {
        this.selectedReports.push(rowId)
      }
    },

    /**
     * xóa một id vào mảng dòng đã chọn
     * @author: TTANH (11/07/2024)
     * @param {string} rowId id của dòng được chọn
     */
    deleteSelectedRow(rowId) {
      let index = findIndexByAttribute(this.selectedReports, '', rowId)

      if (index !== -1) {
        this.selectedReports.splice(index, 1)
      }
    },

    /**
     * xử lý khi chọn checkbox ở header
     * @author: TTANH (27/06/2024)
     */
    checkedAllRow() {
      try {
        this.reports.forEach((Report) => {
          this.addSelectedRow(Report.ReportId)
        })
      } catch (error) {
        console.log('🚀 ~ file: ReportList.vue:463 ~ checkedAllRow ~ error:', error)
      }
    },

    /**
     * xử lý khi bỏ chọn checkbox ở header
     * @author: TTANH (27/06/2024)
     */
    uncheckedAllRow() {
      try {
        this.reports.forEach((Report) => {
          this.deleteSelectedRow(Report.ReportId)
        })
      } catch (error) {
        console.log('🚀 ~ file: ReportList.vue:475 ~ uncheckedAllRow ~ error:', error)
      }
    },

    /**
     * xử lý khi chọn checkbox ở 1 row
     * @author: TTANH (27/06/2024)
     * @param {string} rowId: id của record được chọn
     */
    checkedRow(rowId) {
      try {
        let indexNewChecked = findIndexByAttribute(this.reports, 'ReportId', rowId)

        if (event.shiftKey) {
          event.preventDefault()

          if (this.previouslySelectedIndex === -1) {
            this.addSelectedRow(rowId)
          } else {
            if (this.previouslySelectedIndex > indexNewChecked) {
              for (let index = indexNewChecked; index <= this.previouslySelectedIndex; index++) {
                const Report = this.reports[index]

                this.addSelectedRow(Report.ReportId)
              }
            } else if (this.previouslySelectedIndex < indexNewChecked) {
              for (let index = this.previouslySelectedIndex; index <= indexNewChecked; index++) {
                const Report = this.reports[index]

                this.addSelectedRow(Report.ReportId)
              }
            } else {
            }
          }
        } else {
          this.addSelectedRow(rowId)
        }

        this.previouslySelectedIndex = indexNewChecked
      } catch (error) {
        console.log('🚀 ~ file: ReportList.vue:492 ~ uncheckedAllRow ~ error:', error)
      }
    },

    /**
     * xử lý khi bỏ chọn checkbox ở 1 row
     * @author: TTANH (27/06/2024)
     * @param {string} rowId: id của record được bỏ chọn
     */
    uncheckedRow(rowId) {
      try {
        let indexNewChecked = findIndexByAttribute(this.reports, 'ReportId', rowId)

        if (event.shiftKey) {
          event.preventDefault()

          if (this.previouslySelectedIndex === -1) {
            this.deleteSelectedRow(rowId)
          } else {
            if (this.previouslySelectedIndex > indexNewChecked) {
              for (let index = indexNewChecked; index <= this.previouslySelectedIndex; index++) {
                const Report = this.reports[index]

                this.deleteSelectedRow(Report.ReportId)
              }
            } else if (this.previouslySelectedIndex < indexNewChecked) {
              for (let index = this.previouslySelectedIndex; index <= indexNewChecked; index++) {
                const Report = this.reports[index]

                this.deleteSelectedRow(Report.ReportId)
              }
            } else {
            }
          }
        } else {
          this.deleteSelectedRow(rowId)
        }

        this.previouslySelectedIndex = indexNewChecked
      } catch (error) {
        console.log('🚀 ~ file: ReportList.vue:492 ~ uncheckedAllRow ~ error:', error)
      }
    },

    /**
     * mở form update
     * @param {string} rowId id của đối tượng muốn update
     */
    openFormUpdate(rowId) {
      try {
        let indexRow = findIndexByAttribute(this.reports, 'ReportId', rowId)

        this.isShowAddReportPopup = true
        this.dataUpdate = this.reports[indexRow]
      } catch (error) {
        console.log('🚀 ~ file: ReportContent.vue:529 ~ openFormUpdate ~ error:', error)
      }
    },

    /**
     * mở form xác nhận xóa
     * @author: TTANH (01/07/2024)
     * @param {string} id id của bản ghi cần xóa
     */
    openConfirmDeletePopup(id) {
      try {
        let index = findIndexByAttribute(this.reports, 'ReportId', id)

        if (index !== -1) {
          this.ReportCodeDelete = this.reports[index].ReportCode
          this.ReportIdDelete = id
          this.isShowConfirmDeletePopup = true
        } else {
          this.$store.commit('addToast', {
            type: 'error',
            text: this.$t('errorHandle.ReportSubsystem.notFoundReport')
          })

          this.reloadData()
        }
      } catch (error) {
        console.log('🚀 ~ file: ReportContent.vue:351 ~ openConfirmDeletePopup ~ error:', error)
      }
    },

    /**
     * mở form nhân bản
     * @param {string} rowId id của đối tượng muốn nhân bản
     */
    openFormDuplicate(rowId) {
      try {
        let indexRow = findIndexByAttribute(this.reports, 'ReportId', rowId)

        this.isShowAddReportPopup = true
        this.dataUpdate = this.reports[indexRow]

        this.$nextTick(() => {
          // thay đổi trạng thái form thành thêm mới
          this.$refs.addReportPopup.changeFormModeToAdd()

          // lấy mã code mới
          this.$refs.addReportPopup.getNewReportCode()
        })
      } catch (error) {
        console.log('🚀 ~ file: ReportContent.vue:529 ~ openFormUpdate ~ error:', error)
      }
    },

    /**
     * đóng form xác nhận xóa
     * @author: TTANH (01/07/2024)
     */
    closeConfirmDeletePopup() {
      try {
        this.ReportCodeDelete = ''
        this.ReportIdDelete = ''
        this.isShowConfirmDeletePopup = false
      } catch (error) {
        console.log('🚀 ~ file: ReportContent.vue:386 ~ closeConfirmDeletePopup ~ error:', error)
      }
    },

    /**
     * hủy xóa
     * @author: TTANH (01/07/2024)
     */
    noDeleteBtnClick() {
      try {
        this.closeConfirmDeletePopup()
      } catch (error) {
        console.log('🚀 ~ file: ReportContent.vue:401 ~ noDeleteBtnClick ~ error:', error)
      }
    },

    /**
     * xác nhận xóa
     * @author: TTANH (01/07/2024)
     */
    yesDeleteBtnClick() {
      try {
        this.deleteSelectedRow(this.ReportIdDelete)
        this.deleteRecord()
        this.closeConfirmDeletePopup()
      } catch (error) {
        console.log('🚀 ~ file: ReportContent.vue:416 ~ yesDeleteBtnClick ~ error:', error)
      }
    },

    /**
     * thực hiện xóa 1 bản ghi
     * @author: TTANH (01/07/2024)
     */
    async deleteRecord() {
      try {
        this.isLoading = true
        const ReportCode = this.ReportCodeDelete
        const res = await ReportService.delete(this.ReportIdDelete)

        if (res.success) {
          this.$store.commit('addToast', {
            type: 'success',
            text: 'Xóa báo cáo thành công'
          })

          this.reloadData()
        } else {
          if (res.errorCode === this.$_TTANHEnum.ERROR_CODE.NOT_FOUND_DATA) {
            this.$store.commit('addToast', {
              type: 'error',
              text: res.userMsg
            })
          } else {
            CommonErrorHandle()
          }
        }

        this.isLoading = false
      } catch (error) {
        console.log('🚀 ~ file: ReportContent.vue:582 ~ deleteRecord ~ error:', error)
      }
    },

    /**
     * thực hiện thay đổi kích thước của cột
     * @author: TTANH (04/07/2024)
     */
    resizeReportColumn(index, resizeWidth) {
      try {
        this.isUpdateColumnsInfo = true
        this.ReportColumnsInfo[index].size = resizeWidth
      } catch (error) {
        console.log('🚀 ~ file: ReportContent.vue:524 ~ resizeReportColumn ~ error:', error)
      }
    },

    /**
     * xử lý các phím tắt
     * @author: TTANH (11/07/2024)
     */
    handleKeydown(event) {
      if (event.keyCode === this.$_TTANHEnum.KEY_CODE.INSERT && event.ctrlKey) {
        this.$router.push('/app/Report/import')
      } else if (event.keyCode === this.$_TTANHEnum.KEY_CODE.INSERT) {
        this.showAddReportPopup()
      } else if (event.keyCode === this.$_TTANHEnum.KEY_CODE.F && event.shiftKey && event.ctrlKey) {
        this.$refs.searchTextTable.focus()
      }
    },

    /**
     * chuyển đổi 1 cột thành dạng ttanh table có thể hiểu
     * @author: TTANH (03/08/2024)
     * @param {Object} rawData dữ liệu thông tin cột raw
     * @returns 1 object đã được chuyển đổi
     */
    mapColumnInfoFromRawToCode(rawData) {
      let langCode = this.$store.state.langCode

      let tempMap = {}

      tempMap.id = rawData.ServerColumnName
      tempMap.name = rawData[`${langCode}ClientColumnName`]
      tempMap.size = rawData.ColumnWidth
      tempMap.textAlign = rawData.ColumnTextAlign
      tempMap.format = rawData.ColumnFormat
      tempMap.isShow = rawData.ColumnIsShow
      tempMap.isPin = rawData.ColumnIsPin
      tempMap.tooltip = rawData[`${langCode}Tooltip`]
      tempMap.clientColumnNameDefault = rawData[`${langCode}ClientColumnNameDefault`]
      tempMap.orderNumber = rawData.OrderNumber

      return tempMap
    },

    /**
     * chuyển đổi mảng cột thành dạng ttanh table có thể hiểu
     * @author: TTANH (03/08/2024)
     * @param {Array} rawsData dữ liệu thông tin cột raw
     * @returns 1 mảng object đã được chuyển đổi
     */
    mapColumnsInfoFromRawToCode(rawsData) {
      let tempMapArray = []

      rawsData.forEach((e) => {
        let tempMap = this.mapColumnInfoFromRawToCode(e)

        tempMapArray.push(tempMap)
      })

      return tempMapArray
    },

    /**
     * chuyển đổi dữ liệu cập nhật sang dữ liệu truyền cho api để cập nhật
     * @author: TTANH (03/08/2024)
     * @param {Object} codeData dữ liệu thông tin cột
     * @returns 1 object đã được chuyển đổi
     */
    mapColumnInfoFromCodeToRawForUpdate(codeData) {
      let langCode = this.$store.state.langCode

      let indexInRaw = findIndexByAttribute(this.ReportColumnsInfoRaw, 'ServerColumnName', codeData.id)

      let ReportColumnInfoRaw = this.ReportColumnsInfoRaw[indexInRaw]

      let tempMap = {}

      tempMap.ReportLayoutId = ReportColumnInfoRaw.ReportLayoutId
      tempMap.viClientColumnName = ReportColumnInfoRaw.viClientColumnName
      tempMap.enClientColumnName = ReportColumnInfoRaw.enClientColumnName
      tempMap.OrderNumber = ReportColumnInfoRaw.OrderNumber

      tempMap[`${langCode}ClientColumnName`] = codeData.name
      tempMap.ColumnWidth = codeData.size
      tempMap.ColumnIsShow = codeData.isShow
      tempMap.ColumnIsPin = codeData.isPin
      return tempMap
    },

    /**
     * chuyển đổi dữ liệu cập nhật sang dữ liệu truyền cho api để cập nhật
     * @author: TTANH (03/08/2024)
     * @param {Array} codesData dữ liệu thông tin cột
     * @returns 1 mảng object đã được chuyển đổi
     */
    mapColumnsInfoFromCodeToRawForUpdate(codesData) {
      let tempMapArray = []

      codesData.forEach((e) => {
        let tempMap = this.mapColumnInfoFromCodeToRawForUpdate(e)

        tempMapArray.push(tempMap)
      })

      return tempMapArray
    },

    /**
     * thực hiện cập nhật thông tin cột trên db
     */
    async updateColumnsInfoToDB(newData) {
      let datasUpdate = this.mapColumnsInfoFromCodeToRawForUpdate(newData)

      const res = await ReportLayoutService.updateMultiple(datasUpdate)

      if (res.success) {
      } else {
        CommonErrorHandle()
      }
    },

    async getProvinces() {
      let provinces = await AddressService.province()

      if (provinces.status === 200) {
        this.dataAddress.provinces = provinces.data.results
      } else {
        this.dataAddress.provinces = []
      }
    },

    async getDistricts() {
      let districts = await AddressService.district(this.dataFilter.provinceId)

      if (districts.status === 200) {
        this.dataAddress.districts = districts.data.results
      } else {
        this.dataAddress.districts = []
      }
    },

    async getWards() {
      let wards = await AddressService.ward(this.dataFilter.districtId)

      if (wards.status === 200) {
        this.dataAddress.wards = wards.data.results
      } else {
        this.dataAddress.wards = []
      }
    }
  },
  computed: {
    /* thêm id để phân biệt các phần tử với nhau */
    computedReports() {
      try {
        let haveIdReports = []

        this.reports.forEach((Report, index) => {
          let id = Report.ReportId
          haveIdReports.push({
            id,
            ...Report
          })
        })

        return haveIdReports
      } catch (error) {
        console.log('🚀 ~ file: ReportList.vue:457 ~ computedReports ~ error:', error)
      }
    },

    computedProvinces() {
      try {
        let provincesFormat = []

        this.dataAddress.provinces.forEach((province) => {
          let id = province.province_id
          let name = province.province_name
          let code = province.province_name

          provincesFormat.push({
            id,
            name,
            code
          })
        })

        return provincesFormat
      } catch (error) {
        console.log('🚀 ~ file: EmployeeList.vue:457 ~ computedEmployees ~ error:', error)
      }
    },

    computedDistricts() {
      try {
        let districtsFormat = []

        this.dataAddress.districts.forEach((district) => {
          let id = district.district_id
          let name = district.district_name
          let code = district.district_name

          districtsFormat.push({
            id,
            name,
            code
          })
        })

        return districtsFormat
      } catch (error) {
        console.log('🚀 ~ file: EmployeeList.vue:457 ~ computedEmployees ~ error:', error)
      }
    },

    computedWards() {
      try {
        let wardsFormat = []

        this.dataAddress.wards.forEach((ward) => {
          let id = ward.ward_id
          let name = ward.ward_name
          let code = ward.ward_name

          wardsFormat.push({
            id,
            name,
            code
          })
        })

        return wardsFormat
      } catch (error) {
        console.log('🚀 ~ file: EmployeeList.vue:457 ~ computedEmployees ~ error:', error)
      }
    },

    computedSelectedReports() {
      if (this.selectedReports.length <= 1) {
        this.batchExecutionDisable = true
      } else {
        this.batchExecutionDisable = false
      }
      return this.selectedReports
    },

    computedNoData() {
      return this.noData
    },

    computedDeletePopupText() {
      if (this.isShowConfirmDeletePopup) {
        return this.$t('Bạn có thực sự muốn xóa báo cáo không?')
      } else if (this.isShowConfirmDeleteMultiplePopup) {
        return this.$t('ReportSubsystem.ReportContent.deleteMultiplePopupTitle', {
          count: this.selectedReports.length
        })
      } else {
        return ''
      }
    }
  },
  watch: {
    searchText: debounce(function () {
      this.pagingData.pageNumber = 1
      this.reloadData()
    }, 500),

    pagingData: {
      handler: function (newValue) {
        localStorage.setItem('pageNumber', newValue.pageNumber)
        localStorage.setItem('pageSize', newValue.pageSize)
      },

      deep: true
    },

    ReportColumnsInfoRaw(newValue) {
      let tempMapArray = this.mapColumnsInfoFromRawToCode(newValue)
      /**
       * do là việc lấy dữ liệu gây ra thay đổi cho ReportColumnsInfo
       * nên không gọi đến hàm cập nhật
       */
      this.isUpdateColumnsInfo = false

      this.ReportColumnsInfo = tempMapArray
    },

    ReportColumnsInfo: {
      handler: debounce(function () {
        if (this.isUpdateColumnsInfo) {
          this.updateColumnsInfoToDB(this.ReportColumnsInfo)
        }
      }, 500),

      deep: true
    }
  }
}
</script>

<style scoped>
@import url(./report-content.css);
</style>
