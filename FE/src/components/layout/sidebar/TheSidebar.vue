<template>
  <div
    class="sidebar"
    :class="$store.state.isExpandSidebar ? '' : 'sidebar--collapse'"
  >
    <div class="sidebar__logo-container">

      <router-link
        :class="`logo-${$store.state.langCode}`"
        to="/app/employee"
        :title="$t('sidebar.overview')"
        v-if="$store.state.isExpandSidebar"
      ></router-link>

      <h1 style="font-size: 22px; width: 75%;">iFAWCast</h1>
    </div>

    <div class="sidebar__nav">
      <div class="sidebar__menu-item-container">
        <router-link
          to="/app/employee"
          class="menu-item"
          exact
          activeClass="menu-item--selected"
        >
          <div class="sidebar__text">Quản lý test</div>
        </router-link>
        <router-link
          to="/app/predict-management"
          class="menu-item"
          exact
          activeClass="menu-item--selected"
        >
          <div class="sidebar__text">Quản lý dự báo</div>
        </router-link>
        <router-link
          to="/app/report-management"
          class="menu-item"
          exact
          activeClass="menu-item--selected"
        >
          <div class="sidebar__text">Quản lý báo cáo</div>
        </router-link>
        <router-link
          to="/app/user-management"
          class="menu-item"
          exact
          activeClass="menu-item--selected"
        >
          <div class="sidebar__text">Quản lý người dùng</div>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheSidebar",
  methods: {
  },
};
</script>

<style>
@import url(./sidebar.css);
</style>
