<template>
  <div class="sidebar" :class="$store.state.isExpandSidebar ? '' : 'sidebar--collapse'">
    <div class="sidebar__logo-container">
      <router-link
        :class="`logo-${$store.state.langCode}`"
        to="/app/employee"
        :title="$t('sidebar.overview')"
        v-if="$store.state.isExpandSidebar"
      ></router-link>

      <h1 style="font-size: 22px; width: 75%">iFAWCast</h1>
    </div>

    <div class="sidebar__nav">
      <div class="sidebar__menu-item-container">
        <router-link v-if="checkRoleUserAccept([TTANHEnums.ROLE_ID.EXPERT, TTANHEnums.ROLE_ID.FARMER])" to="/app/predict-management" class="menu-item" exact activeClass="menu-item--selected">
          <div class="sidebar__text">{{ userRole == TTANHEnums.ROLE_ID.FARMER ? 'Thông tin dự báo' : 'Quản lý dự báo' }}</div>
        </router-link>
        <router-link v-if="checkRoleUserAccept([TTANHEnums.ROLE_ID.FARMER])" to="/app/planning-management" class="menu-item" exact activeClass="menu-item--selected">
          <div class="sidebar__text">Kế hoạch sản xuất</div>
        </router-link>
        <router-link v-if="checkRoleUserAccept([TTANHEnums.ROLE_ID.EXPERT])" to="/app/report-management" class="menu-item" exact activeClass="menu-item--selected">
          <div class="sidebar__text">Quản lý báo cáo</div>
        </router-link>
        <router-link v-if="checkRoleUserAccept([TTANHEnums.ROLE_ID.ADMIN])" to="/app/user-management" class="menu-item" exact activeClass="menu-item--selected">
          <div class="sidebar__text">Quản lý người dùng</div>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'TheSidebar',
  data() {
    return {
      TTANHEnums: this.$_TTANHEnum,
      userRole: -1
    }
  },
  methods: {
    checkRoleUserAccept(roles) {
      this.userRole = Number(localStorage.getItem('roleId'))

      if (roles.includes(this.userRole)) {
        return true
      } else {
        return false
      }
    }
  }
}
</script>

<style>
@import url(./sidebar.css);
</style>
