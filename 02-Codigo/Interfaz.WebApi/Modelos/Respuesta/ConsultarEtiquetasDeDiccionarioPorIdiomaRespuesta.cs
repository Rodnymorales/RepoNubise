using System;
using System.Linq;
using appModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
    public class ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta : comunes.RespuestaWeb<ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta>
	{
		public comunes.Etiquetas Etiquetas { get; set; }

        #region Constructores de la clase
        private ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta(appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta respuestaApp)
		{
            this.Etiquetas = new comunes.Etiquetas();
            this.Etiquetas.ListaEtiquetas = utilitario.MapeoDominioAWebApiComunes.MapearEtiquetas(respuestaApp.ListaDeEtiquetas);
		}

		#endregion

        public static ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta CrearNuevaRespuesta(appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta respuestaApp)
        {
            return new ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta(respuestaApp);
        }

        
	}
}