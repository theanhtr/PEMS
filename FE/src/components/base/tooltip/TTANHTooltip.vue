<template>
  <div :style="styleTooltip" class="tooltip">
    <div class="tooltip-arrow"></div>
    <div class="tooltip-text">
      <slot></slot>
    </div>
  </div>
</template>

<script>
export default {
  name: "TTANHTooltip",
  props: {
    error: {
      default: true,
    },
  },
  computed: {
    styleTooltip() {
      return {
        borderColor: this.error ? "#f77" : "#505050",
        background: this.error ? "#f77" : "#505050",
      };
    },
  },
};
</script>

<style>
@import url(./tooltip.css);
</style>
