using System;
using System.Linq;
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using appModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
    public class ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta : RespuestaWeb<ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta>
	{
		public comunes.Etiquetas Etiquetas { get; set; }


		#region constructores

        private ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta(appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta respuestaApp)
		{
            this.Etiquetas = new comunes.Etiquetas();
            this.Etiquetas.ListaEtiquetas = utilitario.MapeoDominioAWebApiComunes.MapearEtiquetas(respuestaApp.ListaDeEtiquetas);
		}

		#endregion

        public static ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta CrearNuevaRespuesta(appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta respuestaApp)
        {
            return new ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta(respuestaApp);
        }

        
    }
}