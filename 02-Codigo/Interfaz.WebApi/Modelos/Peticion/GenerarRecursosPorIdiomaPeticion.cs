using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;
using Newtonsoft.Json;
using appModelosPeticion = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using System.Net.Http;
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion
{
	public class GenerarRecursosPorIdiomaPeticion : PeticionWeb<GenerarRecursosPorIdiomaPeticion>
	{
        public appModelosPeticion.GenerarRecursosPorIdiomaPeticion AppGenerarRecursos { get; set; }

        public string formatoAceptadoPorElCliente { get; set;}

        public string Respuesta { get; set; }
		#region constructores

        private GenerarRecursosPorIdiomaPeticion(string ambiente, HttpRequestMessage peticionHttp)
		{
            this.AppGenerarRecursos = appModelosPeticion.GenerarRecursosPorIdiomaPeticion.CrearNuevaInstancia();
            this.AppGenerarRecursos.Ambiente = ambiente;
            var peticion = JsonConvert.DeserializeObject<EtiquetasRecursos>(peticionHttp.Content.ReadAsStringAsync().Result);
            if (peticion != null)
            {
                this.AppGenerarRecursos.ListaDeEtiquetas = peticion.Etiquetas;
                this.formatoAceptadoPorElCliente = string.Empty;
                this.formatoAceptadoPorElCliente = peticionHttp.Headers.Accept.ToString();
            }
		}
        
        #endregion

        public static GenerarRecursosPorIdiomaPeticion CrearNuevaPeticion(string ambiente, HttpRequestMessage peticionHttp)
        {
            return new GenerarRecursosPorIdiomaPeticion(ambiente, peticionHttp);
        }
	}
}
