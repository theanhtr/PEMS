const en = {
  common: {
    search: "Search",
    helpIconTooltip: "Help (F1)",
    closeIconTooltip: "Close (ESC)",
    gender: {
      male: "Male",
      female: "Female",
      other: "Other",
    },
    button: {
      edit: "Edit",
      no: "No",
      yes: "Yes",
      add: "Add",
      store: "Save",
      storeAndAdd: "Save and Add",
      close: "Close",
      cancel: "Cancel",
      continue: "Continue",
      rollBack: "Roll Back",
      dataInsert: "Insert Data",
      implicitSampling: "Implicit Sampling",
      allCategory: "All Categories",
    },
    buttonTooltip: {
      store: "Save (Ctrl + S)",
      storeAndAdd: "Save and Add (Ctrl + Shift + S)",
    },
  },

  validate: {
    nameFieldDefault: "Field",
    paramPassToFunctionError: "Invalid data passed to the function.",
    maxLengthError: "The maximum length of <%s> is %d characters.",
    minLengthError: "The minimum length of <%s> is %d characters.",
    emptyError: "%s cannot be empty.",
    dateNotGreaterThanToday: "%s cannot be greater than the current date.",
    formatError: "%s is not in the correct format.",
  },

  errorHandle: {
    serverError: {
      defaultError: "Network error, please try again later.",
    },

    importExcel: {
      selectSourceFile: {
        fileNotFound: "File not selected.",
        maxSizeError: "File is too large, cannot be imported.",
        formatError: "Invalid attachment, only .xlsx files are allowed.",
        headerIndexGreaterThanZero: "The header row must be greater than 0.",
        sheetNotFound: "Sheet not found.",
      },

      headerMerge: {
        headerRequiredNotMap:
          "Cannot continue data import if required columns are not mapped with corresponding columns in the Excel file. Please check again.",
      },
    },

    employeeSubsystem: {
      addEmployeePopup: {
        newCodeError: "Failed to get new code.",
        departmentNotFound: "Data for <Department> not found in the list.",
        dateNotGreaterThanToday:
          "{name} cannot be greater than the current date.",
      },

      notFoundEmployee: "Employee not found.",
    },
  },

  successHandle: {
    employeeSubsystem: {
      addEmployeePopup: {
        add: "Employee has been added.",
        update: "Employee information has been updated.",
      },

      exportExcel: "Successfully exported data to Excel file.",
      delete: "Successfully deleted employee <{code}>.",
      deleteMultiple: "Successfully deleted {count} employees.",
    },
  },

  component: {
    notFound: {
      title: "Page not found",
    },

    datePicker: {
      dayName: {
        monday: "Mon",
        tuesday: "Tue",
        wednesday: "Wed",
        thursday: "Thu",
        friday: "Fri",
        saturday: "Sat",
        sunday: "Sun",
      },
      monthYearLabel: "Month {month} Year {year}",
      cancelSelectMonthYear: "Cancel",
      today: "Today",
      todayTooltip: "Select today's date",

      monthRange: {
        january: "Jan",
        february: "Feb",
        march: "Mar",
        april: "Apr",
        may: "May",
        june: "Jun",
        july: "Jul",
        august: "Aug",
        september: "Sep",
        october: "Oct",
        november: "Nov",
        december: "Dec",
      },
    },

    paging: {
      total: "Total",
      record: "record",
      previous: "Previous",
      next: "Next",
      recordPerPage: "{count} records per page",
    },

    popup: {
      outConfirmPopupTitle: "Data has been changed. Do you want to save it?",
    },

    toast: {
      defaultTitle: {
        info: "Info!",
        warning: "Warning!",
        error: "Error!",
        success: "Success!",
      },
    },

    table: {
      valid: "Valid",
      invalid: "Invalid",
      noData: "No data",
      function: "Function",
      editData: {
        copy: "Copy",
        delete: "Delete",
        stopUsing: "Stop using",
      },
    },
  },

  sidebar: {
    tooltipExpandSidebar: "Expand sidebar",
    quickAddButton: "Quick Add",
    overview: "Overview",
    cash: "Cash",
    deposits: "Deposits",
    purchase: "Purchase",
    sell: "Sell",
    invoiceManagement: "Invoice Management",
    warehouse: "Warehouse",
    tool: "Tool",
    fixedAssets: "Fixed Assets",
    salary: "Salary",
    tax: "Tax",
    price: "Price",
    loans: "Loans",
    synthetic: "Synthetic",
    budget: "Budget",
    report: "Report",
    financialAnalysis: "Financial Analysis",
    category: "Category",
    initialBalance: "Initial Balance",
    knowledge: "Knowledge",
  },

  mainContent: {
    tooltipCollapseSidebar: "Collapse sidebar",
    headerDownload: "Download excel file",
    headerSearchPlaceholder: "Enter keywords to search",
    headerSettingTooltip: "Utilities and settings",
    headerMessageTooltip: "Exchange between DN and KTDV",
    headerHelpTooltip: "Help",
    headerNotificationTooltip: "Notifications",
    headerAccountInfo: "User Information",
  },

  importLayout: {
    headerTitle: "Import Data from Excel",
    step1Name: "SELECT SOURCE FILE",
    step2Name: "MERGE DATA",
    step3Name: "CHECK DATA",
    step4Name: "RESULT",
  },

  importExcel: {
    dataCheck: {
      employeeColumnsInfo: {
        lineNumber: "Line Number",
        statusQuo: "Status",
        errorDetail: "Error Details",
        employeeCode: "Employee Code",
        fullname: "Full Name",
        departmentCode: "Department Code",
        gender: "Gender",
        dateOfBirth: "Date of Birth",
        identityNumber: "Identity Number",
        identityDate: "Identity Date",
        identityPlace: "Identity Place",
        position: "Position",
        supplierCustomerGroup: "Supplier/Customer Group",
        payAccount: "Pay Account",
        receiveAccount: "Receive Account",
        salary: "Salary",
        salaryCoefficients: "Salary Coefficients",
        salaryPaidForInsurance: "Salary Paid for Insurance",
        personalTaxCode: "Personal Tax Code",
        typeOfContract: "Type of Contract",
        numberOfDependents: "Number of Dependents",
        accountNumber: "Account Number",
        bankName: "Bank Name",
        bankBranch: "Bank Branch",
        bankProvince: "Bank Province",
        contactAddress: "Contact Address",
        contactPhoneNumber: "Mobile Phone",
        contactLandlinePhoneNumber: "Landline Phone",
        contactEmail: "Email",
      },
      header: {
        validTitle: "{validCount} valid employee data rows",
        invalidTitle: "{invalidCount} invalid employee data rows",
        statusFilter: {
          title: "Status Filter",
          all: "All",
          valid: "Valid",
          invalid: "Invalid",
        },
      },
    },

    headerMerge: {
      employeeHeaderInfo: {
        employeeCode: {
          showName: "Employee Code",
          description: "Enter employee code",
        },
        fullname: {
          showName: "Full Name",
          description: "Enter full name",
        },
        departmentCode: {
          showName: "Department Code",
          description: "Enter department code",
        },
        gender: {
          showName: "Gender",
          description: "Male; Female; Other",
        },
        dateOfBirth: {
          showName: "Date of Birth",
          description: "",
        },
        identityNumber: {
          showName: "Identity Number",
          description: "Enter identity number",
        },
        identityDate: {
          showName: "Identity Date",
          description: "Enter identity date",
        },
        identityPlace: {
          showName: "Identity Place",
          description: "Enter identity place",
        },
        position: {
          showName: "Position",
          description: "",
        },
        supplierCustomerGroup: {
          showName: "Supplier/Customer Group",
          description: "Supplier/customer group",
        },
        payAccount: {
          showName: "Pay Account",
          description: "",
        },
        receiveAccount: {
          showName: "Receive Account",
          description: "",
        },
        salary: {
          showName: "Salary",
          description: "",
        },
        salaryCoefficients: {
          showName: "Salary Coefficients",
          description: "",
        },
        salaryPaidForInsurance: {
          showName: "Salary Paid for Insurance",
          description: "",
        },
        personalTaxCode: {
          showName: "Personal Tax Code",
          description: "",
        },
        typeOfContract: {
          showName: "Type of Contract",
          description: "",
        },
        numberOfDependents: {
          showName: "Number of Dependents",
          description: "",
        },
        accountNumber: {
          showName: "Account Number",
          description: "",
        },
        bankName: {
          showName: "Bank Name",
          description: "",
        },
        bankBranch: {
          showName: "Bank Branch",
          description: "",
        },
        bankProvince: {
          showName: "Bank Province",
          description: "",
        },
        contactAddress: {
          showName: "Contact Address",
          description: "",
        },
        contactPhoneNumber: {
          showName: "Mobile Phone",
          description: "",
        },
        contactLandlinePhoneNumber: {
          showName: "Landline Phone",
          description: "",
        },
        contactEmail: {
          showName: "Email",
          description: "",
        },
      },
      headerMergeColumnsInfo: {
        requiredInformation: "Required Information",
        softwareColumn: "Column in Software",
        dataFileColumn: "Column in Data File",
        description: "Detailed Description",
      },
      header: {
        title: "Merge columns in software with columns in data file",
      },
    },

    importResult: {
      header: {
        successTitle:
          "{recordSuccess}/{totalRecord} data rows imported successfully",
        failTitle: "{recordFail}/{totalRecord} data rows failed to import",
      },
    },

    selectSourceFile: {
      importMode: {
        add: "Add New",
        update: "Update",
      },
      selectExcelFile: "Select Excel File",
      maximumSize: "Maximum size is 2MB",
      downloadBasicTemplate: "Download Basic Template",
      downloadFullTemplate: "Download Full Template",
      sheetContainsData: "Sheet Contains Data",
      headerRowIndex: "Header row is row number",
      importModeTitle: "Import Mode",
      suggestionLabel: "Suggestion",
      moreInfoBasicTemplate: "to import basic information.",
      moreInfoFullTemplate: " to import all information.",
      nextUseInfo:
        "Settings during the import process will be saved for future use.",
      importModeAddDescription:
        "Data in the file that does not exist in the system will be added. Data that already exists in the system will not be updated.",
      importModeUpdateDescription:
        "Data in the file that does not exist in the system will be added. Data that already exists in the system will be updated.",
    },
  },

  employeeSubsystem: {
    pageTitle: "Employees | ttanh | Accounting",
    title: "Employees",
    employeeContent: {
      searchPlaceHolder: "Search",
      batchExecution: "Batch Execution",
      searchInputTooltip: "Search (Ctrl + Shift + F)",
      reloadTooltip: "Reload data",
      exportExcelTooltip: "Export to Excel",
      layoutSettingTooltip: "Customize Interface",
      insertTooltip: "Insert (Insert)",
      deletePopupTitle: "Do you really want to delete employee <{code}>?",
      deleteMultiplePopupTitle:
        "Do you really want to delete {count} employees?",
      batchExecutionData: {
        delete: "Delete",
      },
      addDataDropdown: {
        importExcel: "Import from Excel",
      },
    },

    layoutSetting: {
      headerTitle: "Customize Interface",
      columnsInfo: {
        display: "Display",
        dataColumnName: "Data Column Name",
        interfaceColumnName: "Column Name on Interface",
        width: "Width",
        fixedColumn: "Fixed Column",
      },
    },

    addEmployeePopup: {
      headerTitle: "Employee Information",
      isCustomer: "Is Customer",
      isSupplier: "Is Supplier",

      labelInput: {
        code: "Code",
        name: "Name",
        department: "Department",
        position: "Position",
        supplierCustomerGroup: "Supplier/Customer Group",
        dateOfBirth: "Date of Birth",
        gender: "Gender",
        identityNumber: "Identity Number",
        identityDate: "Identity Date",
        identityPlace: "Identity Place",
        receiveAccount: "Receive Account",
        payAccount: "Pay Account",
        salary: "Salary",
        salaryCoefficients: "Salary Coefficients",
        salaryPaidForInsurance: "Salary Paid for Insurance",
        personalTaxCode: "Personal Tax Code",
        typeOfContract: "Type of Contract",
        numberOfDependents: "Number of Dependents",
        accountNumber: "Account Number",
        bankName: "Bank Name",
        bankBranch: "Bank Branch",
        bankCity: "Bank City",
        contactAddress: "Contact Address",
        contactPhoneNumber: "Mobile Phone",
        contactLandlinePhoneNumber: "Landline Phone",
        contactEmail: "Email",
      },

      labelInputTooltip: {
        code: "Code",
        name: "Name",
        department: "Department",
        position: "Position",
        supplierCustomerGroup: "Supplier/Customer Group",
        dateOfBirth: "Date of Birth",
        gender: "Gender",
        identityNumber: "Identity Number",
        identityDate: "Identity Date",
        identityPlace: "Identity Place",
        receiveAccount: "Receive Account",
        payAccount: "Pay Account",
        salary: "Salary",
        salaryCoefficients: "Salary Coefficients",
        salaryPaidForInsurance: "Salary Paid for Insurance",
        personalTaxCode: "Personal Tax Code",
        typeOfContract: "Type of Contract",
        numberOfDependents: "Number of Dependents",
        accountNumber: "Account Number",
        bankName: "Bank Name",
        bankBranch: "Bank Branch",
        bankCity: "Bank City",
        contactAddress: "Contact Address",
        contactPhoneNumber: "Mobile Phone",
        contactLandlinePhoneNumber: "Landline Phone",
        contactEmail: "Email",
      },

      moreInfoNav: {
        salaryInfo: "Salary Information",
        bankInfo: "Bank Information",
        contactInfo: "Contact Information",
      },

      departmentColumnsInfo: {
        code: "Department Code",
        name: "Department Name",
      },

      nameField: {
        employeeId: "Employee",
        employeeCode: "Employee Code",
        fullName: "Full Name",
        gender: "Gender",
        dateOfBirth: "Date of Birth",

        identityNumber: "Identity Number",
        identityDate: "Identity Date",
        identityPlace: "Identity Place",

        departmentId: "Department",
        departmentCode: "Department Code",
        departmentName: "Department Name",

        supplierCustomerGroup: "Supplier/Customer Group",
        payAccount: "Pay Account",
        receiveAccount: "Receive Account",

        position: "Position",

        /* Salary Information */
        salary: "Salary",
        salaryPaidForInsurance: "Salary Paid for Insurance",
        salaryCoefficients: "Salary Coefficients",

        personalTaxCode: "Personal Tax Code",
        typeOfContract: "Type of Contract",
        numberOfDependents: "Number of Dependents",

        /* Bank Account */
        accountNumber: "Bank Account Number",
        bankName: "Bank Name",
        bankBranch: "Bank Branch",
        bankProvince: "Bank Province",

        /* Contact Information */
        contactAddress: "Contact Address",
        contactEmail: "Email",
        contactPhoneNumber: "Mobile Phone",
        contactLandlinePhoneNumber: "Landline Phone",
      },
    },
  },
};
export default en;
