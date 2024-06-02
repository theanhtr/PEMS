<template>
  <div
    class="m-checkbox"
    :style="styleCheckbox"
    @click="!disable ? clickCheckbox($event) : ''"
    :class="{ 'animate-checked': checked, 'animate-unchecked': !checked }"
  >
    <ttanh-icon
      :style="{ cursor: !this.disable ? 'pointer' : 'default' }"
      v-if="checked"
      icon="checkbox-icon"
    />
  </div>
</template>

<script>
export default {
  name: "TTANHCheckboxInput",
  props: {
    isCheck: {
      default: false,
    },
    disable: {
      default: false,
    },
  },
  data() {
    return { checked: this.isCheck };
  },
  methods: {
    /**
     * tạo hiệu ứng khi người dùng click vào m-checkbox,
     * đồng thời gửi emit checked và unchecked lên parent component
     * @author: TTANH (26/06/2023)
     */
    clickCheckbox(event) {
      try {
        event.stopPropagation();

        if (!this.checked) {
          this.checked = true;
          this.$emit("checked");
        } else {
          this.checked = false;
          this.$emit("unchecked");
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHCheckboxInput.vue:23 ~ clickCheckbox ~ error:",
          error
        );
      }
    },
  },
  computed: {
    styleCheckbox() {
      return {
        border: this.checked
          ? "1px solid var(--control-border-color--active)"
          : "1px solid var(--border-color-default)",

        backgroundColor: !this.disable ? "" : "antiquewhite",

        cursor: !this.disable ? "pointer" : "default",
      };
    },
  },
  watch: {
    isCheck(newValue) {
      this.checked = newValue;
    },
  },
};
</script>

<style scoped>
@import url(./checkbox-input.css);
</style>
