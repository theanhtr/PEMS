<template>
  <div class="user-content">
    <div class="page__filter">
      <h1 class="page__filter-title">Bộ lọc</h1>
      <ttanh-separation-line
        style="width: 98%; border-top: 2px solid var(--border-color-default); margin-bottom: 4px"
      />
      <div class="page__filter-group page__filter-group-1">
        <ttanh-textfield
          v-model="dataFilter.userName"
          ref="userNameFilter"
          idInput="userNameFilter"
          labelText="Tên tài khoản hoặc Họ và tên"
          style="padding-left: 6px"
          class="w1/4"
        />
      </div>
      <div class="page__filter-group page__filter-group-3">
        <ttanh-button
          width="90px"
          type="main"
          borderRadius="var(--border-radius-default)"
          :border="batchExecutionDisable ? '' : '2px solid black'"
          :tabindex="-1"
          @clickButton="getUsers"
          >Tìm kiếm</ttanh-button
        >
        <ttanh-button
          width="80px"
          type="sub"
          borderRadius="var(--border-radius-default)"
          :border="batchExecutionDisable ? '' : '2px solid black'"
          :tabindex="-1"
          @clickButton="clearFilter"
          >Xóa lọc</ttanh-button
        >
      </div>
    </div>
    <div class="page__action">
      <div class="page__action-left"></div>
      <div class="page__action-right">
        <ttanh-icon
          :icon="'page__reload--' + (pageButtonHover['page__reload'] ? 'black' : 'grey')"
          :tooltip="$t('UserSubsystem.UserContent.reloadTooltip')"
          @mouseenter="pageButtonHover['page__reload'] = true"
          @mouseleave="pageButtonHover['page__reload'] = false"
          @click="reloadDataWithSelectedRows"
        />
        <ttanh-button
          width="85px"
          type="main"
          borderRadius="var(--border-radius-default)"
          :border="batchExecutionDisable ? '' : '2px solid black'"
          :tabindex="-1"
          @clickButton="showAddUserPopup"
          >Tạo mới</ttanh-button
        >
      </div>
    </div>
    <div class="page__table">
      <ttanh-table
        ref="ttanhTable"
        :columnsInfo="UserColumnsInfo"
        :rowsData="computedUsers"
        :selectedRows="computedSelectedUsers"
        :noData="computedNoData"
        :oneRowSelect="true"
        @checked-all="checkedAllRow"
        @unchecked-all="uncheckedAllRow"
        @checked-row="checkedRow"
        @unchecked-row="uncheckedRow"
        @doubleClickRow="openFormUpdate"
        @clickFixBtn="openFormUpdate"
        @clickContextDeleteBtn="openConfirmDeletePopup"
        @resizeColumn="resizeUserColumn"
      />
    </div>
    <div class="page__footer">
      <ttanh-paging v-if="!this.noData" v-model="pagingData" @reloadData="reloadData" />
    </div>

    <AddUserPopup
      v-if="isShowAddUserPopup"
      :dataUpdate="dataUpdate"
      @clickCancelBtn="isShowAddUserPopup = false"
      @reloadData="reloadData"
      ref="addUserPopup"
    />

    <ttanh-delete-popup
      :titleText="computedDeletePopupText"
      v-if="isShowConfirmDeletePopup || isShowConfirmDeleteMultiplePopup"
      @no-click="isShowConfirmDeletePopup ? noDeleteBtnClick() : noDeleteMultipleUser()"
      @yes-click="isShowConfirmDeletePopup ? yesDeleteBtnClick() : yesDeleteMultipleUser()"
    />

    <ttanh-loading-spinner v-if="isLoading" size="large" />
  </div>
</template>

<script>
import UserService from '@/service/UserService.js'
import AddUserPopup from './AddUserPopup.vue'
import { CommonErrorHandle } from '@/helper/error-handle'
import { findIndexByAttribute, sortArrayByAttribute } from '@/helper/common.js'
import { formatToNumber } from '@/helper/textfield-format-helper.js'
import { debounce } from '@/helper/debounce.js'
import { isProxy, toRaw } from 'vue'
import { roles } from '../../../data_combobox/role'

export default {
  name: 'UserContent',
  components: {
    AddUserPopup
  },
  data() {
    return {
      users: [],

      /* lưu dữ id các người dùng đã được chọn */
      selectedUsers: [],

      UserColumnsInfo: [
        {
          id: 'Username',
          name: 'Tên tài khoản',
          size: '150px',
          textAlign: 'left',
          format: 'text',
          isShow: true,
          isPin: false
        },
        {
          id: 'Fullname',
          name: 'Họ và tên',
          size: '150px',
          textAlign: 'center',
          format: 'text',
          isShow: true,
          isPin: false
        },
        {
          id: 'RoleID',
          name: 'Chức vụ',
          size: '150px',
          textAlign: 'center',
          format: 'input-combobox',
          isShow: true,
          isPin: false,
          comboboxRowData: roles,
          disableCombobox: true
        }
      ],

      /* thông tin cột thuần được gửi từ api đã sắp xếp */
      UserColumnsInfoRaw: [],

      isLoading: false,
      /* các biến để xác định trạng thái trên page_action */
      tableSearchFocus: false,
      pageButtonHover: {
        page__setting: false,
        page__reload: false,
        page__reload: false
      },

      /* biến xác định nút "Thực hiện hàng loạt" có disable hay không */
      batchExecutionDisable: true,

      /*== các biến sử dụng cho add-User-popup ==*/
      isShowAddUserPopup: false,

      dataUpdate: null,

      /* biến sử dụng cho việc xác nhận xóa */
      isShowConfirmDeletePopup: false,
      UserCodeDelete: '',
      UserIdDelete: '',

      isShowConfirmDeleteMultiplePopup: false,

      searchText: '',

      /* biến sử dụng cho phân trang */
      pagingData: {
        pageSize: 10,
        pageNumber: 1,
        totalPage: 0,
        totalRecord: 0
      },

      // xử lý khi không có dữ liệu trả về
      noData: false,

      //biến sử dụng cho việc thao tác giữ shift khi chọn
      previouslySelectedIndex: -1,

      //biến dùng để phân biệt việc có update thông tin cột khi sử dụng watch không
      isUpdateColumnsInfo: false,

      //biến dùng cho việc tùy chỉnh giao diện
      isShowLayoutSetting: false,

      dataFilter: {
        userName: ''
      }
    }
  },

  created() {
    // lấy dữ liệu phân trang được lưu trong local storage
    this.pagingData.pageNumber = formatToNumber(localStorage.getItem('pageNumber')) ?? 1
    this.pagingData.pageSize = formatToNumber(localStorage.getItem('pageSize')) ?? 10

    //lấy dữ liệu người dùng
    this.getUsers()
  },

  methods: {
    clearFilter() {
      this.dataFilter = {
        userName: ''
      }

      this.getUsers()
    },
    /**
     * Sắp xếp theo ordernumber và isPin để hiển thị đúng
     * @author: TTANH (04/08/2024)
     */
    sortUserColumnsInfo(columnsInfoTemp) {
      try {
        // sắp xếp theo thứ tự
        columnsInfoTemp = sortArrayByAttribute(columnsInfoTemp, 'OrderNumber', false)

        //đưa những cột được ghim lên đầu
        columnsInfoTemp = sortArrayByAttribute(columnsInfoTemp, 'ColumnIsPin')

        return columnsInfoTemp
      } catch (error) {
        console.log('🚀 ~ file: UserContent.vue:263 ~ sortUserColumnsInfo ~ error:', error)
      }
    },

    /**
     * hàm thực hiện mở thêm người dùng
     * @author: TTANH (11/07/2024)
     */
    showAddUserPopup() {
      this.isShowAddUserPopup = true
      this.dataUpdate = null
    },

    /**
     * thực hiện get dữ liệu người dùng khi component được render
     * @author: TTANH (30/06/2024)
     */
    async getUsers() {
      try {
        var dataFilter = {
          searchText: this.dataFilter.userName,
          PageSize: this.pagingData.pageSize,
          PageNumber: this.pagingData.pageNumber
        }

        const res = await UserService.filter('User', dataFilter)

        if (res.success) {
          if (res.data.Data.length != 0) {
            this.users = res.data.Data
            this.pagingData.totalPage = res.data.TotalPage
            this.pagingData.totalRecord = res.data.TotalRecord
            this.pagingData.pageNumber = res.data.CurrentPage
            this.noData = false
          } else {
            this.noData = true
          }
        } else {
          CommonErrorHandle()
        }
      } catch (error) {
        console.log('🚀 ~ file: UserList.vue:116 ~ getUsers ~ error:', error)
      }
    },

    /**
     * sự kiện click vào các item con của "Thực hiện hàng loạt":
     * { id: "delete", title: "Xóa" },
     * { id: "merge", title: "Gộp" },
     * @author: TTANH (17/07/2024)
     * @param {string} idItem id của nút được click
     */
    clickItemBatchExecution(idItem) {
      if (idItem === 'delete') {
        this.isShowConfirmDeleteMultiplePopup = true
      }
    },

    /**
     * bỏ lệnh xóa nhiều người dùng
     * @author: TTANH (31/07/2024)
     */
    noDeleteMultipleUser() {
      this.isShowConfirmDeleteMultiplePopup = false
    },

    /**
     * xóa nhiều người dùng
     * @author: TTANH (17/07/2024)
     */
    async yesDeleteMultipleUser() {
      var dataSendApi = null

      if (isProxy(this.selectedUsers)) {
        dataSendApi = toRaw(this.selectedUsers)
      } else {
        dataSendApi = this.selectedUsers
      }

      this.isLoading = true

      const res = await UserService.deleteMultiple(dataSendApi)

      this.isLoading = false

      if (res.success) {
        this.$store.commit('addToast', {
          type: 'success',
          text: this.$t('successHandle.UserSubsystem.deleteMultiple', {
            count: res.data
          })
        })

        this.selectedUsers = []
        this.isShowConfirmDeleteMultiplePopup = false

        this.reloadData()
      } else {
        CommonErrorHandle()
      }
    },

    /**
     * cập nhật lại users mới
     * @author: TTANH (03/07/2024)
     */
    reloadData() {
      try {
        this.previouslySelectedIndex = -1
        this.users = []
        this.getUsers()
      } catch (error) {
        console.log('🚀 ~ file: UserContent.vue:465 ~ reloadData ~ error:', error)
      }
    },

    /**
     * bỏ hết dữ liệu đã chọn khi ấn vào nút "Lấy lại dữ liệu"
     * @author: TTANH (03/07/2024)
     */
    reloadDataWithSelectedRows() {
      try {
        this.selectedUsers = []
        this.reloadData()
      } catch (error) {
        console.log('🚀 ~ file: UserContent.vue:282 ~ reloadDataWithSelectedRows ~ error:', error)
      }
    },

    /**
     * thêm một id vào mảng dòng đã chọn
     * @author: TTANH (11/07/2024)
     * @param {string} rowId id của dòng được chọn
     */
    addSelectedRow(rowId) {
      let index = findIndexByAttribute(this.selectedUsers, '', rowId)

      if (index === -1) {
        this.selectedUsers.push(rowId)
      }
    },

    /**
     * xóa một id vào mảng dòng đã chọn
     * @author: TTANH (11/07/2024)
     * @param {string} rowId id của dòng được chọn
     */
    deleteSelectedRow(rowId) {
      let index = findIndexByAttribute(this.selectedUsers, '', rowId)

      if (index !== -1) {
        this.selectedUsers.splice(index, 1)
      }
    },

    /**
     * xử lý khi chọn checkbox ở header
     * @author: TTANH (27/06/2024)
     */
    checkedAllRow() {
      try {
        this.users.forEach((User) => {
          this.addSelectedRow(User.UserId)
        })
      } catch (error) {
        console.log('🚀 ~ file: UserList.vue:463 ~ checkedAllRow ~ error:', error)
      }
    },

    /**
     * xử lý khi bỏ chọn checkbox ở header
     * @author: TTANH (27/06/2024)
     */
    uncheckedAllRow() {
      try {
        this.users.forEach((User) => {
          this.deleteSelectedRow(User.UserId)
        })
      } catch (error) {
        console.log('🚀 ~ file: UserList.vue:475 ~ uncheckedAllRow ~ error:', error)
      }
    },

    /**
     * xử lý khi chọn checkbox ở 1 row
     * @author: TTANH (27/06/2024)
     * @param {string} rowId: id của record được chọn
     */
    checkedRow(rowId) {
      try {
        let indexNewChecked = findIndexByAttribute(this.users, 'UserId', rowId)

        if (event.shiftKey) {
          event.preventDefault()

          if (this.previouslySelectedIndex === -1) {
            this.addSelectedRow(rowId)
          } else {
            if (this.previouslySelectedIndex > indexNewChecked) {
              for (let index = indexNewChecked; index <= this.previouslySelectedIndex; index++) {
                const User = this.users[index]

                this.addSelectedRow(User.UserId)
              }
            } else if (this.previouslySelectedIndex < indexNewChecked) {
              for (let index = this.previouslySelectedIndex; index <= indexNewChecked; index++) {
                const User = this.users[index]

                this.addSelectedRow(User.UserId)
              }
            } else {
            }
          }
        } else {
          this.addSelectedRow(rowId)
        }

        this.previouslySelectedIndex = indexNewChecked
      } catch (error) {
        console.log('🚀 ~ file: UserList.vue:492 ~ uncheckedAllRow ~ error:', error)
      }
    },

    /**
     * xử lý khi bỏ chọn checkbox ở 1 row
     * @author: TTANH (27/06/2024)
     * @param {string} rowId: id của record được bỏ chọn
     */
    uncheckedRow(rowId) {
      try {
        let indexNewChecked = findIndexByAttribute(this.users, 'UserId', rowId)

        if (event.shiftKey) {
          event.preventDefault()

          if (this.previouslySelectedIndex === -1) {
            this.deleteSelectedRow(rowId)
          } else {
            if (this.previouslySelectedIndex > indexNewChecked) {
              for (let index = indexNewChecked; index <= this.previouslySelectedIndex; index++) {
                const User = this.users[index]

                this.deleteSelectedRow(User.UserId)
              }
            } else if (this.previouslySelectedIndex < indexNewChecked) {
              for (let index = this.previouslySelectedIndex; index <= indexNewChecked; index++) {
                const User = this.users[index]

                this.deleteSelectedRow(User.UserId)
              }
            } else {
            }
          }
        } else {
          this.deleteSelectedRow(rowId)
        }

        this.previouslySelectedIndex = indexNewChecked
      } catch (error) {
        console.log('🚀 ~ file: UserList.vue:492 ~ uncheckedAllRow ~ error:', error)
      }
    },

    /**
     * mở form update
     * @param {string} rowId id của đối tượng muốn update
     */
    openFormUpdate(rowId) {
      try {
        let indexRow = findIndexByAttribute(this.users, 'UserId', rowId)

        this.isShowAddUserPopup = true
        this.dataUpdate = this.users[indexRow]
      } catch (error) {
        console.log('🚀 ~ file: UserContent.vue:529 ~ openFormUpdate ~ error:', error)
      }
    },

    /**
     * mở form xác nhận xóa
     * @author: TTANH (01/07/2024)
     * @param {string} id id của bản ghi cần xóa
     */
    openConfirmDeletePopup(id) {
      try {
        let index = findIndexByAttribute(this.users, 'UserId', id)

        if (index !== -1) {
          this.UserCodeDelete = this.users[index].UserCode
          this.UserIdDelete = id
          this.isShowConfirmDeletePopup = true
        } else {
          this.$store.commit('addToast', {
            type: 'error',
            text: this.$t('errorHandle.UserSubsystem.notFoundUser')
          })

          this.reloadData()
        }
      } catch (error) {
        console.log('🚀 ~ file: UserContent.vue:351 ~ openConfirmDeletePopup ~ error:', error)
      }
    },

    /**
     * mở form nhân bản
     * @param {string} rowId id của đối tượng muốn nhân bản
     */
    openFormDuplicate(rowId) {
      try {
        let indexRow = findIndexByAttribute(this.users, 'UserId', rowId)

        this.isShowAddUserPopup = true
        this.dataUpdate = this.users[indexRow]

        this.$nextTick(() => {
          // thay đổi trạng thái form thành thêm mới
          this.$refs.addUserPopup.changeFormModeToAdd()

          // lấy mã code mới
          this.$refs.addUserPopup.getNewUserCode()
        })
      } catch (error) {
        console.log('🚀 ~ file: UserContent.vue:529 ~ openFormUpdate ~ error:', error)
      }
    },

    /**
     * đóng form xác nhận xóa
     * @author: TTANH (01/07/2024)
     */
    closeConfirmDeletePopup() {
      try {
        this.UserCodeDelete = ''
        this.UserIdDelete = ''
        this.isShowConfirmDeletePopup = false
      } catch (error) {
        console.log('🚀 ~ file: UserContent.vue:386 ~ closeConfirmDeletePopup ~ error:', error)
      }
    },

    /**
     * hủy xóa
     * @author: TTANH (01/07/2024)
     */
    noDeleteBtnClick() {
      try {
        this.closeConfirmDeletePopup()
      } catch (error) {
        console.log('🚀 ~ file: UserContent.vue:401 ~ noDeleteBtnClick ~ error:', error)
      }
    },

    /**
     * xác nhận xóa
     * @author: TTANH (01/07/2024)
     */
    yesDeleteBtnClick() {
      try {
        this.deleteSelectedRow(this.UserIdDelete)
        this.deleteRecord()
        this.closeConfirmDeletePopup()
      } catch (error) {
        console.log('🚀 ~ file: UserContent.vue:416 ~ yesDeleteBtnClick ~ error:', error)
      }
    },

    /**
     * thực hiện xóa 1 bản ghi
     * @author: TTANH (01/07/2024)
     */
    async deleteRecord() {
      try {
        this.isLoading = true
        const UserCode = this.UserCodeDelete
        const res = await UserService.delete(this.UserIdDelete)

        if (res.success) {
          this.$store.commit('addToast', {
            type: 'success',
            text: 'Xóa người dùng thành công'
          })

          this.reloadData()
        } else {
          if (res.errorCode === this.$_TTANHEnum.ERROR_CODE.NOT_FOUND_DATA) {
            this.$store.commit('addToast', {
              type: 'error',
              text: res.userMsg
            })
          } else {
            CommonErrorHandle()
          }
        }

        this.isLoading = false
      } catch (error) {
        console.log('🚀 ~ file: UserContent.vue:582 ~ deleteRecord ~ error:', error)
      }
    },

    /**
     * thực hiện thay đổi kích thước của cột
     * @author: TTANH (04/07/2024)
     */
    resizeUserColumn(index, resizeWidth) {
      try {
        this.isUpdateColumnsInfo = true
        this.UserColumnsInfo[index].size = resizeWidth
      } catch (error) {
        console.log('🚀 ~ file: UserContent.vue:524 ~ resizeUserColumn ~ error:', error)
      }
    },

    /**
     * xử lý các phím tắt
     * @author: TTANH (11/07/2024)
     */
    handleKeydown(event) {
      if (event.keyCode === this.$_TTANHEnum.KEY_CODE.INSERT && event.ctrlKey) {
        this.$router.push('/app/User/import')
      } else if (event.keyCode === this.$_TTANHEnum.KEY_CODE.INSERT) {
        this.showAddUserPopup()
      } else if (event.keyCode === this.$_TTANHEnum.KEY_CODE.F && event.shiftKey && event.ctrlKey) {
        this.$refs.searchTextTable.focus()
      }
    },

    /**
     * chuyển đổi 1 cột thành dạng ttanh table có thể hiểu
     * @author: TTANH (03/08/2024)
     * @param {Object} rawData dữ liệu thông tin cột raw
     * @returns 1 object đã được chuyển đổi
     */
    mapColumnInfoFromRawToCode(rawData) {
      let langCode = this.$store.state.langCode

      let tempMap = {}

      tempMap.id = rawData.ServerColumnName
      tempMap.name = rawData[`${langCode}ClientColumnName`]
      tempMap.size = rawData.ColumnWidth
      tempMap.textAlign = rawData.ColumnTextAlign
      tempMap.format = rawData.ColumnFormat
      tempMap.isShow = rawData.ColumnIsShow
      tempMap.isPin = rawData.ColumnIsPin
      tempMap.tooltip = rawData[`${langCode}Tooltip`]
      tempMap.clientColumnNameDefault = rawData[`${langCode}ClientColumnNameDefault`]
      tempMap.orderNumber = rawData.OrderNumber

      return tempMap
    },

    /**
     * chuyển đổi mảng cột thành dạng ttanh table có thể hiểu
     * @author: TTANH (03/08/2024)
     * @param {Array} rawsData dữ liệu thông tin cột raw
     * @returns 1 mảng object đã được chuyển đổi
     */
    mapColumnsInfoFromRawToCode(rawsData) {
      let tempMapArray = []

      rawsData.forEach((e) => {
        let tempMap = this.mapColumnInfoFromRawToCode(e)

        tempMapArray.push(tempMap)
      })

      return tempMapArray
    },

    /**
     * chuyển đổi dữ liệu cập nhật sang dữ liệu truyền cho api để cập nhật
     * @author: TTANH (03/08/2024)
     * @param {Object} codeData dữ liệu thông tin cột
     * @returns 1 object đã được chuyển đổi
     */
    mapColumnInfoFromCodeToRawForUpdate(codeData) {
      let langCode = this.$store.state.langCode

      let indexInRaw = findIndexByAttribute(this.UserColumnsInfoRaw, 'ServerColumnName', codeData.id)

      let UserColumnInfoRaw = this.UserColumnsInfoRaw[indexInRaw]

      let tempMap = {}

      tempMap.UserLayoutId = UserColumnInfoRaw.UserLayoutId
      tempMap.viClientColumnName = UserColumnInfoRaw.viClientColumnName
      tempMap.enClientColumnName = UserColumnInfoRaw.enClientColumnName
      tempMap.OrderNumber = UserColumnInfoRaw.OrderNumber

      tempMap[`${langCode}ClientColumnName`] = codeData.name
      tempMap.ColumnWidth = codeData.size
      tempMap.ColumnIsShow = codeData.isShow
      tempMap.ColumnIsPin = codeData.isPin
      return tempMap
    },

    /**
     * chuyển đổi dữ liệu cập nhật sang dữ liệu truyền cho api để cập nhật
     * @author: TTANH (03/08/2024)
     * @param {Array} codesData dữ liệu thông tin cột
     * @returns 1 mảng object đã được chuyển đổi
     */
    mapColumnsInfoFromCodeToRawForUpdate(codesData) {
      let tempMapArray = []

      codesData.forEach((e) => {
        let tempMap = this.mapColumnInfoFromCodeToRawForUpdate(e)

        tempMapArray.push(tempMap)
      })

      return tempMapArray
    },

    /**
     * thực hiện cập nhật thông tin cột trên db
     */
    async updateColumnsInfoToDB(newData) {
      let datasUpdate = this.mapColumnsInfoFromCodeToRawForUpdate(newData)

      const res = await UserLayoutService.updateMultiple(datasUpdate)

      if (res.success) {
      } else {
        CommonErrorHandle()
      }
    }
  },
  computed: {
    /* thêm id để phân biệt các phần tử với nhau */
    computedUsers() {
      try {
        let haveIdUsers = []

        this.users.forEach((User, index) => {
          let id = User.UserId
          haveIdUsers.push({
            id,
            ...User
          })
        })

        return haveIdUsers
      } catch (error) {
        console.log('🚀 ~ file: UserList.vue:457 ~ computedUsers ~ error:', error)
      }
    },

    computedSelectedUsers() {
      if (this.selectedUsers.length <= 1) {
        this.batchExecutionDisable = true
      } else {
        this.batchExecutionDisable = false
      }
      return this.selectedUsers
    },

    computedNoData() {
      return this.noData
    },

    computedDeletePopupText() {
      if (this.isShowConfirmDeletePopup) {
        return this.$t('Bạn có thực sự muốn xóa người dùng không?')
      } else if (this.isShowConfirmDeleteMultiplePopup) {
        return this.$t('UserSubsystem.UserContent.deleteMultiplePopupTitle', {
          count: this.selectedUsers.length
        })
      } else {
        return ''
      }
    }
  },
  watch: {
    searchText: debounce(function () {
      this.pagingData.pageNumber = 1
      this.reloadData()
    }, 500),

    pagingData: {
      handler: function (newValue) {
        localStorage.setItem('pageNumber', newValue.pageNumber)
        localStorage.setItem('pageSize', newValue.pageSize)
      },

      deep: true
    },

    UserColumnsInfoRaw(newValue) {
      let tempMapArray = this.mapColumnsInfoFromRawToCode(newValue)
      /**
       * do là việc lấy dữ liệu gây ra thay đổi cho UserColumnsInfo
       * nên không gọi đến hàm cập nhật
       */
      this.isUpdateColumnsInfo = false

      this.UserColumnsInfo = tempMapArray
    },

    UserColumnsInfo: {
      handler: debounce(function () {
        if (this.isUpdateColumnsInfo) {
          this.updateColumnsInfoToDB(this.UserColumnsInfo)
        }
      }, 500),

      deep: true
    }
  }
}
</script>

<style scoped>
@import url(./user-content.css);
</style>
