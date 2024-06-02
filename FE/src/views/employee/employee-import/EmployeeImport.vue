<template>
  <div class="employee-import flex-column">
    <div class="employee-import__header flex-row">
      <div
        class="header__step"
        :class="
          currentStep >= $_TTANHEnum.IMPORT_STEP.SELECT_SOURCE_FILE
            ? 'header__step--active'
            : 'header__step--default'
        "
      >
        <div class="step__index">1</div>
        <div class="step__name">{{ $t("importLayout.step1Name") }}</div>
      </div>
      <div
        class="header__line"
        :class="
          currentStep >= $_TTANHEnum.IMPORT_STEP.HEADER_MERGE
            ? 'header__step--active'
            : 'header__step--default'
        "
      ></div>
      <div
        class="header__step"
        :class="
          currentStep >= $_TTANHEnum.IMPORT_STEP.HEADER_MERGE
            ? 'header__step--active'
            : 'header__step--default'
        "
      >
        <div class="step__index">2</div>
        <div class="step__name">{{ $t("importLayout.step2Name") }}</div>
      </div>
      <div
        class="header__line"
        :class="
          currentStep >= $_TTANHEnum.IMPORT_STEP.DATA_CHECK
            ? 'header__step--active'
            : 'header__step--default'
        "
      ></div>
      <div
        class="header__step"
        :class="
          currentStep >= $_TTANHEnum.IMPORT_STEP.DATA_CHECK
            ? 'header__step--active'
            : 'header__step--default'
        "
      >
        <div class="step__index">3</div>
        <div class="step__name">{{ $t("importLayout.step3Name") }}</div>
      </div>
      <div
        class="header__line"
        :class="
          currentStep >= $_TTANHEnum.IMPORT_STEP.IMPORT_RESULT
            ? 'header__step--active'
            : 'header__step--default'
        "
      ></div>
      <div
        class="header__step"
        :class="
          currentStep >= $_TTANHEnum.IMPORT_STEP.IMPORT_RESULT
            ? 'header__step--active'
            : 'header__step--default'
        "
      >
        <div class="step__index">4</div>
        <div class="step__name">{{ $t("importLayout.step4Name") }}</div>
      </div>
    </div>
    <div class="employee-import__content">
      <EmployeeImportSelectSourceFile
        v-if="currentStep === $_TTANHEnum.IMPORT_STEP.SELECT_SOURCE_FILE"
        ref="selectSourceFile"
        v-model:excelFile="excelFile"
      />
      <EmployeeImportHeaderMerge
        v-if="currentStep === $_TTANHEnum.IMPORT_STEP.HEADER_MERGE"
        ref="headerMerge"
        :headersInfoComboboxData="formatHeadersInfo"
      />
      <EmployeeImportDataCheck
        v-if="currentStep === $_TTANHEnum.IMPORT_STEP.DATA_CHECK"
        ref="dataCheck"
        v-model="dataCheckInfo"
      />
      <EmployeeImportResult
        v-if="currentStep === $_TTANHEnum.IMPORT_STEP.IMPORT_RESULT"
        ref="result"
        :recordSuccess="recordSuccess"
        :recordFail="recordFail"
        :totalRecord="dataCheckInfo.countRecord"
      />
    </div>
    <div class="employee-import__footer">
      <div
        class="footer__button flex-row"
        v-if="currentStep === $_TTANHEnum.IMPORT_STEP.SELECT_SOURCE_FILE"
      >
        <ttanh-button
          type="sub"
          width="58px"
          colorText="#fff"
          borderRadius="var(--border-radius-default)"
          padding="0px 12px"
          @clickBtnContainer="$router.go(-1)"
          tabindex="99"
          >{{ $t("common.button.cancel") }}</ttanh-button
        >
        <ttanh-button
          type="main"
          width="92px"
          borderRadius="var(--border-radius-default)"
          padding="0px 22px"
          @clickBtnContainer="stepOneNext"
          tabindex="100"
          >{{ $t("common.button.continue") }}</ttanh-button
        >
      </div>
      <div
        class="footer__button flex-row"
        v-if="currentStep === $_TTANHEnum.IMPORT_STEP.HEADER_MERGE"
      >
        <ttanh-button
          type="sub"
          width="92px"
          colorText="#fff"
          borderRadius="var(--border-radius-default)"
          padding="0px 22px"
          @clickBtnContainer="currentStep--"
          tabindex="99"
          >{{ $t("common.button.rollBack") }}</ttanh-button
        >
        <ttanh-button
          type="main"
          width="92px"
          borderRadius="var(--border-radius-default)"
          padding="0px 22px"
          @clickBtnContainer="stepTwoNext"
          tabindex="100"
          >{{ $t("common.button.continue") }}</ttanh-button
        >
      </div>
      <div
        class="footer__button"
        v-if="currentStep === $_TTANHEnum.IMPORT_STEP.DATA_CHECK"
      >
        <div
          class="flex-row"
          style="width: 100%; justify-content: space-between"
          v-if="dataCheckInfo.validRecord !== 0"
        >
          <ttanh-button
            type="sub"
            width="92px"
            colorText="#fff"
            borderRadius="var(--border-radius-default)"
            padding="0px 22px"
            @clickBtnContainer="currentStep--"
            tabindex="99"
            >{{ $t("common.button.rollBack") }}</ttanh-button
          >
          <ttanh-button
            type="main"
            width="120px"
            borderRadius="var(--border-radius-default)"
            padding="0px 20px"
            @clickBtnContainer="stepThreeNext"
            tabindex="100"
            >{{ $t("common.button.dataInsert") }}</ttanh-button
          >
        </div>
        <div class="flex-row" style="width: 100%; justify-content: end" v-else>
          <ttanh-button
            type="main"
            width="68px"
            borderRadius="var(--border-radius-default)"
            padding="0px 18px"
            @clickBtnContainer="$router.go(-1)"
            tabindex="99"
            >{{ $t("common.button.close") }}</ttanh-button
          >
        </div>
      </div>
      <div
        class="footer__button flex-row"
        style="justify-content: end"
        v-if="currentStep === $_TTANHEnum.IMPORT_STEP.IMPORT_RESULT"
      >
        <ttanh-button
          type="main"
          width="68px"
          borderRadius="var(--border-radius-default)"
          padding="0px 18px"
          @clickBtnContainer="$router.go(-1)"
          tabindex="99"
          >{{ $t("common.button.close") }}</ttanh-button
        >
      </div>
    </div>
  </div>
</template>

<script>
import EmployeeImportDataCheck from "./import-data-check/EmployeeImportDataCheck.vue";
import EmployeeImportHeaderMerge from "./import-header-merge/EmployeeImportHeaderMerge.vue";
import EmployeeImportResult from "./import-result/EmployeeImportResult.vue";
import EmployeeImportSelectSourceFile from "./import-select-source-file/EmployeeImportSelectSourceFile.vue";

export default {
  name: "EmployeeImport",

  components: {
    EmployeeImportSelectSourceFile,
    EmployeeImportHeaderMerge,
    EmployeeImportDataCheck,
    EmployeeImportResult,
  },

  data() {
    return {
      currentStep: this.$_TTANHEnum.IMPORT_STEP.SELECT_SOURCE_FILE,

      //step 1
      excelFile: "",

      //step 2
      // trả về từ server là: "HeaderColumnIndex": 1,
      //                      "HeaderName": "EmployeeCode"
      headersInfo: [],

      //step 3
      dataCheckInfo: {
        validRecord: 0,
        invalidRecord: 0,
        countRecord: 0,
      },

      //step 4
      recordSuccess: 0,
      recordFail: 0,
    };
  },

  methods: {
    /**
     * Hàm xử lỗi chung
     * @author: TTANH (24/07/2023)
     */
    handleErrorCodeApi(errorCode, userMsg) {
      if (errorCode === this.$_TTANHEnum.ERROR_CODE.SESSION_IS_OVER) {
        this.$store.commit("addToast", {
          type: "error",
          text: userMsg,
        });
      }
    },

    /**
     * Xử lý khi ấn "tiếp tục" tại bước 1: Chọn tệp nguồn
     * @author: TTANH (23/07/2023)
     */
    async stepOneNext() {
      const res = await this.$refs.selectSourceFile.addExcelSetting();

      if (res.success) {
        this.currentStep++;
        this.headersInfo = res.data;
      } else {
        this.handleErrorCodeApi(res.errorCode, res.userMsg);
      }
    },

    /**
     * Xử lý khi ấn "tiếp tục" tại bước 2: Ghép dữ liệu
     * @author: TTANH (24/07/2023)
     */
    async stepTwoNext() {
      const res = await this.$refs.headerMerge.headerMapColumnExcel();

      if (res.success) {
        if (
          res.data.SuccessCode ===
          this.$_TTANHEnum.SUCCESS_CODE.IMPORT_EXCEL_REQUEST_SUCCESS
        ) {
          this.currentStep++;
        }
      } else {
        this.handleErrorCodeApi(res.errorCode, res.userMsg);
      }
    },

    /**
     * Xử lý khi ấn "nhập dữ liệu" tại bước 3: Kiểm tra dữ liệu
     * @author: TTANH (26/07/2023)
     */
    async stepThreeNext() {
      const res = await this.$refs.dataCheck.confirmImportExcelFile();

      if (res.success) {
        this.currentStep++;
        this.recordSuccess = res.data;
        this.recordFail = this.dataCheckInfo.countRecord - this.recordSuccess;
      } else {
        this.handleErrorCodeApi(res.errorCode, res.userMsg);
      }
    },
  },

  computed: {
    formatHeadersInfo() {
      var formatedHeadersInfo = [];

      this.headersInfo.forEach((headerInfo) => {
        var formatHeaderInfo = { ...headerInfo };

        formatHeaderInfo.id = headerInfo.HeaderColumnIndex;
        formatHeaderInfo.name = headerInfo.HeaderName;
        formatHeaderInfo.code = headerInfo.HeaderName;

        formatedHeadersInfo.push(formatHeaderInfo);
      });

      return formatedHeadersInfo;
    },
  },
};
</script>

<style scoped>
@import url(./employee-import.css);
</style>
