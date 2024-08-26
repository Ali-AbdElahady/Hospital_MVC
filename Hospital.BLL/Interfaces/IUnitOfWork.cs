using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Interfaces
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenericRepository<T> GenerateGenericRepo<T>() where T : class;
    }
}
