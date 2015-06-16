using System;
using System.Linq;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using appModelosPeticion = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using System.Net.Http;
using Newtonsoft.Json;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion
{
	public class AgregarEtiquetasAUnDiccionarioPeticion
	{
        public appModelosPeticion.AgregarEtiquetasAUnDiccionarioPeticion AppEtiquetasDiccionarioPeticion { get; set; }

        public string Respuesta { get; set; }

		#region constructores

		private AgregarEtiquetasAUnDiccionarioPeticion(string id1,HttpRequestMessage peticionHttp)
		{
            Respuesta = string.Empty;
			this.AppEtiquetasDiccionarioPeticion = appModelosPeticion.AgregarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            var etiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(peticionHttp.Content.ReadAsStringAsync().Result);

            this.AppEtiquetasDiccionarioPeticion.DiccionarioId = new Guid(id1);
            if(etiquetas != null && etiquetas.ListaEtiquetas.Count() >= 1)
            {
                this.AppEtiquetasDiccionarioPeticion.ListaDeEtiquetas = utilitario.MapeoWebApiComunesADominio.MapearEtiquetas(etiquetas.ListaEtiquetas);
            }
            else
            {
                Respuesta = "Formato de la lista de etiquetas se encuentra mal definida";
            }
		}

		public static AgregarEtiquetasAUnDiccionarioPeticion CrearNuevaPeticion(string id1,HttpRequestMessage peticionHttp)
		{
            return new AgregarEtiquetasAUnDiccionarioPeticion(id1,peticionHttp);
		}
        #endregion

    }
}