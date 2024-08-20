import BaseService from './BaseService.js'

class UserServiceClass extends BaseService {
  constructor() {
    super('User')
  }

  getMyinfo() {
    return this.baseAxios.get(this.endpoint('/myinfo'))
  }
}

const UserService = new UserServiceClass()
export default UserService
