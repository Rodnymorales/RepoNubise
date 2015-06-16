using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using Newtonsoft.Json;
using System;
using System.Linq;
using appModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
    public class ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta : RespuestaWeb<ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta>
    {
        [JsonProperty]
        public comunes.Etiquetas Etiquetas { get; set; }

        private ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta(appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta respuestaApp)
        {
            this.Etiquetas = new comunes.Etiquetas();
            this.Etiquetas.ListaEtiquetas = utilitario.MapeoDominioAWebApiComunes.MapearEtiquetas (respuestaApp.ListaDeEtiquetas);
            
        }

        public static ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta CrearNuevaRespuesta(appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta respuestaApp)
        {
            return new ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta(respuestaApp);
        }

        
    }
}