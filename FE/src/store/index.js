import { createStore } from "vuex";
import { findIndexByAttribute, generateUuid } from "@/helper/common.js";
import { ProjectConfig } from "../config/config";
import TTANHAxios from "@/axios";

function getLangCodeFromLocalStorage() {
  let langCode = localStorage.getItem("langCode");

  if (!langCode) {
    localStorage.setItem("langCode", "vi");

    langCode = localStorage.getItem("langCode");
  }

  return langCode;
}

export default createStore({
  state: {
    isLoggedIn: false,
    isExpandSidebar: true,
    langCode: getLangCodeFromLocalStorage(),

    /**
     * mỗi 1 toast sẽ có định dạng
     * {
     *  id: "",
     *  type: "",
     *  text: ""
     * }
     */
    toasts: [],

    /**
     * có 2 định dạng:
     *  + dd/MM/yyyy
     *  + MM/dd/yyyy
     */
    formatDate: ProjectConfig.FormatDate,
  },
  getters: {},
  mutations: {
    async setUserLogin(state, loginRes) {
      await localStorage.setItem("userToken", loginRes.token.token);
      await localStorage.setItem("userTokenExpirationTime", loginRes.token.expirationTime);
      await localStorage.setItem("roleId", loginRes.RoleID);

      state.isLoggedIn = true;
      TTANHAxios.defaults.headers.Authorization = `Bearer ${localStorage.getItem("userToken")}`;
      TTANHAxios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem("userToken")}`;
    },
    logout(state) {
      localStorage.removeItem("userToken");
      localStorage.removeItem("userTokenExpirationTime");
      localStorage.removeItem("roleId");
      localStorage.removeItem("provinceId");
      localStorage.removeItem("districtId");
      localStorage.removeItem("wardId");
      localStorage.removeItem("provinceName");
      localStorage.removeItem("districtName");
      localStorage.removeItem("wardName");

      state.isLoggedIn = false;
    },
    setLoginStatus(state, status) {
      state.isLoggedIn = status;
    },
    /**
     * hàm thay đổi trạng thái ẩn hiện của sidebar
     * @author: TTANH (02/07/2024)
     * @param {*} state
     */
    toggleSidebar(state) {
      state.isExpandSidebar = !state.isExpandSidebar;
    },

    /**
     * set ngôn ngữ cho app
     * @author: TTANH (05/08/2024)
     * @param {*} state
     * @param {string} newLangCode mã ngôn ngữ: vi, en
     */
    setLangCode(state, newLangCode) {
      if (state.langCode !== newLangCode) {
        localStorage.setItem("langCode", newLangCode);
        location.reload();
      }
    },

    /**
     * thêm 1 toast mới
     * @author: TTANH (02/07/2024)
     * @param {*} state
     * @param {string} type loại toast
     * @param {string} text thông tin trong toast
     */
    addToast(state, { type, text }) {
      try {
        const id = generateUuid();

        state.toasts.push({
          id,
          type,
          text,
        });

        setTimeout(() => {
          let index = findIndexByAttribute(state.toasts, "id", id);

          if (index !== -1) {
            state.toasts.splice(index, 1);
          }
        }, 5000);
      } catch (error) {
        console.log("🚀 ~ file: index.js:40 ~ addToast ~ error:", error);
      }
    },

    /**
     * xóa 1 toast
     * @author: TTANH (02/07/2024)
     * @param {*} state
     * @param {*} id id của toast
     */
    removeToast(state, { id }) {
      try {
        let index = findIndexByAttribute(state.toasts, "id", id);

        if (index !== -1) {
          state.toasts.splice(index, 1);
        }
      } catch (error) {
        console.log("🚀 ~ file: index.js:65 ~ removeToast ~ error:", error);
      }
    },
  },
  actions: {
    checkSession({ commit }) {
      let userTokenExpirationTime = localStorage.getItem("userTokenExpirationTime");
      let userToken = localStorage.getItem("userToken");

      if (userTokenExpirationTime && userToken) {
        let currentTime = new Date().getTime();
        let expirationTime = new Date(userTokenExpirationTime).getTime();

        if (currentTime < expirationTime) {
          commit("setLoginStatus", true);
        } else {
          commit("setLoginStatus", false);
          localStorage.removeItem("userTokenExpirationTime");
          localStorage.removeItem("userToken");
        }
      } else {
        localStorage.removeItem("userTokenExpirationTime");
        localStorage.removeItem("userToken");
      }
    }
  },
  modules: {},
});
