using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Entities.common;

namespace TaroTime.Domain.Entities
{
    public class Color : BaseNameableEntity
    {
        public ICollection<ProductColor> ProductColors { get; set; }
    }
}
