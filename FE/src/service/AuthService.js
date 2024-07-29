import BaseService from "./BaseService.js";

class AuthServiceClass extends BaseService {
  constructor() {
    super("Auth");
  }

  async login(params) {
    const res = await this.baseAxios.post(
      this.endpoint("/login"),
      params
    );

    return res;
  }
}

const AuthService = new AuthServiceClass();
export default AuthService;
