<template>
  <div class="m-overlay">
    <ttanh-popup
      :haveHeader="false"
      width="444px"
      height="auto"
      style="padding: 16px 20px 10px"
    >
      <template #content__input-control>
        <div class="input-container">
          <ttanh-icon height="auto" width="auto" icon="warning--large" />
          <span>{{ titleText }}</span>
        </div>
      </template>

      <template #footer>
        <ttanh-separation-line
          style="
            border-color: var(--border-color-default);
            margin: 16px 0px 10px;
          "
        />
        <div class="footer__button">
          <ttanh-button
            type="sub"
            width="72px"
            borderRadius="var(--border-radius-default)"
            padding="0px 12px"
            @clickBtnContainer="$emit('no-click')"
            @keydown="onKeyDownNoButton"
            tabindex="1000"
            >{{ $t("common.button.no") }}</ttanh-button
          >
          <ttanh-button
            type="main"
            width="50px"
            borderRadius="var(--border-radius-default)"
            padding="0px 16px"
            @clickBtnContainer="$emit('yes-click')"
            @keydown="onKeyDownYesButton"
            tabindex="1001"
            >{{ $t("common.button.yes") }}</ttanh-button
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
    titleText: {
      default: "",
    },
    errorText: {
      default: "",
    },
  },

  methods: {
    /**
     * xử lý sự kiện khi keydown khi đang focus vào "Không"
     * @author: TTANH (31/07/2024)
     * @param {*} event
     */
    onKeyDownYesButton(event) {
      try {
        if (event.keyCode === this.$_TTANHEnum.KEY_CODE.TAB && event.shiftKey) {
          event.preventDefault();
          document.querySelector('[tabindex="1001"]').focus();
        } else {
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHDeletePopup.vue:86 ~ onKeyDownCancelButton ~ error:",
          error
        );
      }
    },

    /**
     * xử lý sự kiện khi keydown khi đang focus vào "Có"
     * @author: TTANH (31/07/2024)
     * @param {*} event
     */
    onKeyDownYesButton(event) {
      try {
        if (event.keyCode === this.$_TTANHEnum.KEY_CODE.TAB) {
          event.preventDefault();
          document.querySelector('[tabindex="1000"]').focus();
        } else {
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHDeletePopup.vue:86 ~ onKeyDownCancelButton ~ error:",
          error
        );
      }
    },
  },
};
</script>

<style scoped>
@import url(./delete-popup.css);
</style>
