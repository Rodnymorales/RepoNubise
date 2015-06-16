using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{

    public class ConsultarDiccionariosRespuesta : RespuestaWeb<ConsultarDiccionariosRespuesta>
    {
        [JsonProperty("diccionarios")]
        public List<comunes.Diccionario> ListaDeDiccionarios { get; set; }

		#region constructores
        [JsonConstructor]
		private ConsultarDiccionariosRespuesta()
		{
            //No implementa nada, solo se utiliza para el serializador de json
		}

        private ConsultarDiccionariosRespuesta(app.Respuesta.ConsultarDiccionariosRespuesta respuestaApp)
        {
            this.ListaDeDiccionarios = new List<comunes.Diccionario>();
            this.ListaDeDiccionarios = utilitario.MapeoDominioAWebApiComunes.MapearDiccionarios(respuestaApp.ListaDeDiccionarios);
        }

        public static ConsultarDiccionariosRespuesta CrearNuevaRespuestaConRespuestaDeAplicacion(app.Respuesta.ConsultarDiccionariosRespuesta respuestaApp)
        {
            return new ConsultarDiccionariosRespuesta(respuestaApp);
        }

		#endregion

        
    }
}
