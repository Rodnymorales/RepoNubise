using System;
using System.Linq;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using appModelosPeticion = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using Newtonsoft.Json;
using System.Net.Http;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion
{
	public class ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion
    {
        #region Propiedades

        public appModelosPeticion.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion AppEtiquetasDiccionarioPeticion { get; set; }

        public string Respuesta { get; set; }


        #endregion

        #region Constructores

        private ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion(string id1,HttpRequestMessage peticionHttp,string id2)
        {
            Respuesta = string.Empty;
            this.AppEtiquetasDiccionarioPeticion = appModelosPeticion.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion.CrearNuevaInstancia();
            var traducciones = JsonConvert.DeserializeObject<comunes.Traducciones>(peticionHttp.Content.ReadAsStringAsync().Result);

            this.AppEtiquetasDiccionarioPeticion.DiccionarioId = new Guid(id1);
            this.AppEtiquetasDiccionarioPeticion.EtiquetaId = new Guid(id2);
            if (traducciones != null)
            {
                this.AppEtiquetasDiccionarioPeticion.ListaDeTraducciones = utilitario.MapeoWebApiComunesADominio.MapearTraducciones(traducciones.Traducciones1);
            }
            else
            {
                Respuesta = "Formato de la lista de etiquetas se encuentra mal definida";
            }
		}

        public static ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion CrearNuevaPeticion(string id1, string id2, HttpRequestMessage peticionHttp)
	    {
	        return new ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion(id1,peticionHttp,id2);
	    }

        #endregion
    }
}