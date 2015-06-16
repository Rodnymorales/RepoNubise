using System;
using System.Collections.Generic;
using System.Linq;

using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Repositorios;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada.Implementacion
{
    public class AdministradorDeTraducciones : IAdministradorDeTraducciones
    {
        private readonly IDiccionarioRepositorio diccionarioRepositorio;

		/// <summary>
		/// Fecha creación:	Mayo, 2015.
		/// Descripción:	Constructor de la clase con la inyección del repositorio.
		/// </summary>
		/// <param name="repositorioDiccionario">Interfaz de acceso al reposiotrio.</param>
        public AdministradorDeTraducciones(IDiccionarioRepositorio repositorioDiccionario)
        {
			diccionarioRepositorio = repositorioDiccionario;
		}


        /// <summary>
        /// Fecha creación:	Mayo, 2015.
		/// Descripción:	Método que agrega una o más traducciones de una etiqueta de un diccionario.
		/// </summary>
		/// <param name="peticion">Se recibe un objeto de tipo AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion que contiene la lista de traducciones a agregar de una etiqueta de un diccionario.</param>
		/// <returns>Retorna un objeto de tipo AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta que contiene la lista de traducciones de una etiqueta de un diccionario.</returns>
        public AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta AgregarTraduccionesAUnaEtiquetaDeUnDiccionario(AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion peticion)
        {
			var agregarTraduccionesAUnaEtiquetaDeUnDiccionario = AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

			try
			{
				if (peticion.ListaDeTraducciones.Any())
				{
					var diccionario = diccionarioRepositorio.ObtenerUnDiccionario(peticion.DiccionarioId);

					var etiqueta = diccionario.Etiquetas.Where(e => e.Id == peticion.EtiquetaId).ToList().FirstOrDefault();

					if (etiqueta != null)
					{
						etiqueta.AgregarTraducciones(peticion.ListaDeTraducciones);

						diccionario = diccionario.ModificarEtiquetas(new List<Etiqueta> {etiqueta});

						var diccionarioModificado = diccionarioRepositorio.SalvarUnDiccionario(diccionario);

						if (diccionarioModificado != null)
						{
							agregarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = diccionarioModificado.Etiquetas.Where(e => e.Id == peticion.EtiquetaId).ToList().FirstOrDefault().Textos.ToList();
							agregarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones["diccionario"] = diccionarioModificado.Id;
							agregarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones["etiqueta"] = diccionarioModificado.Etiquetas.Where(e => e.Id == peticion.EtiquetaId).ToList().FirstOrDefault().Id;
						}
						else
						{
							throw new Exception("Ocurrió un error guardando los cambios en el diccionario.");
						}
					}
					else
					{
						throw new Exception("La consulta no retornó la etiqueta solicitada.");
					}
				}
				else
				{
					throw new Exception("No se enviaron traducciones que agregar.");
				}
			}
			catch (Exception ex)
			{
				// TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
				throw ex;
			}

            return agregarTraduccionesAUnaEtiquetaDeUnDiccionario;
        }

		/// <summary>
		/// Fecha creación:	Mayo, 2015.
		/// Descripción:	Método que modifica una o más traducciones de una etiqueta de un diccionario.
		/// </summary>
		/// <param name="peticion">Se recibe un objeto de tipo ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion que contiene la lista de traducciones a modificar de una etiqueta de un diccionario.</param>
		/// <returns>Retorna un objeto de tipo ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta que contiene la lista de traducciones de una etiqueta de un diccionario.</returns>
        public ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta ModificarTraduccionesAUnaEtiquetaDeUnDiccionario(ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion peticion)
        {
			var modificarTraduccionesAUnaEtiquetaDeUnDiccionario = ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

			try
			{
				if (peticion.ListaDeTraducciones.Any())
				{
					var diccionario = diccionarioRepositorio.ObtenerUnDiccionario(peticion.DiccionarioId);

					var etiqueta = diccionario.Etiquetas.Where(e => e.Id == peticion.EtiquetaId).ToList().FirstOrDefault();

					if (etiqueta != null)
					{
						etiqueta.ModificarTraducciones(peticion.ListaDeTraducciones);

						diccionario = diccionario.ModificarEtiquetas(new List<Etiqueta> { etiqueta });

						var diccionarioModificado = diccionarioRepositorio.SalvarUnDiccionario(diccionario);

						if (diccionarioModificado != null)
						{
							modificarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = diccionarioModificado.Etiquetas.Where(e => e.Id == peticion.EtiquetaId).ToList().FirstOrDefault().Textos.ToList();
							modificarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones["diccionario"] = diccionarioModificado.Id;
							modificarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones["etiqueta"] = diccionarioModificado.Etiquetas.Where(e => e.Id == peticion.EtiquetaId).ToList().FirstOrDefault().Id;
						}
						else
						{
							throw new Exception("Ocurrió un error guardando los cambios en el diccionario.");
						}
					}
					else
					{
						throw new Exception("La consulta no retornó la etiqueta solicitada.");
					}
				}
				else
				{
					throw new Exception("No se enviaron traducciones que modificar.");
				}
			}
			catch (Exception ex)
			{
				// TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
				throw ex;
			}

			return modificarTraduccionesAUnaEtiquetaDeUnDiccionario;
        }

		/// <summary>
		/// Fecha creación:	Mayo, 2015.
		/// Descripción:	Método que elimina una o más traducciones de una etiqueta de un diccionario.
		/// </summary>
		/// <param name="peticion">Se recibe un objeto de tipo EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion que contiene la lista de traducciones a eliminar de una etiqueta de un diccionario.</param>
		/// <returns>Retorna un objeto de tipo EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta que contiene la lista de traducciones de una etiqueta de un diccionario.</returns>
        public EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta EliminarTraduccionesAUnaEtiquetaDeUnDiccionario(EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion peticion)
        {
			var eliminarTraduccionesAUnaEtiquetaDeUnDiccionario = EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

			try
			{
				if (peticion.ListaDeTraducciones.Any())
				{
					var diccionario = diccionarioRepositorio.ObtenerUnDiccionario(peticion.DiccionarioId);

					var etiqueta = diccionario.Etiquetas.Where(e => e.Id == peticion.EtiquetaId).ToList().FirstOrDefault();

					if (etiqueta != null)
					{
						etiqueta.EliminarTraducciones(peticion.ListaDeTraducciones);

						diccionario = diccionario.ModificarEtiquetas(new List<Etiqueta> { etiqueta });

						var diccionarioModificado = diccionarioRepositorio.SalvarUnDiccionario(diccionario);

						if (diccionarioModificado != null)
						{
							eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = diccionarioModificado.Etiquetas.Where(e => e.Id == peticion.EtiquetaId).ToList().FirstOrDefault().Textos.ToList();
							eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones["diccionario"] = diccionarioModificado.Id;
							eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones["etiqueta"] = diccionarioModificado.Etiquetas.Where(e => e.Id == peticion.EtiquetaId).ToList().FirstOrDefault().Id;
						}
						else
						{
							throw new Exception("Ocurrió un error guardando los cambios en el diccionario.");
						}
					}
					else
					{
						throw new Exception("La consulta no retornó la etiqueta solicitada.");
					}
				}
				else
				{
					throw new Exception("No se enviaron traducciones que eliminar.");
				}
			}
			catch (Exception ex)
			{
				// TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
				throw ex;
			}

			return eliminarTraduccionesAUnaEtiquetaDeUnDiccionario;
        }
    }
    
}
