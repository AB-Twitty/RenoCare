using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Extensions;
using RenoCare.Core.Helpers;
using RenoCare.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenoCare.Persistence
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Fields

        private readonly AppDbContext _dbContext;

        #endregion

        #region Ctor

        public EntityRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public IQueryable<TEntity> Table => _dbContext.Set<TEntity>();

        #endregion

        #region Utils

        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="getAllAsync">Function to select entries</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        protected virtual async Task<IList<TEntity>> GetEntitiesAsync(Func<Task<IList<TEntity>>> getAllAsync)
        {
            return await GetAllAsync();
        }

        /// <summary>
        /// Adds "deleted" filter to query which contains <see cref="ISoftDeletedEntity"/> entries, if its need
        /// </summary>
        /// <param name="query">Entity entries</param>
        /// <param name="includeDeleted">Whether to include deleted items</param>
        /// <returns>Entity entries</returns>
        protected virtual IQueryable<TEntity> AddDeletedFilter(IQueryable<TEntity> query, in bool includeDeleted)
        {
            if (includeDeleted)
                return query;

            if (typeof(TEntity).GetInterface(nameof(ISoftDeletedEntity)) == null)
                return query;

            return query.OfType<ISoftDeletedEntity>().Where(entry => !entry.IsDeleted).OfType<TEntity>();
        }

        #endregion

        #region Methods

        /// <summary>
		/// Apply a specific query on the entity entries.
		/// </summary>
		/// <typeparam name="T">Type of the query return value.</typeparam>
		/// <param name="func">Function to apply on entries.</param>
		/// <param name="includeDeleted">Whether to include deleted items.</param>
		/// <returns>
		/// A task represents the asynchronous operation, 
		/// The task result contains the return value of the query.
		/// </returns>
		public async Task<T> ApplyQueryAsync<T>(Func<IQueryable<TEntity>, Task<T>> func, bool includeDeleted = false)
        {
            async Task<T> getAsync()
            {
                var query = AddDeletedFilter(Table, includeDeleted);
                return await func(query);
            }

            return await getAsync();
        }

        /// <summary>
        /// Get the entity entry
        /// </summary>
        /// <param name="id">Entity entry identifier</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="ISoftDeletedEntity"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entry
        /// </returns>
        public async Task<TEntity> GetByIdAsync(int? id, bool includeDeleted = true)
        {
            if (!id.HasValue || id == 0)
                return null;

            async Task<TEntity> getEntityAsync()
            {
                return await AddDeletedFilter(Table, includeDeleted).FirstOrDefaultAsync(entity => entity.Id == id);
            }


            return await getEntityAsync();
        }

        /// <summary>
        /// Get entity entries by identifiers
        /// </summary>
        /// <param name="ids">Entity entry identifiers</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="ISoftDeletedEntity"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        public async Task<IList<TEntity>> GetByIdsAsync(IList<int> ids, bool includeDeleted = true)
        {
            if (!ids?.Any() ?? true)
                return new List<TEntity>();

            async Task<IList<TEntity>> getByIdsAsync()
            {
                var query = AddDeletedFilter(Table, includeDeleted);

                //get entries
                var entries = await query.Where(entry => ids.Contains(entry.Id)).ToListAsync();

                //sort by passed identifiers
                var sortedEntries = new List<TEntity>();
                foreach (var id in ids)
                {
                    var sortedEntry = entries.Find(entry => entry.Id == id);
                    if (sortedEntry != null)
                        sortedEntries.Add(sortedEntry);
                }

                return sortedEntries;
            }

            return await getByIdsAsync();
        }

        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="ISoftDeletedEntity"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        public async Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null, bool includeDeleted = true)
        {
            async Task<IList<TEntity>> getAllAsync()
            {
                var query = AddDeletedFilter(Table, includeDeleted);
                query = func != null ? func(query) : query;

                return await query.ToListAsync();
            }

            return await GetEntitiesAsync(getAllAsync);
        }

        /// <summary>
        /// Get paged list of all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="indexFrom">The start index value.</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="Nop.Core.Domain.Common.ISoftDeletedEntity"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the paged list of entity entries
        /// </returns>
        public virtual async Task<IPagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            int pageIndex = 0, int pageSize = int.MaxValue, int indexFrom = 1, bool includeDeleted = true)
        {
            var query = AddDeletedFilter(Table, includeDeleted);

            query = func != null ? func(query) : query;

            return await query.ToPagedListAsync(pageIndex, pageSize, indexFrom);
        }

        /// <summary>
        /// Insert the entity entry
        /// </summary>
        /// <param name="entity">Entity entry</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dbContext.AddAsync(entity);
        }

        /// <summary>
        /// Update the entity entry
        /// </summary>
        /// <param name="entity">Entity entry</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Update(entity);
        }

        /// <summary>
        /// Delete the entity entry
        /// </summary>
        /// <param name="entity">Entity entry</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task DeleteAsync(TEntity entity)
        {
            switch (entity)
            {
                case null:
                    throw new ArgumentNullException(nameof(entity));

                case ISoftDeletedEntity softDeletedEntity:
                    softDeletedEntity.IsDeleted = true;
                    _dbContext.Update(entity);
                    break;

                default:
                    _dbContext.Remove(entity);
                    break;
            }
        }

        /// <summary>
        /// Save db commands performed
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        #endregion
    }
}
