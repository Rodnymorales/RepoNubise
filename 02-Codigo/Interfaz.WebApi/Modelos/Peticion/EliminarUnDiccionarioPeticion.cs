using System;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using System.Net.Http;
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion
{
    public class EliminarUnDiccionarioPeticion : PeticionWeb<EliminarUnDiccionarioPeticion>
	{

        public comunes.Diccionarios Diccionario { get; set; }
        public app.EliminarUnDiccionarioPeticion AppDiccionarioPeticion { get; set; }

        private EliminarUnDiccionarioPeticion(HttpRequestMessage peticionHttp, string iddiccionario)
        {
            //Diccionario = JsonConvert.des
            this.AppDiccionarioPeticion = app.EliminarUnDiccionarioPeticion.CrearNuevaInstancia();
            this.AppDiccionarioPeticion.DiccionarioId = new Guid(iddiccionario);
        }

        public static EliminarUnDiccionarioPeticion CrearUnaNuevaPeticionDeEliminar(HttpRequestMessage peticionHttp, string iddiccionario)
        {
            return new EliminarUnDiccionarioPeticion(peticionHttp,iddiccionario);
        }
    }
}
