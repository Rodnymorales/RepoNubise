using Newtonsoft.Json;
using System;
using System.Linq;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
    [JsonObject]
	public class ModificarUnDiccionarioRespuesta
    {

        #region Propiedades

        [JsonProperty("diccionario")]
        public comunes.Diccionario Diccionario { get; set; }
        
        #endregion
       
        #region Constructores

        [JsonConstructor]
        private ModificarUnDiccionarioRespuesta()
        {
            //No implementa nada solo se coloca para que pueda ser serializada la clase con las propiedades de tipo json
        }

	    /// <summary>
	    /// 
	    /// </summary>
        private ModificarUnDiccionarioRespuesta(app.Respuesta.ModificarUnDiccionarioRespuesta respuestaApp)
	    {
	        Diccionario = new comunes.Diccionario();
            this.Diccionario = utilitario.MapeoDominioAWebApiComunes.MapearDiccionario(respuestaApp.Diccionario);
            this.Diccionario.Ambiente = respuestaApp.Diccionario.Ambiente;
	    }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ModificarUnDiccionarioRespuesta CrearNuevaRespuesta(app.Respuesta.ModificarUnDiccionarioRespuesta respuestaApp)
	    {
            return new ModificarUnDiccionarioRespuesta(respuestaApp);
	    }

		#endregion

        

    }
}
