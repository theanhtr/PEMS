<template>
  <div class="predict-content">
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
          idField="province_id"
          nameField="province_name"
          :rowsData="dataAddress.provinces"
          class="w1/5"
          tabindex="1"
        />
        <ttanh-combobox
          v-model="dataFilter.wardId"
          ref="wardId"
          type="single-row"
          labelText="Phường/Xã"
          :inputRequired="false"
          @show-combobox="getWards"
          idField="ward_id"
          nameField="ward_name"
          :rowsData="dataAddress.wards"
          class="w1/5"
          tabindex="3"
        />
        <ttanh-combobox
          v-model="dataFilter.cropId"
          ref="cropId"
          type="single-row"
          labelText="Tên cây trồng"
          :rowsData="cropsRowData"
          @show-combobox="getCrops"
          idField="CropId"
          nameField="CropName"
          class="w1/5"
          tabindex="5"
        />
        <ttanh-combobox
          v-model="dataFilter.pestId"
          ref="pestId"
          type="single-row"
          labelText="Tên sâu bệnh"
          :rowsData="pestsRowData"
          @show-combobox="getPests"
          idField="PestId"
          nameField="PestName"
          class="w1/5"
          tabindex="7"
        />
      </div>
      <div class="page__filter-group page__filter-group-2">
        <ttanh-combobox
          v-model="dataFilter.districtId"
          ref="districtId"
          type="single-row"
          labelText="Quận/Huyện"
          :inputRequired="false"
          @show-combobox="getDistricts"
          idField="district_id"
          nameField="district_name"
          :rowsData="dataAddress.districts"
          class="w1/5"
          tabindex="2"
        />
        <div class="w1/5" >
          <label class="label-input"> Khoảng thời gian cảnh báo </label>
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
        <ttanh-combobox
          v-model="dataFilter.cropStageId"
          ref="cropStageId"
          type="single-row"
          labelText="Giai đoạn cây trồng"
          idField="CropStageId"
          nameField="CropStageName"
          :inputRequired="false"
          :rowsData="cropStagesRowData"
          @show-combobox="getCropStages"
          class="w1/5"
          tabindex="6"
        />
        <ttanh-combobox
          v-model="dataFilter.pestStageId"
          ref="pestStageId"
          type="single-row"
          labelText="Giai đoạn sâu bệnh"
          idField="PestStageId"
          nameField="PestStageName"
          :inputRequired="false"
          :rowsData="pestStagesRowData"
          @show-combobox="getPestStages"
          class="w1/5"
          tabindex="8"
        />
      </div>
      <div class="page__filter-group page__filter-group-3">
        <ttanh-button
          width="90px"
          type="main"
          borderRadius="var(--border-radius-default)"
          :border="batchExecutionDisable ? '' : '2px solid black'"
          :tabindex="-1"
          @clickButton="getPredicts"
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
        <div class="season-end-container">
          <div
            class="season-end-item"
            :class="!seasonEnd ? 'season-end-selected' : ''"
            @click="() => { seasonEnd = false; getPredicts()}"
          >
            Đang trong mùa vụ
          </div>
          <div
            class="season-end-item"
            :class="seasonEnd ? 'season-end-selected' : ''"
            @click="() => { seasonEnd = true; getPredicts()}"
          >
            Mùa vụ đã kết thúc
          </div>
        </div>
      </div>
      <div class="page__action-right">
        <ttanh-icon
          :icon="'page__reload--' + (pageButtonHover['page__reload'] ? 'black' : 'grey')"
          :tooltip="$t('common.reloadTooltip')"
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
          @clickButton="showAddPredictPopup"
          v-if="!farmerLimit"
          >Tạo mới</ttanh-button
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
        :oneRowSelect="true"
        :endOfSeason="!seasonEnd"
        :farmerLimit="farmerLimit"
        @checked-all="checkedAllRow"
        @unchecked-all="uncheckedAllRow"
        @checked-row="checkedRow"
        @unchecked-row="uncheckedRow"
        @doubleClickRow="openFormView"
        @clickFixBtn="openFormUpdate"
        @clickContextDeleteBtn="openConfirmDeletePopup"
        @resizeColumn="resizePredictColumn"
        @clickEndOfSeason="openConfirmEndOfSeasonPopup"
        @clickContextViewBtn="openFormView"
      />
    </div>
    <div class="page__footer">
      <ttanh-paging v-if="!this.noData" v-model="pagingData" @reloadData="reloadData" />
    </div>

    <AddPredictPopup
      v-if="isShowAddPredictPopup"
      :dataUpdate="dataUpdate"
      @clickCancelBtn="isShowAddPredictPopup = false"
      @reloadData="reloadData"
      ref="addPredictPopup"
    />

    <ViewPredictPopup
      v-if="isShowViewPredictPopup"
      :predictId="dataUpdate.PredictId"
      @clickCancelBtn="isShowViewPredictPopup = false"
      ref="viewPredictPopup"
    />

    <ttanh-delete-popup
      :titleText="computedDeletePopupText"
      v-if="isShowConfirmDeletePopup || isShowConfirmDeleteMultiplePopup"
      @no-click="isShowConfirmDeletePopup ? noDeleteBtnClick() : noDeleteMultiplePredict()"
      @yes-click="isShowConfirmDeletePopup ? yesDeleteBtnClick() : yesDeleteMultiplePredict()"
    />

    <ttanh-delete-popup
      titleText="Bạn có chắc chắn muốn kết thúc mùa vụ đã chọn không?"
      v-if="isShowConfirmEndOfSeasonPopup"
      @no-click="() => {
        this.PredictIdEndOfSeason = null
        this.isShowConfirmEndOfSeasonPopup = false
      }"
      @yes-click="confirmEndOfSeason()"
    />

    <ttanh-loading-spinner v-if="isLoading" size="large" />
  </div>
</template>

<script>
import VueDatePicker from '@vuepic/vue-datepicker'
import PredictService from '@/service/PredictService.js'
import AddressService from '@/service/AddressService.js'
import AddPredictPopup from './AddPredictPopup.vue'
import ViewPredictPopup from './ViewPredictPopup.vue'
import { CommonErrorHandle } from '@/helper/error-handle'
import { findIndexByAttribute, sortArrayByAttribute } from '@/helper/common.js'
import { formatToNumber } from '@/helper/textfield-format-helper.js'
import { debounce } from '@/helper/debounce.js'
import { isProxy, toRaw } from 'vue'
import UserService from '@/service/UserService'

export default {
  name: 'PredictContent',
  components: {
    AddPredictPopup,
    VueDatePicker,
    ViewPredictPopup
  },
  data() {
    return {
      farmerLimit: false,
      isShowViewPredictPopup: false,
      PredictIdEndOfSeason: null,
      isShowConfirmEndOfSeasonPopup: false,
      predicts: [],

      seasonEnd: false,

      cropsRowData: [],
      cropStagesRowData: [],
      pestsRowData: [],
      pestStagesRowData: [],

      dataAddress: {
        provinces: [],
        districts: [],
        wards: []
      },

      /* lưu dữ id các dự báo đã được chọn */
      selectedPredicts: [],

      PredictColumnsInfo: [
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
          id: 'CurrentStartDate',
          name: 'KHOẢNG THỜI GIAN CẢNH BÁO',
          size: '150px',
          textAlign: 'center',
          format: 'date',
          isShow: true,
          isPin: false
        },
        {
          id: 'CropName',
          name: 'TÊN CÂY TRỒNG',
          size: '150px',
          textAlign: 'center',
          format: 'text',
          isShow: true,
          isPin: false
        },
        {
          id: 'CropStageName',
          name: 'GIAI ĐOẠN CÂY TRỒNG',
          size: '150px',
          textAlign: 'center',
          format: 'text',
          isShow: true,
          isPin: false
        },
        {
          id: 'PestName',
          name: 'TÊN SÂU BỆNH',
          size: '150px',
          textAlign: 'center',
          format: 'text',
          isShow: true,
          isPin: false
        },
        {
          id: 'PestStageName',
          name: 'GIAI ĐOẠN SÂU BỆNH',
          size: '150px',
          textAlign: 'center',
          format: 'text',
          isShow: true,
          isPin: false
        },
        {
          id: 'LevelWarningName',
          name: 'MỨC ĐỘ CẢNH BÁO',
          size: '150px',
          textAlign: 'left',
          format: 'text',
          isShow: true,
          isPin: false,
          disableCombobox: true
        }
      ],

      /* thông tin cột thuần được gửi từ api đã sắp xếp */
      PredictColumnsInfoRaw: [],

      isLoading: false,
      /* các biến để xác định trạng thái trên page_action */
      tableSearchFocus: false,
      pageButtonHover: {
        page__setting: false,
        page__reload: false
      },

      /* biến xác định nút "Thực hiện hàng loạt" có disable hay không */
      batchExecutionDisable: true,

      /*== các biến sử dụng cho add-Predict-popup ==*/
      isShowAddPredictPopup: false,

      dataUpdate: null,

      /* biến sử dụng cho việc xác nhận xóa */
      isShowConfirmDeletePopup: false,
      PredictIdDelete: '',

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
        provinceId: null,
        districtId: null,
        wardId: null,
        dateRange: null,
        cropStageId: null,
        pestStageId: null,
        cropId: null,
        pestId: null
      }
    }
  },

  created() {
    // lấy dữ liệu phân trang được lưu trong local storage
    this.pagingData.pageNumber = formatToNumber(localStorage.getItem('pageNumber')) ?? 1
    this.pagingData.pageSize = formatToNumber(localStorage.getItem('pageSize')) ?? 10
  },

  async mounted() {
    if (!localStorage.getItem('provinceId')) {
      let res = await UserService.get('User/info');
      let user = res.data;
      localStorage.setItem("provinceId", user.ProvinceId);
      localStorage.setItem("districtId", user.DistrictId);
      localStorage.setItem("wardId", user.WardId);
      localStorage.setItem("provinceName", user.ProvinceName);
      localStorage.setItem("districtName", user.DistrictName);
      localStorage.setItem("wardName", user.WardName);
      localStorage.setItem("roleId", user.RoleID);
    }

    this.farmerLimit = Number(localStorage.getItem('roleId')) == this.$_TTANHEnum.ROLE_ID.FARMER

    // nếu là nông dân thì lọc trước dự báo theo tỉnh
    if (this.farmerLimit) {
      this.dataFilter.provinceId = localStorage.getItem('provinceId')
      this.dataFilter.districtId = localStorage.getItem('districtId')
      this.dataFilter.wardId = localStorage.getItem('wardId')
      this.$refs.provinceId.$refs.inputSearch.value = localStorage.getItem('provinceName')
      this.$refs.districtId.$refs.inputSearch.value = localStorage.getItem('districtName')
      this.$refs.wardId.$refs.inputSearch.value = localStorage.getItem('wardName')
    }

    //lấy dữ liệu dự báo
    this.getPredicts()
  },

  methods: {
    async getCrops() {
      let res = await PredictService.get('Predict/crop')

      if (res.statusCode === 200) {
        this.cropsRowData = res.data
      } else {
        this.cropsRowData = []
      }
    },
    async getCropStages() {
      let res = await PredictService.get('Predict/crop-stage?cropId=' + this.dataFilter.cropId)

      if (res.statusCode === 200) {
        this.cropStagesRowData = res.data
      } else {
        this.cropStagesRowData = []
      }
    },
    async getPests() {
      let res = await PredictService.get('Predict/pest')

      if (res.statusCode === 200) {
        this.pestsRowData = res.data
      } else {
        this.pestsRowData = []
      }
    },
    async getPestStages() {
      let res = await PredictService.get('Predict/pest-stage?pestId=' + this.dataFilter.pestId)

      if (res.statusCode === 200) {
        this.pestStagesRowData = res.data
      } else {
        this.pestStagesRowData = []
      }
    },

    clearFilter() {
      this.dataFilter = {
        provinceId: null,
        districtId: null,
        wardId: null,
        dateRange: null,
        cropStageId: null,
        pestStageId: null,
        cropId: null,
        pestId: null
      }

      this.$refs.provinceId.$refs.inputSearch.value = ''
      this.$refs.districtId.$refs.inputSearch.value = ''
      this.$refs.wardId.$refs.inputSearch.value = ''
      this.$refs.cropStageId.$refs.inputSearch.value = ''
      this.$refs.pestStageId.$refs.inputSearch.value = ''
      this.$refs.cropId.$refs.inputSearch.value = ''
      this.$refs.pestId.$refs.inputSearch.value = ''

      this.getPredicts()
    },
    /**
     * Sắp xếp theo ordernumber và isPin để hiển thị đúng
     * @author: TTANH (04/08/2024)
     */
    sortPredictColumnsInfo(columnsInfoTemp) {
      try {
        // sắp xếp theo thứ tự
        columnsInfoTemp = sortArrayByAttribute(columnsInfoTemp, 'OrderNumber', false)

        //đưa những cột được ghim lên đầu
        columnsInfoTemp = sortArrayByAttribute(columnsInfoTemp, 'ColumnIsPin')

        return columnsInfoTemp
      } catch (error) {
        console.log('🚀 ~ file: PredictContent.vue:263 ~ sortPredictColumnsInfo ~ error:', error)
      }
    },

    /**
     * hàm thực hiện mở thêm dự báo
     * @author: TTANH (11/07/2024)
     */
    showAddPredictPopup() {
      this.isShowAddPredictPopup = true
      this.dataUpdate = null
    },

    /**
     * thực hiện get dữ liệu dự báo khi component được render
     * @author: TTANH (30/06/2024)
     */
    async getPredicts() {
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
          StartDate: startDate,
          EndDate: endDate,
          CropStageId: this.dataFilter.cropStageId,
          PestStageId: this.dataFilter.pestStageId,
          PestId: this.dataFilter.pestId,
          CropId: this.dataFilter.cropId,
          SeasonEnd: this.seasonEnd,
          PageSize: this.pagingData.pageSize,
          PageNumber: this.pagingData.pageNumber
        }

        const res = await PredictService.filterAdvanced('Predict', dataFilter)

        if (res.success) {
          if (res.data.Data.length != 0) {
            this.predicts = res.data.Data
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
        console.log('🚀 ~ file: PredictList.vue:116 ~ getPredicts ~ error:', error)
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
     * bỏ lệnh xóa nhiều dự báo
     * @author: TTANH (31/07/2024)
     */
    noDeleteMultiplePredict() {
      this.isShowConfirmDeleteMultiplePopup = false
    },

    /**
     * xóa nhiều dự báo
     * @author: TTANH (17/07/2024)
     */
    async yesDeleteMultiplePredict() {
      var dataSendApi = null

      if (isProxy(this.selectedPredicts)) {
        dataSendApi = toRaw(this.selectedPredicts)
      } else {
        dataSendApi = this.selectedPredicts
      }

      this.isLoading = true

      const res = await PredictService.deleteMultiple('Predict', dataSendApi)

      this.isLoading = false

      if (res.success) {
        this.$store.commit('addToast', {
          type: 'success',
          text: this.$t('successHandle.PredictSubsystem.deleteMultiple', {
            count: res.data
          })
        })

        this.selectedPredicts = []
        this.isShowConfirmDeleteMultiplePopup = false

        this.reloadData()
      } else {
        CommonErrorHandle()
      }
    },

    /**
     * cập nhật lại predicts mới
     * @author: TTANH (03/07/2024)
     */
    reloadData() {
      try {
        this.previouslySelectedIndex = -1
        this.predicts = []
        this.getPredicts()
      } catch (error) {
        console.log('🚀 ~ file: PredictContent.vue:465 ~ reloadData ~ error:', error)
      }
    },

    /**
     * bỏ hết dữ liệu đã chọn khi ấn vào nút "Lấy lại dữ liệu"
     * @author: TTANH (03/07/2024)
     */
    reloadDataWithSelectedRows() {
      try {
        this.selectedPredicts = []
        this.reloadData()
      } catch (error) {
        console.log('🚀 ~ file: PredictContent.vue:282 ~ reloadDataWithSelectedRows ~ error:', error)
      }
    },

    /**
     * thêm một id vào mảng dòng đã chọn
     * @author: TTANH (11/07/2024)
     * @param {string} rowId id của dòng được chọn
     */
    addSelectedRow(rowId) {
      let index = findIndexByAttribute(this.selectedPredicts, '', rowId)

      if (index === -1) {
        this.selectedPredicts.push(rowId)
      }
    },

    /**
     * xóa một id vào mảng dòng đã chọn
     * @author: TTANH (11/07/2024)
     * @param {string} rowId id của dòng được chọn
     */
    deleteSelectedRow(rowId) {
      let index = findIndexByAttribute(this.selectedPredicts, '', rowId)

      if (index !== -1) {
        this.selectedPredicts.splice(index, 1)
      }
    },

    /**
     * xử lý khi chọn checkbox ở header
     * @author: TTANH (27/06/2024)
     */
    checkedAllRow() {
      try {
        this.predicts.forEach((Predict) => {
          this.addSelectedRow(Predict.PredictId)
        })
      } catch (error) {
        console.log('🚀 ~ file: PredictList.vue:463 ~ checkedAllRow ~ error:', error)
      }
    },

    /**
     * xử lý khi bỏ chọn checkbox ở header
     * @author: TTANH (27/06/2024)
     */
    uncheckedAllRow() {
      try {
        this.predicts.forEach((Predict) => {
          this.deleteSelectedRow(Predict.PredictId)
        })
      } catch (error) {
        console.log('🚀 ~ file: PredictList.vue:475 ~ uncheckedAllRow ~ error:', error)
      }
    },

    /**
     * xử lý khi chọn checkbox ở 1 row
     * @author: TTANH (27/06/2024)
     * @param {string} rowId: id của record được chọn
     */
    checkedRow(rowId) {
      try {
        let indexNewChecked = findIndexByAttribute(this.predicts, 'PredictId', rowId)

        if (event.shiftKey) {
          event.preventDefault()

          if (this.previouslySelectedIndex === -1) {
            this.addSelectedRow(rowId)
          } else {
            if (this.previouslySelectedIndex > indexNewChecked) {
              for (let index = indexNewChecked; index <= this.previouslySelectedIndex; index++) {
                const Predict = this.predicts[index]

                this.addSelectedRow(Predict.PredictId)
              }
            } else if (this.previouslySelectedIndex < indexNewChecked) {
              for (let index = this.previouslySelectedIndex; index <= indexNewChecked; index++) {
                const Predict = this.predicts[index]

                this.addSelectedRow(Predict.PredictId)
              }
            } else {
            }
          }
        } else {
          this.addSelectedRow(rowId)
        }

        this.previouslySelectedIndex = indexNewChecked
      } catch (error) {
        console.log('🚀 ~ file: PredictList.vue:492 ~ uncheckedAllRow ~ error:', error)
      }
    },

    /**
     * xử lý khi bỏ chọn checkbox ở 1 row
     * @author: TTANH (27/06/2024)
     * @param {string} rowId: id của record được bỏ chọn
     */
    uncheckedRow(rowId) {
      try {
        let indexNewChecked = findIndexByAttribute(this.predicts, 'PredictId', rowId)

        if (event.shiftKey) {
          event.preventDefault()

          if (this.previouslySelectedIndex === -1) {
            this.deleteSelectedRow(rowId)
          } else {
            if (this.previouslySelectedIndex > indexNewChecked) {
              for (let index = indexNewChecked; index <= this.previouslySelectedIndex; index++) {
                const Predict = this.predicts[index]

                this.deleteSelectedRow(Predict.PredictId)
              }
            } else if (this.previouslySelectedIndex < indexNewChecked) {
              for (let index = this.previouslySelectedIndex; index <= indexNewChecked; index++) {
                const Predict = this.predicts[index]

                this.deleteSelectedRow(Predict.PredictId)
              }
            } else {
            }
          }
        } else {
          this.deleteSelectedRow(rowId)
        }

        this.previouslySelectedIndex = indexNewChecked
      } catch (error) {
        console.log('🚀 ~ file: PredictList.vue:492 ~ uncheckedAllRow ~ error:', error)
      }
    },

    /**
     * mở form update
     * @param {string} rowId id của đối tượng muốn update
     */
    openFormUpdate(rowId) {
      try {
        let indexRow = findIndexByAttribute(this.predicts, 'PredictId', rowId)

        this.isShowAddPredictPopup = true
        this.dataUpdate = this.predicts[indexRow]
      } catch (error) {
        console.log('🚀 ~ file: PredictContent.vue:529 ~ openFormUpdate ~ error:', error)
      }
    },

    openFormView(rowId) {
      try {
        let indexRow = findIndexByAttribute(this.predicts, 'PredictId', rowId)

        this.isShowViewPredictPopup = true
        this.dataUpdate = this.predicts[indexRow]
      } catch (error) {
        console.log('🚀 ~ file: PredictContent.vue:529 ~ openFormUpdate ~ error:', error)
      }
    },

    /**
     * mở form xác nhận xóa
     * @author: TTANH (01/07/2024)
     * @param {string} id id của bản ghi cần xóa
     */
    openConfirmDeletePopup(id) {
      try {
        let index = findIndexByAttribute(this.predicts, 'PredictId', id)

        if (index !== -1) {
          this.PredictIdDelete = id
          this.isShowConfirmDeletePopup = true
        } else {
          this.$store.commit('addToast', {
            type: 'error',
            text: this.$t('errorHandle.PredictSubsystem.notFoundPredict')
          })

          this.reloadData()
        }
      } catch (error) {
        console.log('🚀 ~ file: PredictContent.vue:351 ~ openConfirmDeletePopup ~ error:', error)
      }
    },

    /**
     * mở form xác nhận xóa
     * @author: TTANH (01/07/2024)
     * @param {string} id id của bản ghi cần xóa
     */
    openConfirmEndOfSeasonPopup(id) {
      try {
        let index = findIndexByAttribute(this.predicts, 'PredictId', id)

        if (index !== -1) {
          this.PredictIdEndOfSeason = id
          this.isShowConfirmEndOfSeasonPopup = true
        } else {
          this.$store.commit('addToast', {
            type: 'error',
            text: this.$t('errorHandle.PredictSubsystem.notFoundPredict')
          })

          this.reloadData()
        }
      } catch (error) {
        console.log('🚀 ~ file: PredictContent.vue:351 ~ openConfirmEndOfSeasonPopup ~ error:', error)
      }
    },

    /**
     * mở form nhân bản
     * @param {string} rowId id của đối tượng muốn nhân bản
     */
    openFormDuplicate(rowId) {
      try {
        let indexRow = findIndexByAttribute(this.predicts, 'PredictId', rowId)

        this.isShowAddPredictPopup = true
        this.dataUpdate = this.predicts[indexRow]

        this.$nextTick(() => {
          // thay đổi trạng thái form thành thêm mới
          this.$refs.addPredictPopup.changeFormModeToAdd()
        })
      } catch (error) {
        console.log('🚀 ~ file: PredictContent.vue:529 ~ openFormUpdate ~ error:', error)
      }
    },

    /**
     * đóng form xác nhận xóa
     * @author: TTANH (01/07/2024)
     */
    closeConfirmDeletePopup() {
      try {
        this.PredictIdDelete = ''
        this.isShowConfirmDeletePopup = false
      } catch (error) {
        console.log('🚀 ~ file: PredictContent.vue:386 ~ closeConfirmDeletePopup ~ error:', error)
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
        console.log('🚀 ~ file: PredictContent.vue:401 ~ noDeleteBtnClick ~ error:', error)
      }
    },

    /**
     * xác nhận xóa
     * @author: TTANH (01/07/2024)
     */
    yesDeleteBtnClick() {
      try {
        this.deleteSelectedRow(this.PredictIdDelete)
        this.deleteRecord()
        this.closeConfirmDeletePopup()
      } catch (error) {
        console.log('🚀 ~ file: PredictContent.vue:416 ~ yesDeleteBtnClick ~ error:', error)
      }
    },

    /**
     * thực hiện xóa 1 bản ghi
     * @author: TTANH (01/07/2024)
     */
    async deleteRecord() {
      try {
        this.isLoading = true
        const res = await PredictService.delete('Predict', this.PredictIdDelete)

        if (res.success) {
          this.$store.commit('addToast', {
            type: 'success',
            text: 'Xóa dự báo thành công'
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
        console.log('🚀 ~ file: PredictContent.vue:582 ~ deleteRecord ~ error:', error)
      }
    },
    
    async confirmEndOfSeason() {
      this.isShowConfirmEndOfSeasonPopup = false

      try {
        this.isLoading = true
        const res = await PredictService.get(`Predict/season-end?PredictId=${this.PredictIdEndOfSeason}`)

        if (res.success) {
          this.$store.commit('addToast', {
            type: 'success',
            text: 'Kết thúc mùa vụ thành công'
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
        console.log('🚀 ~ file: PredictContent.vue:582 ~ confirmEndOfSeason ~ error:', error)
      }
    },

    /**
     * thực hiện thay đổi kích thước của cột
     * @author: TTANH (04/07/2024)
     */
    resizePredictColumn(index, resizeWidth) {
      try {
        this.isUpdateColumnsInfo = true
        this.PredictColumnsInfo[index].size = resizeWidth
      } catch (error) {
        console.log('🚀 ~ file: PredictContent.vue:524 ~ resizePredictColumn ~ error:', error)
      }
    },

    /**
     * xử lý các phím tắt
     * @author: TTANH (11/07/2024)
     */
    handleKeydown(event) {
      if (event.keyCode === this.$_TTANHEnum.KEY_CODE.INSERT && event.ctrlKey) {
        this.$router.push('/app/Predict/import')
      } else if (event.keyCode === this.$_TTANHEnum.KEY_CODE.INSERT) {
        this.showAddPredictPopup()
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

      let indexInRaw = findIndexByAttribute(this.PredictColumnsInfoRaw, 'ServerColumnName', codeData.id)

      let PredictColumnInfoRaw = this.PredictColumnsInfoRaw[indexInRaw]

      let tempMap = {}

      tempMap.PredictLayoutId = PredictColumnInfoRaw.PredictLayoutId
      tempMap.viClientColumnName = PredictColumnInfoRaw.viClientColumnName
      tempMap.enClientColumnName = PredictColumnInfoRaw.enClientColumnName
      tempMap.OrderNumber = PredictColumnInfoRaw.OrderNumber

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

      const res = await PredictLayoutService.updateMultiple(datasUpdate)

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
    computedPredicts() {
      try {
        let haveIdPredicts = []

        this.predicts.forEach((Predict, index) => {
          let id = Predict.PredictId
          haveIdPredicts.push({
            id,
            ...Predict
          })
        })

        return haveIdPredicts
      } catch (error) {
        console.log('🚀 ~ file: PredictList.vue:457 ~ computedPredicts ~ error:', error)
      }
    },

    computedSelectedPredicts() {
      if (this.selectedPredicts.length <= 1) {
        this.batchExecutionDisable = true
      } else {
        this.batchExecutionDisable = false
      }
      return this.selectedPredicts
    },

    computedNoData() {
      return this.noData
    },

    computedDeletePopupText() {
      if (this.isShowConfirmDeletePopup) {
        return this.$t('Bạn có thực sự muốn xóa dự báo không?')
      } else if (this.isShowConfirmDeleteMultiplePopup) {
        return this.$t('PredictSubsystem.PredictContent.deleteMultiplePopupTitle', {
          count: this.selectedPredicts.length
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

    PredictColumnsInfoRaw(newValue) {
      let tempMapArray = this.mapColumnsInfoFromRawToCode(newValue)
      /**
       * do là việc lấy dữ liệu gây ra thay đổi cho PredictColumnsInfo
       * nên không gọi đến hàm cập nhật
       */
      this.isUpdateColumnsInfo = false

      this.PredictColumnsInfo = tempMapArray
    },

    PredictColumnsInfo: {
      handler: debounce(function () {
        if (this.isUpdateColumnsInfo) {
          this.updateColumnsInfoToDB(this.PredictColumnsInfo)
        }
      }, 500),

      deep: true
    }
  }
}
</script>

<style scoped>
@import url(./predict-content.css);
</style>
