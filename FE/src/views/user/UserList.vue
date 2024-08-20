<template>
  <div class="page">
    <div class="page__container">
      <UserContent />
    </div>
  </div>
</template>

<script>
import UserContent from './child-component/UserContent.vue'

export default {
  name: 'UserList',
  components: {
    UserContent
  }
}
</script>

<style scoped>
@import url(./user-list.css);
</style>
