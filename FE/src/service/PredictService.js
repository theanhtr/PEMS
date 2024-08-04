import BaseService from "./BaseService.js";

class PredictServiceClass extends BaseService {
  constructor() {
    super("Predicts");
  }

  /**
   * lấy dữ liệu bằng phân trang và filter
   * @author: TTANH (03/07/2024)
   */
  async filter(dataFilter) {
    const res = await this.baseAxios.post(this.endpoint("/filter"), dataFilter);

    return res;
  }
}

const PredictService = new PredictServiceClass();
export default PredictService;
