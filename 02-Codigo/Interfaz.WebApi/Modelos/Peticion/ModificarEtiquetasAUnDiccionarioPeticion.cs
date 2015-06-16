using System;
using System.Linq;
using appModelosPeticion = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using Newtonsoft.Json;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;
using System.Net.Http;
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;


namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion
{
    public class ModificarEtiquetasAUnDiccionarioPeticion : PeticionWeb<ModificarEtiquetasAUnDiccionarioPeticion>
    {

        #region Propiedades



        public appModelosPeticion.ModificarEtiquetasAUnDiccionarioPeticion AppEtiquetasDiccionarioPeticion { get; set; }

        public string Respuesta {get;set;}
        #endregion

        #region Constructores

        private ModificarEtiquetasAUnDiccionarioPeticion(HttpRequestMessage peticionHttp, string id1)
	    {
            Respuesta = string.Empty;
			this.AppEtiquetasDiccionarioPeticion = appModelosPeticion.ModificarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            var etiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(peticionHttp.Content.ReadAsStringAsync().Result);

            this.AppEtiquetasDiccionarioPeticion.DiccionarioId = new Guid(id1);
            if (etiquetas != null && etiquetas.ListaEtiquetas.Count() >= 1)
            {
                this.AppEtiquetasDiccionarioPeticion.ListaDeEtiquetas = utilitario.MapeoWebApiComunesADominio.MapearEtiquetas(etiquetas.ListaEtiquetas);
            }
            else
            {
                Respuesta = "Formato de la lista de etiquetas se encuentra mal definida";
            }
                

	    }

        public static ModificarEtiquetasAUnDiccionarioPeticion CrearNuevaPeticion(HttpRequestMessage peticionHttp, string id1)
	    {
            return new ModificarEtiquetasAUnDiccionarioPeticion(peticionHttp, id1);
	    }

        #endregion

        
    }
}
