using RenoCare.Core.Helpers;
using RenoCare.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenoCare.Core.Conatracts.Persistence
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }

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
		Task<T> ApplyQueryAsync<T>(Func<IQueryable<TEntity>, Task<T>> func, bool includeDeleted = true);

        /// <summary>
        /// Get the entity entry
        /// </summary>
        /// <param name="id">Entity entry identifier</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="Nop.Core.Domain.Common.ISoftDeletedEntity"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entry
        /// </returns>
        Task<TEntity> GetByIdAsync(int? id, bool includeDeleted = true);

        /// <summary>
        /// Get entity entries by identifiers
        /// </summary>
        /// <param name="ids">Entity entry identifiers</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="ISoftDeletedEntity"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        Task<IList<TEntity>> GetByIdsAsync(IList<int> ids, bool includeDeleted = true);

        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="ISoftDeletedEntity"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null, bool includeDeleted = true);

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
        public Task<IPagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            int pageIndex = 0, int pageSize = int.MaxValue, int indexFrom = 1, bool includeDeleted = true);

        /// <summary>
        /// Insert the entity entry
        /// </summary>
        /// <param name="entity">Entity entry</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Update the entity entry
        /// </summary>
        /// <param name="entity">Entity entry</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete the entity entry
        /// </summary>
        /// <param name="entity">Entity entry</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Save db commands performed
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task SaveAsync();

        #endregion
    }
}
