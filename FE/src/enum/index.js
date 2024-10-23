const TTANHEnum = {
  API_SERVER_KEY: {
    AUTH: 'auth',
    USER: 'user',
    PREDICT: 'predict',
    REPORT: 'report',
  },

  /* các bước khi import excel */
  IMPORT_STEP: {
    SELECT_SOURCE_FILE: 0,
    HEADER_MERGE: 1,
    DATA_CHECK: 2,
    IMPORT_RESULT: 3,
  },

  /* loại import excel */
  IMPORT_MODE: {
    ADD: 0,
    UPDATE: 1,
  },

  /* kiểm tra các dòng trong file excel */
  RECORD_CHECK: {
    INVALID: 0,
    VALID: 1,
  },

  /* loại lọc excel */
  FILTER_EXCEL_DATA_VALIDATE_TYPE: {
    INVALID: 0,
    VALID: 1,
    ALL: 2,
  },

  GENDER: {
    MALE: 0,
    FEMALE: 1,
    OTHER: 2,
  },

  FORM_MODE: {
    ADD: 0,
    UPDATE: 1,
    VIEW: 2,
  },

  /* enum sử dụng trong more info nav của add employee popup */
  MORE_INFO_NAV: {
    SALARY_INFO: 0,
    BANK_INFO: 1,
    CONTACT_INFO: 2,
  },

  /* enum sử dụng cho sự kiện keypress, keyup, keydown */
  KEY_CODE: {
    TAB: 9,
    ENTER: 13,
    CTRL: 17,
    SHIFT: 16,
    ESC: 27,
    ALT: 18,
    ARROW_UP: 38,
    ARROW_DOWN: 40,
    ARROW_RIGHT: 39,
    ARROW_LEFT: 37,
    INSERT: 45,
    S: 83,
    F: 70,
    0: 48,
    1: 49,
    2: 50,
    3: 51,
    4: 52,
    5: 53,
    6: 54,
    7: 55,
    8: 56,
    9: 57,
  },

  SUCCESS_CODE: {
    ADD_INFO_SUCCESS: 1207,
    IMPORT_EXCEL_REQUEST_SUCCESS: 1210,
  },

  ERROR_CODE: {
    CODE_DUPLICATE: 1407,
    WRONG_FORMAT_CODE: 1408,
    NOT_FOUND_DATA: 1409,
    SESSION_IS_OVER: 1410,
    WRONG_FORMAT_DATE: 1412,
    EXCEL_HEADER_DUPLICATE: 1425,
    EXCEL_HEADER_EMPTY: 1426,
    SHEET_IS_EMPTY: 1427,
    FORMAT_EXCEL_ERROR: 1428,
    MAX_SIZE_FILE_ERROR: 1429,
    EXCEL_HEADER_REQUIRED_NOT_MAP: 1430,
  },
};

export default TTANHEnum;
