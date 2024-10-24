import BaseService from './BaseService.js'
import TTANHEnum from '../enum/index.js'

class ReportServiceClass extends BaseService {
  constructor() {
    super(TTANHEnum.API_SERVER_KEY.USER)
  }
}

const ReportService = new ReportServiceClass()
export default ReportService