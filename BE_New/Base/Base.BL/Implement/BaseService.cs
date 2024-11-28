using Base.Model;
using Base.DL;
using Microsoft.AspNetCore.Http;

namespace Base.BL
{
    public abstract class BaseService<TEntity> : ReadOnlyService<TEntity>, IBaseService<TEntity> where TEntity : IEntityHasKey
    {
        #region Fields
        protected readonly IBaseRepository<TEntity> _baseRepository;
        #endregion

        #region Constructor
        protected BaseService(IBaseRepository<TEntity> baseRepository, IHttpContextAccessor httpContextAccessor) : base(baseRepository, httpContextAccessor)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Thêm xử lý khi thêm bản ghi
        /// </summary>
        /// CreatedBy: TTANH (21/07/2024)
        public virtual async Task BeforeInsertAsync(TEntity entity) { }
        public virtual async Task AfterInsertAsync(TEntity entity) { }

        /// <summary>
        /// Hàm thêm bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu của bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (18/07/2024)
        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            await BeforeInsertAsync(entity);

            entity.SetKey(Guid.NewGuid());

            if (entity is BaseModel baseAuditEntity)
            {
                baseAuditEntity.CreatedDate = DateTime.Now;
                baseAuditEntity.CreatedBy = _httpContextAccessor?.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "Fullname")?.Value ?? "User create";
            }

            var result = await _baseRepository.InsertAsync(entity);

            await AfterInsertAsync(entity);

            return result;
        }

        /// <summary>
        /// Thêm xử lý khi cập nhật bản ghi
        /// </summary>
        /// CreatedBy: TTANH (21/07/2024)
        public virtual async Task BeforeUpdateAsync(Guid id, TEntity entity) { }
        public virtual async Task AfterUpdateAsync(Guid id, TEntity entity) { }

        /// <summary>
        /// Hàm cập nhật thông tin bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <param name="entity">Dữ liệu update bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (18/07/2024)
        public virtual async Task<int> UpdateAsync(Guid id, TEntity entity)
        {
            await BeforeUpdateAsync(id, entity);

            foreach (var property in entity.GetType().GetProperties())
            {
                var propertyName = property?.Name;
                var propertyValue = property?.GetValue(entity);

                if ((propertyName != null))
                {
                    var propertyInfo = entity.GetType().GetProperty(propertyName);

                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(entity, propertyValue, null);
                    }
                }
            }

            if (entity is BaseModel baseAuditEntity)
            {
                baseAuditEntity.ModifiedDate = DateTime.Now;
                baseAuditEntity.ModifiedBy = "USER UPDATE";
            }

            var result = await _baseRepository.UpdateAsync(id, entity);
            
            await AfterUpdateAsync(id, entity);

            return result;
        }

        /// <summary>
        /// Hàm xóa bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (18/07/2024)
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);

            var result = await _baseRepository.DeleteAsync(entity);

            return result;
        }

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">List các id của bản ghi</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: TTANH (18/07/2024)
        public virtual async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            if (ids.Count() == 0)
            {
                throw new ValidateException(StatusErrorCode.DeleteEmptyError, Resource.Delete_Empty_Error, null);
            }

            var entities = await _baseRepository.GetListByIdsAsync(ids);

            var result = await _baseRepository.DeleteMultipleAsync(entities);

            return result;
        }
        #endregion
    }
}
