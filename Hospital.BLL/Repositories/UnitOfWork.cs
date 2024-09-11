using Hospital.BLL.Interfaces;
using Hospital.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HospitalDbContext _dbContext;
        private Hashtable _repositorys;

        public UnitOfWork(HospitalDbContext dbContext)
        {
            this._dbContext = dbContext;
			_repositorys = new Hashtable();
        }

        public async Task<int> CompleteAsync() { return await _dbContext.SaveChangesAsync(); }

        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

        public IGenericRepository<T> GenerateGenericRepo<T>() where T : class
        {
            var type = typeof(T).Name; 
            if (!_repositorys.ContainsKey(type))
            {
                var Repository = new GenericRepository<T>(_dbContext);
                _repositorys.Add(type, Repository);
            }
            return _repositorys[type] as IGenericRepository<T>;
        }
    }
}
