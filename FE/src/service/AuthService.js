import BaseService from './BaseService.js';
import TTANHEnum from '../enum/index.js';

class AuthServiceClass extends BaseService {
  constructor() {
    super(TTANHEnum.API_SERVER_KEY.AUTH)
  }
}

const AuthService = new AuthServiceClass()
export default AuthService
