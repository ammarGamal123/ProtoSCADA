using Microsoft.EntityFrameworkCore;
using ProtoSCADA.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly ApplicationDbContext _context;
        public readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentException(nameof(entity));    
                }
                await _dbSet.AddAsync(entity);
            }
            catch (Exception ex) 
            {
                throw new ApplicationException($"Error adding entity: {ex.Message}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"Entity with ID {id} not found.");

                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error deleting entity: {ex.Message}", ex);
            }
        }


        public async Task<IEnumerable<T>> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate));

                return await _dbSet.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error finding entities: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving all entities: {ex.Message}", ex);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving entity by ID: {ex.Message}", ex);
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _dbSet.Update(entity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error updating entity: {ex.Message}", ex);
            }
        }
    }
}
