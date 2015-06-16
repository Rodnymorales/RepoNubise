using System;
using System.Linq;
using System.Collections.Generic;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;
using appModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
	public class AgregarEtiquetasAUnDiccionarioRespuesta
	{
		public comunes.Etiquetas  Etiquetas { get; set; }

		public Dictionary<string, Guid> Relaciones { get; set; }

        public string Repuesta { get; set; }
		#region constructores

        private AgregarEtiquetasAUnDiccionarioRespuesta(appModelosRespuesta.AgregarEtiquetasAUnDiccionarioRespuesta respuestaApp)
		{
            this.Etiquetas = new comunes.Etiquetas();
            this.Etiquetas.ListaEtiquetas = utilitario.MapeoDominioAWebApiComunes.MapearEtiquetas(respuestaApp.ListaDeEtiquetas);
            this.Relaciones = new Dictionary<string, Guid>();
            this.Relaciones = respuestaApp.Relaciones;
		}

		public static AgregarEtiquetasAUnDiccionarioRespuesta CrearNuevaRespuesta(appModelosRespuesta.AgregarEtiquetasAUnDiccionarioRespuesta respuestaApp)
		{
            return new AgregarEtiquetasAUnDiccionarioRespuesta(respuestaApp);
		}

		#endregion

       
	}
}