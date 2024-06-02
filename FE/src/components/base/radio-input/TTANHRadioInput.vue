<template>
  <div :style="styleContainer" class="radio-input__container">
    <label
      v-if="labelText !== ''"
      :style="{ margin: marginLabel }"
      class="radio-input__label"
    >
      {{ labelText }}
      <div v-if="inputRequired" style="color: red; padding-left: 3px">*</div>
    </label>

    <div class="radio-input__group" :style="styleRadioGroup">
      <label
        v-for="value in values"
        :key="value.id"
        class="radio-input__ele-container"
      >
        <input
          type="radio"
          :id="value.id"
          :name="nameRadioGroup"
          :value="value.id"
          @change="changeValueSelect(value.id)"
          @keydown="onKeyDownButton($event, value.id)"
          :checked="modelValue === value.id"
          :tabindex="tabindex"
          class="ele__input"
        />
        <span class="ele__checkmark"></span>
        {{ value.name }}
      </label>
    </div>
  </div>
</template>

<script>
export default {
  name: "TTANHRadioInput",
  props: {
    /**
     * thông tin 1 value phải theo định dạng
        {
          id: "gd1",
          name: "Nam",
        },
     * @author: TTANH (29/06/2023)
     */
    values: {
      required: true,
      type: Array,
    },

    modelValue: {
      required: true,
    },
    align: {
      default: "row",
      validator: function (val) {
        return ["row", "column"].includes(val);
      },
    },
    nameRadioGroup: {
      required: true,
      type: String,
    },
    labelText: {
      default: "",
    },
    inputRequired: {
      default: false,
    },
    heightRadioGroup: {
      default: "var(--primary-button-height)",
    },
    widthRadioGroup: {
      default: "100%",
    },
    heightContainer: {
      default: "auto",
    },
    widthContainer: {
      default: "100%",
    },
    error: {
      default: false,
    },
    tabindex: {
      default: "0",
    },
    marginLabel: {
      default: "",
    },
  },
  methods: {
    /**
     * thay đổi giá trị được chọn
     * @param {*} newValueSelect giá trị id mới được truyện vào
     */
    changeValueSelect(newValueSelect) {
      try {
        this.$emit("update:modelValue", newValueSelect);
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHRadioInput.vue:80 ~ changeValueSelect ~ error:",
          error
        );
      }
    },

    /**
     * xử lý sự kiện khi keydown
     * @author: TTANH (10/07/2023)
     * @param {*} event
     */
    onKeyDownButton(event, valueId) {
      try {
        if (event.keyCode === this.$_TTANHEnum.KEY_CODE.ENTER) {
          this.changeValueSelect(valueId);
        } else {
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHRadioInput.vue:112 ~ onKeyDownButton ~ error:",
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
    styleRadioGroup() {
      return {
        flexDirection: this.align,
        alignItems: this.align === "row" ? "center" : "start",
        columnGap: "10px",
        rowGap: "20px",
        height: this.heightRadioGroup,
        width: this.widthRadioGroup,
      };
    },
  },
};
</script>

<style scoped>
@import url(./radio-input.css);
</style>
