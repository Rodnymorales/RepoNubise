using System;
using System.Linq;
using Newtonsoft.Json;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
    public class ConsultarUnDiccionarioRespuesta : comunes.RespuestaWeb<ConsultarUnDiccionarioRespuesta>
    {
        [JsonProperty("diccionario")]
        public comunes.Diccionario Diccionario { get; set; }

        #region constructores

        [JsonConstructor]
        private ConsultarUnDiccionarioRespuesta()
        {
            //No implementada ninguna funcionalidad solo como constructor base para el tipo de dato del deserializador de Json
        }

        private ConsultarUnDiccionarioRespuesta(app.Respuesta.ConsultarUnDiccionarioarioRespuesta respuestaApp)
        {
            this.Diccionario = new comunes.Diccionario();
            this.Diccionario = utilitario.MapeoDominioAWebApiComunes.MapearDiccionario(respuestaApp.Diccionario);
        }

        public static ConsultarUnDiccionarioRespuesta CrearNuevaRespuestaConRespuestaDeAplicacion(app.Respuesta.ConsultarUnDiccionarioarioRespuesta respuestaApp)
        {
            return new ConsultarUnDiccionarioRespuesta(respuestaApp);
        }

        #endregion
        

        
    }
}