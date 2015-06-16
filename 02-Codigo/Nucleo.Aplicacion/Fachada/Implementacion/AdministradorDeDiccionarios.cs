using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Repositorios;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada.Implementacion
{
    public class AdministradorDeDiccionarios : IAdministradorDeDiccionarios
    {
        private readonly IDiccionarioRepositorio diccionarioRepositorio;

		/// <summary>
		/// Fecha creación:	Mayo, 2015.
		/// Descripción:	Constructor de la clase con la inyección del repositorio.
		/// </summary>
		/// <param name="repositorioDiccionario">Interfaz de acceso al reposiotrio.</param>
        public AdministradorDeDiccionarios(IDiccionarioRepositorio repositorioDiccionario)
        {
			diccionarioRepositorio = repositorioDiccionario;
		}

        /// <summary>
        /// Fecha creación:	Mayo, 2015.
        /// Descripción:	Método que realiza la consulta de todos los diccionarios que existen en el repositorio.
        /// </summary>
        /// <returns>Retorna un objeto de tipo ConsultarDiccionariosRespuesta que contiene el resultado de la consulta.</returns>
        public ConsultarDiccionariosRespuesta ConsultarDiccionarios()
        {
            var diccionariosRespuesta = ConsultarDiccionariosRespuesta.CrearNuevaInstancia();

            try
            {
                var diccionarios = diccionarioRepositorio.ObtenerDiccionarios();

                diccionariosRespuesta.ListaDeDiccionarios = diccionarios;
            }
            catch (Exception ex)
            {
                // TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
                throw ex;
            }

            return diccionariosRespuesta;
        }

        /// <summary>
        /// Fecha creación:	Mayo, 2015.
        /// Descripción:	Método que realiza la consulta de un diccionario, según su identificador, en el repositorio.	
        /// </summary>
        /// <param name="peticion">Se recibe un objeto de tipo ConsultarUnDiccionarioPeticion que contiene el identificador del diccionario que se desea consultar.</param>
        /// <returns>Retorna un objeto de tipo ConsultarUnDiccionarioarioRespuesta que contiene el resultado de la consulta.</returns>
        public ConsultarUnDiccionarioarioRespuesta ConsultarUnDiccionario(ConsultarUnDiccionarioPeticion peticion)
        {
            var unDiccionarioRespuesta = ConsultarUnDiccionarioarioRespuesta.CrearNuevaInstancia(String.Empty);

            try
            {
                var diccionario = diccionarioRepositorio.ObtenerUnDiccionario(peticion.DiccionarioId);

                unDiccionarioRespuesta.Diccionario = diccionario;
                unDiccionarioRespuesta.Relaciones["diccionario"] = diccionario.Id;
            }
            catch (Exception ex)
            {
                // TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
                throw ex;
            }

            return unDiccionarioRespuesta;
        }

        /// <summary>
        /// Fecha creación:	Mayo, 2015.
        /// Descripción:	Método que crea un nuevo diccionario.
        /// </summary>
        /// <param name="peticion">Se recibe un objeto de tipo CrearUnDiccionarioPeticion que contiene los datos del diccionario a crear.</param>
        /// <returns>Retorna un objeto de tipo CrearUnDiccionarioRespuesta que contiene el diccionario creado.</returns>
        public CrearUnDiccionarioRespuesta CrearUnDiccionario(CrearUnDiccionarioPeticion peticion)
        {
            var respuesta = CrearUnDiccionarioRespuesta.CrearNuevaInstancia(string.Empty);

            try
            {
                var diccionarioNuevo = Diccionario.CrearNuevoDiccionario(peticion.Ambiente);

                var diccionarioNuevoCreado = diccionarioRepositorio.SalvarUnDiccionario(diccionarioNuevo);

                if (diccionarioNuevoCreado != null)
                {
                    respuesta.DiccionarioNuevo = diccionarioNuevoCreado;
                    respuesta.Relaciones["diccionario"] = diccionarioNuevoCreado.Id;
                }
                else
                {
                    throw new Exception("Ocurrió un error guardando los cambios en el diccionario.");
                }
            }
            catch (Exception ex)
            {
                // TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
                throw ex;
            }

            return respuesta;
        }

        /// <summary>
        /// Fecha creación:	Mayo, 2015.
        /// Descripción:	Método que modifica los datos de un diccionario.
        /// </summary>
        /// <param name="peticion">Se recibe un objeto de tipo ModificarUnDiccionarioPeticion que contiene los datos a modificar de un diccionario.</param>
        /// <returns>Retorna un objeto de tipo ModificarUnDiccionarioRespuesta que contiene el diccionario modificado.</returns>
        public ModificarUnDiccionarioRespuesta ModificarUnDiccionario(ModificarUnDiccionarioPeticion peticion)
        {
            var unDiccionarioRespuesta = ModificarUnDiccionarioRespuesta.CrearNuevaInstancia();

            try
            {
                var diccionario = diccionarioRepositorio.ObtenerUnDiccionario(peticion.Diccionario.Id);

                diccionario.Ambiente = peticion.Diccionario.Ambiente;

                var diccionarioModificado = diccionarioRepositorio.SalvarUnDiccionario(diccionario);

                if (diccionarioModificado != null)
                {
                    unDiccionarioRespuesta.Diccionario = diccionarioModificado;
                    unDiccionarioRespuesta.Relaciones["diccionario"] = diccionarioModificado.Id;
                }
                else
                {
                    throw new Exception("Ocurrió un error guardando los cambios en el diccionario.");
                }

            }
            catch (Exception ex)
            {
                // TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
                throw ex;
            }

            return unDiccionarioRespuesta;
        }

        /// <summary>
        /// Fecha creación:	Mayo, 2015.
        /// Descripción:	Método que elimina un diccionario.
        /// </summary>
        /// <param name="peticion">Se recibe un objeto de tipo EliminarUnDiccionarioPeticion que contiene el identificador del diccionario a eliminar.</param>
        /// <returns>Retorna un objeto de tipo EliminarUnDiccionarioRespuesta que contiene la lista de los diccionarios restantes, es decir, los que no se eliminaron.</returns>
        public EliminarUnDiccionarioRespuesta EliminarUnDiccionario(EliminarUnDiccionarioPeticion peticion)
        {
            var eliminarDiccionario = EliminarUnDiccionarioRespuesta.CrearNuevaInstancia();

            try
            {
                var diccionariosRestantes = diccionarioRepositorio.EliminarUnDiccionario(peticion.DiccionarioId);

                var diccionarioModificado = diccionarioRepositorio.SalvarDiccionarios(diccionariosRestantes);

                if (diccionarioModificado != null)
                {
                    eliminarDiccionario.ListaDeDiccionarios = diccionarioModificado.ToList();
                }
                else
                {
                    throw new Exception("Ocurrió un error guardando los cambios en el diccionario.");
                }
            }
            catch (Exception ex)
            {
                // TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
                throw ex;
            }

            return eliminarDiccionario;
        }
    }
}
