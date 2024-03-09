using SGestionData.DataContext;
using SGestionData.EntityContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGestionEntities;

namespace SGestionBussiness
{
    public class VentaBussiness
    {
        public bool AgregarVenta(List<Producto> productos, int idUsuarios)
        {
            using (SGestionContext context = new())
            {
                VentumContext venta = new();
                List<string> listaProduc = new();
                foreach (Producto item in productos)
                {
                    listaProduc.Add(item.Descripciones);
                }
                venta.Comentarios = String.Join("-", listaProduc);
                venta.IdUsuario = idUsuarios;
                context.Venta.Add(venta);
                context.SaveChanges();
                DescontarStock(productos, venta.Id);
                return true;
            }
        }

        public List<Venta> ListarVentas(int idUser)
        {
            List<VentumContext> ventas;
            using (SGestionContext context = new())
            {
                ventas = context.Venta.Where(v => v.IdUsuario == idUser).ToList();
                return MapearVentas(ventas);
            }
        }

        public bool EliminarVenta(int idVenta)
        {
            using (SGestionContext context = new())
            {
                try
                {
                    VentumContext venta = context.Venta.Where(v => v.Id == idVenta).FirstOrDefault();
                    context.Venta.Remove(venta);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                    throw new Exception($"Error al eliminar la venta\n {ex}");
                }
            }
        }

        private void DescontarStock(List<Producto> productos, int idVenta)
        {
            using (SGestionContext context = new())
            {
                ProductoContext product;
                foreach (Producto item in productos)
                {
                    product = context.Productos.Where(b => b.Id == item.Id).FirstOrDefault();
                    product.Stock -= item.Stock;
                    context.Productos.Update(product);
                    context.SaveChanges();
                    CargarProductosVendidos(item, idVenta);
                }
            }
        }
        private void CargarProductosVendidos(Producto productos, int idVenta)
        {
            using (SGestionContext context = new())
            {
                ProductoVendido productVendido = new();
                productVendido.Stock = productos.Stock;
                productVendido.IdProducto = productos.Id;
                productVendido.IdVenta = idVenta;
                context.ProductoVendidos.Add(MapearProductoVendido(productVendido));
                context.SaveChanges();
            }
        }
        private ProductoVendidoContext MapearProductoVendido(ProductoVendido productMap)
        {
            ProductoVendidoContext newProduct = new();
            newProduct.Stock = productMap.Stock;
            newProduct.IdProducto = productMap.IdProducto;
            newProduct.IdVenta = productMap.IdVenta;

            return newProduct;
        }
        private List<Venta> MapearVentas(List<VentumContext> listVentas)
        {
            List<Venta> ventas = new();
            foreach (VentumContext item in listVentas)
            {
                Venta venta = new();
                venta.Id = item.Id;
                venta.Comentarios = item.Comentarios;
                venta.IdUsuario = item.IdUsuario;
                ventas.Add(venta);
            }
            return ventas;
        }
    }
}
