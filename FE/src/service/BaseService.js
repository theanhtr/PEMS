import TTANHAxios from "@/axios";
import { ProjectConfig } from '../config/config.js';
import TTANHEnum from "../enum";

class BaseService {
  constructor(apiServerKey) {
    this.baseUrl = this.getUrlApiService(apiServerKey);
    this.baseAxios = TTANHAxios;
  }

  getUrlApiService(apiServerKey) {
    switch (apiServerKey) {
      case TTANHEnum.API_SERVER_KEY.AUTH:
        return ProjectConfig.AuthenApiUrl;
      case TTANHEnum.API_SERVER_KEY.USER:
        return ProjectConfig.UserApiUrl;
      case TTANHEnum.API_SERVER_KEY.PREDICT:
        return ProjectConfig.PredictApiUrl;
      case TTANHEnum.API_SERVER_KEY.REPORT:
        return ProjectConfig.ReportApiUrl;
      default:
        return "";
    }
  }

  /**
   * thực hiện với 1 endpoint riêng
   * @author: TTANH (10/07/2024)
   * @param {string} url đường dẫn riêng
   */
  endpoint(url) {
    return this.baseUrl + url;
  }

  /**
   * thực hiện lấy dữ dữ liệu
   * @author: TTANH (01/07/2024)
   */
  async get(url) {
    const res = await this.baseAxios.get(this.endpoint(url));
    return res;
  }

  /**
   * * thực hiện thêm mới dữ liệu
   * @author: TTANH (01/07/2024)
   * @param {Object} dataAdd dữ liệu cần thêm
   */
  async post(url, dataAdd) {
    const res = await this.baseAxios.post(this.endpoint(url), dataAdd);
    return res;
  }

  /**
   * * thực hiện cập nhật dữ liệu của bản ghi
   * @author: TTANH (01/07/2024)
   * @param {string} id id của bản ghi
   * @param {Object} dataUpdate dữ liệu cần thêm
   */
  async put(url, id, dataUpdate) {
    const res = await this.baseAxios.put(this.endpoint(url) + `/${id}`, dataUpdate);
    return res;
  }

  /**
   * xóa 1 bản ghi
   * @author: TTANH (02/07/2024)
   * @param {string} id id của bản ghi
   */
  async delete(url, id) {
    const res = await this.baseAxios.delete(this.endpoint(url) + `/${id}`);
    return res;
  }

  /**
   * xóa nhiều bản ghi
   * @author: TTANH (17/07/2024)
   * @param {Array} ids mảng chứa các id của bản ghi
   */
  async deleteMultiple(url, ids) {
    const res = await this.baseAxios.delete(this.endpoint(url), { data: ids });
    return res;
  }

  /**
   * lấy dữ liệu bằng phân trang và filter
   * @author: TTANH (03/07/2024)
   */
  async filter(url, dataFilter) {
    const res = await this.baseAxios.get(this.endpoint(url + "/filter"), {
      params: dataFilter,
    });

    return res;
  }

  

  /**
   * lấy dữ liệu bằng phân trang và filter
   * lọc nâng cao với bộ lọc phức tạp
   * @author: TTANH (03/07/2024)
   */
  async filterAdvanced(url, dataFilter) {
    const res = await this.baseAxios.post(this.endpoint(url + "/filter"), dataFilter);

    return res;
  }
}

export default BaseService;
