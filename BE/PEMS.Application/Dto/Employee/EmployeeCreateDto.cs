﻿using PEMS.Domain;
using System.ComponentModel.DataAnnotations;

namespace PEMS.Application
{
    /// <summary>
    /// Dto tạo nhân viên
    /// </summary>
    public class EmployeeCreateDto
    {
        #region Property
        /// <summary>
        /// Mã code của nhân viên
        /// </summary>
        [Required]
        [StringLength(20)]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên của nhân viên
        /// </summary>
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        /// <summary>
        /// Id của phòng ban
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã code của phòng ban
        /// </summary>
        [Required]
        [StringLength(20)]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên của phòng ban
        /// </summary>
        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Giới tính của nhân viên
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Ngày sinh của nhân viên
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Chức danh của nhân viên
        /// </summary>
        [StringLength(100)]
        public string? Position { get; set; }

        /// <summary>
        /// Thuộc tính là khách hàng, nhà cung cấp của nhân viên
        /// </summary>
        [StringLength(255)]
        public string? SupplierCustomerGroup { get; set; }

        /// <summary>
        /// Số CMND của nhân viên
        /// </summary>
        [StringLength(25)]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp CMND của nhân viên
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp CMND của nhân viên
        /// </summary>
        [StringLength(255)]
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// TK công nợ phải trả của nhân viên
        /// </summary>
        [StringLength(100)]
        public string? PayAccount { get; set; }

        /// <summary>
        /// TK công nợ phải thu của nhân viên
        /// </summary>
        [StringLength(100)]
        public string? ReceiveAccount { get; set; }

        /// <summary>
        /// Lương thỏa thuận của nhân viên
        /// </summary>
        public double? Salary { get; set; }

        /// <summary>
        /// Hệ số lương của nhân viên
        /// </summary>
        public double? SalaryCoefficients { get; set; }

        /// <summary>
        /// Lương đóng bảo hiểm của nhân viên
        /// </summary>
        public double? SalaryPaidForInsurance { get; set; }

        /// <summary>
        /// Mã số thuế của nhân viên
        /// </summary>
        [StringLength(25)]
        public string? PersonalTaxCode { get; set; }

        /// <summary>
        /// Loại hợp đồng của nhân viên
        /// </summary>
        [StringLength(255)]
        public string? TypeOfContract { get; set; }

        /// <summary>
        /// Số người phụ thuộc
        /// </summary>
        public int? NumberOfDependents { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng của nhân viên
        /// </summary>
        [StringLength(25)]
        public string? AccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [StringLength(255)]
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        [StringLength(255)]
        public string? BankBranch { get; set; }

        /// <summary>
        /// Tỉnh/TP của ngân hàng
        /// </summary>
        [StringLength(255)]
        public string? BankProvince { get; set; }

        /// <summary>
        /// Địa chỉ của nhân viên
        /// </summary>
        [StringLength(255)]
        public string? ContactAddress { get; set; }

        /// <summary>
        /// Điện thoại di động của nhân viên
        /// </summary>
        [StringLength(50)]
        public string? ContactPhoneNumber { get; set; }

        /// <summary>
        /// ĐT cố định của nhân viên
        /// </summary>
        [StringLength(50)]
        public string? ContactLandlinePhoneNumber { get; set; }

        /// <summary>
        /// Email của nhân viên
        /// </summary>
        [EmailAddress(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "Wrong_Format_Email")]
        [StringLength(100)]
        public string? ContactEmail { get; set; }
        #endregion
    }
}
