using EFTutorials.CRUD.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.Repository
{
    public class PriceRepository : Repository<Price>, IPriceRepository
    {
    }

    public interface IPriceRepository : IRepository<Price>
    {
    }
}
