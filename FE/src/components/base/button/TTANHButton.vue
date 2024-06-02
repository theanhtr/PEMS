<template>
  <button
    :style="styleOfButton"
    :class="classOfButton"
    class="style-button"
    :disabled="disable"
    ref="buttonContainer"
    :tabindex="tabindex"
    @click="clickButton"
    @focus="isFocusButton = true"
    @focusout="isFocusButton = false"
    @keydown="onKeyDownButton"
  >
    <div
      class="ttanh-button--container"
      :style="styleOfButtonContainer"
      :class="isFocusButton && type !== 'link' ? 'ttanh-button--focus' : ''"
      ref="btnContainerRef"
      @click="!disable ? clickBtnContainer() : ''"
      @keydown="onKeyDownButton"
    >
      <ttanh-icon
        :scale="this.scaleIcon"
        v-if="icon !== ''"
        :icon="icon"
        class="button__icon"
        :tooltip="tooltip"
        :disable="disable"
        @mouseenter="this.buttonHover = true"
        @mouseleave="this.buttonHover = false"
      />
      <div
        class="button__title"
        tabindex="-1"
        @mouseenter="this.buttonHover = true"
        @mouseleave="this.buttonHover = false"
      >
        <slot></slot>
      </div>
      <div
        v-if="type === 'combo' || type === 'dropdown'"
        class="button__dropdown"
        :style="styleButtonDropdown"
        @click="clickDropdownButton"
        tabindex="-1"
      >
        <ttanh-icon
          :icon="
            type === 'combo' ? 'dropdown--solid-white' : 'dropdown--solid-grey'
          "
          :disable="disable"
        />
      </div>
    </div>
    <div
      v-if="(type === 'combo' || type === 'dropdown') && isShowDataDropdown"
      class="data__dropdown"
      :style="styleDataDropdown"
    >
      <div
        v-for="data in dataDropdown"
        :key="data.id"
        @click="clickDataDropdownItem($event, data.id)"
        class="data__item"
        :class="
          dataDropdownSelectedValue === data.value ? 'data__item--selected' : ''
        "
      >
        {{ data.title }}
      </div>
    </div>

    <ttanh-tooltip v-if="tooltip !== '' && buttonHover" :error="false">{{
      tooltip
    }}</ttanh-tooltip>
  </button>
</template>

<script>
export default {
  name: "TTANHButton",
  props: {
    type: {
      required: true,
      type: String,
      validator: function (val) {
        return [
          "main",
          "icon",
          "text",
          "sub",
          "link",
          "combo",
          "dropdown",
        ].includes(val);
      },
    },

    /**
     * thông tin data dropdown phải theo định dạng
        {
          id: "item",
          title: "Tương tác 1",
        },
     * @author: TTANH (27/06/2023)
     */
    dataDropdown: {
      default: [],
    },

    tooltip: {
      default: "",
      type: String,
    },
    icon: {
      default: "",
      type: String,
    },
    width: {
      default: "auto",
    },
    height: {
      default: "var(--primary-button-height)",
    },
    colorText: {
      default: "unset",
    },
    fontWeight: {
      default: "",
    },
    scaleIcon: {
      default: 1,
    },
    borderRadius: {
      default: "var(--btn-border-radius-default)",
    },
    padding: {
      default: "",
    },
    border: {
      default: "",
    },
    disable: {
      default: false,
    },

    /* biến để xác định data__dropdown hiện ở trên */
    dataDropdownTop: {
      default: false,
    },

    /* biến sử dụng cho việc hiện màu đã chọn của data_dropdown */
    dataDropdownSelectedValue: {
      default: "",
    },

    tabindex: {
      default: "0",
    },
  },
  data() {
    return {
      buttonHover: false,
      isShowDataDropdown: false,
      isFocusButton: false,
    };
  },
  methods: {
    /**
     * hàm để check sự kiện clickoutside
     * @author: TTANH (27/06/2023)
     * @param {*} event
     */
    clickOutSideButton(event) {
      try {
        if (this.$refs.buttonContainer) {
          if (!this.$refs.buttonContainer.contains(event.target)) {
            this.hideDataDropdown();
          }
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHButton.vue:132 ~ clickOutSideButton ~ error:",
          error
        );
      }
    },

    /**
     * xử lý hiển thị dataDropdown
     * @author: TTANH (27/06/2023)
     */
    showDataDropdown() {
      try {
        this.isShowDataDropdown = true;
        window.addEventListener("click", this.clickOutSideButton);
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHButton.vue:131 ~ showDataDropdown ~ error:",
          error
        );
      }
    },

    /**
     * xử lý ẩn dataDropdown
     * @author: TTANH (27/06/2023)
     */
    hideDataDropdown() {
      try {
        this.isShowDataDropdown = false;
        window.removeEventListener("click", this.clickOutSideButton);
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHButton.vue:146 ~ hideDataDropdown ~ error:",
          error
        );
      }
    },

    /**
     * xử lý sự kiện khi nhấn vào button bọc tất cả nội dung
     * @author: TTANH (27/06/2023)
     */
    clickButton() {
      try {
        this.$emit("clickButton");

        if (this.type === "dropdown") {
          if (this.isShowDataDropdown) {
            this.hideDataDropdown();
          } else {
            this.showDataDropdown();
          }
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHButton.vue:110 ~ clickButton ~ error:",
          error
        );
      }
    },

    /**
     * xử lý sự kiện khi nhấn vào button chính
     * @author: TTANH (10/07/2023)
     */
    clickBtnContainer() {
      try {
        this.$emit("clickBtnContainer");
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHButton.vue:110 ~ clickBtnContainer ~ error:",
          error
        );
      }
    },

    /**
     * xử lý sự kiện khi nhấn vào button dropdown đối với Combo button
     * @author: TTANH (27/06/2023)
     * @param {*} event
     */
    clickDropdownButton(event) {
      try {
        if (this.type === "combo") {
          event.stopPropagation();

          if (this.isShowDataDropdown) {
            this.hideDataDropdown();
          } else {
            this.showDataDropdown();
          }
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHButton.vue:126 ~ clickDropdownButton ~ error:",
          error
        );
      }
    },

    /**
     * xử lý sự kiện khi nhấn vào dropdown item đối với Combo button và dropdown button
     * @author: TTANH (27/06/2023)
     * @param {*} event
     */
    clickDataDropdownItem(event, id) {
      try {
        if (this.type === "combo" || this.type === "dropdown") {
          event.stopPropagation();
          this.isShowDataDropdown = false;
          this.$emit("clickItem", id);
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHButton.vue:142 ~ clickDropdownItem ~ error:",
          error
        );
      }
    },

    /**
     * gọi button từ cha
     * @author: TTANH (03/07/2023)
     */
    getBtnContainerRef() {
      try {
        return this.$refs.btnContainerRef;
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHButton.vue:377 ~ getBtnContainerRef ~ error:",
          error
        );
      }
    },

    /**
     * focus vào btn chứa tất cả từ cha
     * @author: TTANH (03/07/2023)
     */
    focusButtonContainer() {
      try {
        this.isFocusButton = true;
        this.$refs.buttonContainer.focus();
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHButton.vue:396 ~ focusBtnContainerRef ~ error:",
          error
        );
      }
    },

    /**
     * focus vào btn từ cha
     * @author: TTANH (03/07/2023)
     */
    focus() {
      try {
        this.isFocusButton = true;
        this.$refs.btnContainerRef.focus();
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHButton.vue:396 ~ focusBtnContainerRef ~ error:",
          error
        );
      }
    },

    /**
     * focusout vào btn từ cha
     * @author: TTANH (03/07/2023)
     */
    focusout() {
      try {
        this.isFocusButton = false;
        this.$refs.btnContainerRef.blur();
        this.$refs.buttonContainer.blur();
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHButton.vue:412 ~ focusoutBtnContainerRef ~ error:",
          error
        );
      }
    },

    /**
     * xử lý sự kiện khi keydown
     * @author: TTANH (10/07/2023)
     * @param {*} event
     */
    onKeyDownButton(event) {
      try {
        if (event.keyCode === this.$_TTANHEnum.KEY_CODE.ENTER) {
          this.clickButton();
          this.clickBtnContainer();
        } else {
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHButton.vue:291 ~ onKeyDownButton ~ error:",
          error
        );
      }
    },
  },
  computed: {
    classOfButton() {
      switch (this.type) {
        case "main":
          return "m-button";

        case "icon":
          return "i-button";

        case "text":
          return "t-button";

        case "sub":
          return "s-button";

        case "link":
          return "l-button";

        case "combo":
          return "cb-button";

        case "dropdown":
          return "dd-button";

        default:
          return "";
      }
    },
    defaultColorText() {
      const typesButtonColorWhite = ["main", "icon", "text", "combo"];
      if (typesButtonColorWhite.includes(this.type)) {
        return "white";
      } else if (this.type === "sub") {
        return "black";
      } else if (this.type === "link") {
        return "var(--primary-btn-background-color)";
      }
    },
    styleOfButton() {
      return {
        width: this.width,
        minWidth: this.width,
        maxWidth: this.width,
        height: this.height,
        color:
          this.colorText === "unset" ? this.defaultColorText : this.colorText,
        borderRadius: this.borderRadius,
      };
    },
    styleOfButtonContainer() {
      return {
        borderRadius: this.borderRadius,
        padding: this.paddingDefault,
        border: this.borderDefault,
        cursor: this.disable ? "not-allowed" : "pointer",
        backgroundColor: this.backgroundColorEvent,
        color: this.disable ? "#9e9e9e" : "",
        fontWeight: this.fontWeight !== "" ? this.fontWeight : "",
      };
    },
    styleButtonDropdown() {
      return {
        padding: "0px 8px",
        width: "32px",
        height: "100%",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        borderLeft:
          this.type === "combo" && "1px solid var(--border-color-default)",
        borderTopRightRadius: "var(--btn-border-radius-default)",
        borderBottomRightRadius: "var(--btn-border-radius-default)",
      };
    },
    paddingDefault() {
      if (this.padding !== "") {
        return this.padding;
      } else if (
        this.type === "main" ||
        this.type === "text" ||
        this.type === "sub" ||
        this.type === "dropdown"
      ) {
        return "0px 16px";
      } else if (this.type === "icon") {
        return "0px 8px";
      } else if (this.type === "link" || this.type === "combo") {
        return "0px";
      } else {
        return "";
      }
    },
    borderDefault() {
      if (this.border !== "") {
        return this.border;
      } else if (
        this.type === "dropdown" ||
        this.type === "icon" ||
        this.type === "sub"
      ) {
        return "1px solid var(--border-color-default)";
      } else {
        return "";
      }
    },

    /**
     * tính background color tương ứng với: default, hover, disable
     * @author: TTANH (26/06/2023)
     */
    backgroundColorEvent() {
      if (this.type === "dropdown") {
        if (this.disable) {
          return "transparent";
        } else if (this.buttonHover) {
          return "#d2d3d6";
        } else {
          return "transparent";
        }
      } else {
        return "";
      }
    },

    styleDataDropdown() {
      return {
        bottom: this.dataDropdownTop ? "calc(100% + 4px)" : "",
        top: this.dataDropdown ? "unset" : "",
      };
    },
  },
};
</script>

<style scoped>
@import url(./button.css);
</style>
