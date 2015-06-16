using System.Net.Http;
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using Newtonsoft.Json;
using System;
using System.Linq;
using appModelosPeticion = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion
{
    public class ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion : PeticionWeb<ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion>
    {
        
        private comunes.Etiqueta  Etiqueta { get; set; }

        public appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion AppEtiquetasDiccionarioPeticion { get; set; }

        private ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion(string id1,HttpRequestMessage peticionHttp)
        {
            this.Etiqueta = JsonConvert.DeserializeObject<comunes.Etiqueta>(peticionHttp.Content.ReadAsStringAsync().Result);
            this.AppEtiquetasDiccionarioPeticion = appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion.CrearNuevaInstancia();
            this.AppEtiquetasDiccionarioPeticion.Descripcion = this.Etiqueta.Descripcion;
            this.AppEtiquetasDiccionarioPeticion.DiccionarioId = new Guid(id1);

        }

        public static ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion CrearNuevaPeticion(string id1,HttpRequestMessage peticionHttp)
        {
            return new ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion(id1,peticionHttp);
        }
    }
}