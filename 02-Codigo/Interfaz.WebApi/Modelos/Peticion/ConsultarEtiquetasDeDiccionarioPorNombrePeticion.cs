using System;
using System.Linq;
using appModelosPeticion = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion
{
    public class ConsultarEtiquetasDeDiccionarioPorNombrePeticion : PeticionWeb<ConsultarEtiquetasDeDiccionarioPorNombrePeticion>
	{

        public appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorNombrePeticion AppEtiquetasDiccionarioPeticion { get; set; }

        public string Respuesta { get; set; }

		#region constructores

        private ConsultarEtiquetasDeDiccionarioPorNombrePeticion(string id1, string nombre)
		{
            AppEtiquetasDiccionarioPeticion = appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorNombrePeticion.CrearNuevaInstancia();
            AppEtiquetasDiccionarioPeticion.DiccionarioId = new Guid(id1);
            AppEtiquetasDiccionarioPeticion.Nombre = nombre;
		}
        
		public static ConsultarEtiquetasDeDiccionarioPorNombrePeticion CrearNuevaPeticion(string id1, string nombre)
		{
			return new ConsultarEtiquetasDeDiccionarioPorNombrePeticion(id1,nombre);
		}

		#endregion

    }
}