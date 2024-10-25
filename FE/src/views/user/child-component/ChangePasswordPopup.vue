<template>
  <div class="m-overlay" id="change-password-popup">
    <ttanh-popup style="overflow: visible" title="Đổi mật khẩu" width="800px">
      <template #header__close>
        <ttanh-icon
          @click="$emit('clickCancelBtn')"
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
                v-model="usernameData"
                type="text"
                labelText="Tên người dùng"
                class="w1"
                tabindex="2"
                :disable="true"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-textfield
                :errorText="errorTextChangePasswordData.oldpassword"
                v-model="changePasswordData.oldpassword"
                type="text"
                idInput="add__oldpassword"
                labelText="Mật khẩu cũ"
                :inputRequired="true"
                class="w1"
                ref="oldpassword"
                tabindex="2"
                :isPassword="true"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-textfield
                :errorText="errorTextChangePasswordData.password"
                v-model="changePasswordData.password"
                type="text"
                idInput="add__password"
                labelText="Mật khẩu mới"
                :inputRequired="true"
                class="w1"
                ref="password"
                tabindex="2"
                :isPassword="true"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-textfield
                :errorText="errorTextChangePasswordData.repassword"
                v-model="changePasswordData.repassword"
                type="text"
                idInput="add__repassword"
                labelText="Nhập lại mật khẩu mới"
                :inputRequired="true"
                class="w1"
                ref="repassword"
                tabindex="2"
                :isPassword="true"
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
          $refs.oldpassword.focus()
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
import { isObjectEmpty } from '@/helper/common.js'
import { lengthValidate, emptyValidate, regexValidate } from '@/helper/validate.js'
import { CommonErrorHandle } from '@/helper/error-handle'

export default {
  name: 'ChangePasswordPopup',
  props: {
    userId: {
      type: String,
      default: ''
    },
    username: {
      type: String,
      default: ''
    }
  },

  mounted() {
    //foucs vào User code lần đầu mở form
    this.$refs.oldpassword.focus()
    this.usernameData = this.username
  },

  data() {
    return {
      copychangePasswordData: {
        password: '',
        oldpassword: '',
        repassword: ''
      },
      isShowOutConfirmPopup: false,
      isShowDialogError: false,
      isLoading: false,

      changePasswordData: {
        password: '',
        oldpassword: '',
        repassword: ''
      },

      usernameData: '',

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
        oldpassword: 'Empty',
        password: 'Empty',
        repassword: 'Empty'
      },

      errorTextChangePasswordData: {
        oldpassword: '',
        password: '',
        repassword: ''
      }
    }
  },

  methods: {
    /**
     * xử lý khi ấn vào nút "Cất"
     * @author: TTANH (01/07/2024)
     */
    async storeBtnClick() {
      try {
        let isSuccess = await this.changePassword()

        if (isSuccess) {
          this.$emit('clickCancelBtn')
          this.$emit('reloadData')
        }
      } catch (error) {
        console.log('🚀 ~ file: ChangePasswordPopup.vue:688 ~ storeBtnClick ~ error:', error)
      }
    },

    /**
     * validate và tạo 1 nhân viên mới hoặc update thông tin nhân viên
     * @author: TTANH (01/07/2024)
     */
    async changePassword() {
      if (this.validateData()) {
        let isSuccess = true
        this.isLoading = true

        //lọc loại những trường rỗng
        var dataSendApi = {}

        for (const key in this.changePasswordData) {
          if (key === 'gender') {
            if (this.changePasswordData[key] !== '') {
              dataSendApi[key] = this.changePasswordData[key]
            }
          } else if (this.changePasswordData[key]) {
            dataSendApi[key] = this.changePasswordData[key]
          } else {
            dataSendApi[key] = null
          }
        }

        dataSendApi.UserId = this.userId

        const res = await UserService.post('User/password', dataSendApi)

        if (res.success) {
          this.$store.commit('addToast', {
            type: 'success',
            text: 'Đổi mật khẩu thành công'
          })
        } else {
          if (res.errorCode === 1431) {
            this.$store.commit('addToast', {
              type: 'error',
              text: res.userMsg
            })
          } else {
            CommonErrorHandle()
          }

          isSuccess = false
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
        let nameField = this.$t(`passwordSubsystem.ChangePasswordPopup.nameField.${property}`)

        let valuePropValidateUserData = this.validateUserData[property]

        if (valuePropValidateUserData === '') {
          continue
        } else if (feildCheck == '' || feildCheck == property) {
          // replace: xóa hết khoảng cách
          let validatesProp = valuePropValidateUserData.replace(/\s+/, '').trim().split(',')
          let isValidate = true

          if (this.changePasswordData.password != this.changePasswordData.repassword && property == 'repassword') {
            this.errorTextChangePasswordData.repassword = this.$t('errorHandle.ChangePasswordSubsystem.ChangePasswordPopup.RepasswordNotMatch')
            isValidate = false
          }

          validatesProp.forEach((validate) => {
            if (validate.includes('Empty')) {
              let errorText = emptyValidate(this.changePasswordData[property], nameField, this.$store.state.langCode)

              if (errorText !== '') {
                this.errorTextChangePasswordData[property] = errorText
                isValidate = false
              }
            } else if (validate.includes('MaxLength')) {
              let errorText = lengthValidate(
                this.changePasswordData[property],
                ValidateConfig[validate],
                0,
                nameField,
                this.$store.state.langCode
              )

              if (errorText !== '') {
                this.errorTextChangePasswordData[property] = errorText
                isValidate = false
              }
            } else if (validate.includes('NotGreaterThanToday')) {
              if (this.changePasswordData[property]) {
                const checkDate = new Date(this.changePasswordData[property])
                const today = new Date()

                checkDate.setHours(0, 0, 0, 0)
                today.setHours(0, 0, 0, 0)

                if (checkDate > today) {
                  this.errorTextChangePasswordData[property] = this.$t(
                    'errorHandle.UserSubsystem.ChangePasswordPopup.dateNotGreaterThanToday',
                    { name: nameField }
                  )
                  isValidate = false
                } else {
                  this.errorTextChangePasswordData[property] = ''
                }
              }
            } else if (validate.includes('Email')) {
              let errorText = regexValidate(
                this.changePasswordData[property],
                nameField,
                ValidateConfig.EmailRegex,
                this.$store.state.langCode
              )

              if (errorText !== '') {
                this.errorTextChangePasswordData[property] = errorText
                isValidate = false
              }
            } else if (validate.includes('PhoneNumber')) {
              let errorText = regexValidate(
                this.changePasswordData[property],
                nameField,
                ValidateConfig.PhoneNumberRegex,
                this.$store.state.langCode
              )

              if (errorText !== '') {
                this.errorTextChangePasswordData[property] = errorText
                isValidate = false
              }
            } else if (validate.includes('OnlyNumbers')) {
              let errorText = regexValidate(
                this.changePasswordData[property],
                nameField,
                ValidateConfig.OnlyNumbersRegex,
                this.$store.state.langCode
              )

              if (errorText !== '') {
                this.errorTextChangePasswordData[property] = errorText
                isValidate = false
              }
            }
          })

          if (isValidate) {
            this.errorTextChangePasswordData[property] = ''
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
        for (let property in this.errorTextChangePasswordData) {
          if (this.errorTextChangePasswordData[property] !== '') {
            return false
          }
        }

        return true
      } catch (error) {
        console.log('🚀 ~ file: ChangePasswordPopup.vue:509 ~ validateData ~ error:', error)
      }
    },

    /**
     * làm mới lại thông báo lỗi
     * @author: TTANH (01/07/2024)
     */
    resetErrorText() {
      try {
        for (let attr in this.errorTextChangePasswordData) {
          this.errorTextChangePasswordData[attr] = ''
        }
      } catch (error) {
        console.log('🚀 ~ file: ChangePasswordPopup.vue:594 ~ resetErrorText ~ error:', error)
      }
    },

    /**
     * làm mới lại thông tin thêm
     * @author: TTANH (01/07/2024)
     */
    resetchangePasswordData() {
      try {
        this.copychangePasswordData = {}

        for (let attr in this.changePasswordData) {
          this.changePasswordData[attr] = ''
        }

        if (this.$refs.currentStartDate) {
          this.$refs.currentStartDate.resetDatePicked()
        }

        if (this.$refs.previousEndDate) {
          this.$refs.previousEndDate.resetDatePicked()
        }
      } catch (error) {
        console.log('🚀 ~ file: ChangePasswordPopup.vue:594 ~ resetErrorText ~ error:', error)
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
        console.log('🚀 ~ file: ChangePasswordPopup.vue:777 ~ closeBtnDialogErrorClick ~ error:', error)
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
    getFirstError() {
      let errorAttr = ''
      let errorText = ''

      for (let attr in this.errorTextChangePasswordData) {
        if (this.errorTextChangePasswordData[attr] !== '') {
          errorText = this.errorTextChangePasswordData[attr]
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
    changePasswordData: {
      handler: function (newValue) {
        if (!isObjectEmpty(this.copychangePasswordData)) {
          for (let property in this.changePasswordData) {
            if (newValue[property] !== this.copychangePasswordData[property]) {
              this.commonValidate(property)
            }
          }
        }

        if (this.copychangePasswordData) this.copychangePasswordData = JSON.parse(JSON.stringify(newValue))
      },

      deep: true
    }
  }
}
</script>

<style scoped>
@import url(./change-password-popup.css);
</style>
