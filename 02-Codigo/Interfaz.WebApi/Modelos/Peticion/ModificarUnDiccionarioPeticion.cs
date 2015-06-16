using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion
{
    public class ModificarUnDiccionarioPeticion : PeticionWeb<ModificarUnDiccionarioPeticion>
	{
        public comunes.Diccionario Diccionario { get; set; }
        public app.ModificarUnDiccionarioPeticion AppDiccionarioPeticion { get; set; }
        public string Respuesta { get; set; }

        private ModificarUnDiccionarioPeticion(HttpRequestMessage peticionHttp,string id)
        {
            Respuesta = string.Empty;
            this.Diccionario = JsonConvert.DeserializeObject<comunes.Diccionario>(peticionHttp.Content.ReadAsStringAsync().Result);


            if (Diccionario != null && Diccionario.Ambiente != null)
            {
                this.AppDiccionarioPeticion = app.ModificarUnDiccionarioPeticion.CrearNuevaInstancia(Diccionario.Id, Diccionario.Ambiente);
                this.AppDiccionarioPeticion.Diccionario.Ambiente = Diccionario.Ambiente;
            }
            else
            {
                Respuesta = "El formato del diccionario proporcionado se encuentra mal definido";
            }

        }
        
        public static ModificarUnDiccionarioPeticion CrearUnaNuevaPeticionDeModificacion(HttpRequestMessage peticionHttp,string id)
        {
            return new ModificarUnDiccionarioPeticion(peticionHttp,id);
        }

    }
}
