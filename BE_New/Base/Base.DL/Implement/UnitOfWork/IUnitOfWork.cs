using System.Data.Common;

namespace Base.DL
{
    /// <summary>
    /// Quản lý connection, transaction của db
    /// </summary>
    /// CreatedBy: TTANH (18/07/2024)
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        #region Fields
        DbConnection Connection { get; }
        DbTransaction? Transaction { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Bắt đầu transaction
        /// </summary>
        /// CreatedBy: TTANH (18/07/2024)
        void BeginTransaction();

        /// <summary>
        /// Bắt đầu transaction bất đồng bộ
        /// </summary>
        /// CreatedBy: TTANH (18/07/2024)
        Task BeginTransactionAsync();

        /// <summary>
        /// Xác nhận transaction
        /// </summary>
        /// CreatedBy: TTANH (18/07/2024)
        void Commit();

        /// <summary>
        /// Xác nhận transaction bất đồng bộ
        /// </summary>
        /// CreatedBy: TTANH (18/07/2024)
        Task CommitAsync();

        /// <summary>
        /// Quay trở lại từ điểm bắt đầu transaction
        /// </summary>
        /// CreatedBy: TTANH (18/07/2024)
        void Rollback();

        /// <summary>
        /// Quay trở lại từ điểm bắt đầu transaction bất đồng bộ
        /// </summary>
        /// CreatedBy: TTANH (18/07/2024)
        Task RollbackAsync();
        #endregion
    }
}