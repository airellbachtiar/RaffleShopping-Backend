using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaffleShopping.Services.Catalogs.Dtos
{
    public class AddCatalogDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
