using System;
using System.Linq;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using appModelosPeticion = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using Newtonsoft.Json;
using System.Net.Http;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion
{
	public class EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion
	{
        public appModelosPeticion.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion AppEtiquetasDiccionarioPeticion { get; set; }

        public string Respuesta { get; set; }

		#region constructores

        private EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion(string id1, string id2, HttpRequestMessage peticionHttp)
        {
            Respuesta = string.Empty;
            this.AppEtiquetasDiccionarioPeticion = appModelosPeticion.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion.CrearNuevaInstancia();

            this.AppEtiquetasDiccionarioPeticion.DiccionarioId = new Guid(id1);
            this.AppEtiquetasDiccionarioPeticion.EtiquetaId = new Guid(id2);
            var traducciones = JsonConvert.DeserializeObject<comunes.Traducciones>(peticionHttp.Content.ReadAsStringAsync().Result);

            if (traducciones != null && traducciones.Traducciones1.Count() >= 1)
            {
                this.AppEtiquetasDiccionarioPeticion.ListaDeTraducciones = utilitario.MapeoWebApiComunesADominio.MapearTraducciones(traducciones.Traducciones1);
            }
            else
            {
                Respuesta = "Formato de la lista de traducciones se encuentra mal definida";
            }
		}

        public static EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion CrearNuevaPeticion(string id1,string id2, HttpRequestMessage peticionHttp)
		{
			return new EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion(id1,id2, peticionHttp);
		}
		#endregion

    }
}