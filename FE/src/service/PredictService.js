import BaseService from "./BaseService.js";
import TTANHEnum from '../enum/index.js';

class PredictServiceClass extends BaseService {
  constructor() {
    super(TTANHEnum.API_SERVER_KEY.PREDICT);
  }
}

const PredictService = new PredictServiceClass();
export default PredictService;
