using Newtonsoft.Json;
using System;
using System.Linq;
using appModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
    public class ConsultarEtiquetasPorNombreRespuesta : comunes.RespuestaWeb<ConsultarEtiquetasPorNombreRespuesta>
    {
        [JsonProperty]
        public comunes.Diccionarios Diccionarios { get; set; }

        private ConsultarEtiquetasPorNombreRespuesta(appModelosRespuesta.ConsultarEtiquetasPorNombreRespuesta respuestaApp)
        {
            this.Diccionarios = new comunes.Diccionarios();
            this.Diccionarios.ListaDiccionarios = utilitario.MapeoDominioAWebApiComunes.MapearDiccionarios(respuestaApp.ListaDeDiccionarios);
        }

        public static ConsultarEtiquetasPorNombreRespuesta CrearNuevaRespuesta(appModelosRespuesta.ConsultarEtiquetasPorNombreRespuesta respuestaApp)
        {
            return new ConsultarEtiquetasPorNombreRespuesta(respuestaApp);
        }

        
    }
}