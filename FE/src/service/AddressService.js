import axios from "axios";

const addressAxios = axios.create({
  baseURL: "https://vapi.vnappmob.com/api",
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
    'Access-Control-Allow-Credentials': "true",
  },
  withCredentials: true, // gửi cookie, session lên server
});


class AuthServiceClass {
  constructor() {}

  async province() {
    const res = await addressAxios.get(
      "/province"
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
