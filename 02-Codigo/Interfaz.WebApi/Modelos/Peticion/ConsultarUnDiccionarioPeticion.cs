using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using System;
using System.Net.Http;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion
{
    public class ConsultarUnDiccionarioPeticion : PeticionWeb<ConsultarUnDiccionarioPeticion>
	{

        public comunes.Diccionario Diccionario { get; set; }
        public app.ConsultarUnDiccionarioPeticion AppDiccionarioPeticion { get; set; }

        public string Respuesta { get; set; }

        private ConsultarUnDiccionarioPeticion(HttpRequestMessage peticionHttp, string id)
        {
            this.AppDiccionarioPeticion = app.ConsultarUnDiccionarioPeticion.CrearNuevaInstancia();
            Guid idDiccionario;
            Respuesta = string.Empty;

            if (!Guid.TryParse(id, out idDiccionario))
            {
                Respuesta = "Formato de Guid no valido, la valor del id del diccionario debe tener la siguiente estructura, ejemplo: 9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            }
            else
            {
                this.AppDiccionarioPeticion.DiccionarioId = idDiccionario;
            }

        }

        public static ConsultarUnDiccionarioPeticion CrearUnaNuevaPeticion(HttpRequestMessage peticionHttp, string id)
        {
            return new ConsultarUnDiccionarioPeticion(peticionHttp, id);
        }
    }
}
