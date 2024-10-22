/**
 * cấu hình của app
 * @author: TTANH (29/07/2024)
 */

const ProjectConfig = {
  AuthenApiUrl: import.meta.env.VITE_AUTHEN_API_URL + "/api/v1/",
  UserApiUrl: import.meta.env.VITE_USER_API_URL + "/api/v1/",
  FormatDate: "dd/MM/yyyy",
};

/**
 * Cấu hình các giá trị kiểm tra
 * @author: TTANH (29/07/2024)
 */
const ValidateConfig = {
  MaxSizeFileUpload: 2097152, //2mb

  // validate cho tên có chiều dài trong db
  MaxLength20: 20,
  MaxLength25: 25,
  MaxLength50: 50,
  MaxLength100: 100,
  MaxLength255: 255,

  // email regex
  EmailRegex:
    /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i,

  /**
   * valid format
   *  (123) 456-7890
      (123)456-7890
      123-456-7890
      123.456.7890
      1234567890
      +31636363634
      075-63546725 
   */
  PhoneNumberRegex:
    /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im,

  // validate 1 trường toàn số
  OnlyNumbersRegex: /^\d+$/,
};

export { ProjectConfig, ValidateConfig };
