import BaseService from "./BaseService.js";

class PredictServiceClass extends BaseService {
  constructor() {
    super("Predicts");
  }
}

const PredictService = new PredictServiceClass();
export default PredictService;
