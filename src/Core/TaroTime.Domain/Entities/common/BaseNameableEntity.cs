using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaroTime.Domain.Entities.common
{
    public abstract class BaseNameableEntity : BaseAccountableEntity
    {
        public string Name { get; set; }
    }
}
