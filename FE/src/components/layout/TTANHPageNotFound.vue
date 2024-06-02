<template>
  <div class="not-found">
    <img src="../../assets/img/bg_404.ef80cdb6.svg" />
    <div class="not-found__title">{{ $t("component.notFound.title") }}</div>
    <ttanh-button @click="$router.push('/')" type="main" borderRadius="12px">{{
      $t("common.button.rollBack")
    }}</ttanh-button>
  </div>
</template>

<script>
export default {
  name: "TTANHPageNotFound",
};
</script>

<style scoped>
.not-found {
  width: 100vw;
  height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  row-gap: 12px;
}

.not-found__title {
  font-weight: 700;
  font-size: 24px;
  margin-bottom: 20px;
}
</style>
