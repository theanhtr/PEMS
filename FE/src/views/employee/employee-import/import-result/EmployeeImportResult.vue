<template>
  <div class="step-4">
    <div class="header__step-4 flex-column">
      <div class="step-4__title--success">
        {{
          $t("importExcel.importResult.header.successTitle", {
            recordSuccess: recordSuccess,
            totalRecord: totalRecord,
          })
        }}
      </div>
      <div class="step-4__title--fail">
        {{
          $t("importExcel.importResult.header.failTitle", {
            recordFail: recordFail,
            totalRecord: totalRecord,
          })
        }}
      </div>
    </div>

    <ttanh-loading-spinner size="large" v-if="isLoading" />
  </div>
</template>

<script>
export default {
  name: "EmployeeImportResult",

  props: {
    recordSuccess: {
      default: 0,
    },
    recordFail: {
      default: 0,
    },
    totalRecord: {
      default: 0,
    },
  },
};
</script>

<style scoped>
@import url(./import-result.css);
</style>
