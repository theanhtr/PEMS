import axios from "axios";

const targetLink = 'https://vapi.vnappmob.com/api';

const addressAxios = axios.create({
  baseURL: targetLink,
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
  },
  withCredentials: false, // gửi cookie, session lên server
});


class AuthServiceClass {
  constructor() {}

  async province() {
    const res = await addressAxios.get(
      "/province/"
    );

    return res;
  }

  async district(provinceId) {
    const res = await addressAxios.get(
      "/province/district/" + provinceId
    );

    return res;
  }
  
  async ward(districtId) {
    const res = await addressAxios.get(
      "/province/ward/" + districtId
    );

    return res;
  }
}

const AuthService = new AuthServiceClass();
export default AuthService;
