<template>
  <div
    ref="iconContainer"
    :style="styleIconContainer"
    class="icon-container"
    :title="this.tooltip"
  >
    <div class="ttanh__icon" :class="icon" :style="{ scale: this.scale }"></div>
  </div>
</template>

<script>
export default {
  name: "TTANHIcon",
  props: {
    icon: {
      required: true,
      type: String,
    },
    width: {
      default: "24px",
    },
    height: {
      default: "24px",
    },
    tooltip: {
      default: "",
    },
    scale: {
      default: "1",
    },
    disable: {
      default: false,
    },
  },
  methods: {
    /**
     * lấy vùng chứa của icon
     * @author: TTANH (31/07/2024)
     */
    getIconContainerRef() {
      return this.$refs.iconContainer;
    },
  },
  computed: {
    styleIconContainer() {
      return {
        height: this.height,
        width: this.width,
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        flex: `0 0 ${this.width}`,
        cursor: this.disable ? "default" : "pointer",
      };
    },
  },
};
</script>

<style scoped>
@import url(./icon-common.css);
@import url(./icon-sidebar.css);
@import url(./icon-header.css);
@import url(./icon-status.css);
@import url(./icon-page.css);
@import url(./icon-table.css);
</style>
