using System;
using System.Linq;
using app=Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using Newtonsoft.Json;
using comunes=Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
    public class CrearUnDiccionarioRespuesta : comunes.RespuestaWeb<CrearUnDiccionarioRespuesta>
	{
        [JsonProperty("diccionario")]
		public comunes.Diccionario DiccionarioNuevo { get; set; }
        
      
		#region constructores

        CrearUnDiccionarioRespuesta(app.CrearUnDiccionarioRespuesta respuestaApp)
        {
            this.DiccionarioNuevo = new comunes.Diccionario();
            this.DiccionarioNuevo.Ambiente = respuestaApp.DiccionarioNuevo.Ambiente;
            this.DiccionarioNuevo = utilitario.MapeoDominioAWebApiComunes.MapearDiccionario(respuestaApp.DiccionarioNuevo);
        }

        public CrearUnDiccionarioRespuesta() 
        { 
        }

        public static CrearUnDiccionarioRespuesta CrearNuevaRespuesta(app.CrearUnDiccionarioRespuesta respuestaApp)
		{
			return new CrearUnDiccionarioRespuesta(respuestaApp);
		}

		#endregion



	}
}