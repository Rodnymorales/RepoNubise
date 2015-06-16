using System;
using System.Collections.Generic;
using System.Linq;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;
using Newtonsoft.Json;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
    [JsonObject]
    public class EliminarUnDiccionarioRespuesta : comunes.RespuestaWeb<EliminarUnDiccionarioRespuesta>
	{

        #region Propiedades

        /// <summary>
        ///Propiedad que obtiene o establece la lista de diccionarios que se van a devolver en caso de que se logre eliminar el diccionario en caso contrario su valor es null
        /// </summary>
        [JsonProperty("diccionario")]
        public List<comunes.Diccionario> ListaDiccionarios { get; set; }

        #endregion

        #region Constructores

        [JsonConstructor]
        private EliminarUnDiccionarioRespuesta()
        {
            //No implementada ninguna funcionalidad solo como constructor base para el tipo de dato del deserializador de Json
        }

	    private EliminarUnDiccionarioRespuesta(app.EliminarUnDiccionarioRespuesta respuestaApp)
	    {
            if(respuestaApp.ListaDeDiccionarios != null)
                this.ListaDiccionarios = utilitario.MapeoDominioAWebApiComunes.MapearDiccionarios(respuestaApp.ListaDeDiccionarios);
	    }

        public static EliminarUnDiccionarioRespuesta CrearNuevaInstancia(app.EliminarUnDiccionarioRespuesta respuestaApp)
	    {
            return new EliminarUnDiccionarioRespuesta(respuestaApp);
	    }

        #endregion

        
	}
}
