/**
 * hàm để xử lý khi response là file trả về, và download cho người dùng
 * @author: TTANH (16/07/2023)
 * @param {*} res response
 */
function downloadFileFromRes(res) {
  if (res.config.responseType == "blob") {
    let fileName = "";

    //lấy ra tên file từ content disposition
    let headersContentDisposition =
      res.headers["content-disposition"].split(";");

    for (let i = 0; i < headersContentDisposition.length; i++) {
      if (headersContentDisposition[i].includes("filename=")) {
        let startIndexName = headersContentDisposition[i].indexOf("=") + 1;

        for (
          let j = startIndexName;
          j < headersContentDisposition[i].length;
          j++
        ) {
          fileName += headersContentDisposition[i][j];
        }

        break;
      }
    }

    let FILE = window.URL.createObjectURL(new Blob([res.data]));

    let docUrl = document.createElement("a");
    docUrl.href = FILE;
    docUrl.setAttribute("download", fileName);
    document.body.appendChild(docUrl);
    docUrl.click();

    document.body.removeChild(docUrl);
  }
}

/**
 * Response trả về được format theo dạng:
 *  - success: trạng thái của response (true)
 *  - statusCode: mã code cho trạng thái (từ 100 -> 399)
 *  - data: dữ liệu trả về từ api
 * @author: TTANH (02/07/2023)
 * @param {*} res response
 * @returns response đã được format
 */
export function success(res) {
  downloadFileFromRes(res);

  return {
    success: true,
    statusCode: res.status,
    data: res.data,
  };
}

/**
 * Response trả về được format theo dạng:
 *  - success: trạng thái của response (false)
 *  - statusCode: mã code cho trạng thái (từ 400 -> 599)
 *  - errorCode: mã trạng thái lỗi (thống nhất giữa client và server)
 *  - devMsg: tin nhắn dành cho lập trình viên về lỗi
 *  - userMsg: tin nhắn dành cho người dùng về lỗi
 *  - moreInfo: thông tin thêm về lỗi
 *  - data: dữ liệu về lỗi trả về từ api
 * @author: TTANH (02/07/2023)
 * @param {*} res response
 * @returns response đã được format
 */
export function failure(res) {
  return {
    success: false,
    statusCode: res.status,
    errorCode: res.data.ErrorCode,
    devMsg: res.data.DevMessage,
    userMsg: res.data.UserMessage,
    moreInfo: res.data.MoreInfo,
    data: res.data.Data,
  };
}
