import TTANHResource from "../../../../resource/resource.js";
import store from "../../../../store";

export var headersInfo = [
  {
    id: "EmployeeCode",
    serverName: "EmployeeCode",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.employeeCode.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: true,
    isCannotChangeChecked: true, //những trường bắt buộc phải thêm
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.employeeCode.description,
  },
  {
    id: "FullName",
    serverName: "FullName",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.fullname.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: true,
    isCannotChangeChecked: true,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.fullname.description,
  },
  {
    id: "DepartmentCode",
    serverName: "DepartmentCode",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.departmentCode.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: true,
    isCannotChangeChecked: true,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.departmentCode.description,
  },
  {
    id: "Gender",
    serverName: "Gender",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.gender.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.gender.description,
  },
  {
    id: "DateOfBirth",
    serverName: "DateOfBirth",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.dateOfBirth.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.dateOfBirth.description,
  },
  {
    id: "IdentityNumber",
    serverName: "IdentityNumber",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.identityNumber.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.identityNumber.description,
  },
  {
    id: "IdentityDate",
    serverName: "IdentityDate",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.identityDate.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.identityDate.description,
  },
  {
    id: "IdentityPlace",
    serverName: "IdentityPlace",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.identityPlace.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.identityPlace.description,
  },
  {
    id: "Position",
    serverName: "Position",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.position.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.position.description,
  },
  {
    id: "SupplierCustomerGroup",
    serverName: "SupplierCustomerGroup",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.supplierCustomerGroup.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.supplierCustomerGroup.description,
  },
  {
    id: "PayAccount",
    serverName: "PayAccount",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.payAccount.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.payAccount.description,
  },
  {
    id: "ReceiveAccount",
    serverName: "ReceiveAccount",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.receiveAccount.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.receiveAccount.description,
  },
  {
    id: "Salary",
    serverName: "Salary",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.salary.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.salary.description,
  },
  {
    id: "SalaryCoefficients",
    serverName: "SalaryCoefficients",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.salaryCoefficients.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.salaryCoefficients.description,
  },
  {
    id: "SalaryPaidForInsurance",
    serverName: "SalaryPaidForInsurance",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.salaryPaidForInsurance.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.salaryPaidForInsurance.description,
  },
  {
    id: "PersonalTaxCode",
    serverName: "PersonalTaxCode",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.personalTaxCode.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.personalTaxCode.description,
  },
  {
    id: "TypeOfContract",
    serverName: "TypeOfContract",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.typeOfContract.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.typeOfContract.description,
  },
  {
    id: "NumberOfDependents",
    serverName: "NumberOfDependents",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.numberOfDependents.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.numberOfDependents.description,
  },
  {
    id: "AccountNumber",
    serverName: "AccountNumber",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.accountNumber.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.accountNumber.description,
  },
  {
    id: "BankName",
    serverName: "BankName",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.bankName.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.bankName.description,
  },
  {
    id: "BankBranch",
    serverName: "BankBranch",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.bankBranch.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.bankBranch.description,
  },
  {
    id: "BankProvince",
    serverName: "BankProvince",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.bankProvince.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.bankProvince.description,
  },
  {
    id: "ContactAddress",
    serverName: "ContactAddress",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.contactAddress.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.contactAddress.description,
  },
  {
    id: "ContactPhoneNumber",
    serverName: "ContactPhoneNumber",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.contactPhoneNumber.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.contactPhoneNumber.description,
  },
  {
    id: "ContactLandlinePhoneNumber",
    serverName: "ContactLandlinePhoneNumber",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.contactLandlinePhoneNumber.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.contactLandlinePhoneNumber.description,
  },
  {
    id: "ContactEmail",
    serverName: "ContactEmail",
    showName:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.contactEmail.showName,
    excelColumnIndex: 0,
    isShow: true,
    isRequired: false,
    isCannotChangeChecked: false,
    description:
      TTANHResource[store.state.langCode].importExcel.headerMerge
        .employeeHeaderInfo.contactEmail.description,
  },
];
