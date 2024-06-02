<template>
  <div class="toast" :class="'toast--' + type">
    <div class="toast__left">
      <ttanh-icon
        :icon="type + '--large'"
        scale="0.9"
        height="auto"
        width="auto"
      />
      <div class="toast__text">
        <span class="bold-text">{{ defaultTitle }} </span> <slot></slot>
      </div>
    </div>

    <ttanh-icon icon="close" @click="$emit('clickCloseToast')" />

    <div class="toast__count-down"></div>
  </div>
</template>

<script>
export default {
  name: "TTANHToast",
  props: {
    type: {
      default: "info",
      validator: function (val) {
        return ["info", "error", "warning", "success"].includes(val);
      },
    },
  },
  computed: {
    defaultTitle() {
      return this.$t(`component.toast.defaultTitle.${this.type}`);
    },
  },
};
</script>

<style scoped>
@import url(./toast.css);
</style>
