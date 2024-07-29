namespace PEMS.Domain
{
    /// <summary>
    /// Interface để tương tác với code của các entity
    /// </summary>
    /// CreatedBy: TTANH (25/07/2024)
    public interface IEntityHasCode
    {
        /// <summary>
        /// Lấy ra code của phần tử
        /// </summary>
        /// CreatedBy: TTANH (25/07/2024)
        string GetCode();

        /// <summary>
        /// Hàm set code cho phần tử
        /// </summary>
        /// <param name="code">giá trị của code</param>
        /// CreatedBy: TTANH (25/07/2024)
        void SetCode(string code);
    }
}
