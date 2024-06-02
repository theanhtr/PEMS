<template>
  <div class="m-overlay">
    <ttanh-popup
      :haveHeader="false"
      width="444px"
      height="auto"
      style="padding: 16px 20px 10px"
    >
      <template #content__input-control>
        <div
          style="
            display: flex;
            align-items: center;
            column-gap: 26px;
            padding-top: 8px;
          "
        >
          <ttanh-icon height="auto" width="auto" icon="error--medium" />
          <span>{{ errorText }}</span>
        </div>
      </template>

      <template #footer>
        <ttanh-separation-line
          style="
            border-color: var(--border-color-default);
            margin: 16px 0px 10px;
          "
        />
        <div style="width: 100%; display: flex; justify-content: center">
          <ttanh-button
            type="main"
            width="58px"
            borderRadius="var(--border-radius-default)"
            padding="0px 12px"
            @clickBtnContainer="$emit('close-click')"
            tabindex="1000"
            @keydown="onKeyDownCloselButton"
            >{{ $t("common.button.close") }}</ttanh-button
          >
        </div>
      </template>
    </ttanh-popup>
  </div>
</template>

<script>
export default {
  name: "TTANHErrorPopup",

  mounted() {
    document.querySelector('[tabindex="1000"]').focus();
  },

  props: {
    errorText: {
      default: "",
    },
  },

  methods: {
    /**
     * xử lý sự kiện khi keydown khi đang focus vào "Đóng"
     * @author: TTANH (22/07/2023)
     * @param {*} event
     */
    onKeyDownCancelButton(event) {
      try {
        if (event.keyCode === this.$_TTANHEnum.KEY_CODE.TAB) {
          event.preventDefault();
          document.querySelector('[tabindex="1000"]').focus();
        } else {
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHErrorPopup.vue:74 ~ onKeyDownCancelButton ~ error:",
          error
        );
      }
    },
  },
};
</script>

<style scoped>
@import url(./error-popup.css);
</style>
