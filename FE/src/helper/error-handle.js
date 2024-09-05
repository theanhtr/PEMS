import store from '../store'
import TTANHResource from '../resource/resource'

function CommonErrorHandle() {
  // store.commit("addToast", {
  //   type: "error",
  //   text: TTANHResource[store.state.langCode].errorHandle.serverError
  //     .defaultError,
  // });
}

export { CommonErrorHandle }
