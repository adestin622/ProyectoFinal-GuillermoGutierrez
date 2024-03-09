using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGestionEntities
{
    public class Producto
    {
        public int Id { get; set; }

        public string Descripciones { get; set; } = null!;

        public decimal Costo { get; set; }

        public decimal PrecioVenta { get; set; }

        public int Stock { get; set; }

        public int IdUsuario { get; set; }
    }
}
