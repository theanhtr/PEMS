import TTANHResource from "../resource/resource";
import store from "../store";
import sprintf from "sprintf-js";

/**
 * hàm kiểm tra chiều dài của một chuỗi
 * @param {string} valueCheck đối tượng cần kiểm tra
 * @param {int} maxLength chiều dài tối đa
 * @param {int?} minLength chiều dài tối thiểu
 * @param {string} nameField tên trường
 * @returns thông báo lỗi
 * @author: TTANH (29/07/2024)
 */
function lengthValidate(
  valueCheck,
  maxLength,
  minLength = 0,
  nameField = TTANHResource[store.state.langCode].validate.nameFieldDefault
) {
  let valueLength = valueCheck.length;

  if (maxLength < 0 || minLength < 0) {
    return TTANHResource[store.state.langCode].validate.paramPassToFunctionError;
  } else if (valueLength > maxLength) {
    return sprintf.sprintf(
      TTANHResource[store.state.langCode].validate.maxLengthError,
      nameField,
      maxLength
    );
  } else if (valueLength < minLength) {
    return sprintf.sprintf(
      TTANHResource[store.state.langCode].validate.minLengthError,
      nameField,
      minLength
    );
  } else {
    return "";
  }
}

/**
 * hàm kiểm tra một chuỗi trống
 * @param {string} valueCheck đối tượng cần kiểm tra
 * @param {string} nameField tên trường
 * @returns thông báo lỗi
 * @author: TTANH (29/07/2024)
 */
function emptyValidate(
  valueCheck,
  nameField = TTANHResource[store.state.langCode].validate.nameFieldDefault
) {
  let valueLength = valueCheck.length;

  if (valueLength === 0) {
    return sprintf.sprintf(
      TTANHResource[store.state.langCode].validate.emptyError,
      nameField
    );
  } else {
    return "";
  }
}

/**
 * hàm kiểm tra định dạng theo regex
 * @param {string} valueCheck đối tượng cần kiểm tra
 * @param {string} nameField tên trường
 * @param {string} regex regex kiểm tra
 * @returns thông báo lỗi
 * @author: TTANH (29/07/2024)
 */
function regexValidate(
  valueCheck,
  nameField = TTANHResource[store.state.langCode].validate.nameFieldDefault,
  regex
) {
  if (!String(valueCheck).toLowerCase().match(regex) && valueCheck !== "") {
    return sprintf.sprintf(
      TTANHResource[store.state.langCode].validate.formatError,
      nameField
    );
  } else {
    return "";
  }
}

export { lengthValidate, emptyValidate, regexValidate };
