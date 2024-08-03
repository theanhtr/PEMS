<template>
  <table class="m-table">
    <thead>
      <tr>
        <th
          @dblclick="preventDoubleClick"
          v-if="!oneRowSelect"
          class="m-table__checkbox-column"
        >
          <template v-if="rowsData.length === 0">
            <ttanh-loading-skeleton />
          </template>
          <template v-else>
            <ttanh-checkbox-input
              @checked="$emit('checked-all')"
              @unchecked="$emit('unchecked-all')"
              :isCheck="checkSelectedAll"
            />
          </template>
        </th>
        <div class="m-tbody-viewer">
          <th
            v-for="(column, index) in columnsShow"
            :key="index"
            :style="styleTh(column, index)"
          >
            <div class="m-th" :class="`text-align--${column.textAlign}`">
              <div
                class="m-th__text"
                :style="{ cursor: !oneRowSelect ? 'pointer' : 'default' }"
                :title="column.tooltip ?? ''"
              >
                {{ column.name }}
              </div>
            </div>
            <div
              class="th__resize"
              @mousedown="mouseDownResizeColumn($event, index)"
            ></div>
          </th>
        </div>
        <th
          v-if="!oneRowSelect"
          style="text-align: end"
          class="m-table__row-function"
        >
          <span style="padding-right: 6px">{{
            $t("component.table.function")
          }}</span>
        </th>
      </tr>
    </thead>
    <tbody v-if="!noData">
      <tr
        v-for="(row, index) in rowsLoad"
        :key="index"
        :class="this.selectedRows.includes(row.id) ? 'row--selected' : ''"
        @click="clickRow(row.id)"
        @dblclick="this.$emit('doubleClickRow', row.id)"
        style="cursor: pointer"
      >
        <td
          @dblclick="preventDoubleClick"
          v-if="!oneRowSelect"
          class="m-table__checkbox-column"
          :style="{
            backgroundColor:
              this.rowIsFocus == row.id
                ? 'var(--grid-body__line-focus-background-color) !important'
                : '',
          }"
        >
          <template v-if="rowsData.length === 0">
            <ttanh-loading-skeleton />
          </template>
          <template v-else>
            <ttanh-checkbox-input
              @checked="checkedRow(row.id)"
              @unchecked="uncheckedRow(row.id)"
              :isCheck="this.selectedRows.includes(row.id) ? true : false"
            />
          </template>
        </td>
        <td
          v-for="(column, index) in columnsShow"
          :key="index"
          :class="`text-align--${column.textAlign}`"
          :title="row[column.id]"
          :style="styleTd(column, index, row.id)"
        >
          <template v-if="rowsData.length === 0">
            <ttanh-loading-skeleton />
          </template>
          <template v-else-if="column.format === 'valid-check'">
            <div
              v-if="row[column.id] === $_TTANHEnum.RECORD_CHECK.VALID"
              style="color: var(--tab__item--active-text-color)"
            >
              {{ $t("component.table.valid") }}
            </div>
            <div v-else style="color: #ff0404">
              {{ $t("component.table.invalid") }}
            </div>
          </template>
          <template v-else-if="column.format === 'checkbox'">
            <ttanh-checkbox-input
              @checked="row[column.id] = true"
              @unchecked="row[column.id] = false"
              :isCheck="row[column.id]"
              :disable="row.isCannotChangeChecked"
              style="margin: 0 auto"
            />
          </template>
          <template v-else-if="column.format === 'input-combobox'">
            <ttanh-combobox
              v-model="row[column.id]"
              type="single-row"
              :rowsData="column.comboboxRowData"
              v-if="this.rowIsFocus == row.id"
            />

            <div v-else>{{ nameOfSelectValueCombobox(row[column.id], column.comboboxRowData) }}</div>
          </template>
          <template v-else-if="column.format === 'input-text'">
            <ttanh-textfield
              v-model="row[column.id]"
              type="text"
              v-if="this.rowIsFocus == row.id"
            />
            <div v-else>{{ row[column.id] }}</div>
          </template>
          <template v-else-if="column.format === 'input-number_no_dot'">
            <ttanh-textfield
              v-model="row[column.id]"
              type="number_no_dot"
              v-if="this.rowIsFocus == row.id"
            />
            <div v-else>{{ row[column.id] }}</div>
          </template>
          <template v-else>
            {{ formatData(row[column.id], column.format) }}
          </template>
        </td>
        <td
          v-if="!oneRowSelect"
          class="m-table__row-function"
          :style="{
            backgroundColor:
              this.rowIsFocus == row.id
                ? 'var(--grid-body__line-focus-background-color) !important'
                : '',
            overflow: 'visible',
          }"
          @dblclick="
            (event) => {
              event.stopPropagation();
            }
          "
        >
          <template v-if="rowsData.length === 0">
            <ttanh-loading-skeleton />
          </template>
          <template v-else>
            <div class="tbody__row-function">
              <ttanh-button
                type="link"
                colorText="#0075c0"
                style="
                  font-weight: 600;
                  height: 100%;
                  padding: 6px 1px 6px 16px;
                "
                @clickBtnContainer="$emit('clickFixBtn', row.id)"
                >{{ $t("common.button.edit") }}</ttanh-button
              >

              <ttanh-button
                type="icon"
                width="auto"
                height="20px"
                borderRadius="var(--border-radius-default)"
                padding="0px"
                :border="'none'"
                icon="dropdown--solid-blue"
                :tabindex="-1"
                @click="clickButtonDropdownFunctionContext($event, row.id)"
                :ref="`refDropdownBtn-${row.id}`"
              />
            </div>
          </template>
        </td>
      </tr>
    </tbody>

    <tbody class="m-table__body--no-data" v-else>
      <img src="../../../assets/img/no_data.png" />
      <p>{{ $t("component.table.noData") }}</p>
    </tbody>

    <div
      class="function__context"
      v-show="isShowFunctionContext"
      :style="styleFunctionContext"
      ref="refFunctionContext"
    >
      <div @click="clickDuplicateBtn" class="function__item">
        {{ $t("component.table.editData.copy") }}
      </div>
      <div @click="clickDeleteBtn" class="function__item">
        {{ $t("component.table.editData.delete") }}
      </div>
      <div class="function__item">
        {{ $t("component.table.editData.stopUsing") }}
      </div>
    </div>
  </table>
</template>

<script>
import {
  formatToCurrency,
  formatToDate,
  formatToGenderName,
} from "@/helper/format-helper.js";
import { findIndexByAttribute } from "@/helper/common.js";

export default {
  name: "TTANHTable",
  data() {
    return {
      leftFunctionContext: "0px",
      topFunctionContext: "0px",
      idFunctionContextFocus: "",
      isShowFunctionContext: false,

      //các biến dùng cho việc resize
      prevX: 0,
      rowIndexResize: -1,

      // để xử lý khi chọn 1 dòng
      rowIsFocus: "",

      fix: "",
    };
  },
  props: {
    /**
     * thông tin cột phải theo định dạn
        {
          id: "EmployeeCode",
          name: "Mã nhân viên",
          size: "150px",
          textAlign: "left", // bao gồm: left, center, right
          format: "text", // bao gồm: text, date, currency,
                          // checkbox, input-combobox, input-text, input-number_no_dot, valid-check
                          //  + với checkbox thì "có thể" thêm isCannotChangeChecked để không cho thay đổi checkbox input ở trường đó
                          //  + với input-combobox thì "phải" có thêm comboboxRowData
                          //  + với valid-check chỉ chấp nhận _TTANHEnum.RECORD_CHECK
          isShow: true,
          isPin: false,
        },
     * @author: TTANH (26/06/2024)
     */
    columnsInfo: {
      required: true,
      type: Array,
    },
    /*
     * tất cả các record đều phải có id

     */
    rowsData: {
      default: [],
      required: true,
      type: Array,
    },
    /* id các records đã được chọn */
    selectedRows: {
      default: [],
    },

    /**
     * sử dụng cho table trong combobox chỉ được chọn 1 giá trị
     * và không có nút checkbox và nút sửa
     */
    oneRowSelect: {
      default: false,
    },

    noData: {
      default: false,
    },
  },
  methods: {
    /**
     * xử lý khi checked một dòng
     * @param {string} rowId id của dòng
     * @author: TTANH (30/07/2024)
     */
    checkedRow(rowId) {
      this.rowIsFocus = rowId;
      this.$emit("checked-row", rowId);
    },

    /**
     * xử lý khi unchecked một dòng
     * @param {string} rowId id của dòng
     * @author: TTANH (30/07/2024)
     */
    uncheckedRow(rowId) {
      this.rowIsFocus = rowId;
      this.$emit("unchecked-row", rowId);
    },

    /**
     * hàm bỏ hành vi double click mặc định và ngăn ko cho ảnh hưởng lên cha
     * @param {*} event
     * @author: TTANH (30/07/2024)
     */
    preventDoubleClick(event) {
      event.preventDefault();
      event.stopPropagation();
    },

    /**
     * format dữ liệu tùy theo loại cần format
     * @author: TTANH (26/06/2024)
     * @param {*} valueRaw dữ liệu cần format
     * @param {string} type loại format: "currency", "gender", "date", ""
     * @returns trả về dữ liệu đã được format
     */
    formatData(valueRaw, type = "") {
      try {
        switch (type) {
          case "currency":
            return formatToCurrency(valueRaw);
            break;

          case "date":
            return formatToDate(valueRaw, this.$store.state.formatDate);
            break;

          case "gender":
            return formatToGenderName(valueRaw);
            break;

          default:
            return valueRaw;
            break;
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTable.vue:151 ~ formatData ~ error:",
          error
        );
      }
    },

    /**
     * mở function context
     * @author: TTANH (01/07/2024)
     * @param {*} event
     * @param {string} idFocus id của dòng chứa nút sửa
     */
    clickButtonDropdownFunctionContext(event, idFocus) {
      try {
        let heightButtonTarget = event.currentTarget.offsetHeight;
        let widthButtonTarget = event.currentTarget.offsetWidth;
        this.rowIsFocus = idFocus;

        if (idFocus !== this.idFunctionContextFocus) {
          //trừ đi kích thước của functionContext
          event.preventDefault();
          event.stopPropagation();
          this.leftFunctionContext =
            event.x + widthButtonTarget - event.layerX - 118 + "px";
          this.topFunctionContext =
            event.y + heightButtonTarget - event.layerY + 1 + "px";
          this.isShowFunctionContext = true;
          this.idFunctionContextFocus = idFocus;

          window.addEventListener("click", this.clickOutSideFunctionContext);
        } else {
          this.closeFunctionContext();
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTable.vue:204 ~ openFunctionContext ~ error:",
          error
        );
      }
    },

    /**
     * đóng function context
     * @author: TTANH (01/07/2024)
     */
    closeFunctionContext() {
      try {
        this.isShowFunctionContext = false;
        this.idFunctionContextFocus = "";
        window.removeEventListener("click", this.clickOutSideFunctionContext);
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTable.vue:240 ~ closeFunctionContext ~ error:",
          error
        );
      }
    },

    /**
     * hàm để check sự kiện clickoutside functionContext
     * @author: TTANH (01/07/2024)
     * @param {*} event
     */
    clickOutSideFunctionContext(event) {
      try {
        if (
          this.$refs.refFunctionContext &&
          this.$refs[`refDropdownBtn-${this.idFunctionContextFocus}`][0]
        ) {
          if (
            !this.$refs.refFunctionContext.contains(event.target) &&
            !this.$refs[`refDropdownBtn-${this.idFunctionContextFocus}`][0]
              .getBtnContainerRef()
              .contains(event.target)
          ) {
            this.closeFunctionContext();
          }
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTable.vue:258 ~ clickOutSideFunctionContext ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi ấn vào nút Nhân bản ở function context
     * @author: TTANH (01/07/2024)
     */
    clickDuplicateBtn() {
      try {
        this.$emit("clickContextDuplicateBtn", this.idFunctionContextFocus);
        this.closeFunctionContext();
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTable.vue:376 ~ clickDuplicateBtn ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi ấn vào nút xóa ở function context
     * @author: TTANH (01/07/2024)
     */
    clickDeleteBtn() {
      try {
        this.$emit("clickContextDeleteBtn", this.idFunctionContextFocus);
        this.closeFunctionContext();
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTable.vue:294 ~ clickDeleteBtn ~ error:",
          error
        );
      }
    },

    /**
     * thực hiện sự kiện mouse down khi resize
     * @author: TTANH (04/07/2024)
     * @param {*} event
     */
    mouseDownResizeColumn(event, index) {
      try {
        this.prevX = event.clientX;
        this.rowIndexResize = index;

        window.addEventListener("mousemove", this.mouseMoveResizeColumn);
        window.addEventListener("mouseup", this.mouseUpResizeColumn);
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTable.vue:306 ~ mouseDownResizeColumn ~ error:",
          error
        );
      }
    },

    /**
     * thực hiện sự kiện mouse move khi resize
     * @author: TTANH (04/07/2024)
     * @param {*} event
     */
    mouseMoveResizeColumn(event) {
      try {
        if (this.rowIndexResize !== -1) {
          const currentColumnReizer = this.columnsShow[this.rowIndexResize];
          const currentWidth = currentColumnReizer.size;

          let resizeWidth = currentWidth + (event.clientX - this.prevX);
          this.prevX = event.clientX;

          this.$emit("resizeColumn", this.rowIndexResize, resizeWidth);
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHTable.vue:324 ~ mouseMoveResizeColumn ~ error:",
          error
        );
      }
    },

    /**
     * xử lý sự kiện mouse up
     * @author: TTANH (04/07/2024)
     * @param {*} event
     */
    mouseUpResizeColumn() {
      this.rowIndexResize = -1;
      window.removeEventListener("mousemove", this.mouseMoveResizeColumn);
      window.removeEventListener("mouseup", this.mouseUpResizeColumn);
    },

    /**
     * tính toán kích thước, vị trí của cột nếu nó được cố định
     * @author: TTANH (04/07/2024)
     * @param {object} column object lưu trữ thông tin của cột
     * @param {int} index vị trí của cột trong mảng lưu thông tin
     * @returns style của cột header
     */
    styleTh(column, index) {
      //40 kích thước của checkbox
      let countLeft = 40;

      if (column.isPin) {
        for (let i = 0; i < index; i++) {
          countLeft += this.columnsShow[i].size;
        }
      }

      return {
        maxWidth: column.size + "px",
        minWidth: column.size + "px",
        width: column.size + "px",
        left: column.isPin ? countLeft + "px" : "",
        zIndex: column.isPin ? 5 : "",
      };
    },

    /**
     * tính toán kích thước, vị trí của cột nếu nó được cố định
     * @author: TTANH (04/07/2024)
     * @param {object} column object lưu trữ thông tin của cột
     * @param {int} index vị trí của cột trong mảng lưu thông tin
     * @param {string} rowId id của dòng
     * @returns style của cột body
     */
    styleTd(column, index, rowId) {
      //40 kích thước của checkbox
      let countLeft = 40;

      if (column.isPin) {
        for (let i = 0; i < index; i++) {
          countLeft += this.columnsShow[i].size;
        }
      }

      return {
        position: column.isPin ? "sticky" : "",
        left: column.isPin ? countLeft + "px" : "",
        zIndex: column.isPin ? 2 : "",
        backgroundColor:
          this.rowIsFocus == rowId
            ? "var(--grid-body__line-focus-background-color) !important"
            : "",
        overflow: this.rowIsFocus == rowId ? "visible" : "",
      };
    },

    /**
     * sự kiện khi vào 1 dòng
     * @param {string} rowId id của dòng
     */
    clickRow(rowId) {
      this.$emit("clickRow", rowId);
      this.rowIsFocus = rowId;
    },

    /**
     * tìm tên của hàng đang được chọn
     * với các td có type là input-combobox
     * @author: TTANH (24/07/2024)
     * @param {*} selectId
     */
    nameOfSelectValueCombobox(selectId, comboboxRowData) {
      var index = findIndexByAttribute(comboboxRowData, "id", selectId);

      if (index !== -1) {
        return comboboxRowData[index].name;
      } else {
        return "";
      }
    },
  },
  computed: {
    /* lọc những column không hiển thị */
    columnsShow() {
      return this.columnsInfo.filter((item) => item.isShow);
    },

    /* tạo progress loading */
    rowsLoad() {
      if (this.rowsData.length === 0) {
        // fake data để tạo hiệu ứng
        return [
          {
            id: "loading",
          },
          {
            id: "loading",
          },
          {
            id: "loading",
          },
          {
            id: "loading",
          },
          {
            id: "loading",
          },
          {
            id: "loading",
          },
          {
            id: "loading",
          },
          {
            id: "loading",
          },
          {
            id: "loading",
          },
          {
            id: "loading",
          },
          {
            id: "loading",
          },
          {
            id: "loading",
          },
          {
            id: "loading",
          },
          {
            id: "loading",
          },
          {
            id: "loading",
          },
        ];
      } else {
        return this.rowsData;
      }
    },

    styleFunctionContext() {
      return {
        top: this.topFunctionContext,
        left: this.leftFunctionContext,
      };
    },

    checkSelectedAll() {
      if (this.selectedRows.length === 0) {
        return false;
      }

      for (let i = 0; i < this.rowsData.length; i++) {
        if (
          findIndexByAttribute(this.selectedRows, "", this.rowsData[i].id) ===
          -1
        ) {
          return false;
        }
      }

      return true;
    },
  },
  watch: {
    rowsData() {
      if (this.isShowFunctionContext) {
        this.closeFunctionContext();
      }
    },
  },
};
</script>

<style scoped>
@import url(./table.css);
</style>
