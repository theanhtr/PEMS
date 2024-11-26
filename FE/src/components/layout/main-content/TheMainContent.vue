<template>
  <div id="main-content">
    <div class="header">
      <div class="header__left"></div>
      <div class="header__right">
        <div class="header__action-right">
          <ttanh-button
            type="icon"
            height="100%"
            width="auto"
            class="header__action-icon"
            borderRadius="0"
            border="0"
            @mouseenter="headerButtonHover['header__noti'] = true"
            @mouseleave="headerButtonHover['header__noti'] = false"
            icon="header__noti--grey"
            :tooltip="$t('mainContent.headerNotificationTooltip')"
          />
        </div>
        <div class="header__account-info" :title="$t('mainContent.headerAccountInfo')" @click="showUserInfo">
          <div class="avatar">
            <img
              class="avatar-img"
              src="https://inkythuatso.com/uploads/images/2022/05/hinh-anh-meo-bua-buon-cuoi-nhat-12-09-56-39.jpg"
              alt="Avatar"
            />
          </div>
          <div class="account-name">{{ user.Fullname }}</div>
          <ttanh-icon scale="0.7" icon="dropdown--light-black" />
        </div>
      </div>
    </div>
    <AccountInfo :isShowUserInfo="isShowUserInfo" @close="isShowUserInfo = false" @reloadData="getUserInfo" :user="user"/>
    <div class="page-content">
      <slot></slot>
    </div>
  </div>
</template>

<script>
import AccountInfo from '../../../views/user/child-component/AccountInfo.vue';
import UserService from '@/service/UserService'

export default {
  name: 'TheMainContent',
  async created() {
    await this.getUserInfo()
  },
  components: {
    AccountInfo
  },
  methods: {
    async getUserInfo() {
      let res = await UserService.get('User/info')
      this.user = res.data

      localStorage.setItem("provinceId", this.user.ProvinceId);
      localStorage.setItem("districtId", this.user.DistrictId);
      localStorage.setItem("wardId", this.user.WardId);
      localStorage.setItem("provinceName", this.user.ProvinceName);
      localStorage.setItem("districtName", this.user.DistrictName);
      localStorage.setItem("wardName", this.user.WardName);
      localStorage.setItem("fullName", this.user.Fullname);
    },
    /**
     * bắt sự kiện thu gọn sidebar
     * @author: TTANH (25/06/2024)
     */
    collapseSidebar() {
      try {
        this.$store.commit('toggleSidebar')
      } catch (error) {
        console.log('🚀 ~ file: TheMainContent.vue:83 ~ collapseSidebar ~ error:', error)
      }
    },
    showUserInfo() {
      this.isShowUserInfo = true
    },
  },
  data() {
    return {
      headerButtonHover: {
        header__setting: false,
        header__message: false,
        header__question: false,
        header__noti: false
      },
      searchText: '',
      isShowUserInfo: false,
      user: {},
    }
  }
}
</script>

<style scoped>
@import url(./header.css);
@import url(./page-content.css);

#main-content {
  height: 100%;
  width: calc(100% - 200px);
  display: flex;
  flex-direction: column;
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.icon {
  margin-right: 5px;
}
</style>
