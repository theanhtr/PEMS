import BaseService from "./BaseService";

class EmployeeLayoutServiceClass extends BaseService {
  constructor() {
    super("EmployeeLayouts");
  }

  /**
   * hàm cập nhật nhiều bản ghi layout nhân viên một lúc
   * @author: TTANH (03/08/2023)
   * @param {Array} employeeLayoutUpdates mảng chứa những dữ liệu cần update
   */
  async updateMultiple(employeeLayoutUpdates) {
    const res = await this.baseAxios.put(
      this.endpoint("/update-multiple"),
      employeeLayoutUpdates
    );
    return res;
  }

  /**
   * Thực hiện lấy lại mẫu mặc định của layout
   * @author: TTANH (04/08/2023)
   */
  async setDefaultLayout() {
    const res = await this.baseAxios.post(this.endpoint("/set-default"));
    return res;
  }
}

const EmployeeLayoutService = new EmployeeLayoutServiceClass();
export default EmployeeLayoutService;
