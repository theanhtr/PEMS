<template>
  <div
    class="loading-skeleton__element"
    :style="{
      maxWidth: width,
      maxHeight: height,
      minWidth: width,
      minHeight: height,
    }"
  ></div>
</template>

<script>
export default {
  name: "TTANHLoadingSkeleton",
  props: {
    width: {
      default: "",
    },
    height: {
      default: "",
    },
  },
};
</script>

<style scoped>
@import url(./loading-skeleton.css);
</style>
