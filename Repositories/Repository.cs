using Microsoft.EntityFrameworkCore;
using ShoppingApp.Data;
using System.Linq.Expressions;

namespace ShoppingApp.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Retrieves all entities from the data source asynchronously.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains
        /// a collection of all entities.
        /// </returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Retrieves all entities that match the specified filter condition asynchronously.
        /// </summary>
        /// <param name="predicate">
        /// A LINQ expression used to filter the entities.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains
        /// a collection of entities matching the given condition.
        /// </returns>
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Retrieves an entity by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the entity to retrieve.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains
        /// the entity if found; otherwise, <c>null</c>.
        /// </returns>
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Retrieves the first entity that matches the specified condition, or the default value if no match is found, asynchronously.
        /// </summary>
        /// <param name="predicate">
        /// A LINQ expression used to define the filter condition.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains
        /// the first matching entity, or <c>null</c> if no entity satisfies the condition.
        /// </returns>
        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Adds a new entity to the data source asynchronously and persists the changes.
        /// </summary>
        /// <param name="entity">
        /// The entity to be added to the data source.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains
        /// the newly added entity.
        /// </returns>
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Updates an existing entity in the data source asynchronously and saves the changes.
        /// </summary>
        /// <param name="entity">
        /// The entity containing updated values to be persisted.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous update operation.
        /// </returns>
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the specified entity from the data source asynchronously and persists the changes.
        /// </summary>
        /// <param name="entity">
        /// The entity to be removed from the data source.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous delete operation.
        /// </returns>
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Determines whether any entity in the data source satisfies the specified condition asynchronously.
        /// </summary>
        /// <param name="predicate">
        /// A LINQ expression used to define the condition to check.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result is <c>true</c>
        /// if any entity satisfies the condition; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        /// <summary>
        /// Provides an <see cref="IQueryable{T}"/> for the entity set to enable further querying.
        /// </summary>
        /// <returns>
        /// An <see cref="IQueryable{T}"/> representing the entity set.
        /// </returns>
        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }
    }
}
