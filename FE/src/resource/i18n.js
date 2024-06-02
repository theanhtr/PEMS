import { createI18n } from "vue-i18n";
import TTANHResource from "./resource.js";
import store from "../store";

const i18n = createI18n({
  locale: store.state.langCode,
  fallbackLocale: "vi",
  messages: TTANHResource,
});

export default i18n;
