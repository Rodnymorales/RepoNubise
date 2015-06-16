using System;
using System.Linq;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using Newtonsoft.Json;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
    public class ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta : RespuestaWeb<ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta>
    {
        [JsonProperty]
        public comunes.Etiquetas Etiquetas { get; set; }

        #region constructores

        [JsonConstructor]
        public ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta()
        {
            //No implementa nada solo es utilizado para la instanciacion del objeto json
        }

        private ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta(app.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta respuestaApp)
        {
            this.Etiquetas = new Etiquetas();
            this.Etiquetas.ListaEtiquetas = utilitario.MapeoDominioAWebApiComunes.MapearEtiquetas(respuestaApp.ListaDeEtiquetas);

        }

        #endregion

        public static ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta CrearNuevaRespuesta(app.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta respuestaApp)
        {
            return new ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta(respuestaApp);
        }

        


    }
}