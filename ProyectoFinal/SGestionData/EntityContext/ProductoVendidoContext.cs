using System;
using System.Collections.Generic;

namespace SGestionData.EntityContext
{
    public partial class ProductoVendidoContext
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }

        public virtual ProductoContext IdProductoNavigation { get; set; } = null!;
        public virtual VentumContext IdVentaNavigation { get; set; } = null!;
    }
}
