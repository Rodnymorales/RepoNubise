using System;
using System.Linq;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Repositorios;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada.Implementacion 
{
	/// <summary>
	/// Fecha creación:	Junio, 2015.
	/// Descripción:	Clase encargada de retornar las etiquetas solicitadas en cada uno de los idiomas existentes en el diccionario.
	/// </summary>
	public class GeneradorDeRecursosDeTraduccion : IGeneradorDeRecursosDeTraduccion
	{
		private IDiccionarioRepositorio diccionarioRepositorio;

		/// <summary>
		/// Fecha creación:	Junio, 2015.
		/// Descripción:	Constructor de la clase con la inyección del repositorio.
		/// </summary>
		/// <param name="repositorioDiccionario">Interfaz de acceso al reposiotrio.</param>
		public GeneradorDeRecursosDeTraduccion(IDiccionarioRepositorio repositorioDiccionario)
		{
			diccionarioRepositorio = repositorioDiccionario;
		}

		/// <summary>
		/// Fecha creación:	Junio, 2015.
		/// Descripción:	Método que realiza la consulta de de las etiquetas solicitadas en el diccionario indicado para luego separar las mismas por los idiomas
		///					definidos en las traducciones.
		/// </summary>
		/// <param name="peticion">Se recibe un objeto de tipo GenerarRecursosPorIdiomaPeticion que contiene la lista de los nombres de las etiquetas a consultar y el nombre del ambiente del diccionario donde se consultarán las etiquetas.</param>
		/// <returns>Retorna un objeto de tipo GenerarRecursosPorIdiomaRespuesta que contiene el resultado de la consulta.</returns>
		public GenerarRecursosPorIdiomaRespuesta GenerarRecursos(GenerarRecursosPorIdiomaPeticion peticion)
		{
			var generarRecursosPorIdiomaRespuesta = GenerarRecursosPorIdiomaRespuesta.CrearNuevaInstancia();

			try
			{ 
				if ((peticion.Ambiente.Length != 0) && (peticion.ListaDeEtiquetas.Count() > 0))
				{
					//TODO: cambiar por ambiente luego de que se haga el método.
					var diccionario = diccionarioRepositorio.ObtenerUnDiccionario(new Guid("a1fa3369-bc3f-4ebc-9cac-5677cbaa8114"));

					var listaDeEtiquetasSolicitadas = ListaDeEtiquetasEncontradasEnElDiccionario(peticion.ListaDeEtiquetas, diccionario.Etiquetas.ToList());

					if (listaDeEtiquetasSolicitadas.Count() > 0)
					{
						var listaDeCulturasExistentesEnLasEtiquetasSolicitadas = ListaDeCulturasExistentes(listaDeEtiquetasSolicitadas);

						if (listaDeCulturasExistentesEnLasEtiquetasSolicitadas.Count() > 0)
						{
							foreach (Cultura item in listaDeCulturasExistentesEnLasEtiquetasSolicitadas)
							{
								var listaDeTraduccionesPorUnaCultura = (from etiq in listaDeEtiquetasSolicitadas
																		  from tra in etiq.Textos.ToList()
																		  where tra.Cultura.CodigoIso == item.CodigoIso
																		  select TraduccionRecurso.CrearNuevaInstancia(etiq.Nombre, tra.Texto, tra.ToolTip)
																			  ).ToList();

								generarRecursosPorIdiomaRespuesta.RecursosPorIdioma.Add(item.CodigoIso, listaDeTraduccionesPorUnaCultura);
							}

							generarRecursosPorIdiomaRespuesta.Relaciones["diccionario"] = diccionario.Id;
						}
						else
						{
							throw new Exception("No se encontró ninguna cultura para consultar.");
						}
					}
					else
					{
						throw new Exception("No se encontró ninguna etiqueta solicitada en el diccionario.");
					}
				}
				else 
				{
					throw new Exception("Alguno de los parámetros de entrada se encuentra en vacio.");
				}
			}
			catch (Exception ex)
			{
				// TODO: Agregar el mensaje de error a la respuesta, una vez se defina la clase ModeloRepuesta.
				throw ex;
			}

			return generarRecursosPorIdiomaRespuesta;
		}

		/// <summary>
		/// Fecha creación:	Junio, 2015.
		/// Descripción:	Método encargado de obtener el subconjunto de etiquetas que coinciden con alguno de los nombres de etiquetas enviados para consultar
		///					en una lista de etiqeutas.
		/// </summary>
		/// <param name="listaDeNombresDeEtiquetas">Se recibe una lista de nombres de etiquetas.</param>
		/// <param name="listaDeEtiquetasDelDiccionario">Se recibe una lista de etiquetas.</param>
		/// <returns>Retorna la lista de etiquetas que corresponden con los nombres de etiqueta enviados.</returns>
		private List<Etiqueta> ListaDeEtiquetasEncontradasEnElDiccionario(List<String> listaDeNombresDeEtiquetas, List<Etiqueta> listaDeEtiquetasDelDiccionario)
		{
			return (from item in listaDeEtiquetasDelDiccionario
					where listaDeNombresDeEtiquetas.Contains(item.Nombre)
					select item).ToList();
		}

		/// <summary>
		/// Fecha creación:	Junio, 2015.
		/// Descripción:	Método que dada una lista de etiquetas obtiene las culturas, no repetidas, encontradas en la lista de traducciones de cada etiqueta.
		/// </summary>
		/// <param name="listaDeEtiquetas">Se recibe una lista de etiquetas.</param>
		/// <returns>Retorna la lista de culturas encontradas en una lista de etiquetas.</returns>
		private List<Cultura> ListaDeCulturasExistentes(List<Etiqueta> listaDeEtiquetas)
		{
			return (from etiq in listaDeEtiquetas
					from tra in etiq.Textos.ToList()
					select tra.Cultura).Distinct().ToList();
		}
	}
}
