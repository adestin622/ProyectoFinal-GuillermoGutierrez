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
    public class UsuarioBussiness
    {
        public bool ModificarUsuario(Usuario usuario)
        {
            using (SGestionContext context = new())
            {
                UsuarioContext userSearched = context.Usuarios.Where(b => b.Id == usuario.Id).FirstOrDefault();
                if (userSearched is not null)
                {
                    userSearched.Nombre = usuario.Nombre;
                    userSearched.Apellido = usuario.Apellido;
                    userSearched.NombreUsuario = usuario.NombreUsuario;
                    userSearched.Contraseña = usuario.Contrasena;
                    userSearched.Mail = usuario.Mail;

                    context.Usuarios.Update(userSearched);
                    context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
        }

        public Usuario InicioSesion(string user, string pass)
        {
            using (SGestionContext context = new())
            {
                UsuarioContext? usuario = context.Usuarios.Where(u => u.NombreUsuario == user && u.Contraseña == pass).FirstOrDefault();
                if (usuario is not null)
                {
                    return MapearUsuarioContext(usuario);
                }
                return null;
            }
        }

        public bool CrearUsuario(Usuario user)
        {
            using (SGestionContext context = new())
            {
                try
                {
                    UsuarioContext usuarioNew = MapearUsuario(user);
                    context.Usuarios.Add(usuarioNew);
                    context.SaveChanges();
                    return true;
                }catch (Exception ex) 
                {
                    return false;
                    throw new Exception($"Error al crear el usuario\n{ex}");
                }
            }
        }

        public Usuario TraerUsuario(string nameUser)
        {
            using (SGestionContext context = new())
            {
                UsuarioContext user;
                user = context.Usuarios.Where(u => u.NombreUsuario == nameUser).FirstOrDefault();
                return MapearUsuarioContext(user);
            }
        }

        public bool EliminarUsuario(int idUsuario)
        {
            using (SGestionContext context = new())
            {
                try
                {
                    UsuarioContext? user = context.Usuarios.Where(u => u.Id == idUsuario).FirstOrDefault();
                    context.Usuarios.Remove(user);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                    throw new Exception($"Error al eliminar el usuario\n {ex}");
                }
            }
        }

        private UsuarioContext MapearUsuario(Usuario user)
        {
            UsuarioContext userContext = new();
            if (user is not null)
            {
                userContext.Id = user.Id;
                userContext.Nombre = user.Nombre;
                userContext.Apellido = user.Apellido;
                userContext.NombreUsuario = user.NombreUsuario;
                userContext.Contraseña = user.Contrasena;
                userContext.Mail = user.Mail;
                return userContext;
            }
            else { return null; }
            
        }
        private Usuario MapearUsuarioContext(UsuarioContext user)
        {
            Usuario userMap = new();
            if (user is not null)
            {
                userMap.Id = user.Id;
                userMap.Nombre = user.Nombre;
                userMap.Apellido = user.Apellido;
                userMap.NombreUsuario = user.NombreUsuario;
                userMap.Contrasena = user.Contraseña;
                userMap.Mail = user.Mail;
                return userMap;
            }
            else { return null; }
        }
    }
}
