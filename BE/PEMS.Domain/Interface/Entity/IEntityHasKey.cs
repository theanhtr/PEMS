namespace PEMS.Domain
{
    /// <summary>
    /// Interface để tương tác với key của các entity
    /// </summary>
    /// CreatedBy: TTANH (18/07/2024)
    public interface IEntityHasKey
    {
        /// <summary>
        /// Lấy ra key của phần tử
        /// </summary>
        /// CreatedBy: TTANH (18/07/2024)
        Guid GetKey();

        /// <summary>
        /// Hàm set key cho phần tử
        /// </summary>
        /// <param name="key">giá trị của key</param>
        /// CreatedBy: TTANH (18/07/2024)
        void SetKey(Guid key);
    }
}
