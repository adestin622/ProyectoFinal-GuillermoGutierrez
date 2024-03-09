using System;
using System.Collections.Generic;

namespace SGestionData.EntityContext
{
    public partial class ProductoContext
    {
        public ProductoContext()
        {
            ProductoVendidos = new HashSet<ProductoVendidoContext>();
        }

        public int Id { get; set; }
        public string Descripciones { get; set; } = null!;
        public decimal Costo { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }

        public virtual UsuarioContext IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<ProductoVendidoContext> ProductoVendidos { get; set; }
    }
}
