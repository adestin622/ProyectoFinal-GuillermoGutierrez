using SGestionData.DataContext;
using SGestionData.EntityContext;
using SGestionEntities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SGestionBussiness
{
    public class ProductoVendidoBussiness
    {
        public List<ProductoVendido> TraerProductosVendidos(int idUsuario)
        {
            using (SGestionContext context = new())
            {
                List<VentumContext> ventas = context.Venta.Where(v => v.IdUsuario == idUsuario).ToList();
                List<ProductoVendidoContext> prodVend = new();
                foreach (VentumContext item in ventas)
                {
                    prodVend.AddRange(context.ProductoVendidos.Where(pv => pv.IdVenta == item.Id).ToList());
                }
                return MapearProductoVendidoContext(prodVend);
            }
        }

        private List<ProductoVendido> MapearProductoVendidoContext(List<ProductoVendidoContext> prodVendContext)
        {
            List<ProductoVendido> listProdVend = new();
            foreach (ProductoVendidoContext item in prodVendContext)
            {
                ProductoVendido prodVend = new();
                prodVend.Id = item.Id;
                prodVend.Stock = item.Stock;
                prodVend.IdProducto = item.IdProducto;
                prodVend.IdVenta = item.IdVenta;
                listProdVend.Add(prodVend);
            }
            return listProdVend;
        }
    }
}
