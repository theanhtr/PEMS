<template>
  <div :style="styleContainer" class="combobox" ref="combobox">
    <label
      v-if="labelText !== ''"
      @click="showComboboxData"
      class="label-input"
    >
      {{ labelText }}
      <div v-if="inputRequired" style="color: red; padding-left: 3px">*</div>
    </label>

    <div
      @mouseenter="hoverInput = true"
      @mouseleave="hoverInput = false"
      class="combobox__input-container"
      :style="styleInputWrapper"
      @click="showComboboxData"
    >
      <input
        ref="inputSearch"
        :placeholder="placeholder"
        type="text"
        class="combobox__input"
        @input="changeInput"
        @keydown="handleInputKeydown"
        @focus="focusInput = true"
        :tabindex="tabindex"
        :disabled="disableInput"
        v-TTANHBlackenOut
      />
      <ttanh-icon
        @mouseenter="hoverDropdownIcon = true"
        @mouseleave="hoverDropdownIcon = false"
        @click="clickDropdownIcon"
        :style="styleDropdownIcon"
        icon="dropdown--solid-black"
        scale="0.8"
        height="99%"
      />

      <ttanh-tooltip v-if="errorText !== '' && hoverInput">{{
        errorText
      }}</ttanh-tooltip>
    </div>
    <div v-show="isShowComboboxData" class="combobox__data" ref="comboboxData">
      <div v-if="rowsData.length === 0" class="item--no-data">
        Không có dữ liệu để hiển thị.
      </div>
      <ttanh-table
        v-else-if="type === 'table'"
        :columnsInfo="columnsInfo"
        :rowsData="computedRowsDataFilter"
        :oneRowSelect="true"
        @clickRow="selectValue"
        :selectedRows="selectedRows"
      />
      <div v-else-if="type === 'single-row'" class="combobox__data-single-row">
        <div
          v-for="row in computedRowsDataFilter"
          :key="row.id"
          class="combobox__value"
          :class="
            selectedRows.includes(row.id) ? 'combobox__value--selected' : ''
          "
          @click="selectValue(row.id)"
        >
          {{ row.name }}
        </div>
      </div>
    </div>
    <div name="combobox__add-popup">
      <slot name="combobox__add-popup"></slot>
    </div>
  </div>
</template>

<script>
export default {
  name: "TTANHCombobox",
  props: {
    /**
     * thông tin cột phải theo định dạn
        {
          id: "EmployeeCode",
          name: "Mã nhân viên",
          size: "150px",
          textAlign: "left", //bao gồm: left, center, right
          format: "text", //bao gồm: text, date, currency
          isShow: true,
          isPin: false,
        },
     * @author: TTANH (30/06/2024)
     */
    columnsInfo: {
      default: [],
      type: Array,
    },

    /**
     * tất cả các record trong rowsData đều phải có id, name
     * và code, nếu không có code thì gán code bằng name
     */
    rowsData: {
      default: [],
      required: true,
      type: Array,
    },

    /* model value là giá trị id của đối tượng mình chọn */
    modelValue: {
      default: "",
      required: true,
      type: String,
    },

    /* single row: chỉ có dòng chứ không phải table */
    type: {
      default: "single-row",
      validator: function (val) {
        return ["single-row", "table", "multi-select"].includes(val);
      },
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
    disableInput: {
      default: false,
    },
  },
  data() {
    return {
      focusInput: false,
      hoverInput: false,
      hoverDropdownIcon: false,

      isShowComboboxData: false,

      /* các row được sắp xếp lại phù hợp với giá trị tìm kiếm */
      rowsDataFilter: this.rowsData,

      /**
       * length = 2: giá trị 1 là giá trị tìm kiếm được, giá trị 2 là giá trị đã chọn
       * length = 1: giá trị đã chọn
       * khi ấn tab sẽ chọn giá trị 1
       */
      selectedRows: [this.modelValue],

      indexHover: -1,
    };
  },

  mounted() {
    this.rowsDataFilter = this.rowsData;
  },

  methods: {
    /**
     * hiển thị combobox data, focus vào input và thêm sự kiện click bên ngoài combobox
     * @author: TTANH (30/06/2024)
     */
    showComboboxData() {
      try {
        this.$refs.inputSearch.focus();
        this.focusInput = true;
        this.isShowComboboxData = true;
        this.indexHover = -1;
        this.$refs.comboboxData.scrollTo(0, 0);

        window.addEventListener("click", this.clickOutSideCombobox);
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHCombobox.vue:172 ~ showComboboxData ~ error:",
          error
        );
      }
    },

    /**
     * ẩn combobox data, xóa sự kiện click bên ngoài combobox
     * @author: TTANH (30/06/2024)
     */
    hiddenComboboxData() {
      try {
        this.$refs.inputSearch.blur();
        this.focusInput = false;
        this.isShowComboboxData = false;

        window.removeEventListener("click", this.clickOutSideCombobox);

        this.$emit("blur-input"); //dùng để validate khi blur ra khỏi input
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHCombobox.vue:190 ~ hiddenComboboxData ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi ấn vào nút dropdown cạnh input
     * @author: TTANH (30/06/2024)
     * @param {*} event
     */
    clickDropdownIcon(event) {
      try {
        event.stopPropagation();

        if (this.isShowComboboxData) {
          this.hiddenComboboxData();
        } else {
          this.showComboboxData();
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHCombobox.vue:210 ~ clickDropdownIcon ~ error:",
          error
        );
      }
    },

    /**
     * gán value của input bằng tên của row đã được chọn
     * @author: TTANH (30/06/2024)
     * @param {string} id
     */
    setValueInput(id = "") {
      try {
        if (id === null || id === "") {
          return;
        }

        for (let i = 0; i < this.rowsData.length; i++) {
          if (this.rowsData[i].id === id) {
            if (this.$refs.inputSearch) {
              this.$refs.inputSearch.value = this.rowsData[i].name;
            }
            break;
          }
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHCombobox.vue:229 ~ setValueInput ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi chọn 1 dòng trong combobox data
     * @author: TTANH (30/06/2024)
     * @param {string} id id của dòng được chọn
     */
    selectValue(id) {
      try {
        this.setValueInput(id);
        this.$emit("update:modelValue", id);

        this.hiddenComboboxData();
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHCombobox.vue:249 ~ selectValue ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi thay đổi giá trị input
     * @author: TTANH (30/06/2024)
     */
    changeInput() {
      try {
        this.$emit("update:modelValue", "");
        this.$refs.comboboxData.scrollTo(0, 0);

        // xử lý cho việc được focus input bằng tab
        if (!this.isShowComboboxData) {
          this.isShowComboboxData = true;
        }

        let valueInputSearch = this.$refs.inputSearch.value.trim();
        let rowsDataLength = this.rowsData.length;
        let indexFinded = -1;
        let selectedRowsLength = this.selectedRows.length;
        this.indexHover = -1;

        // so sánh input value với code và name của từng row data
        for (let i = 0; i < rowsDataLength; i++) {
          if (
            (this.rowsData[i].code
              .toLowerCase()
              .includes(valueInputSearch.toLowerCase()) ||
              this.rowsData[i].name
                .toLowerCase()
                .includes(valueInputSearch.toLowerCase())) &&
            valueInputSearch !== ""
          ) {
            indexFinded = i;
            break;
          }
        }

        this.selectedRows[selectedRowsLength - 1] = "";
        // xử lý việc cho hàng được tìm thấy lên đầu và thay đổi background color
        if (selectedRowsLength > 1) {
          this.selectedRows.shift();
        }

        if (indexFinded !== -1) {
          this.rowsDataFilter = [
            ...this.rowsData.slice(0, indexFinded),
            ...this.rowsData.slice(indexFinded + 1, rowsDataLength),
          ];

          this.rowsDataFilter.unshift(this.rowsData[indexFinded]);

          this.selectedRows.unshift(this.rowsData[indexFinded].id);
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHCombobox.vue:266 ~ changeInput ~ error:",
          error
        );
      }
    },

    /**
     * hàm để check sự kiện clickoutside
     * @author: TTANH (30/06/2024)
     * @param {*} event
     */
    clickOutSideCombobox(event) {
      try {
        if (this.$refs.combobox) {
          if (!this.$refs.combobox.contains(event.target)) {
            this.hiddenComboboxData();
          }
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHCombobox.vue:246 ~ clickOutSideCombobox ~ error:",
          error
        );
      }
    },

    //các hàm để sử dụng ở component cha bằng refs
    /**
     * lấy ra value hiện tại của input
     * @author: TTANH (01/07/2024)
     */
    getCurrentInputValue() {
      try {
        return this.$refs.inputSearch.value;
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHCombobox.vue:167 ~ getCurrentInputValue ~ error:",
          error
        );
      }
    },

    /**
     * focus vào input chính
     * @author: TTANH (01/07/2024)
     */
    focus() {
      try {
        this.$refs.inputSearch.focus();
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTextfield.vue:388 ~ focusInput ~ error:",
          error
        );
      }
    },

    /**
     * truy cập vào input từ cha
     * @author: TTANH (01/07/2024)
     */
    getInputRef() {
      try {
        return this.$refs.inputSearch;
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHCombobox.vue:373 ~ getInputRef ~ error:",
          error
        );
      }
    },

    /**
     * xử lý sự kiện key down combobox
     * @author: TTANH (03/07/2024)
     */
    handleInputKeydown(event) {
      try {
        if (
          event.keyCode === this.$_TTANHEnum.KEY_CODE.TAB ||
          event.keyCode === this.$_TTANHEnum.KEY_CODE.ENTER
        ) {
          this.setValueInput(this.selectedRows[0]);
          this.hiddenComboboxData();

          this.$emit("update:modelValue", this.selectedRows[0]);
        } else if (event.keyCode === this.$_TTANHEnum.KEY_CODE.ARROW_UP) {
          if (this.indexHover > 0) {
            this.indexHover--;
          }
        } else if (event.keyCode === this.$_TTANHEnum.KEY_CODE.ARROW_DOWN) {
          if (this.indexHover < this.computedRowsDataFilter.length - 1) {
            this.indexHover++;
          }
        }

        if (
          event.keyCode === this.$_TTANHEnum.KEY_CODE.ARROW_UP ||
          event.keyCode === this.$_TTANHEnum.KEY_CODE.ARROW_DOWN
        ) {
          this.$refs.comboboxData.scrollTo(0, this.indexHover * 25);

          this.selectedRows = [this.computedRowsDataFilter[this.indexHover].id];

          this.setValueInput(this.computedRowsDataFilter[this.indexHover].id);

          if (!this.isShowComboboxData) {
            this.showComboboxData();
          }
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHCombobox.vue:389 ~ handleInputKeydown ~ error:",
          error
        );
      }
    },

    /**
     * lấy dữ liệu đang được chọn
     * @author: TTANH (11/07/2024)
     */
    getIdSelectedData() {
      return this.selectedRows[0];
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

    styleDropdownIcon() {
      return {
        rotate: this.isShowComboboxData ? "180deg" : "0deg",
        backgroundColor: this.hoverDropdownIcon ? "#e0e0e0" : "inherit",
      };
    },

    /**
     * thực hiện đưa hàng đã chọn lên đầu
     */
    computedRowsDataFilter() {
      let rowsDataLength = this.rowsData.length;
      let indexFinded = -1;

      for (let i = 0; i < rowsDataLength; i++) {
        if (this.rowsData[i].id === this.modelValue) {
          indexFinded = i;
          break;
        }
      }

      if (indexFinded !== -1) {
        this.rowsDataFilter = [
          ...this.rowsData.slice(0, indexFinded),
          ...this.rowsData.slice(indexFinded + 1, rowsDataLength),
        ];

        this.rowsDataFilter.unshift(this.rowsData[indexFinded]);
      }

      return this.rowsDataFilter;
    },
  },
  watch: {
    rowsData() {
      this.rowsDataFilter = this.rowsData;
    },
    rowsDataFilter() {
      this.setValueInput(this.modelValue);
    },
    modelValue() {
      this.selectedRows = [this.modelValue];
    },
  },
};
</script>

<style scoped>
@import url(./combobox.css);
</style>
