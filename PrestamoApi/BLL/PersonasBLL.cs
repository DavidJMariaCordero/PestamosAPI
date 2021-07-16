using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrestamoApi.DAL;
using PrestamoApi.Models;

namespace PrestamoApi.BLL
{
    public class PersonasBLL
    {
        public static bool Guardar(Personas persona)
        {
            if (!Existe(persona.PersonaId))
                return Insertar(persona);
            else
                return Modificar(persona);
        }
        public static bool Insertar(Personas persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Personas.Add(persona);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Personas.Any(p => p.PersonaId == id);
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return encontrado;
        }

        public static bool Modificar(Personas persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(persona).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool eliminado = false;
            Contexto contexto = new Contexto();
            try
            {
                var persona = contexto.Personas.Find(id);
                if (persona != null)
                {
                    contexto.Personas.Remove(persona);
                    eliminado = contexto.SaveChanges() > 0;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return eliminado;
        }

        public static Personas Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Personas persona;
            try
            {
                persona = contexto.Personas.Find(id);
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return persona;
        }

        public static bool AsignarBalance(int id, double balance)
        {

            Personas persona = Buscar(id);
            if (persona != null)
            {
                persona.Balance += balance;
                Guardar(persona);
                return true;
            }
            else
                return false;
        }

        public static bool ModificarBalance(int id, double balance, double Monto)
        {

            Personas persona = Buscar(id);
            if (persona != null)
            {
                persona.Balance -= balance;
                persona.Balance += Monto;
                Guardar(persona);
                return true;
            }
            else
                return false;
        }

        public static bool EliminarBalance(int id, double balance)
        {

            Personas persona = Buscar(id);
            if (persona != null)
            {
                persona.Balance -= balance;
                Guardar(persona);
                return true;
            }
            else
                return false;
        }

        public static List<Personas> GetList()
        {
            List<Personas> lista = new List<Personas>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Personas.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;

        }
    }
}
