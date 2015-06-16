using System;
using System.Collections.Generic;
using System.Linq;

using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Repositorios;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada.Implementacion
{
    public class AdministradorDeEtiquetas : IAdministradorDeEtiquetas
    {

         private readonly IDiccionarioRepositorio diccionarioRepositorio;

		/// <summary>
		/// Fecha creación:	Mayo, 2015.
		/// Descripción:	Constructor de la clase con la inyección del repositorio.
		/// </summary>
		/// <param name="repositorioDiccionario">Interfaz de acceso al reposiotrio.</param>
        public AdministradorDeEtiquetas(IDiccionarioRepositorio repositorioDiccionario)
        {
			diccionarioRepositorio = repositorioDiccionario;
		}

        /// <summary>
        /// Fecha creación:	Mayo, 2015.
        /// Descripción:	Método que realiza la consulta de las etiquetas que contengan el nombre indicado por el usuario, en todos los diccionarios que existen en el repositorio.
        /// </summary>
        /// <param name="peticion">Se recibe un objeto de tipo ConsultarEtiquetasPorNombrePeticion que contiene el nombre de las etiquetas que se desean consultar en todos los diccionarios.</param>
        /// <returns>Retorna un objeto de tipo ConsultarEtiquetasPorNombreRespuesta que contiene el resultado de la consulta.</returns>
        public ConsultarEtiquetasPorNombreRespuesta ConsultarEtiquetasPorNombre(ConsultarEtiquetasPorNombrePeticion peticion)
        {
            var etiquetasDeDiccionariosPorNombre = ConsultarEtiquetasPorNombreRespuesta.CrearNuevaInstancia();

            try
            {
                var diccionarios = diccionarioRepositorio.ObtenerDiccionarios();
                var listaDeDiccionarios = new List<Diccionario>();

                foreach (var itemDiccionario in diccionarios)
                {
                    var diccionario = Diccionario.CrearNuevoDiccionario(itemDiccionario.Id, itemDiccionario.Ambiente);

                    var listaDeEtiquetas = itemDiccionario.Etiquetas.Where(e => e.Nombre.Contains(peticion.Nombre)).ToList();

                    diccionario.AgregarEtiquetas(listaDeEtiquetas);

                    listaDeDiccionarios.Add(diccionario);
                }

                etiquetasDeDiccionariosPorNombre.ListaDeDiccionarios = listaDeDiccionarios;
            }
            catch (Exception ex)
            {
                // TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
                throw ex;
            }

            return etiquetasDeDiccionariosPorNombre;
        }

        /// <summary>
        /// Fecha creación:	Mayo, 2015.
        /// Descripción:	Método que realiza la consulta de las etiquetas que contengan una traducción con el idioma indicado por el usuario, en un diccionario.
        /// </summary>
        /// <param name="peticion">Se recibe un objeto de tipo ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion que contiene el idioma de la traducción de las etiquetas que se desea consultar en un diccionario.</param>
        /// <returns>Retorna un objeto de tipo ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta que contiene el resultado de la consulta.</returns>
        public ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta ConsultarEtiquetasDeDiccionarioPorIdioma(ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion peticion)
        {
            var etiquetasDeDiccionarioPorIdiomaRespuesta = ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta.CrearNuevaInstancia();

            try
            {
                var diccionario = diccionarioRepositorio.ObtenerUnDiccionario(peticion.DiccionarioId);
                var listaEtiquetas = new List<Etiqueta>();

                foreach (var itemEtiqueta in diccionario.Etiquetas)
                {
                    foreach (var itemTraduccion in itemEtiqueta.Textos)
                    {
                        if (itemTraduccion.Cultura.CodigoIso != peticion.Idioma) continue;
                        var nuevaEtiqueta = Etiqueta.CrearNuevaEtiqueta(itemEtiqueta.Id);
                        nuevaEtiqueta.IdiomaPorDefecto = itemEtiqueta.IdiomaPorDefecto;
                        nuevaEtiqueta.Nombre = itemEtiqueta.Nombre;
                        nuevaEtiqueta.AgregarTraduccion(itemTraduccion);

                        listaEtiquetas.Add(nuevaEtiqueta);
                    }
                }

                etiquetasDeDiccionarioPorIdiomaRespuesta.ListaDeEtiquetas = listaEtiquetas;
                etiquetasDeDiccionarioPorIdiomaRespuesta.Relaciones["diccionario"] = diccionario.Id;
            }
            catch (Exception ex)
            {
                // TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
                throw ex;

            }

            return etiquetasDeDiccionarioPorIdiomaRespuesta;
        }

        /// <summary>
        /// Fecha creación:	Mayo, 2015.
        /// Descripción:	Método que realiza la consulta de las etiquetas que contengan el nombre indicado por el usuario, en un diccionario.
        /// </summary>
        /// <param name="peticion">Se recibe un objeto de tipo ConsultarEtiquetasDeDiccionarioPorNombrePeticion que contiene el nombre de las etiquetas que se desea consultar en un diccionario.</param>
        /// <returns>Retorna un objeto de tipo ConsultarEtiquetasDeDiccionarioPorNombreRespuesta que contiene el resultado de la consulta.</returns>
        public ConsultarEtiquetasDeDiccionarioPorNombreRespuesta ConsultarEtiquetasDeDiccionarioPorNombre(ConsultarEtiquetasDeDiccionarioPorNombrePeticion peticion)
        {
            var etiquetasDeDiccionarioPorNombreRespuesta = ConsultarEtiquetasDeDiccionarioPorNombreRespuesta.CrearNuevaInstancia();

            try
            {
                var diccionario = diccionarioRepositorio.ObtenerUnDiccionario(peticion.DiccionarioId);

                List<Etiqueta> listaEtiquetas;
                listaEtiquetas = diccionario.Etiquetas.Where(e => e.Nombre.Contains(peticion.Nombre)).ToList();

                etiquetasDeDiccionarioPorNombreRespuesta.ListaDeEtiquetas = listaEtiquetas;
                etiquetasDeDiccionarioPorNombreRespuesta.Relaciones["diccionario"] = diccionario.Id;
            }
            catch (Exception ex)
            {
                // TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
                throw ex;

            }

            return etiquetasDeDiccionarioPorNombreRespuesta;
        }

        /// <summary>
        /// Fecha creación:	Mayo, 2015.
        /// Descripción:	Método que realiza la consulta de las etiquetas que contengan la descripción indicada por el usuario, en un diccionario.
        /// </summary>
        /// <param name="peticion">Se recibe un objeto de tipo ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion que contiene la descripción de las etiquetas que se desea consultar en un diccionario.</param>
        /// <returns>Retorna un objeto de tipo ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta que contiene el resultado de la consulta.</returns>
        public ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta ConsultarEtiquetasDeDiccionarioPorDescripcion(ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion peticion)
        {
            var etiquetasDeDiccionarioPorDescripcionRespuesta = ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta.CrearNuevaInstancia();

            try
            {
                var diccionario = diccionarioRepositorio.ObtenerUnDiccionario(peticion.DiccionarioId);

                List<Etiqueta> listaEtiquetas;
                listaEtiquetas = diccionario.Etiquetas.Where(e => e.Descripcion.Contains(peticion.Descripcion)).ToList();

                etiquetasDeDiccionarioPorDescripcionRespuesta.ListaDeEtiquetas = listaEtiquetas;
                etiquetasDeDiccionarioPorDescripcionRespuesta.Relaciones["diccionario"] = diccionario.Id;
            }
            catch (Exception ex)
            {
                // TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
                throw ex;

            }

            return etiquetasDeDiccionarioPorDescripcionRespuesta;
        }

        /// <summary>
        /// Fecha creación:	Mayo, 2015.
        /// Descripción:	Método que realiza la consulta de las etiquetas que contengan el estatus indicado por el usuario, en un diccionario.
        /// </summary>
        /// <param name="peticion">Se recibe un objeto de tipo ConsultarEtiquetasDeDiccionarioPorEstatusPeticion que contiene el estatus de las etiquetas que se desea consultar en un diccionario.</param>
        /// <returns>Retorna un objeto de tipo ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta que contiene el resultado de la consulta.</returns>
        public ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta ConsultarEtiquetasDeDiccionarioPorEstatus(ConsultarEtiquetasDeDiccionarioPorEstatusPeticion peticion)
        {
            var etiquetasDeDiccionarioPorEstatusRespuesta = ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta.CrearNuevaInstancia();

            try
            {
                var diccionario = diccionarioRepositorio.ObtenerUnDiccionario(peticion.DiccionarioId);

                List<Etiqueta> listaEtiquetas;
                listaEtiquetas = diccionario.Etiquetas.Where(e => e.Activo.Equals(peticion.Estatus)).ToList();

                etiquetasDeDiccionarioPorEstatusRespuesta.ListaDeEtiquetas = listaEtiquetas;
                etiquetasDeDiccionarioPorEstatusRespuesta.Relaciones["diccionario"] = diccionario.Id;
            }
            catch (Exception ex)
            {
                // TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
                throw ex;

            }

            return etiquetasDeDiccionarioPorEstatusRespuesta;
        }

        /// <summary>
        /// Fecha creación:	Mayo, 2015.
        /// Descripción:	Método que realiza la consulta de las etiquetas que contengan el idioma por defecto indicado por el usuario, en un diccionario.
        /// </summary>
        /// <param name="peticion">Se recibe un objeto de tipo ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion que contiene el idioma por defecto de las etiquetas que se desea consultar en un diccionario.</param>
        /// <returns>Retorna un objeto de tipo ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta que contiene el resultado de la consulta.</returns>
        public ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefecto(ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion peticion)
        {
            var etiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta = ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta.CrearNuevaInstancia();

            try
            {
                var diccionario = diccionarioRepositorio.ObtenerUnDiccionario(peticion.DiccionarioId);

                List<Etiqueta> listaEtiquetas;
                listaEtiquetas = diccionario.Etiquetas.Where(e => e.IdiomaPorDefecto.Contains(peticion.IdiomaPorDefecto)).ToList();

                etiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta.ListaDeEtiquetas = listaEtiquetas;
                etiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta.Relaciones["diccionario"] = diccionario.Id;
            }
            catch (Exception ex)
            {
                // TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
                throw ex;
            }

            return etiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta;
        }



        /// <summary>
        /// Fecha creación:	Mayo, 2015.
        /// Descripción:	Método que agrega una o más etiquetas de un diccionario.
        /// </summary>
        /// <param name="peticion">Se recibe un objeto de tipo AgregarEtiquetasAUnDiccionarioPeticion que contiene la lista de etiquetas a agregar de un diccionario.</param>
        /// <returns>Retorna un objeto de tipo AgregarEtiquetasAUnDiccionarioRespuesta que contiene la lista de etiquetas que contiene el diccionario.</returns>
        public AgregarEtiquetasAUnDiccionarioRespuesta AgregarEtiquetasAUnDiccionario(AgregarEtiquetasAUnDiccionarioPeticion peticion)
        {
            var agregarEtiquetasAUnDiccionario = AgregarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            try
            {
                if (peticion.ListaDeEtiquetas.Any())
                {
                    var diccionario = diccionarioRepositorio.ObtenerUnDiccionario(peticion.DiccionarioId);

                    diccionario = diccionario.AgregarEtiquetas(peticion.ListaDeEtiquetas);

                    var diccionarioModificado = diccionarioRepositorio.SalvarUnDiccionario(diccionario);

                    if (diccionarioModificado != null)
                    {
                        agregarEtiquetasAUnDiccionario.ListaDeEtiquetas = diccionarioModificado.Etiquetas.ToList();
                        agregarEtiquetasAUnDiccionario.Relaciones["diccionario"] = diccionarioModificado.Id;
                    }
                    else
                    {
                        throw new Exception("Ocurrió un error guardando los cambios en el diccionario.");
                    }
                }
                else
                {
                    throw new Exception("No se enviaron etiquetas que agregar.");
                }
            }
            catch (Exception ex)
            {
                // TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
                throw ex;
            }

            return agregarEtiquetasAUnDiccionario;
        }

        /// <summary>
        /// Fecha creación:	Mayo, 2015.
        /// Descripción:	Método que modifica una o más etiquetas de un diccionario.
        /// </summary>
        /// <param name="peticion">Se recibe un objeto de tipo ModificarEtiquetasAUnDiccionarioPeticion que contiene la lista de etiquetas a modificar de un diccionario.</param>
        /// <returns>Retorna un objeto de tipo ModificarEtiquetasAUnDiccionarioRespuesta que contiene la lista de etiquetas que contiene el diccionario.</returns>
        public ModificarEtiquetasAUnDiccionarioRespuesta ModificarEtiquetasAUnDiccionario(ModificarEtiquetasAUnDiccionarioPeticion peticion)
        {
            var unDiccionarioRespuesta = ModificarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            try
            {
                if (peticion.ListaDeEtiquetas.Any())
                {
                    var diccionario = diccionarioRepositorio.ObtenerUnDiccionario(peticion.DiccionarioId);

                    diccionario = diccionario.ModificarEtiquetas(peticion.ListaDeEtiquetas);

                    var diccionarioModificado = diccionarioRepositorio.SalvarUnDiccionario(diccionario);

                    if (diccionarioModificado != null)
                    {
                        unDiccionarioRespuesta.ListaDeEtiquetas = diccionarioModificado.Etiquetas.ToList();
                        unDiccionarioRespuesta.Relaciones["diccionario"] = diccionarioModificado.Id;
                    }
                    else
                    {
                        throw new Exception("Ocurrió un error guardando los cambios en el diccionario.");
                    }
                }
                else
                {
                    throw new Exception("No se enviaron etiquetas que modificar.");
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
        /// Descripción:	Método que elimina una o más etiquetas de un diccionario.
        /// </summary>
        /// <param name="peticion">Se recibe un objeto de tipo EliminarEtiquetasAUnDiccionarioPeticion que contiene la lista de etiquetas a eliminar de un diccionario.</param>
        /// <returns>Retorna un objeto de tipo EliminarEtiquetasAUnDiccionarioRespuesta que contiene la lista de etiquetas que contiene el diccionario.</returns>
        public EliminarEtiquetasAUnDiccionarioRespuesta EliminarEtiquetasAUnDiccionario(EliminarEtiquetasAUnDiccionarioPeticion peticion)
        {
            var eliminarEtiquetasDiccionario = EliminarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            try
            {
                if (peticion.ListaDeEtiquetas.Any())
                {
                    var diccionario = diccionarioRepositorio.ObtenerUnDiccionario(peticion.DiccionarioId);

                    var etiquetasDiccionario = peticion.ListaDeEtiquetas;

                    diccionario = etiquetasDiccionario.Aggregate(diccionario, (current, etiquetaDiccionario) => current.EliminarEtiqueta(etiquetaDiccionario));

                    var diccionarioModificado = diccionarioRepositorio.SalvarUnDiccionario(diccionario);

                    if (diccionarioModificado != null)
                    {
                        eliminarEtiquetasDiccionario.ListaDeEtiquetas = diccionarioModificado.Etiquetas.ToList();
                        eliminarEtiquetasDiccionario.Relaciones["diccionario"] = diccionarioModificado.Id;
                    }
                    else
                    {
                        throw new Exception("Ocurrió un error guardando los cambios en el diccionario.");
                    }
                }
                else
                {
                    throw new Exception("No se enviaron etiquetas que eliminar.");
                }

            }
            catch (Exception ex)
            {
                // TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
                throw ex;
            }

            return eliminarEtiquetasDiccionario;
        }
    }
}
