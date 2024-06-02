import BaseService from "./BaseService.js";

class EmployeeServiceClass extends BaseService {
  constructor() {
    super("Employees");
  }

  /**
   * lấy 1 code mới từ server
   * @author: TTANH (01/07/2023)
   */
  async getNewCode() {
    const res = await this.baseAxios.get(this.endpoint("/new-code"));
    return res;
  }

  /**
   * lấy dữ liệu bằng phân trang và filter
   * @author: TTANH (03/07/2023)
   */
  async filter(dataFilter) {
    const res = await this.baseAxios.get(this.endpoint("/filter"), {
      params: dataFilter,
    });

    return res;
  }

  /**
   * xuất file excel
   * @author: TTANH (16/07/2023)
   */
  async getExcel(data) {
    const res = await this.baseAxios.get(this.endpoint("/excel"), {
      params: data,
      responseType: "blob",
    });

    return res;
  }

  /**
   * Tải mẫu cơ bản excel cơ bản của nhân viên
   * @author: TTANH (26/07/2023)
   */
  async downloadBasicTemplate() {
    const res = await this.baseAxios.get(this.endpoint("/excel/template"), {
      responseType: "blob",
    });

    return res;
  }

  /**
   * Tải mẫu excel đầy đủ của nhân viên
   * @author: TTANH (26/07/2023)
   */
  async downloadFullTemplate() {
    const res = await this.baseAxios.get(
      this.endpoint("/excel/template-full"),
      {
        responseType: "blob",
      }
    );

    return res;
  }

  /**
   * nhập file excel để server xử lý
   * @author: TTANH (23/07/2023)
   * @param {File} file file excel gửi lên server
   */
  async postExcel(file) {
    const formData = new FormData();
    formData.append("excelFile", file);

    const res = await this.baseAxios.post(this.endpoint("/excel"), formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });

    return res;
  }

  /**
   * hàm gọi api để thêm các cài đặt cho file excel
   * @author: TTANH (23/07/2023)
   * @param {Object} data
   */
  async addSettingExcel(data) {
    const res = await this.baseAxios.post(
      this.endpoint("/excel/add-setting"),
      data
    );

    return res;
  }

  /**
   * Hàm thêm các cài đặt cho việc ánh xạ từ header trong excel
   * @author: TTANH (24/07/2023)
   * @param {*} data
   */
  async headerMapColumnExcel(data) {
    const res = await this.baseAxios.post(
      this.endpoint("/excel/header-map-column"),
      data
    );

    return res;
  }

  /**
   * Api kiểm tra dữ liệu trong file excel
   * @param {object} dataFilter lấy dữ liệu bằng phân trang và loại lọc
   * @author: TTANH (24/07/2023)
   */
  async checkDataExcelFilter(dataFilter) {
    const res = await this.baseAxios.get(
      this.endpoint("/excel/check-data-filter"),
      {
        params: dataFilter,
      }
    );

    return res;
  }

  /**
   * Api xác nhận import dữ liệu từ file
   * @author: TTANH (26/07/2023)
   */
  async confirmImportExcelFile() {
    const res = await this.baseAxios.post(
      this.endpoint("/excel/confirm-import-excel-file")
    );

    return res;
  }
}

const EmployeeService = new EmployeeServiceClass();
export default EmployeeService;
