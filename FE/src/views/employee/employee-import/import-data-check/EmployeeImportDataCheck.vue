<template>
  <div class="step-3">
    <div class="header__step-3 flex-row">
      <div class="header__step-3__left flex-column">
        <div class="step-3__title--valid">
          {{
            $t("importExcel.dataCheck.header.validTitle", {
              validCount: modelValue.validRecord,
            })
          }}
        </div>
        <div class="step-3__title--invalid">
          {{
            $t("importExcel.dataCheck.header.invalidTitle", {
              invalidCount: modelValue.invalidRecord,
            })
          }}
        </div>
      </div>
      <div class="header__step-3__right flex-row">
        <div class="header__step-3__right--title">
          {{ $t("importExcel.dataCheck.header.statusFilter.title") }}:
        </div>
        <ttanh-combobox
          v-model="filterExcelDataValiateType"
          type="single-row"
          :rowsData="filterOptions"
          :disableInput="true"
        />
      </div>
    </div>
    <div class="content__step-3">
      <ttanh-table
        ref="headerMergeTable"
        :columnsInfo="dataCheckColumnsInfo"
        :rowsData="employeesValidate"
        :oneRowSelect="true"
        :noData="computedNoData"
      />
    </div>
    <div class="footer__step-3">
      <ttanh-paging
        v-if="!this.noData"
        v-model="pagingData"
        @reloadData="reloadData"
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
import EmployeeService from "@/service/EmployeeService.js";
import { dataCheckColumnsInfo } from "./DataCheckColumnsInfo.js";
import { CommonErrorHandle } from "@/helper/error-handle";

export default {
  name: "EmployeeImportDataCheck",

  props: {
    modelValue: {
      required: true,
    },
  },

  data() {
    return {
      employeesValidate: [],

      dataCheckColumnsInfo: [...dataCheckColumnsInfo],

      filterExcelDataValiateType:
        this.$_TTANHEnum.FILTER_EXCEL_DATA_VALIDATE_TYPE.ALL,

      filterOptions: [
        {
          id: this.$_TTANHEnum.FILTER_EXCEL_DATA_VALIDATE_TYPE.ALL,
          name: this.$t("importExcel.dataCheck.header.statusFilter.all"),
          code: this.$t("importExcel.dataCheck.header.statusFilter.all"),
        },
        {
          id: this.$_TTANHEnum.FILTER_EXCEL_DATA_VALIDATE_TYPE.INVALID,
          name: this.$t("importExcel.dataCheck.header.statusFilter.invalid"),
          code: this.$t("importExcel.dataCheck.header.statusFilter.invalid"),
        },
        {
          id: this.$_TTANHEnum.FILTER_EXCEL_DATA_VALIDATE_TYPE.VALID,
          name: this.$t("importExcel.dataCheck.header.statusFilter.valid"),
          code: this.$t("importExcel.dataCheck.header.statusFilter.valid"),
        },
      ],

      isShowDialogError: false,
      isLoading: false,

      noData: false,

      /* biến sử dụng cho phân trang */
      pagingData: {
        pageSize: 100,
        pageNumber: 1,
        totalPage: 0,
        totalRecord: 0,
      },
    };
  },

  created() {
    this.countRecordValidate();
    this.getEmployeesValidate();
  },

  methods: {
    /**
     * Đếm số bản ghi hợp lệ và không hợp lệ
     * @author: TTANH (25/07/2023)
     */
    async countRecordValidate() {
      const res = await EmployeeService.checkDataExcelFilter({
        pageSize: 99999999,
        pageNumber: 1,
        filterExcelDataValidateType:
          this.$_TTANHEnum.FILTER_EXCEL_DATA_VALIDATE_TYPE.ALL,
      });

      var employeesValidateCount;

      if (res.success) {
        employeesValidateCount = res.data.Data;

        // đếm số bản ghi hợp lệ và không hợp lệ
        employeesValidateCount.forEach((employeeValidate) => {
          if (
            employeeValidate.ValidCheck == this.$_TTANHEnum.RECORD_CHECK.VALID
          ) {
            this.modelValue.validRecord++;
          } else {
            this.modelValue.invalidRecord++;
          }
        });

        this.modelValue.countRecord = res.data.TotalRecord;
      } else {
        CommonErrorHandle();
      }
    },

    /**
     * thực hiện get dữ liệu trong file excel
     * @author: TTANH (25/07/2023)
     */
    async getEmployeesValidate() {
      try {
        const res = await EmployeeService.checkDataExcelFilter({
          pageSize: this.pagingData.pageSize,
          pageNumber: this.pagingData.pageNumber,
          filterExcelDataValidateType: this.filterExcelDataValiateType,
        });

        if (res.success) {
          this.employeesValidate = res.data.Data;
          this.pagingData.totalPage = res.data.TotalPage;
          this.pagingData.totalRecord = res.data.TotalRecord;

          if (res.data.Data.length != 0) {
            this.employeesValidate = res.data.Data;
            this.pagingData.totalPage = res.data.TotalPage;
            this.pagingData.totalRecord = res.data.TotalRecord;
            this.pagingData.pageNumber = res.data.CurrentPage;
            this.noData = false;
          } else {
            this.noData = true;
          }
        } else {
          if (res.errorCode === this.$_TTANHEnum.ERROR_CODE.SESSION_IS_OVER) {
            this.$store.commit("addToast", {
              type: "error",
              text: res.userMsg,
            });
          } else {
            CommonErrorHandle();
          }
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeImportDataCheck.vue:103 ~ getEmployeesValidate ~ error:",
          error
        );
      }
    },

    /**
     * cập nhật lại employees mới
     * @author: TTANH (03/07/2023)
     */
    reloadData() {
      try {
        this.employeesValidate = [];
        this.getEmployeesValidate();
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeImportDataCheck.vue:136 ~ reloadData ~ error:",
          error
        );
      }
    },

    /**
     * Api xác nhận import dữ liệu từ file
     * @author: TTANH (26/07/2023)
     */
    async confirmImportExcelFile() {
      try {
        this.isLoading = true;

        const res = await EmployeeService.confirmImportExcelFile();

        this.isLoading = false;

        return res;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeImportDataCheck.vue:208 ~ confirmImportExcelFile ~ error:",
          error
        );
      }
    },
  },

  computed: {
    computedNoData() {
      return this.noData;
    },
  },

  watch: {
    filterExcelDataValiateType() {
      this.reloadData();
    },
  },
};
</script>

<style scoped>
@import url(./import-data-check.css);
</style>
