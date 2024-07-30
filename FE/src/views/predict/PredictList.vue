<template>
  <div class="page">
    <div class="page__container">
      <PredictContent />
    </div>
  </div>
</template>

<script>
import PredictContent from "./child-component/PredictContent.vue";

export default {
  name: "PredictList",
  components: {
    PredictContent,
  },
};
</script>

<style scoped>
@import url(./predict-list.css);
</style>
