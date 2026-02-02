using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.Interfaces.Repositories.Horoscope;
using TaroTime.Domain.Entities;
using TaroTime.Persistence.Contexts;
using TaroTime.Persistence.Implementations.Repositories.Generic;

namespace TaroTime.Persistence.Implementations.Repositories.Horoscope
{
    internal class CompatibilityZodiacRepository:Repository<CompatibilityZodiac> , ICompatibilityZodiacRepository
    {
        public CompatibilityZodiacRepository(AppDbContext context) : base(context) { }
    }
}
