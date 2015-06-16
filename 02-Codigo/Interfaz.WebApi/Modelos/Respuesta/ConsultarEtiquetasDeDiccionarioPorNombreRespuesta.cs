using System;
using System.Linq;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using appModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
    public class ConsultarEtiquetasDeDiccionarioPorNombreRespuesta : comunes.RespuestaWeb<ConsultarEtiquetasDeDiccionarioPorNombreRespuesta>
	{
		public comunes.Etiquetas Etiquetas { get; set; }

		#region constructores

		private ConsultarEtiquetasDeDiccionarioPorNombreRespuesta(appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorNombreRespuesta respuestaApp)
		{
            this.Etiquetas = new comunes.Etiquetas();
            this.Etiquetas.ListaEtiquetas = utilitario.MapeoDominioAWebApiComunes.MapearEtiquetas(respuestaApp.ListaDeEtiquetas);
		}

        public static ConsultarEtiquetasDeDiccionarioPorNombreRespuesta CrearNuevaRespuesta(appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorNombreRespuesta respuestaApp)
		{
			return new ConsultarEtiquetasDeDiccionarioPorNombreRespuesta(respuestaApp);
		}

		#endregion

        
    }
}