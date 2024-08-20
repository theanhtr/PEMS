const vi = {
  common: {
    search: 'Tìm kiếm',
    helpIconTooltip: 'Giúp (F1)',
    closeIconTooltip: 'Đóng (ESC)',
    gender: {
      male: 'Nam',
      female: 'Nữ',
      other: 'Khác'
    },
    button: {
      edit: 'Sửa',
      no: 'Không',
      yes: 'Có',
      add: 'Thêm',
      store: 'Cất',
      storeAndAdd: 'Cất và thêm',
      close: 'Đóng',
      cancel: 'Hủy',
      continue: 'Tiếp tục',
      rollBack: 'Quay lại',
      dataInsert: 'Nhập dữ liệu',
      implicitSampling: 'Lấy mẫu ngầm định',
      allCategory: 'Tất cả danh mục'
    },
    buttonTooltip: {
      store: 'Cất (Ctrl + S)',
      storeAndAdd: 'Cất và thêm (Ctrl + Shift + S)'
    }
  },

  validate: {
    nameFieldDefault: 'Trường',
    paramPassToFunctionError: 'Dữ liệu truyền vào hàm không hợp lệ.',
    maxLengthError: 'Độ dài tối đa của <%s> là %d kí tự.',
    minLengthError: 'Độ dài tối thiểu của <%s> là %d kí tự.',
    emptyError: '%s không được phép để trống.',
    dateNotGreaterThanToday: '%s không được lớn hơn ngày hiện tại.',
    formatError: '%s không đúng định dạng.'
  },

  errorHandle: {
    serverError: {
      defaultError: 'Lỗi đường truyền, vui lòng thử lại sau.'
    },

    importExcel: {
      selectSourceFile: {
        fileNotFound: 'Chưa nhập file.',
        maxSizeError: 'File quá lớn, không thể nhập khẩu.',
        formatError: 'Tệp đính kèm không hợp lệ, chỉ chấp file .xlsx.',
        headerIndexGreaterThanZero: 'Dòng tiêu đề phải lớn hơn 0.',
        sheetNotFound: 'Không tìm thấy sheet.'
      },

      headerMerge: {
        headerRequiredNotMap:
          'Không thể tiếp tục nhập dữ liệu nếu các cột bắt buộc chưa được ghép với cột tương ứng trên tệp Excel. Vui lòng kiểm tra lại.'
      }
    },

    employeeSubsystem: {
      addEmployeePopup: {
        newCodeError: 'Lấy mã mới thất bại.',
        departmentNotFound: 'Dữ liệu <Đơn vị> không có trong danh mục.',
        dateNotGreaterThanToday: '{name} không được lớn hơn ngày hiện tại.'
      },

      notFoundEmployee: 'Không tìm thấy nhân viên.'
    }
  },

  successHandle: {
    employeeSubsystem: {
      addEmployeePopup: {
        add: 'Nhân viên đã được thêm.',
        update: 'Thông tin nhân viên đã được cập nhật.'
      },

      exportExcel: 'Xuất thành công dữ liệu ra file excel.',
      delete: 'Xóa thành công nhân viên <{code}>.',
      deleteMultiple: 'Xóa thành công {count} nhân viên.'
    }
  },

  component: {
    notFound: {
      title: 'Không tìm thấy trang'
    },

    datePicker: {
      dayName: {
        monday: 'T2',
        tuesday: 'T3',
        wednesday: 'T4',
        thursday: 'T5',
        friday: 'T6',
        saturday: 'T7',
        sunday: 'CN'
      },
      monthYearLabel: 'Tháng {month} Năm {year}',
      cancelSelectMonthYear: 'Hủy bỏ',
      today: 'Hôm nay',
      todayTooltip: 'Chọn ngày hôm nay',

      monthRange: {
        january: 'Thg 1',
        february: 'Thg 2',
        march: 'Thg 3',
        april: 'Thg 4',
        may: 'Thg 5',
        june: 'Thg 6',
        july: 'Thg 7',
        august: 'Thg 8',
        september: 'Thg 9',
        october: 'Thg 10',
        november: 'Thg 11',
        december: 'Thg 12'
      }
    },

    paging: {
      total: 'Tổng số',
      record: 'bản ghi',
      previous: 'Trước',
      next: 'Sau',
      recordPerPage: '{count} bản ghi trên 1 trang'
    },

    popup: {
      outConfirmPopupTitle: 'Dữ liệu đã bị thay đổi. Bạn có muốn cất không?'
    },

    toast: {
      defaultTitle: {
        info: 'Thông tin!',
        warning: 'Cảnh báo!',
        error: 'Lỗi!',
        success: 'Thành công!'
      }
    },

    table: {
      valid: 'Hợp lệ',
      invalid: 'Không hợp lệ',
      noData: 'Không có dữ liệu',
      function: 'Chức năng',
      editData: {
        copy: 'Nhân bản',
        delete: 'Xóa',
        stopUsing: 'Ngừng sử dụng'
      }
    }
  },

  sidebar: {
    tooltipExpandSidebar: 'Mở rộng sidebar',
    quickAddButton: 'Thêm nhanh',
    overview: 'Tổng quan',
    cash: 'Tiền mặt',
    deposits: 'Tiền gửi',
    purchase: 'Mua hàng',
    sell: 'Bán hàng',
    invoiceManagement: 'Quản lý hóa đơn',
    warehouse: 'Kho',
    tool: 'Công cụ',
    fixedAssets: 'Tài sản cố định',
    salary: 'Tiền lương',
    tax: 'Thuế',
    price: 'Giá thành',
    loans: 'Vay vốn',
    synthetic: 'Tổng hợp',
    budget: 'Ngân sách',
    report: 'Báo cáo',
    financialAnalysis: 'Phân tích tài chính',
    category: 'Danh mục',
    initialBalance: 'Số dư ban đầu',
    knowledge: 'Kiến thức'
  },

  mainContent: {
    changeLanguage: {
      vi: 'Chuyển sang tiếng Việt',
      en: 'Chuyển sang tiếng Anh'
    },
    tooltipCollapseSidebar: 'Thu gọn sidebar',
    headerDownload: 'Tải tệp excel',
    headerSearchPlaceholder: 'Nhập từ khóa cần tìm kiếm',
    headerSettingTooltip: 'Các tiện ích và thiết lập',
    headerMessageTooltip: 'Trao đổi giữa DN và KTDV',
    headerHelpTooltip: 'Giúp',
    headerNotificationTooltip: 'Thông báo',
    headerAccountInfo: 'Thông tin người dùng'
  },

  importLayout: {
    headerTitle: 'Nhập dữ liệu từ Excel',
    step1Name: 'CHỌN TỆP NGUỒN',
    step2Name: 'GHÉP DỮ LIỆU',
    step3Name: 'KIỂM TRA DỮ LIỆU',
    step4Name: 'KẾT QUẢ'
  },

  importExcel: {
    dataCheck: {
      employeeColumnsInfo: {
        lineNumber: 'Dòng số',
        statusQuo: 'Tình trạng',
        errorDetail: 'Chi tiết lỗi',
        employeeCode: 'Mã nhân viên',
        fullname: 'Tên nhân viên',
        departmentCode: 'Mã đơn vị',
        gender: 'Giới tính',
        dateOfBirth: 'Ngày sinh',
        identityNumber: 'Số CMND',
        identityDate: 'Ngày cấp',
        identityPlace: 'Nơi cấp',
        position: 'Chức danh',
        supplierCustomerGroup: 'Nhóm KH, NCC',
        payAccount: 'TK công nợ phải trả',
        receiveAccount: 'TK công nợ phải thu',
        salary: 'Lương thỏa thuận',
        salaryCoefficients: 'Hệ số lương',
        salaryPaidForInsurance: 'Lương đóng bảo hiểm',
        personalTaxCode: 'Mã số thuế',
        typeOfContract: 'Loại hợp đồng',
        numberOfDependents: 'Số người phụ thuộc',
        accountNumber: 'Số tài khoản',
        bankName: 'Tên ngân hàng',
        bankBranch: 'Chi nhánh TK ngân hàng',
        bankProvince: 'Tỉnh/TP ngân hàng',
        contactAddress: 'Địa chỉ',
        contactPhoneNumber: 'ĐT di động',
        contactLandlinePhoneNumber: 'ĐT cố định',
        contactEmail: 'Email'
      },
      header: {
        validTitle: '{validCount} dòng dữ liệu nhân viên hợp lệ',
        invalidTitle: '{invalidCount} dòng dữ liệu nhân viên không hợp lệ',
        statusFilter: {
          title: 'Lọc tình trạng',
          all: 'Tất cả',
          valid: 'Hợp lệ',
          invalid: 'Không hợp lệ'
        }
      }
    },

    headerMerge: {
      employeeHeaderInfo: {
        employeeCode: {
          showName: 'Mã nhân viên',
          description: 'Nhập mã nhân viên'
        },
        fullname: {
          showName: 'Tên nhân viên',
          description: 'Nhập tên nhân viên'
        },
        departmentCode: {
          showName: 'Mã đơn vị',
          description: 'Nhập mã đơn vị'
        },
        gender: {
          showName: 'Giới tính',
          description: 'Nam; Nữ; Khác'
        },
        dateOfBirth: {
          showName: 'Ngày sinh',
          description: ''
        },
        identityNumber: {
          showName: 'Số CMND',
          description: 'Nhập số chứng minh nhân dân'
        },
        identityDate: {
          showName: 'Ngày cấp',
          description: 'Ngày cấp chứng minh nhân dân'
        },
        identityPlace: {
          showName: 'Nơi cấp',
          description: 'Nơi cấp chứng minh nhân dân'
        },
        position: {
          showName: 'Chức danh',
          description: ''
        },
        supplierCustomerGroup: {
          showName: 'Nhóm KH, NCC',
          description: 'Nhóm khách hàng, nhà cung cấp'
        },
        payAccount: {
          showName: 'TK công nợ phải trả',
          description: ''
        },
        receiveAccount: {
          showName: 'TK công nợ phải thu',
          description: ''
        },
        salary: {
          showName: 'Lương thỏa thuận',
          description: ''
        },
        salaryCoefficients: {
          showName: 'Hệ số lương',
          description: ''
        },
        salaryPaidForInsurance: {
          showName: 'Lương đóng bảo hiểm',
          description: ''
        },
        personalTaxCode: {
          showName: 'Mã số thuế',
          description: ''
        },
        typeOfContract: {
          showName: 'Loại hợp đồng',
          description: ''
        },
        numberOfDependents: {
          showName: 'Số người phụ thuộc',
          description: ''
        },
        accountNumber: {
          showName: 'Số tài khoản',
          description: ''
        },
        bankName: {
          showName: 'Tên ngân hàng',
          description: ''
        },
        bankBranch: {
          showName: 'Chi nhánh TK ngân hàng',
          description: ''
        },
        bankProvince: {
          showName: 'Tỉnh/TP ngân hàng',
          description: ''
        },
        contactAddress: {
          showName: 'Địa chỉ',
          description: ''
        },
        contactPhoneNumber: {
          showName: 'ĐT di động',
          description: ''
        },
        contactLandlinePhoneNumber: {
          showName: 'ĐT cố định',
          description: ''
        },
        contactEmail: {
          showName: 'Email',
          description: ''
        }
      },
      headerMergeColumnsInfo: {
        requiredInformation: 'Thông tin bắt buộc',
        softwareColumn: 'Cột trên phần mềm',
        dataFileColumn: 'Cột trên tệp dữ liệu',
        description: 'Diễn giải chi tiết'
      },
      header: {
        title: 'Ghép cột trên phần mềm với cột trên tệp dữ liệu'
      }
    },

    importResult: {
      header: {
        successTitle: '{recordSuccess}/{totalRecord} dòng dữ liệu nhập thành công',
        failTitle: '{recordFail}/{totalRecord} dòng dữ liệu nhập không thành công'
      }
    },

    selectSourceFile: {
      importMode: {
        add: 'Thêm mới',
        update: 'Cập nhật'
      },
      selectExcelFile: 'Chọn tệp Excel',
      maximumSize: 'Dung lượng tối đa 2MB',
      downloadBasicTemplate: 'Tải tệp mẫu cơ bản',
      downloadFullTemplate: 'Tải tệp mẫu đầy đủ',
      sheetContainsData: 'Sheet chứa dữ liệu',
      headerRowIndex: 'Dòng tiêu đề là dòng số',
      importModeTitle: 'Phương thức nhập',
      suggestionLabel: 'Gợi ý',
      moreInfoBasicTemplate: 'để nhập những thông tin cơ bản.',
      moreInfoFullTemplate: ' để nhập tất cả các thông tin.',
      nextUseInfo: 'Các thiết lập trong quá trình nhập sẽ được lưu để sử dụng trong các lần tiếp sau.',
      importModeAddDescription:
        'Dữ liệu trên tệp mà chưa có trong hệ thống sẽ được thêm mới vào. Dữ liệu mà đã có trên hệ thống sẽ không được cập nhật vào.',
      importModeUpdateDescription:
        'Dữ liệu trên tệp mà chưa có trong hệ thống sẽ được thêm mới vào. Dữ liệu mà đã có trên hệ thống sẽ được cập nhật vào.'
    }
  },

  predictSubsystem: {
    addPredictPopup: {
      nameField: {
        provinceId: 'Tỉnh/Thành phố',
        districtId: 'Quận/Huyện',
        wardId: 'Phường/Xã',
        address: 'Địa chỉ',
        previousEndDate: 'Ngày kết thúc kỳ trước',
        previousLevelWarning: 'Cảnh báo cấp độ kỳ trước',
        currentStartDate: 'Ngày bắt đầu kỳ hiện tại'
      }
    }
  },

  userSubsystem: {
    addUserPopup: {
      nameField: {
        roleId: 'Chức vụ',
        fullname: 'Họ và tên',
        username: 'Tên tài khoản'
      }
    }
  },

  employeeSubsystem: {
    pageTitle: 'Nhân viên | ttanh | Kế toán',
    title: 'Nhân viên',
    employeeContent: {
      searchPlaceHolder: 'Tìm kiếm',
      batchExecution: 'Thực hiện hàng loạt',
      searchInputTooltip: 'Tìm kiếm (Ctrl + Shift + F)',
      reloadTooltip: 'Lấy lại dữ liệu',
      exportExcelTooltip: 'Xuất ra excel',
      layoutSettingTooltip: 'Tùy chỉnh giao diện',
      insertTooltip: 'Thêm (Insert)',
      deletePopupTitle: 'Bạn có thực sự muốn xóa Nhân viên <{code}> không?',
      deleteMultiplePopupTitle: 'Bạn có thực sự muốn xóa {count} nhân viên không?',
      batchExecutionData: {
        delete: 'Xoá'
      },
      addDataDropdown: {
        importExcel: 'Nhập từ Excel'
      }
    },

    layoutSetting: {
      headerTitle: 'Tùy chỉnh giao diện',
      columnsInfo: {
        display: 'Hiển thị',
        dataColumnName: 'Tên cột dữ liệu',
        interfaceColumnName: 'Tên cột trên giao diện',
        width: 'Độ rộng',
        fixedColumn: 'Cố định cột'
      }
    },

    addEmployeePopup: {
      headerTitle: 'Thông tin nhân viên',
      isCustomer: 'Là khách hàng',
      isSupplier: 'Là nhà cung cấp',

      labelInput: {
        code: 'Mã',
        name: 'Tên',
        department: 'Đơn vị',
        position: 'Chức danh',
        supplierCustomerGroup: 'Nhóm khách hàng, nhà cung cấp',
        dateOfBirth: 'Ngày sinh',
        gender: 'Giới tính',
        identityNumber: 'Số CMND',
        identityDate: 'Ngày cấp',
        identityPlace: 'Nơi cấp',
        receiveAccount: 'TK công nợ phải thu',
        payAccount: 'TK công nợ phải trả',
        salary: 'Lương thỏa thuận',
        salaryCoefficients: 'Hệ số lương',
        salaryPaidForInsurance: 'Lương đóng bảo hiểm',
        personalTaxCode: 'Mã số thuế',
        typeOfContract: 'Loại hợp đồng',
        numberOfDependents: 'Số người phụ thuộc',
        accountNumber: 'Số tài khoản',
        bankName: 'Tên ngân hàng',
        bankBranch: 'Chi nhánh',
        bankCity: 'Tỉnh/TP của ngân hàng',
        contactAddress: 'Địa chỉ',
        contactPhoneNumber: 'ĐT di động',
        contactLandlinePhoneNumber: 'ĐT cố định',
        contactEmail: 'Email'
      },

      labelInputTooltip: {
        code: 'Mã',
        name: 'Tên',
        department: 'Đơn vị',
        position: 'Chức danh',
        supplierCustomerGroup: 'Nhóm khách hàng, nhà cung cấp',
        dateOfBirth: 'Ngày sinh',
        gender: 'Giới tính',
        identityNumber: 'Số chứng minh nhân dân',
        identityDate: 'Ngày cấp',
        identityPlace: 'Nơi cấp',
        receiveAccount: 'TK công nợ phải thu',
        payAccount: 'TK công nợ phải trả',
        salary: 'Lương thỏa thuận',
        salaryCoefficients: 'Hệ số lương',
        salaryPaidForInsurance: 'Lương đóng bảo hiểm',
        personalTaxCode: 'Mã số thuế',
        typeOfContract: 'Loại hợp đồng',
        numberOfDependents: 'Số người phụ thuộc',
        accountNumber: 'Số tài khoản',
        bankName: 'Tên ngân hàng',
        bankBranch: 'Chi nhánh',
        bankCity: 'Tỉnh/Thành phố của ngân hàng',
        contactAddress: 'Địa chỉ',
        contactPhoneNumber: 'Điện thoại di động',
        contactLandlinePhoneNumber: 'Điện thoại cố định',
        contactEmail: 'Email'
      },

      moreInfoNav: {
        salaryInfo: 'Thông tin tiền lương',
        bankInfo: 'Thông tin ngân hàng',
        contactInfo: 'Thông tin liên hệ'
      },

      departmentColumnsInfo: {
        code: 'Mã đơn vị',
        name: 'Tên đơn vị'
      },

      nameField: {
        employeeId: 'Nhân viên',
        employeeCode: 'Mã code nhân viên',
        fullName: 'Tên nhân viên',
        gender: 'Giới tính',
        dateOfBirth: 'Ngày sinh',

        identityNumber: 'Số CMND',
        identityDate: 'Ngày cấp CMND',
        identityPlace: 'Nơi cấp CMND',

        departmentId: 'Đơn vị',
        departmentCode: 'Mã đơn vị',
        departmentName: 'Tên đơn vị',

        supplierCustomerGroup: 'Nhóm khách hàng, nhà cung cấp',
        payAccount: 'TK công nợ phải trả',
        receiveAccount: 'TK công nợ phải thu',

        position: 'Chức danh',

        /* thông tin tiền lương */
        salary: 'Lương thỏa thuận',
        salaryPaidForInsurance: 'Lương đóng bảo hiểm',
        salaryCoefficients: 'Hệ số lương',

        personalTaxCode: 'Mã số thuế',
        typeOfContract: 'Loại hợp đồng',
        numberOfDependents: 'Số người phụ thuộc',

        /* tài khoản ngân hàng */
        accountNumber: 'Số tài khoản ngân hàng',
        bankName: 'Tên ngân hàng',
        bankBranch: 'Chi nhánh ngân hàng',
        bankProvince: 'Tỉnh/TP của ngân hàng',

        /* thông tin liên hệ */
        contactAddress: 'Địa chỉ',
        contactEmail: 'Email',
        contactPhoneNumber: 'ĐT di động',
        contactLandlinePhoneNumber: 'ĐT cố định'
      }
    }
  }
}
export default vi
