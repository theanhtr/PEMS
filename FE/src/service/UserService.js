import BaseService from "./BaseService.js";

class UserServiceClass extends BaseService {
  constructor() {
    super("User");
  }
}

const UserService = new UserServiceClass();
export default UserService;
