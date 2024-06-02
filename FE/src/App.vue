<template>
  <div class="app-layout">
    <div class="toast-group">
      <ttanh-toast
        v-for="toast in $store.state.toasts"
        :key="toast.id"
        :type="toast.type"
        @clickCloseToast="closeToast(toast.id)"
      >
        {{ toast.text }}
      </ttanh-toast>
    </div>
    <router-view />
  </div>
</template>

<script>
export default {
  name: "App",
  methods: {
    /**
     * xử lý xóa 1 toast
     * @author: TTANH (01/07/2023)
     * @param {string} id id của toast
     */
    closeToast(id) {
      try {
        this.$store.commit("removeToast", { id: id });
      } catch (error) {
        console.log("🚀 ~ file: App.vue:36 ~ closeToast ~ error:", error);
      }
    },
  },
};
</script>

<style>
@import url(./css/main.css);
.app-layout {
  width: 100vw;
  height: 100vh;
}
</style>
