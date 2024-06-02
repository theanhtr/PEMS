<template>
  <div
    class="sidebar"
    :class="$store.state.isExpandSidebar ? '' : 'sidebar--collapse'"
  >
    <div class="sidebar__logo-container">
      <ttanh-icon
        icon="sidebar__control--white"
        :tooltip="$t('sidebar.tooltipExpandSidebar')"
        @click="expandSidebar"
        v-if="!$store.state.isExpandSidebar"
        height="fit-content"
        width="fit-content"
      ></ttanh-icon>
      <a
        href="https://attanhpp.ttanh.vn/home"
        class="toggle-icon"
        title="ttanh App"
        v-else
      ></a>

      <router-link
        :class="`logo-${$store.state.langCode}`"
        to="/app/dashboard"
        :title="$t('sidebar.overview')"
        v-if="$store.state.isExpandSidebar"
      ></router-link>
    </div>

    <div class="sidebar__nav">
      <div class="sidebar__quick-add-button">
        <ttanh-button
          type="sub"
          icon="plus--green"
          width="124px"
          colorText="white"
          v-if="$store.state.isExpandSidebar"
          :border="'2px solid white'"
          >{{ $t("sidebar.quickAddButton") }}</ttanh-button
        >
        <ttanh-button type="sub" icon="plus--green" width="30px" v-else />
        >
      </div>
      <div class="sidebar__menu-item-container">
        <router-link
          to="/app/dashboard"
          class="menu-item"
          exact
          activeClass="menu-item--selected"
          :title="$t('sidebar.overview')"
        >
          <ttanh-icon icon="sidebar__overview"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.overview") }}</div>
        </router-link>
        <router-link
          to="/app/employee"
          class="menu-item"
          exact
          activeClass="menu-item--selected"
          :title="$t('sidebar.cash')"
        >
          <ttanh-icon icon="sidebar__cash"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.cash") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.deposits')"
        >
          <ttanh-icon icon="sidebar__deposits"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.deposits") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.purchase')"
        >
          <ttanh-icon icon="sidebar__purchase"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.purchase") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.sell')"
        >
          <ttanh-icon icon="sidebar__sell"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.sell") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.invoiceManagement')"
        >
          <ttanh-icon icon="sidebar__invoice-management"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.invoiceManagement") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.warehouse')"
        >
          <ttanh-icon icon="sidebar__warehouse"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.warehouse") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.tool')"
        >
          <ttanh-icon icon="sidebar__tool"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.tool") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.fixedAssets')"
        >
          <ttanh-icon icon="sidebar__fixed-assets"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.fixedAssets") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.salary')"
        >
          <ttanh-icon icon="sidebar__salary"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.salary") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.tax')"
        >
          <ttanh-icon icon="sidebar__tax"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.tax") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.price')"
        >
          <ttanh-icon icon="sidebar__price"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.price") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.loans')"
        >
          <ttanh-icon icon="sidebar__loans"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.loans") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.synthetic')"
        >
          <ttanh-icon icon="sidebar__synthetic"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.synthetic") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.budget')"
        >
          <ttanh-icon icon="sidebar__budget"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.budget") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.report')"
        >
          <ttanh-icon icon="sidebar__report"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.report") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.financialAnalysis')"
        >
          <ttanh-icon icon="sidebar__financial-analysis"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.financialAnalysis") }}</div>
        </router-link>
        <ttanh-separation-line></ttanh-separation-line>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.category')"
        >
          <ttanh-icon icon="sidebar__category"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.category") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.initialBalance')"
        >
          <ttanh-icon icon="sidebar__initial-balance"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.initialBalance") }}</div>
        </router-link>
        <router-link
          to="/"
          activeClass="menu-item--selected"
          exact
          class="menu-item"
          :title="$t('sidebar.knowledge')"
        >
          <ttanh-icon icon="sidebar__knowledge"></ttanh-icon>
          <div class="sidebar__text">{{ $t("sidebar.knowledge") }}</div>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheSidebar",
  methods: {
    /**
     * bắt sự kiện mở rộng sidebar
     * @author: TTANH (25/06/2023)
     */
    expandSidebar() {
      try {
        this.$store.commit("toggleSidebar");
      } catch (error) {
        console.log(
          "🚀 ~ file: TheSidebar.vue:129 ~ expandSidebar ~ error:",
          error
        );
      }
    },
  },
};
</script>

<style>
@import url(./sidebar.css);
</style>
