<template>
  <div :style="styleContainer" class="input-container">
    <label
      :title="labelTooltip"
      v-if="labelText !== ''"
      :for="idInput"
      class="label-input"
      style="white-space: nowrap"
    >
      {{ labelText }}
      <div v-if="inputRequired" style="color: red; padding-left: 3px">*</div>
    </label>

    <div
      class="input-wrapper"
      :style="styleInputWrapper"
      @mouseenter="hoverInput = true"
      @mouseleave="hoverInput = false"
      @click="focus"
    >
      <input
        @focus="onFocusInput"
        @focusout="onUnFocusInput"
        @input="pickChangeValueInput"
        class="m-input"
        :class="disable ? 'input-disable' : ''"
        :id="idInput"
        ref="myInput"
        :disabled="disable"
        :style="styleInput"
        :value="inputMiddleFormated"
        :placeholder="placeholder"
        :tabindex="tabindex"
        v-TTANHBlackenOut
      />

      <div v-if="haveButtonFunction && !disable" class="input-function">
        <ttanh-icon
          @click="onClickUpIcon"
          :icon="'dropdown--solid-black'"
          style="margin-bottom: 2px; rotate: 180deg"
          scale="0.8"
          height="fit-content"
          width="fit-content"
        />
        <ttanh-icon
          @click="onClickDownIcon"
          scale="0.8"
          :icon="'dropdown--solid-black'"
          height="fit-content"
          width="fit-content"
        />
      </div>

      <ttanh-tooltip v-if="errorText !== '' && hoverInput">{{
        errorText
      }}</ttanh-tooltip>
    </div>
  </div>
</template>

<script>
import {
  formatNumberToPercent,
  formatPercentToNumber,
  countDecimalPlaces,
  plusPercent,
  minusPercent,
  formatNumberToMoney,
  formatMoneyToNumber,
  formatToNumber,
} from "@/helper/textfield-format-helper.js";

export default {
  name: "TTANHTextfield",
  data() {
    return {
      focusInput: false,
      hoverInput: false,
      inputMiddle: this.modelValue,
    };
  },
  props: {
    type: {
      default: "text",
      validator: function (val) {
        return ["percent", "money", "number_no_dot", "code", "text"].includes(
          val
        );
      },
    },
    /* bind 2 chiều */
    modelValue: {
      required: true,
    },
    idInput: {
      required: true,
      type: String,
    },
    disable: {
      default: false,
    },
    haveButtonFunction: {
      default: false,
    },
    labelText: {
      default: "",
    },
    inputRequired: {
      default: false,
    },
    placeholder: {
      default: "",
    },
    heightInput: {
      default: "var(--primary-button-height)",
    },
    widthInput: {
      default: "100%",
    },
    heightContainer: {
      default: "auto",
    },
    widthContainer: {
      default: "100%",
    },
    errorText: {
      default: "",
    },
    tabindex: {
      default: "0",
    },
    labelTooltip: {
      default: "",
    },
  },
  methods: {
    onFocusInput() {
      this.focusInput = true;
    },
    onUnFocusInput() {
      this.focusInput = false;
      this.$emit("blur-input"); //dùng để validate khi blur ra khỏi input
    },

    /**
     * xử lý sự kiện khi ấn vào nút icon tăng giá trị
     * @author: TTANH (27/06/2023)
     */
    onClickUpIcon() {
      try {
        if (this.type === "percent") {
          this.inputMiddle = formatPercentToNumber(
            plusPercent(this.inputMiddle)
          );
        } else if (this.type === "number_no_dot") {
          this.inputMiddle++;
        } else {
          //không thêm money vì ít ai cộng 1 giá trị
          console.log("function not work");
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTextfield.vue:132 ~ onClickUpIcon ~ error:",
          error
        );
      }
    },

    /**
     * xử lý sự kiện khi ấn vào nút icon giảm giá trị
     * @author: TTANH (27/06/2023)
     */
    onClickDownIcon() {
      try {
        if (this.type === "percent") {
          this.inputMiddle = formatPercentToNumber(
            minusPercent(this.inputMiddle)
          );
        } else if (this.type === "number_no_dot") {
          if (this.inputMiddle - 1 >= 0) {
            this.inputMiddle--;
          }
        } else {
          console.log("function not work");
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTextfield.vue:158 ~ onClickDownIcon ~ error:",
          error
        );
      }
    },

    /**
     * chọn hàm xử lý thay đổi cho từng type
     * @author: TTANH (28/06/2023)
     * @param {*} e event
     */
    pickChangeValueInput(e) {
      if (this.type === "percent") {
        return this.onChangeValueInputPercent(e);
      } else if (this.type === "money") {
        return this.onChangeValueInputMoney(e);
      } else if (this.type === "number_no_dot") {
        return this.onChangeValueInputNumberNoDot(e);
      } else if (this.type === "code") {
        return this.onChangeValueInputCode(e);
      } else if (this.type === "text") {
        return this.onChangeValueInputText();
      } else {
        return "";
      }
    },

    /**
     * thực hiện bỏ qua các nút và set lại vị trí focus hiện tại
     * @author: TTANH (30/06/2023)
     */
    skipKeypress(selStart) {
      try {
        let left = this.$refs.myInput.value.slice(0, selStart - 1);
        let right = this.$refs.myInput.value.slice(selStart);

        this.$refs.myInput.value = left + right;
        this.$refs.myInput.setSelectionRange(selStart - 1, selStart - 1);
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTextfield.vue:206 ~ skipKeypress ~ error:",
          error
        );
      }
    },

    /**
     * xử lý thay đổi input đối với type = 'percent'
     * @author: TTANH (28/06/2023)
     * @param {*} e event
     */
    onChangeValueInputPercent(e) {
      try {
        let selStart = this.$refs.myInput.selectionStart;

        //bỏ qua e vì trong number có số e
        if (e.data === " " || e.data === "e") {
          this.skipKeypress(selStart);
          return;
        }

        let temp = formatPercentToNumber(this.$refs.myInput.value);
        this.inputMiddle = formatPercentToNumber(this.$refs.myInput.value);

        this.$nextTick(() => {
          if (isNaN(temp) || temp >= 100 || countDecimalPlaces(temp) > 2) {
            this.$refs.myInput.setSelectionRange(selStart - 1, selStart - 1);
          } else {
            this.$refs.myInput.setSelectionRange(selStart, selStart);
          }
        });
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTextfield.vue:183 ~ onChangeValueInputPercent ~ error:",
          error
        );
      }
    },

    /**
     * xử lý thay đổi input đối với type = 'money'
     * @author: TTANH (28/06/2023)
     * @param {*} e event
     */
    onChangeValueInputMoney(e) {
      try {
        let selStart = this.$refs.myInput.selectionStart;

        if (e.data === "." || e.data === " " || e.data === "e") {
          this.skipKeypress(selStart);
          return;
        }

        for (let i = 0; i < this.$refs.myInput.value.length; i++) {
          if (isNaN(Number(this.$refs.myInput.value[i]))) {
            selStart--;
          }
        }

        this.inputMiddle = formatMoneyToNumber(this.$refs.myInput.value);

        this.$nextTick(() => {
          for (let i = 0; i < this.$refs.myInput.value.length; i++) {
            if (this.$refs.myInput.value[i] === ".") {
              selStart++;
            }
          }

          this.$refs.myInput.setSelectionRange(selStart, selStart);
        });
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTextfield.vue:242 ~ onChangeValueInputMoney ~ error:",
          error
        );
      }
    },

    /**
     * xử lý thay đổi input đối với type = 'number_no_dot'
     * @author: TTANH (28/06/2023)
     * @param {*} e event
     */
    onChangeValueInputNumberNoDot(e) {
      try {
        let selStart = this.$refs.myInput.selectionStart;

        if (e.data === "." || e.data === " " || e.data === "e") {
          this.skipKeypress(selStart);
          return;
        }

        for (let i = 0; i < this.$refs.myInput.value.length; i++) {
          if (isNaN(Number(this.$refs.myInput.value[i]))) {
            selStart--;
          }
        }

        this.inputMiddle = formatToNumber(this.$refs.myInput.value);

        this.$nextTick(() => {
          this.$refs.myInput.setSelectionRange(selStart, selStart);
        });
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTextfield.vue:285 ~ onChangeValueInputNumberNoDot ~ error:",
          error
        );
      }
    },

    /**
     * xử lý thay đổi input đối với type = 'code'
     * @author: TTANH (28/06/2023)
     * @param {*} e event
     */
    onChangeValueInputCode(e) {
      try {
        let selStart = this.$refs.myInput.selectionStart;

        if (e.data === "." || e.data === " " || e.data === ",") {
          this.skipKeypress(selStart);
          return;
        }

        this.inputMiddle = this.$refs.myInput.value.toUpperCase();

        this.$nextTick(() => {
          this.$refs.myInput.setSelectionRange(selStart, selStart);
        });
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTextfield.vue:322 ~ onChangeValueInputCode ~ error:",
          error
        );
      }
    },

    /**
     * xử lý thay đổi input đối với type = 'text'
     * @author: TTANH (28/06/2023)
     * @param {*} e event
     */
    onChangeValueInputText() {
      try {
        let selStart = this.$refs.myInput.selectionStart;

        this.inputMiddle = this.$refs.myInput.value;

        this.$nextTick(() => {
          this.$refs.myInput.setSelectionRange(selStart, selStart);
        });
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTextfield.vue:353 ~ onChangeValueInputText ~ error:",
          error
        );
      }
    },

    /**
     * hàm để thay đổi giá trị của inputMiddle từ cha
     * @author: TTANH (28/06/2023)
     * @param {*} newInputMiddle giá trị mới
     */
    onUpdateInputMiddle(newInputMiddle) {
      try {
        this.inputMiddle = newInputMiddle;
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTextfield.vue:384 ~ onUpdateInputMiddle ~ error:",
          error
        );
      }
    },

    //các hàm để sử dụng ở component cha bằng refs
    /**
     * lấy ra value hiện tại của input
     * @author: TTANH (01/07/2023)
     */
    getCurrentInputValue() {
      try {
        return this.$refs.myInput.value;
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTextfield.vue:400 ~ getCurrentInputValue ~ error:",
          error
        );
      }
    },

    /**
     * focus vào input chính
     * @author: TTANH (01/07/2023)
     */
    focus() {
      try {
        this.$refs.myInput.focus();
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTextfield.vue:388 ~ focusInput ~ error:",
          error
        );
      }
    },
  },

  computed: {
    styleContainer() {
      return {
        width: this.widthContainer,
        height: this.heightContainer,
      };
    },

    styleInputWrapper() {
      return {
        width: this.widthInput,
        height: this.heightInput,
        border: `1px solid ${this.borderInputColor}`,
      };
    },

    styleInput() {
      return {
        width: this.haveButtonFunction && !this.disable ? "90%" : "100%",
        paddingLeft: this.haveButtonFunction && !this.disable ? "" : "10px",
        paddingRight: this.haveButtonFunction && !this.disable ? "" : "10px",
        borderRadius: this.haveButtonFunction && !this.disable ? "" : "2px",
        textAlign: this.inputAlignStyle,
      };
    },

    borderInputColor() {
      if (this.errorText !== "") {
        return "red";
      } else if (this.focusInput) {
        return "var(--primary-btn--focus-background-color)";
      } else if (this.hoverInput) {
        return "var(--primary-btn--hover-background-color)";
      } else {
        return "var(--border-color-default)";
      }
    },

    inputAlignStyle() {
      if (
        this.type === "money" ||
        this.type === "percent" ||
        this.type === "number_no_dot"
      ) {
        return "end";
      } else if (this.type === "text" || this.type === "code") {
        return "start";
      } else {
        return "";
      }
    },

    /* giá trị để gán cho value của input sau khi đã được format */
    inputMiddleFormated() {
      if (this.type === "percent") {
        return formatNumberToPercent(this.inputMiddle);
      } else if (this.type === "money") {
        return formatNumberToMoney(this.inputMiddle);
      } else if (this.type === "code") {
        return this.inputMiddle.toUpperCase();
      } else if (this.type === "text") {
        return this.inputMiddle;
      } else {
        return this.inputMiddle;
      }
    },
  },
  watch: {
    /* kiểm tra điều kiện của inputMiddle với từng type */
    inputMiddle(newValue, oldValue) {
      if (
        this.type === "money" &&
        (isNaN(newValue) || newValue >= 999999999999999999)
      ) {
        this.inputMiddle = oldValue;
      } else if (
        this.type === "percent" &&
        (isNaN(newValue) || newValue >= 100 || countDecimalPlaces(newValue) > 2)
      ) {
        this.inputMiddle = oldValue;
      } else if (
        this.type === "number_no_dot" &&
        (isNaN(newValue) || newValue >= 999999999999999999)
      ) {
        this.inputMiddle = oldValue;
      } else {
        this.$emit("update:modelValue", newValue);
        this.$nextTick(() => {
          if (newValue === 0) {
            this.$refs.myInput.setSelectionRange(2, 2);
          }
        });
      }
    },
    modelValue() {
      this.inputMiddle = this.modelValue;
    },
  },
};
</script>

<style scoped>
@import url(./textfield.css);
</style>
