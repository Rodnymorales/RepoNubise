using System.Net.Http;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using Newtonsoft.Json;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using System;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion
{
    public class CrearUnDiccionarioPeticion : PeticionWeb<CrearUnDiccionarioPeticion>
	{
        
        public comunes.Diccionario Diccionario { get; set; }
        public app.CrearUnDiccionarioPeticion AppDiccionarioPeticion { get; set; }

        public string Respuesta { get; set; }

        public CrearUnDiccionarioPeticion(HttpRequestMessage peticionHttp)
        {
            Respuesta = string.Empty;
            Diccionario = JsonConvert.DeserializeObject<comunes.Diccionario>(peticionHttp.Content.ReadAsStringAsync().Result);
            
            if(Diccionario != null && Diccionario.Ambiente != null )
            {
                AppDiccionarioPeticion = app.CrearUnDiccionarioPeticion.CrearNuevaInstancia(Diccionario.Ambiente);
            }else
            {
                Respuesta = "El formato del diccionario proporcionado se encuentra mal definido";
            }
 
        }

        public static CrearUnDiccionarioPeticion CrearUnaNuevaPeticion(HttpRequestMessage peticionHttp)
        {
            return new CrearUnDiccionarioPeticion(peticionHttp) ;
        }
        
	}
}
