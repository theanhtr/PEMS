<template>
  <div class="import-layout">
    <div class="import-header">
      <div class="header-left">
        <p class="bold-text">{{ $t("importLayout.headerTitle") }}</p>
      </div>
      <div class="header-right">
        <ttanh-icon icon="help" :title="$t('common.helpIconTooltip')" />
        <ttanh-icon
          @click="closeImport"
          icon="close"
          style="margin-left: 3px"
          :title="$t('common.closeIconTooltip')"
        />
      </div>
    </div>
    <div class="import-content">
      <router-view />
    </div>
  </div>
</template>

<script>
export default {
  name: "ImportLayout",

  mounted() {
    window.addEventListener("keydown", this.onKeydown);
  },

  unmounted() {
    window.removeEventListener("keydown", this.onKeydown);
  },

  methods: {
    /**
     * Hàm để xử lý khi ấn "Đóng"
     * @author: TTANH (23/07/2024)
     */
    closeImport() {
      this.$router.push("/");
    },

    /**
     * Hàm để xử lý khi người dùng sd phím
     * @param {*} event
     */
    onKeydown(event) {
      if (event.keyCode === this.$_TTANHEnum.KEY_CODE.ESC) {
        event.preventDefault();
        this.closeImport();
      }
    },
  },
};
</script>

<style scoped>
@import url(./import-layout.css);
</style>
