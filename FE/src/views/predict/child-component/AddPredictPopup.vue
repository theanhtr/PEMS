<template>
  <div class="m-overlay" id="add-predict-popup">
    <ttanh-popup
      style="overflow: visible"
      title="Tạo mới dự báo"
    >
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
            <div class="flex-row p-b-8 label-add-group">
              Thông tin khu vực
            </div>
            <div class="flex-row p-b-8">
              <ttanh-combobox
                :errorText="errorTextPredictData.provinceId"
                v-model="addPredictData.provinceId"
                ref="province"
                type="single-row"
                labelText="Tỉnh/Thành phố"
                :inputRequired="true"
                @show-combobox="getProvinces"
                :rowsData="computedProvinces"
                class="w1"
                tabindex="1"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-combobox
                :errorText="errorTextPredictData.districtId"
                v-model="addPredictData.districtId"
                ref="district"
                type="single-row"
                labelText="Quận/Huyện"
                :inputRequired="true"
                @show-combobox="getDistricts"
                :rowsData="computedDistricts"
                class="w1"
                tabindex="2"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-combobox
                :errorText="errorTextPredictData.wardId"
                v-model="addPredictData.wardId"
                ref="ward"
                type="single-row"
                labelText="Phường/Xã"
                :inputRequired="true"
                @show-combobox="getWards"
                :rowsData="computedWards"
                class="w1"
                tabindex="3"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-textfield
                :errorText="errorTextPredictData.address"
                v-model="addPredictData.address"
                type="text"
                idInput="add__address"
                labelText="
                  Địa chỉ
                "
                :inputRequired="false"
                class="w1"
                ref="fullName"
                tabindex="2"
              />
            </div>
          </div>
          <div class="w1/2">
            <div class="flex-row p-b-8 label-add-group">
              Thông tin vụ trước
            </div>
            <div class="flex-row p-b-8">
              <ttanh-date-picker
                :errorText="errorTextPredictData.previousEndDate"
                v-model="addPredictData.previousEndDate"
                class="w1"
                idInput="add__previousEndDate"
                labelText="Thời điểm kết thúc"
                tabindex="6"
                ref="previousEndDate"
              />
            </div>
            <div class="flex-row p-b-8">
              <ttanh-combobox
                :errorText="errorTextPredictData.previousLevelWarning"
                v-model="addPredictData.previousLevelWarning"
                ref="previousLevelWarning"
                type="single-row"
                labelText="Mức độ cảnh báo"
                :inputRequired="false"
                :rowsData="previousLevelWarning"
                class="w1"
                tabindex="3"
              />
            </div>
            <div class="flex-row p-b-8 label-add-group m-t-26">
              Thông tin vụ này
            </div>
            <div class="flex-row p-b-8">
              <ttanh-date-picker
                :errorText="errorTextPredictData.currentStartDate"
                v-model="addPredictData.currentStartDate"
                class="w1"
                idInput="add__currentStartDate"
                labelText="Thời điểm bắt đầu"
                tabindex="6"
                ref="currentStartDate"
                :inputRequired="true"
              />
            </div>
          </div>
        </div>
      </template>
      <template #footer>
        <ttanh-separation-line
          style="border-color: var(--border-color-default); margin: 16px 0px"
        />
        <div
          class="flex-row"
          style="justify-content: space-between; padding-bottom: 16px"
        >
          <div>
            <ttanh-button
              type="sub"
              width="58px"
              tabindex="20"
              borderRadius="var(--border-radius-default)"
              @clickBtnContainer="$emit('clickCancelBtn')"
              >{{ $t("common.button.cancel") }}</ttanh-button
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
          isShowOutConfirmPopup = false;
          $refs.province.focus();
        }
      "
      @no-click="$emit('clickCancelBtn')"
      @yes-click="
        () => {
          isShowOutConfirmPopup = false;
          storeBtnClick();
        }
      "
      v-if="isShowOutConfirmPopup"
    />

    <ttanh-loading-spinner v-if="isLoading" size="large" />
  </div>
</template>

<script>
import PredictService from "@/service/PredictService.js";
import { ValidateConfig } from "@/config/config.js";
import { findIndexByAttribute, isObjectEmpty } from "@/helper/common.js";
import {
  lengthValidate,
  emptyValidate,
  regexValidate,
} from "@/helper/validate.js";
import { CommonErrorHandle } from "@/helper/error-handle";
import { capitalizeFirstLetter } from "@/helper/format-helper";
import AddressService from "@/service/AddressService.js";

export default {
  name: "AddPredictPopup",
  props: {
    dataUpdate: {
      default: null,
    },
  },

  async created() {
    //cập nhật thông tin cho form: form_mode, data
    this.addInfoForm();

    this.copyAddPredictData = JSON.parse(JSON.stringify(this.addPredictData));
  },

  mounted() {
    //foucs vào Predict code lần đầu mở form
    this.$refs.province.focus();
  },

  unmounted() {
  },

  data() {
    return {
      dataAddress: {
        provinces: [],
        districts: [],
        wards: [],
      },

      isShowOutConfirmPopup: false,
      isShowDialogError: false,
      isLoading: false,

      formMode: this.$_TTANHEnum.FORM_MODE.ADD,
      
      previousLevelWarning: [
        {
          id: 1,
          name: "Mức độ 1"
        },
        {
          id: 2,
          name: "Mức độ 2"
        },
        {
          id: 3,
          name: "Mức độ 3"
        },
      ],

      addPredictData: {
        provinceId: -1,
        provinceName: "",
        districtId: -1,
        districtName: "",
        wardId: -1,
        wardName: "",
        address: "",
        previousEndDate: null,
        previousLevelWarning: -1,
        currentStartDate: null
      },

      /**
       * Dùng để kiểm tra thay đổi của addPredictData
       * do vue sẽ lưu tham chiếu nên không ktra trực tiếp
       * được ở watch
       */
      copyAddPredictData: {},

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
      validatePredictData: {
        provinceId: "Empty",
        districtId: "Empty",
        wardId: "Empty",
        address: "MaxLength255",
        previousEndDate: "",
        previousLevelWarning: "",
        currentStartDate: "Empty"
      },

      errorTextPredictData: {
        provinceId: "",
        districtId: "",
        wardId: "",
        address: "",
        previousEndDate: "",
        previousLevelWarning: "",
        currentStartDate: ""
      },
    };
  },

  methods: {
    async getProvinces() {
      let provinces = await AddressService.province();

      if (provinces.status === 200) {
        this.dataAddress.provinces = provinces.data.results;
      } else {
        this.dataAddress.provinces = [];
      }
    },

    async getDistricts() {
      let districts = await AddressService.district(this.addPredictData.provinceId);

      if (districts.status === 200) {
        this.dataAddress.districts = districts.data.results;
      } else {
        this.dataAddress.districts = [];
      }
    },

    async getWards() {
      let wards = await AddressService.ward(this.addPredictData.districtId);

      if (wards.status === 200) {
        this.dataAddress.wards = wards.data.results;
      } else {
        this.dataAddress.wards = [];
      }
    },

    /**
     * thực hiện kiểm tra trước khi đóng form
     * @author: TTANH (07/08/2024)
     */
    closeAddForm() {
      if (this.formMode == this.$_TTANHEnum.FORM_MODE.ADD) {
        this.isShowOutConfirmPopup = true;
      } else {
        let difference = false;

        for (let attr in this.addPredictData) {
          let Attr = capitalizeFirstLetter(attr);

          let newData = this.addPredictData[attr];
          let oldData = this.dataUpdate[Attr];

          if (
            ((newData == "" || newData == null) &&
              (oldData == "" || oldData == null))
          ) {
          } else {
            if (oldData !== newData) {
              difference = true;
            }
          }
        }

        if (difference) {
          this.isShowOutConfirmPopup = true;
        } else {
          this.$emit("clickCancelBtn");
        }
      }
    },

    /**
     * cập nhật thông tin cho form: form_mode, data
     * @author: TTANH (01/07/2024)
     */
    addInfoForm() {
      this.formMode = this.computedFormMode;

      if (this.formMode === this.$_TTANHEnum.FORM_MODE.ADD) {
        this.resetAddPredictData();
      } else if (this.formMode === this.$_TTANHEnum.FORM_MODE.UPDATE) {
        for (let attr in this.dataUpdate) {
          let formatAttr = attr[0].toLowerCase() + attr.slice(1, attr.length);

          this.addPredictData[formatAttr] =
            this.dataUpdate[attr] !== null ? this.dataUpdate[attr] : "";
        }
      }
    },

    /**
     * thay đổi form cập nhật thành form thêm mới cho chức năng nhân bản
     * @author: TTANH (01/07/2024)
     */
    changeFormModeToAdd() {
      try {
        this.formMode = this.$_TTANHEnum.FORM_MODE.ADD;
      } catch (error) {
        console.log(
          "🚀 ~ file: AddPredictPopup.vue:675 ~ changeFormModeToAdd ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi ấn vào nút "Cất"
     * @author: TTANH (01/07/2024)
     */
    async storeBtnClick() {
      try {
        let isSuccess = await this.createNewPredict();

        if (isSuccess) {
          this.$emit("clickCancelBtn");
          this.$emit("reloadData");
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: AddPredictPopup.vue:688 ~ storeBtnClick ~ error:",
          error
        );
      }
    },

    /**
     * validate và tạo 1 nhân viên mới hoặc update thông tin nhân viên
     * @author: TTANH (01/07/2024)
     */
    async createNewPredict() {
      if (this.validateData()) {
        let isSuccess = true;
        this.isLoading = true;
        
        this.addPredictData.provinceName = this.$refs.province.getCurrentInputValue();
        this.addPredictData.districtName = this.$refs.district.getCurrentInputValue();
        this.addPredictData.wardName = this.$refs.ward.getCurrentInputValue();
        
        //lọc loại những trường rỗng
        var dataSendApi = {};

        for (const key in this.addPredictData) {
          if (key === "gender") {
            if (this.addPredictData[key] !== "") {
              dataSendApi[key] = this.addPredictData[key];
            }
          } else if (this.addPredictData[key]) {
            dataSendApi[key] = this.addPredictData[key];
          } else {
            dataSendApi[key] = null;
          }
        }

        if (this.formMode === this.$_TTANHEnum.FORM_MODE.ADD) {
          const res = await PredictService.post(dataSendApi);

          if (res.success) {
            this.$store.commit("addToast", {
              type: "success",
              text: "Thêm dự báo thành công",
            });
          } else {
            CommonErrorHandle();
            isSuccess = false;
          }
        } else if (this.formMode === this.$_TTANHEnum.FORM_MODE.UPDATE) {
          const res = await PredictService.put(
            this.addPredictData.PredictId,
            dataSendApi
          );

          if (res.success) {
            this.$store.commit("addToast", {
              type: "success",
              text: "Cập nhật dự báo thành công",
            });
          } else {
            CommonErrorHandle();
            isSuccess = false;
          }
        }

        this.isLoading = false;
        return isSuccess;
      } else {
        this.isShowDialogError = true;
      }
    },

    /**
     * kiểm tra chung
     * @param {string} feildCheck:
     *    trường muốn kiểm tra,
     *    để trống nếu muốn kiểm tra tất cả
     * @author: TTANH (29/07/2024)
     */
    commonValidate(feildCheck = "") {
      for (let property in this.validatePredictData) {
        let nameField = this.$t(
          `predictSubsystem.addPredictPopup.nameField.${property}`
        );

        let valuePropValidatePredictData = this.validatePredictData[property];

        if (valuePropValidatePredictData === "") {
          continue;
        } else if (feildCheck == "" || feildCheck == property) {
          // replace: xóa hết khoảng cách
          let validatesProp = valuePropValidatePredictData
            .replace(/\s+/, "")
            .trim()
            .split(",");
          let isValidate = true;

          validatesProp.forEach((validate) => {
            if (validate.includes("Empty")) {
              let errorText = emptyValidate(
                this.addPredictData[property],
                nameField,
                this.$store.state.langCode
              );

              if (errorText !== "") {
                this.errorTextPredictData[property] = errorText;
                isValidate = false;
              }
            } else if (validate.includes("MaxLength")) {
              let errorText = lengthValidate(
                this.addPredictData[property],
                ValidateConfig[validate],
                0,
                nameField,
                this.$store.state.langCode
              );

              if (errorText !== "") {
                this.errorTextPredictData[property] = errorText;
                isValidate = false;
              }
            } else if (validate.includes("NotGreaterThanToday")) {
              if (this.addPredictData[property]) {
                const checkDate = new Date(this.addPredictData[property]);
                const today = new Date();

                checkDate.setHours(0, 0, 0, 0);
                today.setHours(0, 0, 0, 0);

                if (checkDate > today) {
                  this.errorTextPredictData[property] = this.$t(
                    "errorHandle.PredictSubsystem.addPredictPopup.dateNotGreaterThanToday",
                    { name: nameField }
                  );
                  isValidate = false;
                } else {
                  this.errorTextPredictData[property] = "";
                }
              }
            } else if (validate.includes("Email")) {
              let errorText = regexValidate(
                this.addPredictData[property],
                nameField,
                ValidateConfig.EmailRegex,
                this.$store.state.langCode
              );

              if (errorText !== "") {
                this.errorTextPredictData[property] = errorText;
                isValidate = false;
              }
            } else if (validate.includes("PhoneNumber")) {
              let errorText = regexValidate(
                this.addPredictData[property],
                nameField,
                ValidateConfig.PhoneNumberRegex,
                this.$store.state.langCode
              );

              if (errorText !== "") {
                this.errorTextPredictData[property] = errorText;
                isValidate = false;
              }
            } else if (validate.includes("OnlyNumbers")) {
              let errorText = regexValidate(
                this.addPredictData[property],
                nameField,
                ValidateConfig.OnlyNumbersRegex,
                this.$store.state.langCode
              );

              if (errorText !== "") {
                this.errorTextPredictData[property] = errorText;
                isValidate = false;
              }
            }
          });

          if (isValidate) {
            this.errorTextPredictData[property] = "";
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
        this.resetErrorText();

        // kiểm tra những validate chung
        this.commonValidate();

        // kiểm tra tất cả các giá trị có lỗi không
        for (let property in this.errorTextPredictData) {
          if (this.errorTextPredictData[property] !== "") {
            return false;
          }
        }

        return true;
      } catch (error) {
        console.log(
          "🚀 ~ file: AddPredictPopup.vue:509 ~ validateData ~ error:",
          error
        );
      }
    },

    /**
     * làm mới lại thông báo lỗi
     * @author: TTANH (01/07/2024)
     */
    resetErrorText() {
      try {
        for (let attr in this.errorTextPredictData) {
          this.errorTextPredictData[attr] = "";
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: AddPredictPopup.vue:594 ~ resetErrorText ~ error:",
          error
        );
      }
    },

    /**
     * làm mới lại thông tin thêm
     * @author: TTANH (01/07/2024)
     */
    resetAddPredictData() {
      try {
        this.copyAddPredictData = {};

        for (let attr in this.addPredictData) {
          this.addPredictData[attr] = "";
        }

        if (this.$refs.currentStartDate) {
          this.$refs.currentStartDate.resetDatePicked();
        }

        if (this.$refs.previousEndDate) {
          this.$refs.previousEndDate.resetDatePicked();
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: AddPredictPopup.vue:594 ~ resetErrorText ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi ấn ẩn dialog thông báo lỗi
     * @author: TTANH (01/07/2024)
     */
    closeBtnDialogErrorClick() {
      try {
        this.isShowDialogError = false;

        this.$refs[this.getFirstError.errorAttr].focus();
      } catch (error) {
        console.log(
          "🚀 ~ file: AddPredictPopup.vue:777 ~ closeBtnDialogErrorClick ~ error:",
          error
        );
      }
    },

    /**
     * xử lý sự kiện keydown của nút "cất và thêm"
     * @param {*} event
     */
    onStoreAndAddBtnKeyDown(event) {
      if (event.keyCode === this.$_TTANHEnum.KEY_CODE.TAB && !event.shiftKey) {
        event.preventDefault();
        this.$refs.province.focus();
      }
    },
  },

  computed: {
    computedProvinces() {
      try {
        let provincesFormat = [];

        this.dataAddress.provinces.forEach((province) => {
          let id = province.province_id;
          let name = province.province_name;
          let code = province.province_name;

          provincesFormat.push({
            id,
            name,
            code
          });
        });

        return provincesFormat;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:457 ~ computedEmployees ~ error:",
          error
        );
      }
    },

    computedDistricts() {
      try {
        let districtsFormat = [];

        this.dataAddress.districts.forEach((district) => {
          let id = district.district_id;
          let name = district.district_name;
          let code = district.district_name;

          districtsFormat.push({
            id,
            name,
            code
          });
        });

        return districtsFormat;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:457 ~ computedEmployees ~ error:",
          error
        );
      }
    },

    computedWards() {
      try {
        let wardsFormat = [];

        this.dataAddress.wards.forEach((ward) => {
          let id = ward.ward_id;
          let name = ward.ward_name;
          let code = ward.ward_name;

          wardsFormat.push({
            id,
            name,
            code
          });
        });

        return wardsFormat;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:457 ~ computedEmployees ~ error:",
          error
        );
      }
    },

    computedFormMode() {
      if (!this.dataUpdate) {
        return this.$_TTANHEnum.FORM_MODE.ADD;
      } else {
        return this.$_TTANHEnum.FORM_MODE.UPDATE;
      }
    },

    getFirstError() {
      let errorAttr = "";
      let errorText = "";

      for (let attr in this.errorTextPredictData) {
        if (this.errorTextPredictData[attr] !== "") {
          errorText = this.errorTextPredictData[attr];
          errorAttr = attr;
          break;
        }
      }

      return {
        errorAttr,
        errorText,
      };
    },
  },
  watch: {
    addPredictData: {
      handler: function (newValue) {
        if (!isObjectEmpty(this.copyAddPredictData)) {
          for (let property in this.addPredictData) {
            if (newValue[property] !== this.copyAddPredictData[property]) {
              this.commonValidate(property);
            }
          }
        }

        if (this.copyAddPredictData)
          this.copyAddPredictData = JSON.parse(JSON.stringify(newValue));
      },

      deep: true,
    },
  },
};
</script>

<style scoped>
@import url(./add-predict-popup.css);
</style>
