<template>
  <div class="m-popup" :style="stylePopup">
    <div v-if="haveHeader" class="m-popup__header">
      <div class="m-popup__title">
        <div class="title-popup">{{ title }}</div>
        <slot name="title__action"></slot>
      </div>
      <div class="m-popup__close">
        <slot name="header__close"></slot>
      </div>
    </div>
    <div class="m-popup__content">
      <div v-if="description" class="m-popup__description">
        {{ description }}
      </div>
      <div class="m-popup__input-control">
        <slot name="content__input-control"></slot>
      </div>
    </div>
    <div class="m-popup__footer">
      <slot name="footer"></slot>
    </div>
  </div>
</template>

<script>
export default {
  name: "TTANHPopup",
  props: {
    haveHeader: {
      default: true,
    },
    title: {
      default: "",
    },
    description: {
      default: "",
    },
    width: {
      default: "900px",
    },
    height: {
      default: "auto",
    },
  },
  computed: {
    stylePopup() {
      return {
        minWidth: this.width,
        maxWidth: this.width,
        width: this.width,
        height: this.height,
      };
    },
  },
};
</script>

<style scoped>
@import url(./popup.css);
</style>
