<template>
  <div class="m-overlay">
    <div id="loading-bar-spinner" class="spinner" :class="`spinner-${size}`">
      <div class="spinner-icon"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TTANHLoadingSpinner",
  props: {
    size: {
      default: "small",
      validator: function (val) {
        return ["small", "large"].includes(val);
      },
    },
  },
};
</script>

<style scoped>
@import url(./loading-spinner.css);
</style>
