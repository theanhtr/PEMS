<template>
  <div class="step-2">
    <div class="header__step-2">
      <div class="header__step-2__left">
        <div class="step-2__title label-text">
          {{ $t("importExcel.headerMerge.header.title") }}
        </div>
      </div>
      <div class="header__step-2__right"></div>
    </div>
    <div class="content__step-2">
      <ttanh-table
        ref="headerMergeTable"
        :columnsInfo="headerMergeColumnsInfo"
        :rowsData="employeeHeadersInfo"
        :oneRowSelect="true"
        :comboboxRowData="headersInfoComboboxData"
        @checked-row="checkedRow"
        @unchecked-row="uncheckedRow"
      />
    </div>

    <ttanh-error-popup
      v-if="isShowDialogError"
      :errorText="errorText"
      @close-click="closeBtnDialogErrorClick"
    />

    <ttanh-loading-spinner size="large" v-if="isLoading" />
  </div>
</template>

<script>
import { headersInfo } from "./employeeHeadersInfo.js";
import { headerMergeColumnsInfo } from "./headerMergeColumnsInfo.js";
import { findIndexByAttributeInclude } from "@/helper/common.js";
import EmployeeService from "@/service/EmployeeService.js";

export default {
  name: "EmployeeImportHeaderMerge",
  props: {
    headersInfoComboboxData: {
      required: true,
    },
  },

  data() {
    return {
      searchText: "",
      employeeHeadersInfo: [...headersInfo],
      headerMergeColumnsInfo: [...headerMergeColumnsInfo],

      isShowDialogError: false,
      isLoading: false,
      errorText: "",
    };
  },

  created() {
    this.employeeHeadersInfo.forEach((e) => {
      let indexFind = findIndexByAttributeInclude(
        this.headersInfoComboboxData,
        "name",
        e.showName
      );

      if (indexFind !== -1) {
        e.excelColumnIndex =
          this.headersInfoComboboxData[indexFind]["HeaderColumnIndex"];
      } else {
        e.excelColumnIndex = 0;
      }
    });
  },

  methods: {
    /**
     * Hàm validate các trường bắt buộc phải ánh xạ
     * @author: TTANH (24/07/2023)
     */
    headerIndexValidate() {
      for (var employeeHeaderInfo of this.employeeHeadersInfo) {
        if (
          employeeHeaderInfo.isRequired &&
          (employeeHeaderInfo.excelColumnIndex <= 0 ||
            employeeHeaderInfo.excelColumnIndex == "")
        ) {
          this.errorText = this.$t(
            "errorHandle.importExcel.headerMerge.headerRequiredNotMap"
          );

          this.isShowDialogError = true;

          return false;
        }
      }

      return true;
    },

    /**
     * Đóng thông báo lỗi
     * @author: TTANH (24/07/2023)
     */
    closeBtnDialogErrorClick() {
      this.isShowDialogError = false;
      this.errorText = "";
    },

    /**
     * Hàm thêm các cài đặt cho việc ánh xạ từ header trong excel
     * @author: TTANH (24/07/2023)
     */
    async headerMapColumnExcel() {
      try {
        if (this.headerIndexValidate()) {
          let dataSendApi = [];

          this.employeeHeadersInfo.forEach((employeeHeaderInfo) => {
            if (employeeHeaderInfo.excelColumnIndex > 0) {
              dataSendApi.push({
                HeaderColumnIndex: employeeHeaderInfo.excelColumnIndex,
                ServerColumnName: employeeHeaderInfo.serverName,
              });
            }
          });

          this.isLoading = true;

          const res = await EmployeeService.headerMapColumnExcel(dataSendApi);

          if (res.success) {
          } else {
            if (
              res.errorCode === this.$_TTANHEnum.ERROR_CODE.WRONG_FORMAT_DATE ||
              this.$_TTANHEnum.ERROR_CODE.EXCEL_HEADER_REQUIRED_NOT_MAP
            ) {
              this.$store.commit("addToast", {
                type: "error",
                text: res.userMsg,
              });
            }
          }

          this.isLoading = false;

          return res;
        } else {
          return {
            success: false,
          };
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeImportHeaderMerge.vue:144 ~ headerMapColumnExcel ~ error:",
          error
        );
      }
    },
  },
};
</script>

<style scoped>
@import url(./import-header-merge.css);
</style>
