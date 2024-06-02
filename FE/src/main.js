import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import sprintf from "sprintf-js"; // sử dụng để dùng biến trong string
import i18n from "./resource/i18n.js";

import TTANHButton from "./components/base/button/TTANHButton.vue";
import TTANHIcon from "./components/base/icon/TTANHIcon.vue";
import TTANHSeparationLine from "./components/base/separation-line/TTANHSeparationLine.vue";
import TTANHSearchInput from "./components/base/input/search-input/TTANHSearchInput.vue";
import TTANHFileInput from "./components/base/input/file-input/TTANHFileInput.vue";
import TTANHDropdownlist from "./components/base/dropdownlist/TTANHDropdownlist.vue";
import TTANHTable from "./components/base/table/TTANHTable.vue";
import TTANHCheckboxInput from "./components/base/input/TTANHCheckboxInput.vue";
import TTANHLoadingSpinner from "./components/base/loading/loading-spinner/TTANHLoadingSpinner.vue";
import TTANHLoadingSkeleton from "./components/base/loading/loading-skeleton/TTANHLoadingSkeleton.vue";
import TTANHPopup from "./components/base/popup/TTANHPopup.vue";
import TTANHOutConfirmPopup from "./components/base/popup/out-confirm-popup/TTANHOutConfirmPopup.vue";
import TTANHErrorPopup from "./components/base/popup/error-popup/TTANHErrorPopup.vue";
import TTANHDeletePopup from "./components/base/popup/delete-popup/TTANHDeletePopup.vue";
import TTANHTextfield from "./components/base/textfield/TTANHTextfield.vue";
import TTANHCombobox from "./components/base/combobox/TTANHCombobox.vue";
import TTANHDatePicker from "./components/base/date-picker/TTANHDatePicker.vue";
import TTANHArrowDown from "./components/base/arrow-down/TTANHArrowDown.vue";
import TTANHRadioInput from "./components/base/radio-input/TTANHRadioInput.vue";
import TTANHTooltip from "./components/base/tooltip/TTANHTooltip.vue";
import TTANHToast from "./components/base/toast/TTANHToast.vue";
import TTANHPaging from "./components/base/paging/TTANHPaging.vue";

import TTANHEnum from "./enum";

const app = createApp(App);

/**
 * sử dụng để chọn bôi đen value khi ấn vào 1 input
 * @author: TTANH (31/07/2023)
 */
app.directive("TTANHBlackenOut", {
  created: (el) => {
    el.onfocus = () => {
      el.select();
    };
  },
});

app.use(store).use(router);

app
  .component("ttanh-separation-line", TTANHSeparationLine)
  .component("ttanh-icon", TTANHIcon)
  .component("ttanh-search-input", TTANHSearchInput)
  .component("ttanh-file-input", TTANHFileInput)
  .component("ttanh-checkbox-input", TTANHCheckboxInput)
  .component("ttanh-dropdownlist", TTANHDropdownlist)
  .component("ttanh-button", TTANHButton)
  .component("ttanh-loading-skeleton", TTANHLoadingSkeleton)
  .component("ttanh-loading-spinner", TTANHLoadingSpinner)
  .component("ttanh-popup", TTANHPopup)
  .component("ttanh-out-confirm-popup", TTANHOutConfirmPopup)
  .component("ttanh-error-popup", TTANHErrorPopup)
  .component("ttanh-delete-popup", TTANHDeletePopup)
  .component("ttanh-textfield", TTANHTextfield)
  .component("ttanh-combobox", TTANHCombobox)
  .component("ttanh-table", TTANHTable)
  .component("ttanh-arrow-down", TTANHArrowDown)
  .component("ttanh-date-picker", TTANHDatePicker)
  .component("ttanh-radio-input", TTANHRadioInput)
  .component("ttanh-tooltip", TTANHTooltip)
  .component("ttanh-toast", TTANHToast)
  .component("ttanh-paging", TTANHPaging);

app.config.globalProperties.$_TTANHEnum = TTANHEnum;
app.config.globalProperties.$sprintf = sprintf.sprintf;

app.use(i18n);

app.mount("#app");

/**
 * Hàm dùng để set thông tin về phân trang nhân viên
 * vào local storage
 * @author: TTANH (01/08/2023)
 */
function employeePageInfo() {
  localStorage.setItem("pageNumber", 1);
  localStorage.setItem("pageSize", 10);
}

if (!localStorage.getItem("pageNumber")) {
  employeePageInfo();
}
