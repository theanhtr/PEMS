<template>
  <div v-show="isShowUserInfo" class="user-info-blur">
    <div class="user-info">
      <div class="header">
        <h1 class="title">Thông tin tài khoản</h1>
        <ttanh-icon
          @click="$emit('close')"
          icon="close"
          style="margin-left: 3px"
          :title="$t('common.closeIconTooltip')"
        />
      </div>
      <div class="content w1">
        <div class="profile w1">
          <div class="avatar">
            <img
              class="avatar-img"
              src="https://inkythuatso.com/uploads/images/2022/05/hinh-anh-meo-bua-buon-cuoi-nhat-12-09-56-39.jpg"
              alt="Avatar"
            />
          </div>
          <div class="info w1">
            <p class="info-fullname">{{ user.Fullname }}</p>
            <p>{{ user.PhoneNumber }} - {{ user.Username }}</p>
          </div>
          <div class="address w1">
            <div class="address-detail w1">Tỉnh/Thành phố: {{ user.ProvinceName && user.ProvinceName != '' ? user.ProvinceName : '<Trống>'   }}</div>
            <div class="address-detail w1">Quận/Huyện: {{ user.DistrictName && user.DistrictName != '' ? user.DistrictName : '<Trống>'   }}</div>
            <div class="address-detail w1">Phường/Xã: {{ user.WardName && user.WardName != '' ? user.WardName : '<Trống>'   }}</div>
            <div class="address-detail w1">Địa chỉ: {{ user.Address && user.Address != '' ? user.Address : '<Trống>'   }}</div>
          </div>
          <div class="update-info">
            <div @click="updateInfoPopupShow = true">Cập nhật thông tin</div>
            <div @click="changePasswordPopupShow = true">Đổi mật khẩu</div>
          </div>
        </div>
        <button @click="logout" class="logout-button">Đăng xuất</button>
      </div>
    </div>
  </div>

  <AddUserPopup
    v-if="updateInfoPopupShow"
    :dataUpdate="user"
    @clickCancelBtn="updateInfoPopupShow = false"
    @reloadData="$emit('reloadData')"
    ref="addUserPopup"
    :isFromAccountInfo="true"
  />

  <ChangePasswordPopup 
    v-if="changePasswordPopupShow"
    @clickCancelBtn="changePasswordPopupShow = false"
    ref="changePasswordPopup"
    :userId="user.UserId"
    :username="user.Username"
  />
</template>

<script>
import AddUserPopup from './AddUserPopup.vue'
import ChangePasswordPopup from './ChangePasswordPopup.vue'

export default {
  name: 'AccountInfo',
  components: {
    AddUserPopup,
    ChangePasswordPopup
  },
  props: {
    isShowUserInfo: {
      type: Boolean,
      default: false
    },
    user: {
      type: Object,
      default: () => ({})
    }
  },
  data() {
    return {
      updateInfoPopupShow: false,
      changePasswordPopupShow: false
    }
  },
  methods: {
    logout() {
      this.$store.commit('logout')
      this.$router.push('/login')
    }
  }
}
</script>

<style scoped>
@import url(./account-info.css);
</style>