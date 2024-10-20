using Microsoft.AspNetCore.Http;
using Base.Model;
using Base.DL;

namespace Base.BL
{
    public abstract class ReadOnlyService<TEntity> : IReadOnlyService<TEntity>
    {
        #region Fields
        protected readonly IReadOnlyRepository<TEntity> _readOnlyRepository;

        public virtual string tableName { get; protected set; } = typeof(TEntity).Name;
        public virtual string tableId { get; protected set; } = typeof(TEntity).Name + "Id";

        public readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public ReadOnlyService(IBaseRepository<TEntity> readOnlyRepository, IHttpContextAccessor httpContextAccessor)
        {
            _readOnlyRepository = readOnlyRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns>Mảng chứa các bản ghi</returns>
        /// Created by: TTANH (18/07/2024)
        public async Task<IEnumerable<TEntity>?> GetAsync()
        {
            var entities = await _readOnlyRepository.GetAsync();

            if (entities == null)
            {
                throw new NotFoundException();
            }

            return entities;
        }

        /// <summary>
        /// Hàm lấy một bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Bản ghi, throw exception nếu không tìm thấy</returns>
        /// Created by: TTANH (18/07/2024)
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await _readOnlyRepository.GetByIdAsync(id);

            return entity;
        }

        /// <summary>
        /// Hàm lọc dữ liệu bản ghi
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Thứ tự trang bao nhiêu</param>
        /// <param name="searchText">Chuỗi tìm kiếm</param>
        /// <returns>Các bản lọc theo các tiêu chí</returns>
        /// Created by: TTANH (18/07/2024)
        public async Task<BaseFilterResponse<TEntity>> FilterAsync(int? pageSize, int? pageNumber, string? searchText)
        {
            if (pageSize == null || pageSize <= 0) { pageSize = 999999; }
            if (pageNumber == null || pageNumber < 1) { pageNumber = 1; }

            var entitiesNotPagging = await _readOnlyRepository.FilterAsync(int.MaxValue, 1, searchText);

            var totalRecord = entitiesNotPagging.Count();
            var totalPage = Convert.ToInt32(Math.Ceiling((double)totalRecord / (double)pageSize));

            if (pageNumber > totalPage && totalPage > 0)
            {
                pageNumber = totalPage;
            }

            var entities = await _readOnlyRepository.FilterAsync(pageSize, pageNumber, searchText);

            if (entities == null)
            {
                throw new NotFoundException();
            }

            var currentPage = pageNumber;
            var currentPageRecords = entities.Count();

            var filterData = new BaseFilterResponse<TEntity>(totalPage, totalRecord, currentPage, currentPageRecords, entities.ToList());

            return filterData;
        }
        #endregion
    }
}
