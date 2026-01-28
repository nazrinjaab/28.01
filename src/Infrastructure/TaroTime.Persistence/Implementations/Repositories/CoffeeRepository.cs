using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.Interfaces.Repositories;
using TaroTime.Domain.Entities;
using TaroTime.Persistence.Contexts;
using TaroTime.Persistence.Implementations.Repositories.Generic;

namespace TaroTime.Persistence.Implementations.Repositories
{
    internal class CoffeeRepository:Repository<CoffeeReading>, ICoffeeRepository
    {
        public CoffeeRepository(AppDbContext context) : base(context) { }
    }
}
