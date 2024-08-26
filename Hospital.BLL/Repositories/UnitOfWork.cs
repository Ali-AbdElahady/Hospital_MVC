using Hospital.BLL.Interfaces;
using Hospital.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Repositories
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly HospitalDbContext _dbContext;

        public UnitOfWork(HospitalDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IGenericRepository<T> GenerateGenericRepo<T>() where T : class
        {
            IGenericRepository<T> repo = new GenericRepository<T>(_dbContext);
            return repo;
        }
    }
}
