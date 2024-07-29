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
        <div
          class="header__account-info"
          :title="$t('mainContent.headerAccountInfo')"
          @click="showUserInfo"
        >
          <div class="account-avatar"></div>
          <div class="account-name">{{ user.Fullname }}</div>
          <ttanh-icon scale="0.7" icon="dropdown--light-black" />
        </div>
      </div>
    </div>
    <div v-show="isShowUserInfo" class="user-info-blur">
      <div class="user-info">
        <div class="header">
        <h1 class="title">Thông tin tài khoản</h1>
        <span @click="isShowUserInfo = false" class="close-button">X</span>
        </div>
        <div class="content">
          <div class="profile">
            <div class="avatar">
              <img class="avatar-img" src="https://inkythuatso.com/uploads/images/2022/05/hinh-anh-meo-bua-buon-cuoi-nhat-12-09-56-39.jpg" alt="Avatar" />
            </div>
            <div class="info">
              <p>{{ user.Fullname }}</p>
            </div>
          </div>
          <button @click="logout" class="logout-button">Đăng xuất</button>
        </div>
      </div>
    </div>
    <div class="page-content">
      <slot></slot>
    </div>
  </div>
</template>

<script>
import userService from "@/service/UserService";
export default {
  name: "TheMainContent",
  async created() {
    await this.getUserInfo();
  },
  methods: {
    /**
     * bắt sự kiện thu gọn sidebar
     * @author: TTANH (25/06/2024)
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
    async getUserInfo() {
      let res = await userService.get();
      this.user = res.data;
    },
    showUserInfo() {
      this.isShowUserInfo = true;
    },
    logout() {
      this.$store.commit("logout");
      this.$router.push("/login");
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
      user: {},
      isShowUserInfo: false,
    };
  },
};
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

.user-info-blur {
  width: 100vw;
  height: 100vh;
  background-color: rgba(0, 0, 0, 0.4);
  position: fixed;
  z-index: 999998;
  top: 0;
  left: 0;
}

.user-info {
  width: 300px;
  background-color: white;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1);
  position: fixed;
  right: 0;
  top: 0;
  height: 100%;
  width: 20%;
  z-index: 999999;
  display: flex;
  flex-direction: column;
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.title {
  font-size: 1.2rem;
  font-weight: bold;
}

.close-button {
  font-size: 1.5rem;
  cursor: pointer;
}

.content {
  text-align: center;
}

.profile {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-bottom: 20px;
}

.avatar {
  width: 80px;
  height: 80px;
  border-radius: 50%;
  margin-right: 12px;
}

.avatar-img {
  width: 100%;
  height: 100%;
  border-radius: 50%;
}

.info {
  text-align: left;
  font-weight: bold;
  font-size: 16px;
}

.info p {
  margin-bottom: 5px;
}

.icon {
  margin-right: 5px;
}

.logout-button {
  background-color: #4CAF50;
  color: white;
  padding: 10px 20px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}
</style>
