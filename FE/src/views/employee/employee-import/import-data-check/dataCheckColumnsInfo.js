import TTANHResource from "../../../../resource/resource.js";
import store from "../../../../store/index.js";

export var dataCheckColumnsInfo = [
  {
    id: "ExcelRowIndex",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.lineNumber,
    size: 100,
    textAlign: "center",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "ValidCheck",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.statusQuo,
    size: 150,
    textAlign: "left",
    format: "valid-check",
    isShow: true,
    isPin: false,
  },
  {
    id: "ValidateDescription",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.errorDetail,
    size: 350,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "EmployeeCode",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.employeeCode,
    size: 150,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "FullName",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.fullname,
    size: 250,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "DepartmentCode",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.departmentCode,
    size: 150,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "Gender",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.gender,
    size: 120,
    textAlign: "left",
    format: "gender",
    isShow: true,
    isPin: false,
  },
  {
    id: "DateOfBirth",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.dateOfBirth,
    size: 150,
    textAlign: "center",
    format: "date",
    isShow: true,
    isPin: false,
  },
  {
    id: "IdentityNumber",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.identityNumber,
    size: 200,
    textAlign: "right",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "IdentityDate",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.identityDate,
    size: 150,
    textAlign: "center",
    format: "date",
    isShow: true,
    isPin: false,
  },
  {
    id: "IdentityPlace",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.identityPlace,
    size: 150,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "Position",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.position,
    size: 250,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "SupplierCustomerGroup",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.supplierCustomerGroup,
    size: 200,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "PayAccount",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.payAccount,
    size: 200,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "ReceiveAccount",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.receiveAccount,
    size: 200,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "Salary",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.salary,
    size: 200,
    textAlign: "right",
    format: "currency",
    isShow: true,
    isPin: false,
  },
  {
    id: "SalaryCoefficients",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.salaryCoefficients,
    size: 200,
    textAlign: "right",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "SalaryPaidForInsurance",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.salaryPaidForInsurance,
    size: 200,
    textAlign: "right",
    format: "currency",
    isShow: true,
    isPin: false,
  },
  {
    id: "PersonalTaxCode",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.personalTaxCode,
    size: 150,
    textAlign: "right",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "TypeOfContract",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.typeOfContract,
    size: 150,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "NumberOfDependents",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.numberOfDependents,
    size: 200,
    textAlign: "right",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "AccountNumber",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.accountNumber,
    size: 150,
    textAlign: "right",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "BankName",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.bankName,
    size: 250,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "BankBranch",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.bankBranch,
    size: 250,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "BankProvince",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.bankProvince,
    size: 180,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "ContactAddress",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.contactAddress,
    size: 200,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "ContactPhoneNumber",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.contactPhoneNumber,
    size: 150,
    textAlign: "right",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "ContactLandlinePhoneNumber",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.contactLandlinePhoneNumber,
    size: 150,
    textAlign: "right",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "ContactEmail",
    name: TTANHResource[store.state.langCode].importExcel.dataCheck
      .employeeColumnsInfo.contactEmail,
    size: 200,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
];
