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
        <div style="overflow: scroll; height: 100vh; width: 99vw;">
          <div class="h1/3 w1 flex-row align-item--start" style="padding-bottom: 4px">
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

          <div class="h1/3 w1 flex-row align-item--start" style="padding: 14px 0px">
            <div class="w1/3" style="padding-right: 26px">
              <div class="flex-row p-b-8 label-add-group">Mức độ hiện tại</div>
              <div class="flex-row p-b-8">
                <ttanh-textfield
                  v-model="viewPredictData.LevelWarningName"
                  idInput="LevelWarningName"
                  labelText="Mức độ cảnh báo"
                  class="w1"
                  tabindex="2"
                  :disable="true"
                />
              </div>
              <div class="flex-row p-b-8">
                <ttanh-textfield
                  v-model="viewPredictData.PestStageName"
                  idInput="PestStageName"
                  labelText="Mức độ sâu bệnh"
                  class="w1"
                  tabindex="2"
                  :disable="true"
                />
              </div>
              <div class="flex-row p-b-8">
                <ttanh-textfield
                  v-model="viewPredictData.CropStageName"
                  idInput="CropStageName"
                  labelText="Giai đoạn cây trồng"
                  class="w1"
                  tabindex="2"
                  :disable="true"
                />
              </div>
            </div>
            <div class="w1/3 flex-column" style="padding-right: 26px">
              <div class="flex-row p-b-8 label-add-group">Ngày kết thúc</div>
                <ttanh-textfield
                  v-model="viewPredictData.CurrentEndDate"
                  idInput="CurrentEndDate"
                  labelText="Ngày kết thúc (dự kiến)"
                  class="w1"
                  tabindex="2"
                  :disable="true"
                />
            </div>
          </div>
          
          <ttanh-separation-line
            style="
              width: 100%;
              border-top: 2px solid var(--border-color-default);
              margin: 0px;
            "
          />

          <div class="w1 flex-row align-item--start" style="padding: 19px 0px; height: 400px">
            <div class="w1">
              <div class="flex-row p-b-8 label-add-group" style="margin-bottom: 8px;">Dữ liệu dự báo</div>
              <div class="daily-forecast-table-group">
                <v-calendar
                  v-model="selectedDate"
                  :attributes="attributes"
                  @dayclick="onDayClick"
                ></v-calendar>

                <div class="details-container">
                  <div class="flex-row p-b-8">
                    <ttanh-textfield
                      v-model="selectedDate"
                      idInput="selectedDate"
                      labelText="Ngày"
                      class="w1"
                      :disable="true"
                    />
                  </div>
                  <div class="flex-row p-b-8">
                    <ttanh-textfield
                      v-model="levelWarning"
                      idInput="levelWarning"
                      labelText="Mức độ cảnh báo"
                      class="w1"
                      :disable="true"
                    />
                  </div>
                  <div class="flex-row p-b-8">
                    <ttanh-textfield
                      v-model="pestStage"
                      idInput="pestStage"
                      labelText="Giai đoạn sinh trưởng của côn trùng"
                      class="w1"
                      :disable="true"
                    />
                  </div>
                  <div class="flex-row p-b-8">
                    <ttanh-textfield
                      v-model="cropStage"
                      idInput="cropStage"
                      labelText="Giai đoạn sinh trưởng của cây"
                      class="w1"
                      :disable="true"
                    />
                  </div>
                </div>

                <div class="details-container">
                  <div class="flex-row p-b-8">
                    <ttanh-textfield
                      v-model="temp"
                      idInput="temp"
                      labelText="Nhiệt độ"
                      class="w1"
                      :disable="true"
                    />
                  </div>
                  <div class="flex-row p-b-8">
                    <ttanh-textfield
                      v-model="humidity"
                      idInput="humidity"
                      labelText="Độ ẩm"
                      class="w1"
                      :disable="true"
                    />
                  </div>
                  <div class="flex-row p-b-8">
                    <ttanh-textfield
                      v-model="rain"
                      idInput="rain"
                      labelText="Lượng mưa"
                      class="w1"
                      :disable="true"
                    />
                  </div>
                </div>
              </div>
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
    this.viewPredictData.CurrentEndDate = formatToDate(this.viewPredictData.CurrentEndDate, this.$store.state.formatDate)

    if (this.viewPredictData.DailyForecast) {
      this.dailyForecast = sortArrayByAttribute(JSON.parse(this.viewPredictData.DailyForecast), 'date');
    } else {
      this.dailyForecast = []
    }
    this.attributes = [];

    for(let i = 0; i < this.dailyForecast.length; i++) {
      this.attributes.push({
        dates: this.dailyForecast[i].date,
        key: this.dailyForecast[i].date,
        customData: this.dailyForecast[i],
        highlight: this.getColorWarning(this.dailyForecast[i].level_warning)
      });
    }
    
    this.isLoading = false
  },
  data() {
    return {
      selectedDate: null,
      levelWarning: null,
      pestStage: null,
      cropStage: null,
      temp: null,
      humidity: null,
      rain: null,
      attributes: [],
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
          id: 'crop_stage_name',
          name: 'Trạng thái cây trồng',
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

    getColorWarning(level) {
      if (level === 'Mức 1') {
        return 'green'
      } else if (level === 'Mức 2') {
        return 'yellow'
      } else if (level === 'Mức 3') {
        return 'red'
      }
    },

    /**
     * thực hiện kiểm tra trước khi đóng form
     * @author: TTANH (07/08/2024)
     */
    closeAddForm() {
      this.$emit('clickCancelBtn')
    },

    onDayClick(day) {
      this.selectedDate = day.ariaLabel;

      let customData = day.attributes[0]?.customData;

      if (customData) {
        this.temp = customData.temp;
        this.humidity = customData.humidity;
        this.rain = customData.rain;
        this.levelWarning = customData.level_warning;
        this.pestStage = customData.stage;
        this.cropStage = customData.crop_stage_name;
      } else {
        this.temp = null;
        this.humidity = null;
        this.rain = null;
        this.levelWarning = null;
        this.pestStage = null;
        this.cropStage = null;
      }

    }
  }
}
</script>

<style>
@import url(./view-predict-popup.css);
</style>
