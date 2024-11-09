<template>
  <div class="register-form-container">
    <h1>iFAWcast</h1>
    <div class="register-form">
      <h2>Đăng ký</h2>
      <p class="register-form-error-message">{{ errorMessage }}</p>
      <div class="form-group">
        <label for="fullname">Họ và tên</label>
        <input class="register-input" type="email" id="fullname" v-model="fullname" required />
      </div>
      <div class="form-group">
        <label for="userName">Tên đăng nhập</label>
        <input class="register-input" type="email" id="userName" v-model="userName" required />
      </div>
      <div class="form-group">
        <label for="password">Mật khẩu</label>
        <input
          class="register-input"
          :type="showPassword ? 'text' : 'password'"
          id="password"
          v-model="password"
          required
        />
        <span class="show-password pointer" @click="showPassword = !showPassword">
          <ttanh-icon v-if="!showPassword" icon="eye" />
          <ttanh-icon v-else icon="eye-slash" />
        </span>
      </div>
      <div class="form-group">
        <label for="repassword">Nhập lại mật khẩu</label>
        <input
          class="register-input"
          :type="showRePassword ? 'text' : 'password'"
          id="repassword"
          v-model="repassword"
          required
        />
        <span class="show-password pointer" @click="showRePassword = !showRePassword">
          <ttanh-icon v-if="!showRePassword" icon="eye" />
          <ttanh-icon v-else icon="eye-slash" />
        </span>
      </div>
      <button @click="register" class="register-button">Đăng ký</button>
      <div class="flex login-line">
        Nếu đã có tài khoản vui lòng
        <router-link to="/login" class="login-link"><div>Đăng nhập</div></router-link>
      </div>
    </div>
  </div>
  <ttanh-loading-spinner size="large" v-if="isLoading" />
</template>

<script>
import AuthService from '@/service/AuthService.js'

export default {
  name: 'register',

  components: {},

  data() {
    return {
      userName: '',
      password: '',
      repassword: '',
      fullname: '',
      isLoading: false,
      showPassword: false,
      showRePassword: false,
      errorMessage: ''
    }
  },

  methods: {
    /**
     * register
     * @author: TTANH (24/07/2024)
     */
    async register() {
      try {
        if (this.fullname === '') {
          this.errorMessage = 'Vui lòng nhập Họ và tên'
          return
        }

        if (this.userName === '') {
          this.errorMessage = 'Vui lòng nhập Tên tài khoản'
          return
        }

        if (this.password === '') {
          this.errorMessage = 'Vui lòng nhập Mật khẩu'
          return
        }

        if (this.repassword === '' || this.repassword !== this.password) {
          this.errorMessage = 'Vui lòng nhập Mật khẩu xác nhận trùng khớp Mật khẩu'
          return
        }

        this.isLoading = true

        this.errorMessage = ''
        let registerParams = {
          Username: this.userName,
          Password: this.password,
          Fullname: this.fullname
        }

        const res = await AuthService.post('Auth/register', registerParams)

        if (res.success) {
          this.$router.push('/login')
          this.$store.commit('addToast', {
            type: 'success',
            text: 'Đăng ký thành công'
          })
        } else {
          this.$store.commit('addToast', {
            type: 'error',
            text: res.userMsg
          })
        }

        this.isLoading = false

        return res
      } catch (error) {
        console.log('🚀 ~ file: registerScreen.vue:52 ~ register ~ error:', error)
      }
    }
  }
}
</script>

<style scoped>
.register-form-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100vh;
  background-color: #f5f5f5;
}

.register-form {
  background-color: #fff;
  padding: 30px;
  border-radius: 5px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 30%;
}

h1 {
  font-size: 2.5rem;
  margin-bottom: 20px;
  color: #333;
}

h2 {
  font-size: 1.5rem;
  margin-bottom: 10px;
  color: #333;
}

.form-group {
  margin-top: 12px;
  width: 100%;
  position: relative;
}

label {
  display: block;
  margin-bottom: 5px;
  font-weight: bold;
}

.register-input {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 3px;
  box-sizing: border-box;
}

.show-password {
  position: absolute;
  bottom: 10%;
  right: 2%;
  cursor: pointer;
  width: fit-content;
  height: fit-content;
}

button {
  background-color: #4caf50;
  color: white;
  padding: 12px 20px;
  border: none;
  border-radius: 3px;
  cursor: pointer;
  width: 100%;
}

.register-form-error-message {
  color: red;
  font-size: 14px;
}

.register-button {
  margin-top: 20px;
}

.login-line {
  margin-top: 12px;
  column-gap: 8px;
  font-size: 14px;
}

.login-link {
  color: var(--primary-color);
  text-decoration: underline;
}
</style>
