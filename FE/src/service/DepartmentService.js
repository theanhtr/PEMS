import BaseService from "./BaseService";

class DepartmentServiceClass extends BaseService {
  constructor() {
    super("Departments");
  }
}

const DepartmentService = new DepartmentServiceClass();
export default DepartmentService;
