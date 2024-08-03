import TTANHAxios from "@/axios";

class BaseService {
  constructor(baseUrl) {
    this.baseUrl = baseUrl;
    this.baseAxios = TTANHAxios;
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
  async get() {
    const res = await this.baseAxios.get(this.baseUrl);
    return res;
  }

  /**
   * * thực hiện thêm mới dữ liệu
   * @author: TTANH (01/07/2024)
   * @param {Object} dataAdd dữ liệu cần thêm
   */
  async post(dataAdd) {
    const res = await this.baseAxios.post(this.baseUrl, dataAdd);
    return res;
  }

  /**
   * * thực hiện cập nhật dữ liệu của bản ghi
   * @author: TTANH (01/07/2024)
   * @param {string} id id của bản ghi
   * @param {Object} dataUpdate dữ liệu cần thêm
   */
  async put(id, dataUpdate) {
    const res = await this.baseAxios.put(this.baseUrl + `/${id}`, dataUpdate);
    return res;
  }

  /**
   * xóa 1 bản ghi
   * @author: TTANH (02/07/2024)
   * @param {string} id id của bản ghi
   */
  async delete(id) {
    const res = await this.baseAxios.delete(this.baseUrl + `/${id}`);
    return res;
  }

  /**
   * xóa nhiều bản ghi
   * @author: TTANH (17/07/2024)
   * @param {Array} ids mảng chứa các id của bản ghi
   */
  async deleteMultiple(ids) {
    const res = await this.baseAxios.delete(this.baseUrl, { data: ids });
    return res;
  }

  /**
   * lấy dữ liệu bằng phân trang và filter
   * @author: TTANH (03/07/2024)
   */
  async filter(dataFilter) {
    const res = await this.baseAxios.get(this.endpoint("/filter"), {
      params: dataFilter,
    });

    return res;
  }
}

export default BaseService;
