<template>
  <div class="content flex-row h1">
    <div class="content__left flex-column w1/2 h1">
      <ttanh-file-input
        ref="excelFile"
        :errorText="errorText.excelFile"
        idInput="excel-file-import"
        width="460px"
        :file="excelFile"
        @insert-file="onInsertFile"
        tabindex="1"
      >
        <template #label>
          <div
            class="flex-row excel-file__input-label"
            style="margin-bottom: 4px"
          >
            <ttanh-icon icon="file-icon" width="19px" height="19px" />
            <div
              class="label-text flex-row"
              style="margin: 0px 4px 2px 6px; height: 16px; white-space: nowrap"
            >
              {{ $t("importExcel.selectSourceFile.selectExcelFile") }}
              <span
                class="regular-text"
                style="font-size: 12px; margin-left: 8px; color: #9a9a9a"
              >
                {{ $t("importExcel.selectSourceFile.maximumSize") }}
              </span>
            </div>
          </div>
        </template>
      </ttanh-file-input>

      <div class="content__left-download-template flex-row">
        <span class="download" @click="downloadBasicTemplate"
          ><span class="flex-row"
            >{{ $t("importExcel.selectSourceFile.downloadBasicTemplate") }}
            <ttanh-icon
              icon="download-icon"
              height="13px"
              width="16px"
              :tooltip="
                $t('importExcel.selectSourceFile.downloadBasicTemplate')
              " /></span
        ></span>
        <span class="download" @click="downloadFullTemplate"
          ><span class="flex-row"
            >{{ $t("importExcel.selectSourceFile.downloadFullTemplate") }}
            <ttanh-icon
              icon="download-icon"
              height="13px"
              width="16px"
              :tooltip="
                $t('importExcel.selectSourceFile.downloadFullTemplate')
              " /></span
        ></span>
      </div>

      <div class="file__add-setting flex-row" style="width: 460px">
        <ttanh-combobox
          :errorText="errorText.sheetContainsData"
          v-model="sheetContainsData"
          ref="sheetContainsData"
          type="single-row"
          :labelText="$t('importExcel.selectSourceFile.sheetContainsData')"
          :inputRequired="true"
          :rowsData="sheetsFormat"
          class="w1/2"
          tabindex="2"
        />
        <ttanh-textfield
          :errorText="errorText.headerRowIndex"
          v-model="headerRowIndex"
          ref="headerRowIndex"
          type="number_no_dot"
          idInput="header-row-index"
          :labelText="$t('importExcel.selectSourceFile.headerRowIndex')"
          :inputRequired="true"
          style="padding-left: 6px"
          class="w1/2"
          tabindex="3"
        />
      </div>

      <div class="file__import-mode">
        <ttanh-radio-input
          :errorText="errorText.importMode"
          v-model="importMode"
          :values="importModeOptions"
          nameRadioGroup="import-mode"
          :align="'column'"
          type="text"
          :labelText="$t('importExcel.selectSourceFile.importModeTitle')"
          class="w1"
          marginLabel="24px 0px 16px"
          tabindex="4"
        />
      </div>
    </div>
    <div class="content__right w1/2 h1">
      <ttanh-separation-line
        style="
          height: 100%;
          width: 1px;
          border-right: 1px solid #000;
          margin: 0px;
        "
      />

      <div class="suggestion">
        <div class="inner-suggestion">
          <div class="flex items-center">
            <ttanh-icon icon="suggestion-icon" width="46px" height="46px" />
            <div class="label-text suggestion-label">
              {{ $t("importExcel.selectSourceFile.suggestionLabel") }}
            </div>
          </div>
          <div class="suggestion-content download-template">
            <ul class="suggest-list">
              <li>
                <div>
                  <span @click="downloadBasicTemplate" class="download">{{
                    $t("importExcel.selectSourceFile.downloadBasicTemplate")
                  }}</span>
                  {{ $t("importExcel.selectSourceFile.moreInfoBasicTemplate") }}
                </div>
              </li>
              <li>
                <span @click="downloadFullTemplate" class="download">{{
                  $t("importExcel.selectSourceFile.downloadFullTemplate")
                }}</span>
                {{ $t("importExcel.selectSourceFile.moreInfoFullTemplate") }}
              </li>
              <li>
                {{ $t("importExcel.selectSourceFile.nextUseInfo") }}
              </li>
              <li>
                <b>{{ $t("importExcel.selectSourceFile.importMode.add") }}:</b>
                {{
                  $t("importExcel.selectSourceFile.importModeAddDescription")
                }}
              </li>
              <li>
                <b
                  >{{
                    $t("importExcel.selectSourceFile.importMode.update")
                  }}:</b
                >
                {{
                  $t("importExcel.selectSourceFile.importModeUpdateDescription")
                }}
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
    <ttanh-error-popup
      v-if="isShowDialogError"
      :errorText="getFirstError.errorText"
      @close-click="closeBtnDialogErrorClick"
    />

    <ttanh-loading-spinner size="large" v-if="isLoading" />
  </div>
</template>

<script>
import EmployeeService from "@/service/EmployeeService.js";
import { CommonErrorHandle } from "@/helper/error-handle";
import { ValidateConfig } from "@/config/config.js";

export default {
  name: "EmployeeImportSelectSourceFile",

  mounted() {
    document.querySelector('[tabindex="1"]').focus();
  },

  props: {
    excelFile: {
      required: true,
    },
  },

  data() {
    return {
      isLoading: false,

      isShowDialogError: false,

      sheetContainsData: "",
      headerRowIndex: 1,
      importMode: this.$_TTANHEnum.IMPORT_MODE.ADD,

      sheets: [],

      importModeOptions: [
        {
          id: this.$_TTANHEnum.IMPORT_MODE.ADD,
          name: this.$t("importExcel.selectSourceFile.importMode.add"),
        },
        {
          id: this.$_TTANHEnum.IMPORT_MODE.UPDATE,
          name: this.$t("importExcel.selectSourceFile.importMode.update"),
        },
      ],

      errorText: {
        excelFile: "",
        sheetContainsData: "",
        headerRowIndex: "",
        importMode: "",
      },
    };
  },

  methods: {
    /**
     * kiểm tra file là đuôi xlsx và size <= 2mb
     * @author: TTANH (23/07/2024)
     * @param {File} file file muốn kiểm tra
     */
    fileValidate(file) {
      try {
        if (!file) {
          this.errorText.excelFile = this.$t(
            "errorHandle.importExcel.selectSourceFile.fileNotFound"
          );

          this.isShowDialogError = true;
          this.$refs.excelFile.setInputToEmpty();
          return false;
        }

        if (file.size > ValidateConfig.MaxSizeFileUpload) {
          this.errorText.excelFile = this.errorText.excelFile = this.$t(
            "errorHandle.importExcel.selectSourceFile.maxSizeError"
          );

          this.isShowDialogError = true;
          this.$refs.excelFile.setInputToEmpty();
          return false;
        }

        let fileExtention = file.name.split(".").pop();

        if (fileExtention !== "xlsx") {
          this.errorText.excelFile = this.$t(
            "errorHandle.importExcel.selectSourceFile.formatError"
          );
          this.isShowDialogError = true;
          this.$refs.excelFile.setInputToEmpty();
          return false;
        }

        this.errorText.excelFile = "";

        return true;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeImportSelectSourceFile.vue:203 ~ fileValidate ~ error:",
          error
        );
      }
    },

    /**
     * kiểm tra sheet được chọn và số thứ tự dòng tiêu đề
     * @author: TTANH (23/07/2024)
     */
    settingValidate() {
      try {
        if (this.headerRowIndex <= 0) {
          this.errorText.headerRowIndex = this.$t(
            "errorHandle.importExcel.selectSourceFile.headerIndexGreaterThanZero"
          );

          this.isShowDialogError = true;
          return false;
        }

        if (!this.sheetContainsData) {
          this.errorText.sheetContainsData = this.errorText.headerRowIndex =
            this.$t("errorHandle.importExcel.selectSourceFile.sheetNotFound");

          this.isShowDialogError = true;
          return false;
        }

        return true;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeImportSelectSourceFile.vue:240 ~ settingValidate ~ error:",
          error
        );
      }
    },

    /**
     * hàm xử lý khi người dùng thay đổi file thêm
     * @author: TTANH (23/07/2024)
     */
    async onInsertFile() {
      try {
        let file = this.$refs.excelFile.getFile();

        this.$emit("update:excelFile", file);

        if (this.fileValidate(file)) {
          this.isLoading = true;

          const res = await EmployeeService.postExcel(file);

          this.isLoading = false;

          if (res.success) {
            this.sheets = res.data.Sheets;
            this.headerRowIndex = res.data.HeaderRowIndex;

            if (this.sheetsFormat.length !== 0) {
              this.$refs.sheetContainsData.selectValue(this.sheetsFormat[0].id);
            }
          } else {
            if (
              res.errorCode ===
                this.$_TTANHEnum.ERROR_CODE.MAX_SIZE_FILE_ERROR ||
              res.errorCode === this.$_TTANHEnum.ERROR_CODE.FORMAT_EXCEL_ERROR
            ) {
              this.$store.commit("addToast", {
                type: "error",
                text: res.userMsg,
              });
            } else {
              CommonErrorHandle();
            }

            this.$refs.excelFile.setInputToEmpty();
          }
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeImportSelectSourceFile.vue:203 ~ onInsertFile ~ error:",
          error
        );
      }
    },

    /**
     * hàm gọi api để thêm các cài đặt cho file excel
     * @author: TTANH (23/07/2024)
     */
    async addExcelSetting() {
      try {
        let file = this.$refs.excelFile.getFile();

        if (this.fileValidate(file) && this.settingValidate()) {
          const dataSendApi = {
            headerRowIndex: this.headerRowIndex,
            sheetContainsData: this.sheetContainsData,
            importMode: this.importMode,
          };

          this.isLoading = true;

          const res = await EmployeeService.addSettingExcel(dataSendApi);

          this.isLoading = false;

          if (res.success) {
          } else {
            if (
              res.errorCode ===
                this.$_TTANHEnum.ERROR_CODE.EXCEL_HEADER_DUPLICATE ||
              res.errorCode === this.$_TTANHEnum.ERROR_CODE.EXCEL_HEADER_EMPTY
            ) {
              this.$store.commit("addToast", {
                type: "error",
                text: res.userMsg,
              });

              this.$refs.headerRowIndex.focus();
            } else if (
              res.errorCode === this.$_TTANHEnum.ERROR_CODE.SHEET_IS_EMPTY
            ) {
              this.$store.commit("addToast", {
                type: "error",
                text: res.userMsg,
              });

              this.$refs.sheetContainsData.focus();
            }
          }

          return res;
        } else {
          return {
            success: false,
          };
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeImportSelectSourceFile.vue:263 ~ addExcelSetting ~ error:",
          error
        );
      }
    },

    /**
     * Tải mẫu cơ bản excel cơ bản của nhân viên
     * @author: TTANH (26/07/2024)
     */
    async downloadBasicTemplate() {
      this.isLoading = true;

      await EmployeeService.downloadBasicTemplate();

      this.isLoading = false;
    },

    /**
     * Tải mẫu excel đầy đủ của nhân viên
     * @author: TTANH (26/07/2024)
     */
    async downloadFullTemplate() {
      this.isLoading = true;

      await EmployeeService.downloadFullTemplate();

      this.isLoading = false;
    },

    /**
     * xử lý khi ấn ẩn dialog thông báo lỗi
     * @author: TTANH (23/07/2024)
     */
    closeBtnDialogErrorClick() {
      try {
        this.isShowDialogError = false;

        if (this.$refs[this.getFirstError.errorAttr].focus) {
          this.$refs[this.getFirstError.errorAttr].focus();
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeImportSelectSourceFile.vue:218 ~ closeBtnDialogErrorClick ~ error:",
          error
        );
      }
    },
  },

  computed: {
    getFirstError() {
      let errorAttr = "";
      let errorText = "";

      for (let attr in this.errorText) {
        if (this.errorText[attr] !== "") {
          errorText = this.errorText[attr];
          errorAttr = attr;
          break;
        }
      }

      return {
        errorAttr,
        errorText,
      };
    },

    /**
     * format lại sheet để truyền vào combobox
     * @author: TTANH (23/07/2024)
     */
    sheetsFormat() {
      let sheetsFormated = [];

      this.sheets.forEach((sheet) => {
        var sheetFormat = {};

        sheetFormat.id = sheet;
        sheetFormat.code = sheet;
        sheetFormat.name = sheet;

        sheetsFormated.push(sheetFormat);
      });

      return sheetsFormated;
    },
  },

  watch: {
    headerRowIndex() {
      this.errorText.headerRowIndex = "";
    },
    sheetContainsData() {
      this.errorText.sheetContainsData = "";
    },
  },
};
</script>

<style scoped>
@import url(./import-select-source-file.css);
</style>
