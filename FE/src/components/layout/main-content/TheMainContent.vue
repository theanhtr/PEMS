<template>
  <div id="main-content">
    <div class="header">
      <div class="header__left">
        <ttanh-icon
          icon="sidebar__control--grey"
          :tooltip="$t('mainContent.tooltipCollapseSidebar')"
          @click="collapseSidebar"
          v-if="$store.state.isExpandSidebar"
        ></ttanh-icon>
        <div class="header__branch" title="Công ty cổ phần ttanh">
          <div class="header__branch-name">Công ty cổ phần ttanh</div>
          <ttanh-icon icon="dropdown--light-black"></ttanh-icon>
        </div>
        <ttanh-button
          :scaleIcon="0.7"
          type="sub"
          icon="dot--success"
          height="var(--primary-button-height)"
          title="ttanh_test_db"
          >ttanh_test_db</ttanh-button
        >
      </div>
      <ttanh-icon
        icon="header__download"
        :tooltip="$t('mainContent.headerDownload')"
      />
      <div class="header__right">
        <div class="change-language flex-row">
          <ttanh-icon
            icon="flag-vi"
            :tooltip="$t('mainContent.changeLanguage.vi')"
            width="24px"
            height="18px"
            class="change-language__item"
            :class="
              $store.state.langCode === 'vi'
                ? 'change-language__item--active'
                : ''
            "
            @click="$store.commit('setLangCode', 'vi')"
          />
          |
          <ttanh-icon
            icon="flag-en"
            :tooltip="$t('mainContent.changeLanguage.en')"
            width="24px"
            height="18px"
            class="change-language__item"
            :class="
              $store.state.langCode === 'en'
                ? 'change-language__item--active'
                : ''
            "
            @click="$store.commit('setLangCode', 'en')"
          />
        </div>
        <ttanh-search-input
          v-model="searchText"
          width="200px"
          :placeholder="$t('mainContent.headerSearchPlaceholder')"
        />
        <div class="header__action-right">
          <ttanh-button
            type="icon"
            height="100%"
            width="auto"
            class="header__action-icon"
            @mouseenter="headerButtonHover['header__setting'] = true"
            @mouseleave="headerButtonHover['header__setting'] = false"
            :icon="
              'header__setting--' +
              (headerButtonHover['header__setting'] ? 'white' : 'grey')
            "
            :tooltip="$t('mainContent.headerSettingTooltip')"
          />
          <ttanh-button
            type="icon"
            height="100%"
            width="auto"
            class="header__action-icon"
            @mouseenter="headerButtonHover['header__message'] = true"
            @mouseleave="headerButtonHover['header__message'] = false"
            :icon="
              'header__message--' +
              (headerButtonHover['header__message'] ? 'white' : 'grey')
            "
            :tooltip="$t('mainContent.headerMessageTooltip')"
          />
          <ttanh-button
            type="icon"
            height="100%"
            width="auto"
            class="header__action-icon"
            @mouseenter="headerButtonHover['header__question'] = true"
            @mouseleave="headerButtonHover['header__question'] = false"
            :icon="
              'header__question--' +
              (headerButtonHover['header__question'] ? 'white' : 'grey')
            "
            :tooltip="$t('mainContent.headerHelpTooltip')"
          />
          <ttanh-button
            type="icon"
            height="100%"
            width="auto"
            class="header__action-icon"
            @mouseenter="headerButtonHover['header__noti'] = true"
            @mouseleave="headerButtonHover['header__noti'] = false"
            :icon="
              'header__noti--' +
              (headerButtonHover['header__noti'] ? 'white' : 'grey')
            "
            :tooltip="$t('mainContent.headerNotificationTooltip')"
          />
        </div>
        <div
          class="header__account-info"
          :title="$t('mainContent.headerAccountInfo')"
        >
          <div class="account-avatar"></div>
          <div class="account-name">Trần Thế Anh</div>
          <ttanh-icon scale="0.7" icon="dropdown--light-black" />
        </div>
      </div>
    </div>
    <div class="page-content">
      <slot></slot>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheMainContent",
  methods: {
    /**
     * bắt sự kiện thu gọn sidebar
     * @author: TTANH (25/06/2023)
     */
    collapseSidebar() {
      try {
        this.$store.commit("toggleSidebar");
      } catch (error) {
        console.log(
          "🚀 ~ file: TheMainContent.vue:83 ~ collapseSidebar ~ error:",
          error
        );
      }
    },
  },
  data() {
    return {
      headerButtonHover: {
        header__setting: false,
        header__message: false,
        header__question: false,
        header__noti: false,
      },
      searchText: "",
    };
  },
};
</script>

<style>
@import url(./header.css);
@import url(./page-content.css);

#main-content {
  height: 100%;
  width: calc(100% - 200px);
  display: flex;
  flex-direction: column;
}
</style>
