using DemoMvcAgain.BLL.Specifications;
using Hospital.BLL.Interfaces;
using Hospital.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HospitalDbContext _dbContext;

        public GenericRepository(HospitalDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var Data = await _dbContext.Set<T>().ToListAsync();
            return Data;
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            var data = await SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), spec).ToListAsync();
            return data;
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public void UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), spec);
        }
    }
}
