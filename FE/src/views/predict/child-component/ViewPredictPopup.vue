<template>
  <div class="m-overlay">
    <ttanh-popup style="overflow: visible" title="Xem chi tiết dự báo" id="view-predict-popup">
      <template #header__close>
        <ttanh-icon
          @click="closeAddForm"
          icon="close"
          style="margin-left: 3px"
          :title="$t('common.closeIconTooltip')"
        />
      </template>
      <template #content__input-control>
        <div class="w1 flex-row align-item--start" style="padding-bottom: 4px">
          <div class="w1/3" style="padding-right: 26px">
            <div class="flex-row p-b-8 label-add-group">Thông tin khu vực</div>
            <div class="flex-row p-b-8">
              <ttanh-textfield
                v-model="viewPredictData.ProvinceName"
                idInput="ProvinceName"
                labelText="Tỉnh/Thành phố"
                class="w1"
                tabindex="2"
                :disable="true"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-textfield
                v-model="viewPredictData.DistrictName"
                idInput="DistrictName"
                labelText="Quận/Huyện"
                class="w1"
                tabindex="2"
                :disable="true"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-textfield
                v-model="viewPredictData.WardName"
                idInput="WardName"
                labelText="Phường/Xã"
                class="w1"
                tabindex="2"
                :disable="true"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-textfield
                v-model="viewPredictData.Address"
                idInput="Address"
                labelText="Địa chỉ"
                class="w1"
                tabindex="2"
                :disable="true"
              />
            </div>
          </div>
          <div class="w1/3 flex-column" style="padding-right: 26px">
            <div class="flex-row p-b-8 label-add-group">Thông tin phòng tránh</div>
              <ttanh-textfield
                v-model="viewPredictData.CropName"
                idInput="CropName"
                labelText="Tên cây trồng"
                class="w1"
                tabindex="2"
                :disable="true"
              />
              <ttanh-textfield
                v-model="viewPredictData.PestName"
                idInput="PestName"
                labelText="Tên sâu bệnh"
                class="w1"
                tabindex="2"
                :disable="true"
              />
          </div>
          <div class="w1/3">
            <div class="flex-row p-b-8 label-add-group">Thông tin vụ trước</div>
            <div class="flex-row p-b-8">
              <ttanh-textfield
                v-model="viewPredictData.PreviousEndDate"
                idInput="PreviousEndDate"
                labelText="Thời điểm kết thúc"
                class="w1"
                tabindex="2"
                :disable="true"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-textfield
                v-model="viewPredictData.PreviousLevelWarningName"
                idInput="PreviousLevelWarningName"
                labelText="Mức độ cảnh báo"
                class="w1"
                tabindex="2"
                :disable="true"
              />
            </div>
            <div class="flex-row p-b-8 label-add-group m-t-26">Thông tin vụ này</div>
            <div class="flex-row p-b-8">
              <ttanh-textfield
                v-model="viewPredictData.CurrentStartDate"
                idInput="CurrentStartDate"
                labelText="Thời điểm bắt đầu"
                class="w1"
                tabindex="2"
                :disable="true"
              />
            </div>
          </div>
        </div>
        
        <ttanh-separation-line
          style="
            width: 100%;
            border-top: 2px solid var(--border-color-default);
            margin: 0px;
          "
        />
        <div class="w1 flex-row align-item--start" style="padding-top: 4px">
          <div class="w1">
            <div class="flex-row p-b-8 label-add-group">Thông tin dự báo</div>
            <div class="daily-forecast-table-group">
              <ttanh-table
                :columnsInfo="dailyForecastColumnsInfo"
                :rowsData="dailyForecast"
                :oneRowSelect="true"
                :noAction="true"
                :noData="dailyForecast.length === 0"
              />
            </div>
          </div>
        </div>
      </template>
    </ttanh-popup>

    <ttanh-loading-spinner v-if="isLoading" size="large" />
  </div>
</template>

<script>
import PredictService from '@/service/PredictService.js'
import { formatToDate } from '@/helper/format-helper.js'
import { sortArrayByAttribute } from '@/helper/common.js'

export default {
  name: 'ViewPredictPopup',
  props: {
    predictId: {
      default: null
    }
  },

  async mounted() {
    this.isLoading = true

    let res = await PredictService.get(`Predict/${this.predictId}`)
    
    this.viewPredictData = res.data
    //format date
    this.viewPredictData.PreviousEndDate = formatToDate(this.viewPredictData.PreviousEndDate, this.$store.state.formatDate)
    this.viewPredictData.CurrentStartDate = formatToDate(this.viewPredictData.CurrentStartDate, this.$store.state.formatDate)

    if (this.viewPredictData.DailyForecast) {
      this.dailyForecast = sortArrayByAttribute(JSON.parse(this.viewPredictData.DailyForecast), 'date');
    } else {
      this.dailyForecast = []
    }
    
    this.isLoading = false
  },

  data() {
    return {
      viewPredictData: {},
      isLoading: false,
      dailyForecast: [],
      dailyForecastColumnsInfo: [
        {
          id: 'date',
          name: 'Ngày',
          size: '150px',
          textAlign: 'left',
          format: 'date',
          isShow: true,
          isPin: false
        },
        {
          id: 'level_warning',
          name: 'Mức độ cảnh báo',
          size: '150px',
          textAlign: 'left',
          format: 'text',
          isShow: true,
          isPin: false
        },
        {
          id: 'stage',
          name: 'Trạng thái sâu bệnh',
          size: '150px',
          textAlign: 'left',
          format: 'text',
          isShow: true,
          isPin: false
        },
        {
          id: 'temp',
          name: 'Nhiệt độ',
          size: '150px',
          textAlign: 'left',
          format: 'text',
          isShow: true,
          isPin: false
        },
        {
          id: 'rain',
          name: 'Lượng mưa',
          size: '150px',
          textAlign: 'left',
          format: 'text',
          isShow: true,
          isPin: false
        },
        {
          id: 'humidity',
          name: 'Độ ẩm',
          size: '150px',
          textAlign: 'left',
          format: 'text',
          isShow: true,
          isPin: false
        }
      ]
    }
  },

  methods: {
    /**
     * thực hiện kiểm tra trước khi đóng form
     * @author: TTANH (07/08/2024)
     */
    closeAddForm() {
      this.$emit('clickCancelBtn')
    },
  }
}
</script>

<style>
@import url(./view-predict-popup.css);
</style>
