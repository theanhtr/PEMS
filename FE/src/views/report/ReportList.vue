<template>
  <div class="page">
    <div class="page__container">
      <ReportContent />
    </div>
  </div>
</template>

<script>
import ReportContent from "./child-component/ReportContent.vue";

export default {
  name: "ReportList",
  components: {
    ReportContent,
  },
};
</script>

<style scoped>
@import url(./report-list.css);
</style>
