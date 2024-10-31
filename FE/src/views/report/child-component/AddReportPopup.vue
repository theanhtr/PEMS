<template>
  <div class="m-overlay" id="add-report-popup">
    <ttanh-popup style="overflow: visible" :title="titleForm">
      <template #header__close>
        <ttanh-icon
          @click="closeAddForm"
          icon="close"
          style="margin-left: 3px"
          :title="$t('common.closeIconTooltip')"
        />
      </template>
      <template #content__input-control>
        <div class="w1 flex-row" style="padding-bottom: 12px">
          <div class="w1/2" style="padding-right: 26px">
            <div class="flex-row p-b-8">
              <ttanh-combobox
                :errorText="errorTextreportData.provinceId"
                v-model="addreportData.provinceId"
                ref="provinceId"
                type="single-row"
                labelText="Tỉnh/Thành phố"
                :inputRequired="true"
                @show-combobox="getProvinces"
                :textInputCreated="addreportData.provinceName"
                idField="province_id"
                nameField="province_name"
                :rowsData="dataAddress.provinces"
                class="w1"
                tabindex="1"
                :disableCombobox="formMode === $_TTANHEnum.FORM_MODE.VIEW"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-combobox
                :errorText="errorTextreportData.districtId"
                v-model="addreportData.districtId"
                ref="districtId"
                type="single-row"
                labelText="Quận/Huyện"
                :inputRequired="true"
                @show-combobox="getDistricts"
                :textInputCreated="addreportData.districtName"
                idField="district_id"
                nameField="district_name"
                :rowsData="dataAddress.districts"
                class="w1"
                tabindex="2"
                :disableCombobox="formMode === $_TTANHEnum.FORM_MODE.VIEW"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-combobox
                :errorText="errorTextreportData.wardId"
                v-model="addreportData.wardId"
                ref="wardId"
                type="single-row"
                labelText="Phường/Xã"
                :inputRequired="true"
                @show-combobox="getWards"
                :textInputCreated="addreportData.wardName"
                idField="ward_id"
                nameField="ward_name"
                :rowsData="dataAddress.wards"
                class="w1"
                tabindex="3"
                :disableCombobox="formMode === $_TTANHEnum.FORM_MODE.VIEW"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-textfield
                :errorText="errorTextreportData.address"
                v-model="addreportData.address"
                type="text"
                idInput="add__address"
                labelText="
                  Địa chỉ
                "
                :inputRequired="false"
                class="w1"
                ref="fullName"
                tabindex="2"
                :disable="formMode === $_TTANHEnum.FORM_MODE.VIEW"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-textfield
                :errorText="errorTextreportData.reportName"
                v-model="addreportData.reportName"
                ref="reportName"
                labelText="Người báo cáo"
                :inputRequired="true"
                class="w1"
                tabindex="3"
                :disable="formMode === $_TTANHEnum.FORM_MODE.VIEW"
              />
            </div>
          </div>
          <div class="w1/2">
            <div class="flex-row p-b-8">
              <ttanh-combobox
                :errorText="errorTextreportData.cropId"
                v-model="addreportData.cropId"
                ref="cropId"
                type="single-row"
                labelText="Tên cây trồng"
                :inputRequired="true"
                :rowsData="cropsRowData"
                @show-combobox="getCrops"
                idField="CropId"
                nameField="CropName"
                :textInputCreated="addreportData.cropName"
                class="w1"
                tabindex="3"
                :disableCombobox="formMode === $_TTANHEnum.FORM_MODE.VIEW"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-combobox
                :errorText="errorTextreportData.cropStageId"
                v-model="addreportData.cropStageId"
                ref="cropStageId"
                type="single-row"
                labelText="Giai đoạn cây trồng"
                :inputRequired="true"
                :rowsData="cropStagesRowData"
                @show-combobox="getCropStages"
                idField="CropStageId"
                nameField="CropStageName"
                :textInputCreated="addreportData.cropStageName"
                class="w1"
                tabindex="3"
                :disableCombobox="formMode === $_TTANHEnum.FORM_MODE.VIEW"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-combobox
                :errorText="errorTextreportData.pestId"
                v-model="addreportData.pestId"
                ref="pestId"
                type="single-row"
                labelText="Tên sâu bệnh"
                :inputRequired="false"
                :rowsData="pestsRowData"
                @show-combobox="getPests"
                idField="PestId"
                nameField="PestName"
                :textInputCreated="addreportData.pestName"
                class="w1"
                tabindex="3"
                :disableCombobox="formMode === $_TTANHEnum.FORM_MODE.VIEW"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-combobox
                :errorText="errorTextreportData.pestStageId"
                v-model="addreportData.pestStageId"
                ref="pestStageId"
                type="single-row"
                labelText="Giai đoạn sâu bệnh"
                :inputRequired="false"
                :rowsData="pestStagesRowData"
                @show-combobox="getPestStages"
                idField="PestStageId"
                nameField="PestStageName"
                :textInputCreated="addreportData.pestStageName"
                class="w1"
                tabindex="3"
                :disableCombobox="formMode === $_TTANHEnum.FORM_MODE.VIEW"
              />
            </div>
          </div>
        </div>
      </template>
      <template #footer v-if="formMode != $_TTANHEnum.FORM_MODE.VIEW">
        <ttanh-separation-line style="border-color: var(--border-color-default); margin: 16px 0px" />
        <div class="flex-row" style="justify-content: space-between; padding-bottom: 16px">
          <div>
            <ttanh-button
              type="sub"
              width="58px"
              tabindex="20"
              borderRadius="var(--border-radius-default)"
              @clickBtnContainer="$emit('clickCancelBtn')"
              >{{ $t('common.button.cancel') }}</ttanh-button
            >
          </div>
          <div>
            <ttanh-button
              type="main"
              width="56px"
              tabindex="22"
              borderRadius="var(--border-radius-default)"
              @clickBtnContainer="storeBtnClick"
              ref="storeBtn"
              :tooltip="$t('common.buttonTooltip.storeAndAdd')"
              >Lưu</ttanh-button
            >
          </div>
        </div>
      </template>
    </ttanh-popup>

    <ttanh-error-popup
      v-if="isShowDialogError"
      :errorText="getFirstError.errorText"
      @close-click="closeBtnDialogErrorClick"
    />

    <ttanh-out-confirm-popup
      @cancel-click="
        () => {
          isShowOutConfirmPopup = false
          $refs.provinceId.focus()
        }
      "
      @no-click="$emit('clickCancelBtn')"
      @yes-click="
        () => {
          isShowOutConfirmPopup = false
          storeBtnClick()
        }
      "
      v-if="isShowOutConfirmPopup"
    />

    <ttanh-loading-spinner v-if="isLoading" size="large" />
  </div>
</template>

<script>
import reportService from '@/service/reportService.js'
import { ValidateConfig } from '@/config/config.js'
import { calTitleForm, isObjectEmpty } from '@/helper/common.js'
import { lengthValidate, emptyValidate, regexValidate } from '@/helper/validate.js'
import { CommonErrorHandle } from '@/helper/error-handle'
import { capitalizeFirstLetter } from '@/helper/format-helper'
import AddressService from '@/service/AddressService.js'
import PredictService from '@/service/PredictService.js'

export default {
  name: 'AddreportPopup',
  props: {
    isViewOnly: {
      default: false
    },
    dataUpdate: {
      default: null
    }
  },

  async created() {
    this.formMode = this.computedFormMode
    this.titleForm = calTitleForm(this.formMode) + 'báo cáo';
    //cập nhật thông tin cho form: form_mode, data
    await this.addInfoForm()
    this.copyAddreportData = JSON.parse(JSON.stringify(this.addreportData))
  },

  mounted() {
    //foucs vào report code lần đầu mở form
    this.$refs.provinceId.focus()
    this.$refs.provinceId.setValueInput(this.addreportData.provinceId)
    this.$refs.districtId.setValueInput(this.addreportData.districtId)
    this.$refs.wardId.setValueInput(this.addreportData.wardId)
  },

  unmounted() {},

  data() {
    return {
      titleForm: '',
      dataAddress: {
        provinces: [],
        districts: [],
        wards: []
      },

      isShowOutConfirmPopup: false,
      isShowDialogError: false,
      isLoading: false,

      formMode: this.$_TTANHEnum.FORM_MODE.ADD,

      cropsRowData: [],
      cropStagesRowData: [],
      pestsRowData: [],
      pestStagesRowData: [],

      addreportData: {
        provinceId: null,
        provinceName: '',
        districtId: null,
        districtName: '',
        wardId: null,
        wardName: '',
        address: '',
        pestStageId: null,
        cropStageId: null,
        pestStageName: '',
        cropStageName: '',
        cropName: '',
        cropId: null,
        pestId: null,
        pestName: '',
        reportName: ''
      },

      /**
       * Dùng để kiểm tra thay đổi của addreportData
       * do vue sẽ lưu tham chiếu nên không ktra trực tiếp
       * được ở watch
       */
      copyAddreportData: {},

      /**
       * xác định những loại validate của từng trường
       * các validate cách nhau bởi dấu ,
       * các loại validate:
       *  +) Empty
       *  +) MaxLength20, MaxLength25, MaxLength50, MaxLength100, MaxLength255
       *  +) NotGreaterThanToday
       *  +) PhoneNumber
       *  +) OnlyNumbers
       */
      validatereportData: {
        provinceId: 'Empty',
        districtId: 'Empty',
        wardId: 'Empty',
        address: 'MaxLength255',
        cropStageId: 'Empty',
        cropId: 'Empty',
        reportName: 'Empty'
      },

      errorTextreportData: {
        provinceId: '',
        districtId: '',
        wardId: '',
        address: '',
        pestStageId: '',
        cropStageId: '',
        pestStageName: '',
        cropStageName: '',
        cropName: '',
        cropId: '',
        pestId: '',
        pestName: '',
        reportName: ''
      }
    }
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
      let res = await PredictService.get('Predict/crop-stage?cropId=' + this.addreportData.cropId)

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
      let res = await PredictService.get('Predict/pest-stage?pestId=' + this.addreportData.pestId)

      if (res.statusCode === 200) {
        this.pestStagesRowData = res.data
      } else {
        this.pestStagesRowData = []
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
      let districts = await AddressService.district(this.addreportData.provinceId)

      if (districts.status === 200) {
        this.dataAddress.districts = districts.data.results
      } else {
        this.dataAddress.districts = []
      }
    },

    async getWards() {
      let wards = await AddressService.ward(this.addreportData.districtId)

      if (wards.status === 200) {
        this.dataAddress.wards = wards.data.results
      } else {
        this.dataAddress.wards = []
      }
    },

    /**
     * thực hiện kiểm tra trước khi đóng form
     * @author: TTANH (07/08/2024)
     */
    closeAddForm() {
      if (this.formMode == this.$_TTANHEnum.FORM_MODE.ADD) {
        this.isShowOutConfirmPopup = true
      } else if (this.formMode == this.$_TTANHEnum.FORM_MODE.UPDATE) {
        let difference = false

        for (let attr in this.addreportData) {
          let Attr = capitalizeFirstLetter(attr)

          let newData = this.addreportData[attr]
          let oldData = this.dataUpdate[Attr]

          if ((newData == '' || newData == null) && (oldData == '' || oldData == null)) {
          } else {
            if (oldData !== newData) {
              difference = true
            }
          }
        }

        if (difference) {
          this.isShowOutConfirmPopup = true
        } else {
          this.$emit('clickCancelBtn')
        }
      } else {
        this.$emit('clickCancelBtn')
      }
    },

    /**
     * cập nhật thông tin cho form: form_mode, data
     * @author: TTANH (01/07/2024)
     */
    async addInfoForm() {
      if (this.formMode === this.$_TTANHEnum.FORM_MODE.ADD) {
        this.resetAddreportData()
      } else if (this.formMode === this.$_TTANHEnum.FORM_MODE.UPDATE || this.formMode === this.$_TTANHEnum.FORM_MODE.VIEW) {
        for (let attr in this.dataUpdate) {
          let formatAttr = attr[0].toLowerCase() + attr.slice(1, attr.length)

          this.addreportData[formatAttr] = this.dataUpdate[attr] !== null ? this.dataUpdate[attr] : ''
        }
      }
    },

    /**
     * thay đổi form cập nhật thành form thêm mới cho chức năng nhân bản
     * @author: TTANH (01/07/2024)
     */
    changeFormModeToAdd() {
      try {
        this.formMode = this.$_TTANHEnum.FORM_MODE.ADD
      } catch (error) {
        console.log('🚀 ~ file: AddreportPopup.vue:675 ~ changeFormModeToAdd ~ error:', error)
      }
    },

    /**
     * xử lý khi ấn vào nút "Cất"
     * @author: TTANH (01/07/2024)
     */
    async storeBtnClick() {
      try {
        let isSuccess = await this.createNewreport()

        if (isSuccess) {
          this.$emit('clickCancelBtn')
          this.$emit('reloadData')
        }
      } catch (error) {
        console.log('🚀 ~ file: AddreportPopup.vue:688 ~ storeBtnClick ~ error:', error)
      }
    },

    /**
     * validate và tạo 1 nhân viên mới hoặc update thông tin nhân viên
     * @author: TTANH (01/07/2024)
     */
    async createNewreport() {
      if (this.validateData()) {
        let isSuccess = true
        this.isLoading = true

        this.addreportData.provinceName = this.$refs.provinceId.getCurrentInputValue()
        this.addreportData.districtName = this.$refs.districtId.getCurrentInputValue()
        this.addreportData.wardName = this.$refs.wardId.getCurrentInputValue()

        this.addreportData.pestStageName = this.$refs.pestStageId.getCurrentInputValue()
        this.addreportData.cropStageName = this.$refs.cropStageId.getCurrentInputValue()
        this.addreportData.cropName = this.$refs.cropId.getCurrentInputValue()
        this.addreportData.pestName = this.$refs.pestId.getCurrentInputValue()

        //lọc loại những trường rỗng
        var dataSendApi = {}

        for (const key in this.addreportData) {
          if (key === 'gender') {
            if (this.addreportData[key] !== '') {
              dataSendApi[key] = this.addreportData[key]
            }
          } else if (this.addreportData[key]) {
            dataSendApi[key] = this.addreportData[key]
          } else {
            dataSendApi[key] = null
          }
        }

        if (this.formMode === this.$_TTANHEnum.FORM_MODE.ADD) {
          const res = await reportService.post('Report', dataSendApi)

          if (res.success) {
            this.$store.commit('addToast', {
              type: 'success',
              text: 'Thêm báo cáo thành công'
            })
          } else {
            CommonErrorHandle()
            isSuccess = false
          }
        } else if (this.formMode === this.$_TTANHEnum.FORM_MODE.UPDATE) {
          const res = await reportService.put('Report', this.addreportData.reportId, dataSendApi)

          if (res.success) {
            this.$store.commit('addToast', {
              type: 'success',
              text: 'Cập nhật báo cáo thành công'
            })
          } else {
            CommonErrorHandle()
            isSuccess = false
          }
        }

        this.isLoading = false
        return isSuccess
      } else {
        this.isShowDialogError = true
      }
    },

    /**
     * kiểm tra chung
     * @param {string} feildCheck:
     *    trường muốn kiểm tra,
     *    để trống nếu muốn kiểm tra tất cả
     * @author: TTANH (29/07/2024)
     */
    commonValidate(feildCheck = '') {
      for (let property in this.validatereportData) {
        let nameField = this.$t(`reportSubsystem.addreportPopup.nameField.${property}`)

        let valuePropValidatereportData = this.validatereportData[property]

        if (valuePropValidatereportData === '') {
          continue
        } else if (feildCheck == '' || feildCheck == property) {
          // replace: xóa hết khoảng cách
          let validatesProp = valuePropValidatereportData.replace(/\s+/, '').trim().split(',')
          let isValidate = true

          validatesProp.forEach((validate) => {
            if (validate.includes('Empty')) {
              let errorText = emptyValidate(this.addreportData[property], nameField, this.$store.state.langCode)

              if (errorText !== '') {
                this.errorTextreportData[property] = errorText
                isValidate = false
              }
            } else if (validate.includes('MaxLength')) {
              let errorText = lengthValidate(
                this.addreportData[property],
                ValidateConfig[validate],
                0,
                nameField,
                this.$store.state.langCode
              )

              if (errorText !== '') {
                this.errorTextreportData[property] = errorText
                isValidate = false
              }
            } else if (validate.includes('NotGreaterThanToday')) {
              if (this.addreportData[property]) {
                const checkDate = new Date(this.addreportData[property])
                const today = new Date()

                checkDate.setHours(0, 0, 0, 0)
                today.setHours(0, 0, 0, 0)

                if (checkDate > today) {
                  this.errorTextreportData[property] = this.$t(
                    'errorHandle.reportSubsystem.addreportPopup.dateNotGreaterThanToday',
                    { name: nameField }
                  )
                  isValidate = false
                } else {
                  this.errorTextreportData[property] = ''
                }
              }
            } else if (validate.includes('Email')) {
              let errorText = regexValidate(
                this.addreportData[property],
                nameField,
                ValidateConfig.EmailRegex,
                this.$store.state.langCode
              )

              if (errorText !== '') {
                this.errorTextreportData[property] = errorText
                isValidate = false
              }
            } else if (validate.includes('PhoneNumber')) {
              let errorText = regexValidate(
                this.addreportData[property],
                nameField,
                ValidateConfig.PhoneNumberRegex,
                this.$store.state.langCode
              )

              if (errorText !== '') {
                this.errorTextreportData[property] = errorText
                isValidate = false
              }
            } else if (validate.includes('OnlyNumbers')) {
              let errorText = regexValidate(
                this.addreportData[property],
                nameField,
                ValidateConfig.OnlyNumbersRegex,
                this.$store.state.langCode
              )

              if (errorText !== '') {
                this.errorTextreportData[property] = errorText
                isValidate = false
              }
            }
          })

          if (isValidate) {
            this.errorTextreportData[property] = ''
          }
        }
      }
    },

    /**
     * thực hiện validate dữ liệu
     * @author: TTANH (01/07/2024)
     * @returns: true nếu form đã được validate
     */
    validateData() {
      try {
        this.resetErrorText()

        // kiểm tra những validate chung
        this.commonValidate()

        // kiểm tra tất cả các giá trị có lỗi không
        for (let property in this.errorTextreportData) {
          if (this.errorTextreportData[property] !== '') {
            return false
          }
        }

        return true
      } catch (error) {
        console.log('🚀 ~ file: AddreportPopup.vue:509 ~ validateData ~ error:', error)
      }
    },

    /**
     * làm mới lại thông báo lỗi
     * @author: TTANH (01/07/2024)
     */
    resetErrorText() {
      try {
        for (let attr in this.errorTextreportData) {
          this.errorTextreportData[attr] = ''
        }
      } catch (error) {
        console.log('🚀 ~ file: AddreportPopup.vue:594 ~ resetErrorText ~ error:', error)
      }
    },

    /**
     * làm mới lại thông tin thêm
     * @author: TTANH (01/07/2024)
     */
    resetAddreportData() {
      try {
        this.copyAddreportData = {}

        for (let attr in this.addreportData) {
          this.addreportData[attr] = ''
        }

        if (this.$refs.currentStartDate) {
          this.$refs.currentStartDate.resetDatePicked()
        }

        if (this.$refs.previousEndDate) {
          this.$refs.previousEndDate.resetDatePicked()
        }
      } catch (error) {
        console.log('🚀 ~ file: AddreportPopup.vue:594 ~ resetErrorText ~ error:', error)
      }
    },

    /**
     * xử lý khi ấn ẩn dialog thông báo lỗi
     * @author: TTANH (01/07/2024)
     */
    closeBtnDialogErrorClick() {
      try {
        this.isShowDialogError = false

        this.$refs[this.getFirstError.errorAttr].focus()
      } catch (error) {
        console.log('🚀 ~ file: AddreportPopup.vue:777 ~ closeBtnDialogErrorClick ~ error:', error)
      }
    },

    /**
     * xử lý sự kiện keydown của nút "cất và thêm"
     * @param {*} event
     */
    onStoreAndAddBtnKeyDown(event) {
      if (event.keyCode === this.$_TTANHEnum.KEY_CODE.TAB && !event.shiftKey) {
        event.preventDefault()
        this.$refs.provinceId.focus()
      }
    }
  },

  computed: {
    computedFormMode() {
      if (this.isViewOnly) {
        return this.$_TTANHEnum.FORM_MODE.VIEW
      }
      else if (!this.dataUpdate) {
        return this.$_TTANHEnum.FORM_MODE.ADD
      } else {
        return this.$_TTANHEnum.FORM_MODE.UPDATE
      }
    },

    getFirstError() {
      let errorAttr = ''
      let errorText = ''

      for (let attr in this.errorTextreportData) {
        if (this.errorTextreportData[attr] !== '') {
          errorText = this.errorTextreportData[attr]
          errorAttr = attr
          break
        }
      }

      return {
        errorAttr,
        errorText
      }
    }
  },
  watch: {
    addreportData: {
      handler: function (newValue) {
        if (!isObjectEmpty(this.copyAddreportData)) {
          for (let property in this.addreportData) {
            if (newValue[property] !== this.copyAddreportData[property]) {
              this.commonValidate(property)
            }
          }
        }

        if (this.copyAddreportData) this.copyAddreportData = JSON.parse(JSON.stringify(newValue))
      },

      deep: true
    }
  }
}
</script>

<style scoped>
@import url(./add-report-popup.css);
</style>
