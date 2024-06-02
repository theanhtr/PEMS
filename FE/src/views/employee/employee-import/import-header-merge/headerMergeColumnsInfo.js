import TTANHResource from "../../../../resource/resource.js";
import store from "../../../../store/index.js";

export var headerMergeColumnsInfo = [
  {
    id: "isRequired",
    name: TTANHResource[store.state.langCode].importExcel.headerMerge
      .headerMergeColumnsInfo.requiredInformation,
    size: 200,
    textAlign: "center",
    format: "checkbox",
    isShow: true,
    isPin: false,
  },
  {
    id: "showName",
    name: TTANHResource[store.state.langCode].importExcel.headerMerge
      .headerMergeColumnsInfo.softwareColumn,
    size: 245,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
  {
    id: "excelColumnIndex",
    name: TTANHResource[store.state.langCode].importExcel.headerMerge
      .headerMergeColumnsInfo.dataFileColumn,
    size: 255,
    textAlign: "left",
    format: "input-combobox",
    isShow: true,
    isPin: false,
  },
  {
    id: "description",
    name: TTANHResource[store.state.langCode].importExcel.headerMerge
      .headerMergeColumnsInfo.description,
    size: 765,
    textAlign: "left",
    format: "text",
    isShow: true,
    isPin: false,
  },
];
