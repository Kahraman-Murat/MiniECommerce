using MiniECommerce.Domain.Entities.Common;
using MiniECommerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
