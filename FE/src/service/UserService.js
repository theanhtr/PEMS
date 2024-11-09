import BaseService from './BaseService.js'
import TTANHEnum from '../enum/index.js'

class UserServiceClass extends BaseService {
  constructor() {
    super(TTANHEnum.API_SERVER_KEY.USER)
  }
}

const UserService = new UserServiceClass()
export default UserService
