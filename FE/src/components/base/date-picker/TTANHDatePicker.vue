<template>
  <div
    @mouseenter="hoverInput = true"
    @mouseleave="hoverInput = false"
    :style="styleContainer"
    class="input-container"
  >
    <label
      v-if="labelText !== ''"
      :for="`dp-input-${idInput}`"
      class="label-input"
    >
      {{ labelText }}
      <div v-if="inputRequired" style="color: red; padding-left: 3px">*</div>
    </label>

    <VueDatePicker
      v-model="date"
      :uid="idInput"
      :placeholder="$store.state.formatDate"
      position="right"
      :input-class-name="`dp-custom-input ${
        errorText !== '' ? 'dp-error-input' : ''
      }`"
      menu-class-name="dp-custom-menu"
      calendar-class-name="dp-custom-calendar"
      calendar-cell-class-name="dp-custom-cell"
      :clearable="false"
      :enable-time-picker="false"
      :format="$store.state.formatDate"
      :day-names="dayNames"
      :max-date="maxDate"
      :disabled="disable"
      text-input
      auto-apply
      timezone="Asia/Novosibirsk"
      ref="InputDatePicker"
      @open="this.isPickMonthYear = false"
      @keydown="handleInputKeydown"
    >
      <template #input-icon> </template>
      <template
        #month-year="{ month, year, updateMonthYear, handleMonthYearChange }"
      >
        <div class="month-year__container">
          <ttanh-icon
            @click="handleMonthYearChange(false)"
            v-if="!isPickMonthYear"
            icon="arrow-left--black"
          />
          <ttanh-icon
            v-else
            @click="!isPickMonth ? reduceRangeYear() : reduceYearSelect()"
            icon="arrow-down--black"
          />

          <div class="month-year__info">
            <div
              v-if="!isPickMonthYear"
              class="bold-text"
              style="cursor: pointer"
              @click="openMonthYearSelect"
            >
              {{
                $t("component.datePicker.monthYearLabel", {
                  month: month + 1,
                  year: year,
                })
              }}
            </div>
            <div v-else class="month-year--picker">
              <div
                class="month-year--picker__value bold-text"
                :style="{ cursor: isPickMonth ? 'pointer' : '' }"
                @click="isPickMonth ? backToSelectYear() : ''"
              >
                {{
                  !isPickMonth
                    ? yearRangePicker[0].text +
                      " - " +
                      yearRangePicker[yearRangePicker.length - 1].text
                    : yearSelected
                }}
              </div>

              <ttanh-arrow-down />
              <div class="select-container">
                <table class="select-table">
                  <tr v-for="countRow in [0, 1, 2, 3]" :key="countRow">
                    <td v-for="countColumn in [0, 1, 2]" :key="countColumn">
                      <div
                        class="select-table__ele"
                        :class="
                          dataSelectRender[countRow * 3 + countColumn].value ===
                          (!isPickMonth ? yearSelected : monthSelected)
                            ? 'select-table__ele--active'
                            : ''
                        "
                        @click="
                          !isPickMonth
                            ? selectYear(
                                dataSelectRender[countRow * 3 + countColumn]
                                  .value
                              )
                            : selectMonth(
                                dataSelectRender[countRow * 3 + countColumn]
                                  .value,
                                updateMonthYear
                              )
                        "
                      >
                        {{ dataSelectRender[countRow * 3 + countColumn].text }}
                      </div>
                    </td>
                  </tr>
                </table>
                <div
                  class="cancel-select"
                  @click="closeMonthYearSelect(month, year)"
                >
                  {{ $t("component.datePicker.cancelSelectMonthYear") }}
                </div>
              </div>
            </div>
          </div>

          <ttanh-icon
            @click="handleMonthYearChange(true)"
            v-if="!isPickMonthYear"
            icon="arrow-right--black"
          />
          <ttanh-icon
            v-else
            @click="!isPickMonth ? increaseRangeYear() : increaseYearSelect()"
            icon="arrow-up--black"
          />
        </div>
      </template>
      <template #action-extra="{ selectCurrentDate }">
        <div
          @click="selectCurrentDate()"
          :title="$t('component.datePicker.todayTooltip')"
          class="date-picker__current-date"
        >
          <ttanh-separation-line
            style="
              width: 100%;
              border-top: 1px solid var(--border-color-default);
            "
          />
          <div class="current-date__text">
            {{ $t("component.datePicker.today") }}
          </div>
        </div>
      </template>
    </VueDatePicker>

    <ttanh-tooltip v-if="errorText !== '' && hoverInput">{{
      errorText
    }}</ttanh-tooltip>
  </div>
</template>

<script>
import VueDatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";

export default {
  name: "TTANHDatePicker",
  components: {
    VueDatePicker,
  },
  props: {
    maxDate: {
      default: null
    },
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
    labelText: {
      default: "",
    },
    inputRequired: {
      default: false,
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
  },
  data() {
    return {
      dayNames: [
        this.$t("component.datePicker.dayName.monday"),
        this.$t("component.datePicker.dayName.tuesday"),
        this.$t("component.datePicker.dayName.wednesday"),
        this.$t("component.datePicker.dayName.thursday"),
        this.$t("component.datePicker.dayName.friday"),
        this.$t("component.datePicker.dayName.saturday"),
        this.$t("component.datePicker.dayName.sunday"),
      ],

      date: this.modelValue,
      isPickMonthYear: false,
      isPickMonth: false,

      yearRangePicker: [],
      monthRange: [
        {
          value: 1,
          text: this.$t("component.datePicker.monthRange.january"),
        },
        {
          value: 2,
          text: this.$t("component.datePicker.monthRange.february"),
        },
        {
          value: 3,
          text: this.$t("component.datePicker.monthRange.march"),
        },
        {
          value: 4,
          text: this.$t("component.datePicker.monthRange.april"),
        },
        {
          value: 5,
          text: this.$t("component.datePicker.monthRange.may"),
        },
        {
          value: 6,
          text: this.$t("component.datePicker.monthRange.june"),
        },
        {
          value: 7,
          text: this.$t("component.datePicker.monthRange.july"),
        },
        {
          value: 8,
          text: this.$t("component.datePicker.monthRange.august"),
        },
        {
          value: 9,
          text: this.$t("component.datePicker.monthRange.september"),
        },
        {
          value: 10,
          text: this.$t("component.datePicker.monthRange.october"),
        },
        {
          value: 11,
          text: this.$t("component.datePicker.monthRange.november"),
        },
        {
          value: 12,
          text: this.$t("component.datePicker.monthRange.december"),
        },
      ],

      yearSelected: "",
      monthSelected: "",

      hoverInput: false,
    };
  },
  created() {
    this.resetPickMonthYear(this.modelValue);
    this.date = !this.modelValue ? "" : this.modelValue;
  },
  mounted() {
    /**
     * xử lý việc thêm tabindex
     * do việc dùng thư viện không thể truy cập trực tiếp vào input được
     */
    var mainInput = document.getElementById(`dp-input-${this.idInput}`);
    mainInput.tabIndex = this.tabindex;
  },
  methods: {
    /**
     * cho dữ liệu ngày đã chọn là trống
     * @author: TTANH (07/08/2024)
     */
    resetDatePicked() {
      this.date = "";
    },

    /**
     * xử lý sự kiện key down
     * @author: TTANH (29/07/2024)
     */
    handleInputKeydown(event) {
      try {
        if (
          event.keyCode === this.$_TTANHEnum.KEY_CODE.TAB ||
          event.keyCode === this.$_TTANHEnum.KEY_CODE.ENTER
        ) {
          this.$refs.InputDatePicker.closeMenu();
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHCombobox.vue:389 ~ handleInputKeydown ~ error:",
          error
        );
      }
    },

    /**
     * reset tháng và năm đang được chọn
     * @param {string} dateSelect date hiện tại để tính tháng năm được reset
     */
    resetPickMonthYear(dateSelect) {
      try {
        let tempDate;

        if (!dateSelect) {
          tempDate = new Date(Date.now());
        } else {
          tempDate = new Date(dateSelect);
        }

        let tempYear = tempDate.getFullYear();
        this.yearRangePicker = [];

        for (let i = 0; i < 12; i++) {
          this.yearRangePicker.push({
            value: tempYear - i,
            text: tempYear - i,
          });
        }

        this.yearSelected = tempYear;
        this.monthSelected = tempDate.getMonth() + 1;
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHDatePicker.vue:248 ~ resetPickMonthYear ~ error:",
          error
        );
      }
    },

    /**
     * reset lại khoảng năm được chọn
     * @param {int} yearStart năm bắt đầu để tính range year mới
     */
    resetRangeYear(yearStart) {
      try {
        this.yearRangePicker = [];

        for (let i = 0; i < 12; i++) {
          this.yearRangePicker.push({
            value: yearStart - i,
            text: yearStart - i,
          });
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHDatePicker.vue:286 ~ resetRangeYear ~ error:",
          error
        );
      }
    },

    /**
     * xử lý việc mở phần chọn tháng và năm
     * @author: TTANH (28/06/2024)
     */
    openMonthYearSelect() {
      try {
        this.isPickMonthYear = true;
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHDatePicker.vue:251 ~ openMonthYearSelect ~ error:",
          error
        );
      }
    },

    /**
     * xử lý đóng phần chọn tháng và năm bằng nút "Hủy bỏ"
     * @author: TTANH (28/06/2024)
     */
    closeMonthYearSelect(currentMonth, currentYear) {
      try {
        this.isPickMonthYear = false;
        this.isPickMonth = false;

        let monthTemp = this.monthSelected - 1;

        if (currentMonth === monthTemp && currentYear === this.yearSelected) {
        } else {
          this.resetPickMonthYear(this.modelValue);
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHDatePicker.vue:275 ~ closeMonthYearSelect ~ error:",
          error
        );
      }
    },

    /**
     * giảm vùng năm đang focus xuống 11 năm
     * @author: TTANH (28/06/2024)
     */
    reduceRangeYear() {
      try {
        let lastYear =
          this.yearRangePicker[this.yearRangePicker.length - 1].value;

        this.resetRangeYear(lastYear);
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHDatePicker.vue:284 ~ reduceRangeYear ~ error:",
          error
        );
      }
    },

    /**
     * tăng vùng năm đang focus lên 12 năm
     * @author: TTANH (28/06/2024)
     */
    increaseRangeYear() {
      try {
        let firstYear = this.yearRangePicker[0].value + 11;

        this.resetRangeYear(firstYear);
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHDatePicker.vue:293 ~ increaseRangeYear ~ error:",
          error
        );
      }
    },

    /**
     * xử lý việc ấn chọn 1 năm
     * @param {int} yearSelect năm được chọn
     * @author: TTANH (28/06/2024)
     */
    selectYear(yearSelect) {
      try {
        this.yearSelected = yearSelect;
        this.isPickMonth = true;
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHDatePicker.vue:328 ~ selectYear ~ error:",
          error
        );
      }
    },

    /**
     * xử lý việc chọn vào 1 tháng
     * @param {int} monthSelect tháng được chọn
     * @param {function} updateMonthYear hàm thay đổi tháng và năm của thư viện
     * @author: TTANH (28/06/2024)
     */
    selectMonth(monthSelect, updateMonthYear) {
      try {
        this.monthSelected = monthSelect;

        this.isPickMonthYear = false;
        this.isPickMonth = false;

        updateMonthYear(this.monthSelected - 1, this.yearSelected);

        this.resetRangeYear(this.yearSelected);
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHDatePicker.vue:337 ~ selectMonth ~ error:",
          error
        );
      }
    },

    /**
     * Giảm năm đi 1 ở phần chọn tháng
     * @author: TTANH (28/06/2024)
     */
    reduceYearSelect() {
      try {
        this.yearSelected--;

        this.resetRangeYear(this.yearSelected);
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHDatePicker.vue:284 ~ reduceYearSelect ~ error:",
          error
        );
      }
    },

    /**
     * tăng năm lên 1 ở phần chọn tháng
     * @author: TTANH (28/06/2024)
     */
    increaseYearSelect() {
      try {
        this.yearSelected++;

        this.resetRangeYear(this.yearSelected);
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHDatePicker.vue:293 ~ increaseYearSelect ~ error:",
          error
        );
      }
    },

    /**
     * trở lại với việc chọn năm
     * @author: TTANH (28/06/2024)
     */
    backToSelectYear() {
      try {
        this.isPickMonth = false;
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHDatePicker.vue:414 ~ backToSelectYear ~ error:",
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
    dataSelectRender() {
      if (!this.isPickMonth) {
        return this.yearRangePicker;
      } else {
        return this.monthRange;
      }
    },
  },
  watch: {
    date(newValue) {
      this.resetPickMonthYear(newValue);

      if (newValue) {
        this.$emit("update:modelValue", newValue.toLocaleString());
      } else {
        this.$emit("update:modelValue", "");
      }
    },
  },
};
</script>

<style>
@import url(./date-picker.css);
</style>
