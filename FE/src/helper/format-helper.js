import TTANHResource from "../resource/resource";
import store from "../store";

/**
 * convert string từ: trần thế anh -> Trần Thế Anh
 * @author: TTANH (26/06/2024)
 * @param {string} rawValue giá trị cần convert
 * @returns giá trị đã convert
 */
export function capitalizeFirstLetter(rawValue) {
  try {
    let valueReturn = "";

    for (let i = 0; i < rawValue.length; i++) {
      if (i === 0) {
        valueReturn += rawValue[i].toUpperCase();
      } else {
        if (rawValue[i - 1] === " ") {
          valueReturn += rawValue[i].toUpperCase();
        } else {
          valueReturn += rawValue[i];
        }
      }
    }

    return valueReturn;
  } catch (error) {
    console.log(
      "🚀 ~ file: helper.js:10 ~ capitalizeFirstLetter ~ error:",
      error
    );
  }
}

/**
 * chuyển đổi sang date format
 * @author: TTANH (26/06/2024)
 * @param {string} rawValue string có thể chuyển định dạng sang được date
 * @param {string} formatDate cách định dạng ngày tháng
 * @returns string đã được định dạng theo 30/06/2024
 */
export function formatToDate(rawValue, formatDate) {
  try {
    if (!rawValue) {
      return "";
    }

    let valueConvert = new Date(rawValue);

    let dateFormatReturn = `/${valueConvert.getFullYear()}`;

    if (formatDate === "dd/MM/yyyy") {
      dateFormatReturn =
        `${
          valueConvert.getDate() < 10
            ? "0" + valueConvert.getDate()
            : valueConvert.getDate()
        }/${
          valueConvert.getMonth() + 1 < 10
            ? "0" + (valueConvert.getMonth() + 1)
            : valueConvert.getMonth() + 1
        }` + dateFormatReturn;
    } else if (formatDate === "MM/dd/yyyy") {
      dateFormatReturn =
        `${
          valueConvert.getMonth() + 1 < 10
            ? "0" + (valueConvert.getMonth() + 1)
            : valueConvert.getMonth() + 1
        }/${
          valueConvert.getDate() < 10
            ? "0" + valueConvert.getDate()
            : valueConvert.getDate()
        }` + dateFormatReturn;
    }

    return dateFormatReturn;
  } catch (error) {
    console.log("🚀 ~ file: helper.js:51 ~ formatToDate ~ error:", error);
  }
}

/**
 * chuyển đổi sang định dạng tiền
 * @author: TTANH (26/06/2024)
 * @param {int} rawValue dữ liệu có thể chuyển sang kiểu tiền
 * @returns chuyển đổi thành dữ liệu dạng: 1.000.000đ, nếu 0 thì thành ""
 */
export function formatToCurrency(rawValue) {
  try {
    if (rawValue === 0 || rawValue === null) {
      return "";
    }

    const formatter = new Intl.NumberFormat("vi-VN", {
      style: "currency",
      currency: "VND",
    });

    return formatter.format(rawValue);
  } catch (error) {
    console.log("🚀 ~ file: helper.js:58 ~ formatToCurrency ~ error:", error);
  }
}

/**
 * chuyển đổi sang định dạng tên giới tính
 * @author: TTANH (15/07/2024)
 * @param {int} rawValue dữ liệu số 0, 1, 2
 * @returns chuyển đổi thành tên giới tính:
 *   0 - Nam,
 *   1 - Nữ,
 *   2 - Khác
 */
export function formatToGenderName(rawValue) {
  try {
    switch (rawValue) {
      case 0:
        return TTANHResource[store.state.langCode].common.gender.male;

      case 1:
        return TTANHResource[store.state.langCode].common.gender.female;

      case 2:
        return TTANHResource[store.state.langCode].common.gender.other;

      default:
        break;
    }
  } catch (error) {
    console.log(
      "🚀 ~ file: format-helper.js:92 ~ formatToGenderName ~ error:",
      error
    );
  }
}
