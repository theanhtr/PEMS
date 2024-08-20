<template>
  <div class="m-overlay" id="add-user-popup">
    <ttanh-popup style="overflow: visible" title="Tạo mới người dùng" width="600px">
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
          <div class="w1" style="padding-right: 26px">
            <div class="flex-row p-b-8">
              <ttanh-textfield
                :errorText="errorTextUserData.username"
                v-model="addUserData.username"
                type="text"
                idInput="add__username"
                labelText="Tên tài khoản"
                :inputRequired="true"
                class="w1"
                ref="username"
                tabindex="2"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-textfield
                :errorText="errorTextUserData.fullname"
                v-model="addUserData.fullname"
                type="text"
                idInput="add__fullname"
                labelText="Họ và tên"
                :inputRequired="true"
                class="w1"
                ref="fullName"
                tabindex="2"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-combobox
                :errorText="errorTextUserData.roleID"
                v-model="addUserData.roleID"
                ref="roleID"
                type="single-row"
                labelText="Chức vụ"
                :inputRequired="true"
                :rowsData="rolesComboboxData"
                class="w1"
                tabindex="1"
              />
            </div>
          </div>
        </div>
      </template>
      <template #footer>
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
          $refs.province.focus()
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
import UserService from '@/service/UserService.js'
import { ValidateConfig } from '@/config/config.js'
import { findIndexByAttribute, isObjectEmpty } from '@/helper/common.js'
import { lengthValidate, emptyValidate, regexValidate } from '@/helper/validate.js'
import { CommonErrorHandle } from '@/helper/error-handle'
import { capitalizeFirstLetter } from '@/helper/format-helper'
import { roles } from '../../../data_combobox/role'

export default {
  name: 'AddUserPopup',
  props: {
    dataUpdate: {
      default: null
    }
  },

  async created() {
    //cập nhật thông tin cho form: form_mode, data
    await this.addInfoForm()

    this.copyAddUserData = JSON.parse(JSON.stringify(this.addUserData))
  },

  mounted() {
    //foucs vào User code lần đầu mở form
    this.$refs.username.focus()
  },

  unmounted() {},

  data() {
    return {
      isShowOutConfirmPopup: false,
      isShowDialogError: false,
      isLoading: false,

      formMode: this.$_TTANHEnum.FORM_MODE.ADD,

      rolesComboboxData: roles,

      addUserData: {
        userId: '',
        username: '',
        fullname: '',
        roleID: ''
      },

      /**
       * Dùng để kiểm tra thay đổi của addUserData
       * do vue sẽ lưu tham chiếu nên không ktra trực tiếp
       * được ở watch
       */
      copyAddUserData: {},

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
      validateUserData: {
        roleID: 'Empty',
        fullname: 'Empty, MaxLength255',
        username: 'Empty, MaxLength255'
      },

      errorTextUserData: {
        roleID: '',
        fullname: '',
        username: ''
      }
    }
  },

  methods: {
    /**
     * thực hiện kiểm tra trước khi đóng form
     * @author: TTANH (07/08/2024)
     */
    closeAddForm() {
      if (this.formMode == this.$_TTANHEnum.FORM_MODE.ADD) {
        this.isShowOutConfirmPopup = true
      } else {
        let difference = false

        for (let attr in this.addUserData) {
          let Attr = capitalizeFirstLetter(attr)

          let newData = this.addUserData[attr]
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
      }
    },

    /**
     * cập nhật thông tin cho form: form_mode, data
     * @author: TTANH (01/07/2024)
     */
    async addInfoForm() {
      this.formMode = this.computedFormMode

      if (this.formMode === this.$_TTANHEnum.FORM_MODE.ADD) {
        this.resetAddUserData()
      } else if (this.formMode === this.$_TTANHEnum.FORM_MODE.UPDATE) {
        for (let attr in this.dataUpdate) {
          let formatAttr = attr[0].toLowerCase() + attr.slice(1, attr.length)

          this.addUserData[formatAttr] = this.dataUpdate[attr] !== null ? this.dataUpdate[attr] : ''
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
        console.log('🚀 ~ file: AddUserPopup.vue:675 ~ changeFormModeToAdd ~ error:', error)
      }
    },

    /**
     * xử lý khi ấn vào nút "Cất"
     * @author: TTANH (01/07/2024)
     */
    async storeBtnClick() {
      try {
        let isSuccess = await this.createNewUser()

        if (isSuccess) {
          this.$emit('clickCancelBtn')
          this.$emit('reloadData')
        }
      } catch (error) {
        console.log('🚀 ~ file: AddUserPopup.vue:688 ~ storeBtnClick ~ error:', error)
      }
    },

    /**
     * validate và tạo 1 nhân viên mới hoặc update thông tin nhân viên
     * @author: TTANH (01/07/2024)
     */
    async createNewUser() {
      if (this.validateData()) {
        let isSuccess = true
        this.isLoading = true

        //lọc loại những trường rỗng
        var dataSendApi = {}

        for (const key in this.addUserData) {
          if (key === 'gender') {
            if (this.addUserData[key] !== '') {
              dataSendApi[key] = this.addUserData[key]
            }
          } else if (this.addUserData[key]) {
            dataSendApi[key] = this.addUserData[key]
          } else {
            dataSendApi[key] = null
          }
        }

        if (this.formMode === this.$_TTANHEnum.FORM_MODE.ADD) {
          dataSendApi['password'] = ''
          const res = await UserService.post(dataSendApi)

          if (res.success) {
            this.$store.commit('addToast', {
              type: 'success',
              text: 'Thêm người dùng thành công'
            })
          } else {
            CommonErrorHandle()
            isSuccess = false
          }
        } else if (this.formMode === this.$_TTANHEnum.FORM_MODE.UPDATE) {
          const res = await UserService.put(this.addUserData.userId, dataSendApi)

          if (res.success) {
            this.$store.commit('addToast', {
              type: 'success',
              text: 'Cập nhật người dùng thành công'
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
      for (let property in this.validateUserData) {
        let nameField = this.$t(`userSubsystem.addUserPopup.nameField.${property}`)

        let valuePropValidateUserData = this.validateUserData[property]

        if (valuePropValidateUserData === '') {
          continue
        } else if (feildCheck == '' || feildCheck == property) {
          // replace: xóa hết khoảng cách
          let validatesProp = valuePropValidateUserData.replace(/\s+/, '').trim().split(',')
          let isValidate = true

          validatesProp.forEach((validate) => {
            if (validate.includes('Empty')) {
              let errorText = emptyValidate(this.addUserData[property], nameField, this.$store.state.langCode)

              if (errorText !== '') {
                this.errorTextUserData[property] = errorText
                isValidate = false
              }
            } else if (validate.includes('MaxLength')) {
              let errorText = lengthValidate(
                this.addUserData[property],
                ValidateConfig[validate],
                0,
                nameField,
                this.$store.state.langCode
              )

              if (errorText !== '') {
                this.errorTextUserData[property] = errorText
                isValidate = false
              }
            } else if (validate.includes('NotGreaterThanToday')) {
              if (this.addUserData[property]) {
                const checkDate = new Date(this.addUserData[property])
                const today = new Date()

                checkDate.setHours(0, 0, 0, 0)
                today.setHours(0, 0, 0, 0)

                if (checkDate > today) {
                  this.errorTextUserData[property] = this.$t(
                    'errorHandle.UserSubsystem.addUserPopup.dateNotGreaterThanToday',
                    { name: nameField }
                  )
                  isValidate = false
                } else {
                  this.errorTextUserData[property] = ''
                }
              }
            } else if (validate.includes('Email')) {
              let errorText = regexValidate(
                this.addUserData[property],
                nameField,
                ValidateConfig.EmailRegex,
                this.$store.state.langCode
              )

              if (errorText !== '') {
                this.errorTextUserData[property] = errorText
                isValidate = false
              }
            } else if (validate.includes('PhoneNumber')) {
              let errorText = regexValidate(
                this.addUserData[property],
                nameField,
                ValidateConfig.PhoneNumberRegex,
                this.$store.state.langCode
              )

              if (errorText !== '') {
                this.errorTextUserData[property] = errorText
                isValidate = false
              }
            } else if (validate.includes('OnlyNumbers')) {
              let errorText = regexValidate(
                this.addUserData[property],
                nameField,
                ValidateConfig.OnlyNumbersRegex,
                this.$store.state.langCode
              )

              if (errorText !== '') {
                this.errorTextUserData[property] = errorText
                isValidate = false
              }
            }
          })

          if (isValidate) {
            this.errorTextUserData[property] = ''
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
        for (let property in this.errorTextUserData) {
          if (this.errorTextUserData[property] !== '') {
            return false
          }
        }

        return true
      } catch (error) {
        console.log('🚀 ~ file: AddUserPopup.vue:509 ~ validateData ~ error:', error)
      }
    },

    /**
     * làm mới lại thông báo lỗi
     * @author: TTANH (01/07/2024)
     */
    resetErrorText() {
      try {
        for (let attr in this.errorTextUserData) {
          this.errorTextUserData[attr] = ''
        }
      } catch (error) {
        console.log('🚀 ~ file: AddUserPopup.vue:594 ~ resetErrorText ~ error:', error)
      }
    },

    /**
     * làm mới lại thông tin thêm
     * @author: TTANH (01/07/2024)
     */
    resetAddUserData() {
      try {
        this.copyAddUserData = {}

        for (let attr in this.addUserData) {
          this.addUserData[attr] = ''
        }

        if (this.$refs.currentStartDate) {
          this.$refs.currentStartDate.resetDatePicked()
        }

        if (this.$refs.previousEndDate) {
          this.$refs.previousEndDate.resetDatePicked()
        }
      } catch (error) {
        console.log('🚀 ~ file: AddUserPopup.vue:594 ~ resetErrorText ~ error:', error)
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
        console.log('🚀 ~ file: AddUserPopup.vue:777 ~ closeBtnDialogErrorClick ~ error:', error)
      }
    },

    /**
     * xử lý sự kiện keydown của nút "cất và thêm"
     * @param {*} event
     */
    onStoreAndAddBtnKeyDown(event) {
      if (event.keyCode === this.$_TTANHEnum.KEY_CODE.TAB && !event.shiftKey) {
        event.preventDefault()
        this.$refs.province.focus()
      }
    }
  },

  computed: {
    computedFormMode() {
      if (!this.dataUpdate) {
        return this.$_TTANHEnum.FORM_MODE.ADD
      } else {
        return this.$_TTANHEnum.FORM_MODE.UPDATE
      }
    },

    getFirstError() {
      let errorAttr = ''
      let errorText = ''

      for (let attr in this.errorTextUserData) {
        if (this.errorTextUserData[attr] !== '') {
          errorText = this.errorTextUserData[attr]
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
    addUserData: {
      handler: function (newValue) {
        if (!isObjectEmpty(this.copyAddUserData)) {
          for (let property in this.addUserData) {
            if (newValue[property] !== this.copyAddUserData[property]) {
              this.commonValidate(property)
            }
          }
        }

        if (this.copyAddUserData) this.copyAddUserData = JSON.parse(JSON.stringify(newValue))
      },

      deep: true
    }
  }
}
</script>

<style scoped>
@import url(./add-user-popup.css);
</style>
