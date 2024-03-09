using SGestionData.DataContext;
using SGestionData.EntityContext;
using SGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGestionBussiness
{
    public class ProductoBussiness
    {
        public List<Producto> BuscarProducto(int idUser)
        {
            using (SGestionContext context = new())
            {
                List<ProductoContext> producSearch = context.Productos.Where(b => b.IdUsuario == idUser).ToList();
                List<Producto> producSearchMap = new List<Producto>();
                foreach (ProductoContext item in producSearch)
                {
                    producSearchMap.Add(MapearProductoContext(item));
                }
                return producSearchMap;
            }
        }
        public bool AgregarProducto(Producto product)
        {
            if (ValidarProducto(product))
            {
                ProductoContext newProduct = MapearProducto(product);
                using (SGestionContext context = new())
                {
                    context.Add(newProduct);
                    context.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool ModificarProducto(Producto product)
        {
            using (SGestionContext context = new())
            {
                ProductoContext producSearched = context.Productos.Where(b => b.Id == product.Id).FirstOrDefault();
                if (producSearched is not null)
                {
                    producSearched.Descripciones = product.Descripciones;
                    producSearched.Costo = product.Costo;
                    producSearched.PrecioVenta = product.PrecioVenta;
                    producSearched.Stock = product.Stock;
                    producSearched.IdUsuario = product.IdUsuario;

                    context.Productos.Update(producSearched);
                    context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
        }
        public bool EliminarProducto(int id)
        {
            using (SGestionContext context = new())
            {
                ProductoContext producSearched = context.Productos.Where(b => b.Id == id).FirstOrDefault();
                List<ProductoVendidoContext> producVendidoSearched = context.ProductoVendidos.Where(v => v.IdProducto == id).ToList();
                if (producSearched is not null)
                {
                    try
                    {
                        foreach (ProductoVendidoContext item in producVendidoSearched)
                        {
                            context.ProductoVendidos.Remove(item);
                            context.SaveChanges();
                        }
                        context.Productos.Remove(producSearched);
                        context.SaveChanges();
                        return true;
                    }
                    catch (Exception ex) { return false; }
                }
                else { return false; }
            }
        }



        private bool ValidarProducto(Producto productCheck)
        {
            if (productCheck.Descripciones != string.Empty)
            {
                if (productCheck.Costo >= 0 && productCheck.PrecioVenta >= 0 && productCheck.Stock >= 0 && productCheck.IdUsuario >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private ProductoContext MapearProducto(Producto productMap)
        {
            ProductoContext newProduct = new();
            newProduct.Id = productMap.Id;
            newProduct.Descripciones = productMap.Descripciones;
            newProduct.Costo = productMap.Costo;
            newProduct.PrecioVenta = productMap.PrecioVenta;
            newProduct.Stock = productMap.Stock;
            newProduct.IdUsuario = productMap.IdUsuario;

            return newProduct;
        }
        private Producto MapearProductoContext(ProductoContext productMap)
        {
            Producto newProduct = new();
            newProduct.Id = productMap.Id;
            newProduct.Descripciones = productMap.Descripciones;
            newProduct.Costo = productMap.Costo;
            newProduct.PrecioVenta = productMap.PrecioVenta;
            newProduct.Stock = productMap.Stock;
            newProduct.IdUsuario = productMap.IdUsuario;

            return newProduct;
        }
    }
}
