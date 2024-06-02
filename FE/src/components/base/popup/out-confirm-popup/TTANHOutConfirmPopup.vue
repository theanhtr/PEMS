<template>
  <div class="m-overlay">
    <ttanh-popup
      :haveHeader="false"
      width="444px"
      height="auto"
      style="padding: 16px 4px 10px"
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
          <ttanh-icon
            style="cursor: default; padding-left: 6px"
            height="auto"
            width="auto"
            icon="question--large"
          />
          <span>{{ $t("component.popup.outConfirmPopupTitle") }}</span>
        </div>
      </template>

      <template #footer>
        <ttanh-separation-line
          style="
            border-color: var(--border-color-default);
            margin: 20px 0px 10px;
          "
        />
        <div class="footer__button">
          <div class="footer__button-left">
            <ttanh-button
              type="sub"
              width="58px"
              borderRadius="var(--border-radius-default)"
              padding="0px 12px"
              @clickBtnContainer="$emit('cancel-click')"
              tabindex="999"
              ref="CancelButtonRef"
              @keydown="onKeyDownCancelButton"
              >{{ $t("common.button.cancel") }}</ttanh-button
            >
          </div>
          <div class="footer__button-right">
            <ttanh-button
              type="sub"
              width="76px"
              borderRadius="var(--border-radius-default)"
              padding="0px 18px"
              @clickBtnContainer="$emit('no-click')"
              tabindex="1000"
              >{{ $t("common.button.no") }}</ttanh-button
            >
            <ttanh-button
              type="main"
              width="48px"
              borderRadius="var(--border-radius-default)"
              padding="0px 16px"
              tabindex="1001"
              @clickBtnContainer="$emit('yes-click')"
              @keydown="onKeyDownYesButton"
              >{{ $t("common.button.yes") }}</ttanh-button
            >
          </div>
        </div>
      </template>
    </ttanh-popup>
  </div>
</template>

<script>
export default {
  name: "TTANHOutConfirmPopup",

  mounted() {
    document.querySelector('[tabindex="1001"]').focus();
  },

  methods: {
    /**
     * xử lý sự kiện khi keydown khi đang focus vào "Hủy"
     * @author: TTANH (22/07/2023)
     * @param {*} event
     */
    onKeyDownCancelButton(event) {
      try {
        if (event.keyCode === this.$_TTANHEnum.KEY_CODE.TAB && event.shiftKey) {
          event.preventDefault();
          document.querySelector('[tabindex="1001"]').focus();
        } else {
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHOutConfirmPopup.vue:100 ~ onKeyDownCancelButton ~ error:",
          error
        );
      }
    },

    /**
     * xử lý sự kiện khi keydown khi đang focus vào "Có"
     * @author: TTANH (22/07/2023)
     * @param {*} event
     */
    onKeyDownYesButton(event) {
      try {
        if (event.keyCode === this.$_TTANHEnum.KEY_CODE.TAB && !event.shiftKey) {
          event.preventDefault();
          document.querySelector('[tabindex="999"]').focus();
        } else {
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHOutConfirmPopup.vue:118 ~ onKeyDownYesButton ~ error:",
          error
        );
      }
    },
  },
};
</script>

<style scoped>
@import url(./out-confirm-popup.css);
</style>
