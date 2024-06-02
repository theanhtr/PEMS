/**
 * hàm thực hiện debounce sau 1 khoản thời gian
 * @author: TTANH (10/07/2023)
 * @param {*} fn hàm thực hiện hành động
 * @param {int} delay thời gian delay
 */
export function debounce(fn, delay) {
  var timeoutID = null;

  return function () {
    clearTimeout(timeoutID);
    var args = arguments;
    var that = this;

    timeoutID = setTimeout(function () {
      fn.apply(that, args);
    }, delay);
  };
}
