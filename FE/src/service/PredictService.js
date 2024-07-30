import BaseService from "./BaseService.js";

class PredictServiceClass extends BaseService {
  constructor() {
    super("Predict");
  }
}

const PredictService = new PredictServiceClass();
export default PredictService;
