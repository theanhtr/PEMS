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
      "/province/district/" + (provinceId < 10 && provinceId[0] != '0' ? "0" + provinceId : provinceId)
    );

    return res;
  }
  
  async ward(districtId) {
    const res = await addressAxios.get(
      "/province/ward/" + (districtId < 10 && districtId[0] != '0' ? "0" + districtId : districtId)
    );

    return res;
  }
}

const AuthService = new AuthServiceClass();
export default AuthService;
