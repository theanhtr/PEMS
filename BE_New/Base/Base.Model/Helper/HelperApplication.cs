﻿namespace Base.Model
{
    public class HelperApplication
    {
        /// <summary>
        /// Chuyển đổi một mảng thành một chuỗi string với phần tử ngăn cách
        /// </summary>
        /// <typeparam name="TType">Loại kiểu dữ liệu</typeparam>
        /// <param name="values">Mảng muốn chuyển</param>
        /// <param name="separatorElement">Phần tử ngăn cách</param>
        /// <returns>Đã được chuyển thành chuỗi với phần tử ngăn cách</returns>
        /// <example>IN: [1, 2, 3] -> "'1','2','3'"</example>
        /// CreatedBy: TTANH (27/07/2024)
        public static string ConvertArrayToStringWithSeparatorElement<TType>(List<TType> values, string separatorElement)
        {
            var strConvert = "";

            foreach (var value in values)
            {
                var valueFormat = "\'" + value?.ToString() + "\'";
                strConvert = strConvert + valueFormat + separatorElement;
            }

            // Xóa phần tử ngăn cách thừa ở cuối
            strConvert = strConvert.Substring(0, strConvert.Length - 1);

            return strConvert;
        }

        public static object GetPropertyValue(object obj, string propertyName)
        {
            // Sử dụng Reflection để lấy PropertyInfo của thuộc tính "a"
            var propertyInfo = obj.GetType().GetProperty(propertyName);

            // Kiểm tra nếu propertyInfo tồn tại và có thể đọc được
            if (propertyInfo != null && propertyInfo.CanRead)
            {
                // Trả về giá trị của thuộc tính
                return propertyInfo.GetValue(obj);
            }

            // Nếu không tìm thấy thuộc tính, trả về null hoặc xử lý ngoại lệ
            return null;
        }

        #region Password
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (password == null || hashedPassword == null)
                throw new ValidateException(StatusErrorCode.WrongPassword, "Sai mật khẩu", "");

            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        #endregion
    }
}
