<template>
  <div class="login-form-container">
    <h1>iFAWcast</h1>
    <div class="login-form">
      <h2>Đăng nhập</h2>
      <p class="login-form-error-message">{{ errorMessage }}</p>
      <div class="form-group">
        <label for="userName">Tên người dùng</label>
        <input class="login-input" type="email" id="userName" v-model="userName" required>
      </div>
      <div class="form-group">
        <label for="password">Mật khẩu</label>
        <input class="login-input" :type="showPassword ? 'text' : 'password'" id="password" v-model="password" required>
        <span class="show-password pointer" @click="showPassword = !showPassword">
          <ttanh-icon
            v-if="!showPassword"
            icon="eye"
          />
          <ttanh-icon
            v-else
            icon="eye-slash"
          />
        </span>
      </div>
      <button @click="login" class="login-button">Đăng nhập</button>
    </div>
  </div>
  <ttanh-loading-spinner size="large" v-if="isLoading" />
</template>

<script>
import AuthService from "@/service/AuthService.js";

export default {
  name: "Login",

  components: {},

  data() {
    return {
      userName: "",
      password: "",
      isLoading: false,
      showPassword: false,
      errorMessage: "",
    };
  },

  methods: {
    /**
     * Login
     * @author: TTANH (24/07/2024)
     */
    async login() {
      try {
        this.isLoading = true;

        if (this.userName === "") {
          this.errorMessage = "Vui lòng nhập tên người dùng";
          this.isLoading = false;
          return;
        }

        if (this.password === "") {
          this.errorMessage = "Vui lòng nhập mật khẩu";
          this.isLoading = false;
          return;
        }

        this.errorMessage = "";
        let loginParams = {
          Username: this.userName,
          Password: this.password,
        };

        const res = await AuthService.login(loginParams);

        if (res.success) {
          this.$store.commit("setUserLogin", res.data);
          this.$router.push("/");
          this.$store.commit("addToast", {
            type: "success",
            text: "Đăng nhập thành công",
          });
        } else {
          this.$store.commit("addToast", {
            type: "error",
            text: res.userMsg,
          });
        }

        this.isLoading = false;

        return res;
      } catch (error) {
        console.log("🚀 ~ file: LoginScreen.vue:52 ~ login ~ error:", error);
      }
    }
  }
};
</script>


<style scoped>
.login-form-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100vh;
  background-color: #f5f5f5;
}

.login-form {
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

.login-input {
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
  background-color: #4CAF50;
  color: white;
  padding: 12px 20px;
  border: none;
  border-radius: 3px;
  cursor: pointer;
  width: 100%;
}

.login-form-error-message {
  color: red;
  font-size: 14px;
}

.login-button {
  margin-top: 20px;
}

</style>