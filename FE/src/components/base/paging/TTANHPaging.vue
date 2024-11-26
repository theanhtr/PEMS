<template>
  <div class="paging">
    <div class="paging-left">
      <div class="total-records">
        {{ $t("component.paging.total") }}:
        <strong>{{ modelValue.totalRecord }}</strong>
        {{ $t("component.paging.record") }}
      </div>
    </div>
    <div class="paging-right">
      <ttanh-button
        type="dropdown"
        width="200px"
        borderRadius="var(--border-radius-default)"
        :dataDropdown="pagingDataDropdown"
        :dataDropdownTop="true"
        :dataDropdownSelectedValue="modelValue.pageSize"
        @clickItem="changePageSize"
        >{{ pagingTitle }}</ttanh-button
      >
      <div class="paging-nav">
        <ttanh-button
          type="link"
          colorText="#111"
          :disable="modelValue.pageNumber === 1"
          fontWeight="400"
          class="m-r-13"
          @clickBtnContainer="clickPreviousPage"
        >
          {{ $t("component.paging.previous") }}
        </ttanh-button>
        <ttanh-button
          type="link"
          colorText="#111"
          v-for="(page, index) in pageCalculate"
          :key="index"
          :fontWeight="page === modelValue.pageNumber ? '700' : '400'"
          :border="
            page === modelValue.pageNumber
              ? '1px solid var(--border-color-default)'
              : ''
          "
          padding="0px 6px"
          borderRadius="var(--border-radius-default)"
          @clickBtnContainer="selectPage(page, index)"
        >
          {{ page }}
        </ttanh-button>
        <ttanh-button
          type="link"
          colorText="#111"
          fontWeight="400"
          :disable="modelValue.pageNumber === modelValue.totalPage"
          class="m-l-13 m-r-8"
          @clickBtnContainer="clickNextPage"
        >
          {{ $t("component.paging.next") }}
        </ttanh-button>
      </div>
    </div>
  </div>
</template>

<script>
import { findIndexByAttribute } from "@/helper/common.js";
import { CommonErrorHandle } from "@/helper/error-handle.js";

export default {
  name: "TTANHPaging",
  props: {
    /**
     * modelValue bao gồm các thuộc tính:
     * {
     *  pageSize: 10,
        pageNumber: 1,
        totalPage: 0,
        totalRecord: 0,
     * }
     */
    modelValue: {
      required: true,
    },
  },

  data() {
    return {
      pagingDataDropdown: [
        {
          id: 10,
          value: 10,
          title: this.$t("component.paging.recordPerPage", { count: 10 }),
        },
        {
          id: 20,
          value: 20,
          title: this.$t("component.paging.recordPerPage", { count: 20 }),
        },
        {
          id: 30,
          value: 30,
          title: this.$t("component.paging.recordPerPage", { count: 30 }),
        },
        {
          id: 50,
          value: 50,
          title: this.$t("component.paging.recordPerPage", { count: 50 }),
        },
        {
          id: 100,
          value: 100,
          title: this.$t("component.paging.recordPerPage", { count: 100 }),
        },
      ],

      currentIndexShow: 0,
    };
  },

  created() {
    this.currentIndexShow = this.modelValue.pageNumber;
  },

  methods: {
    /**
     * thay đổi số lượng bản ghi trên 1 page
     * @param {*} id id của số lượng muốn thay đổi
     */
    changePageSize(id) {
      try {
        const index = findIndexByAttribute(this.pagingDataDropdown, "id", id);

        if (index !== -1) {
          this.$emit("update:modelValue", {
            ...this.modelValue,
            pageNumber: 1,
            pageSize: this.pagingDataDropdown[index].value,
          });
          this.$emit("reloadData");
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHPaging.vue:128 ~ changePageSize ~ error:",
          error
        );
      }
    },

    /**
     * chuyển sang page tiếp theo
     * @author: TTANH (03/07/2024)
     */
    clickNextPage() {
      try {
        this.$emit("update:modelValue", {
          ...this.modelValue,
          pageNumber: this.modelValue.pageNumber + 1,
        });
        this.$emit("reloadData");
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHPaging.vue:154 ~ clickNextPage ~ error:",
          error
        );
      }
    },

    /**
     * chọn 1 stt page mới
     * @param {int} index vị trí của ptử chọn trong mảng
     * @param {*} newPageNumber giá trị page chọn
     * @author: TTANH (25/07/2024)
     */
    selectPage(newPageNumber, index) {
      try {
        if (newPageNumber === "...") {
          if (this.pageCalculate[index + 1] !== this.modelValue.totalPage) {
            this.currentIndexShow -= 2;
          } else {
            this.currentIndexShow += 2;
          }
        } else if (newPageNumber !== this.modelValue.pageNumber) {
          this.$emit("update:modelValue", {
            ...this.modelValue,
            pageNumber: newPageNumber,
          });
          this.$emit("reloadData");
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHPaging.vue:174 ~ selectPage ~ error:",
          error
        );
      }
    },

    /**
     * trở về page trước đó
     * @author: TTANH (03/07/2024)
     */
    clickPreviousPage() {
      try {
        this.$emit("update:modelValue", {
          ...this.modelValue,
          pageNumber: this.modelValue.pageNumber - 1,
        });
        this.$emit("reloadData");
      } catch (error) {
        console.log(
          "🚀 ~ file: TTANHPaging.vue:191 ~ clickPreviousPage ~ error:",
          error
        );
      }
    },
  },
  computed: {
    /**
     * title cho các item dropdown list
     * @author: TTANH (25/07/2024)
     */
    pagingTitle() {
      const index = findIndexByAttribute(
        this.pagingDataDropdown,
        "value",
        this.modelValue.pageSize === 0 ? 10 : this.modelValue.pageSize
      );

      if (index !== -1) {
        return this.$t("component.paging.recordPerPage", {
          count: this.pagingDataDropdown[index].value,
        });
      } else {
        CommonErrorHandle();
      }
    },

    /**
     * Tính toán hiển thị trang
     * @author: TTANH (25/07/2024)
     */
    pageCalculate() {
      let pages = []; // có cả ...
      for (let i = 1; i <= this.modelValue.totalPage; i++) {
        if (
          i === 1 ||
          i === this.modelValue.totalPage ||
          (this.currentIndexShow - 1 <= i && i <= this.currentIndexShow + 1)
        ) {
          pages.push(i);
        } else if (
          i === this.currentIndexShow + 2 ||
          i === this.currentIndexShow - 2
        ) {
          pages.push("...");
        }
      }

      return pages;
    },
  },

  watch: {
    modelValue: {
      handler: function (newValue) {
        this.currentIndexShow = newValue.pageNumber;
      },

      deep: true,
    },
  },
};
</script>

<style>
@import url(./paging.css);
</style>
