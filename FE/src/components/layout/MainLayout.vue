<template>
  <div class="layout-main">
    <TheSidebar />
    <TheMainContent>
      <router-view />
    </TheMainContent>
  </div>
</template>

<script>
import TheSidebar from "@/components/layout/sidebar/TheSidebar.vue";
import TheMainContent from "@/components/layout/main-content/TheMainContent.vue";

export default {
  name: "MainLayout",

  components: {
    TheSidebar,
    TheMainContent,
  },
};
</script>

<style>
.layout-main {
  display: flex;
  height: 100vh;
  width: 100vw;
}
</style>
