using System;
using System.Collections.Generic;

namespace SGestionData.EntityContext
{
    public partial class VentumContext
    {
        public VentumContext()
        {
            ProductoVendidos = new HashSet<ProductoVendidoContext>();
        }

        public int Id { get; set; }
        public string? Comentarios { get; set; }
        public int IdUsuario { get; set; }

        public virtual UsuarioContext IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<ProductoVendidoContext> ProductoVendidos { get; set; }
    }
}
