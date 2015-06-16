using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using System;
using System.Linq;
using appModelosPeticion = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion
{
    public class ConsultarEtiquetasDeDiccionarioPorEstatusPeticion : PeticionWeb<ConsultarEtiquetasDeDiccionarioPorEstatusPeticion>
	{

        public appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorEstatusPeticion AppEtiquetasDiccionarioPeticion { get; set; }

        public string Respuesta { get; set; }

		#region constructores

        private ConsultarEtiquetasDeDiccionarioPorEstatusPeticion(string id1, string estatus)
		{
            this.Respuesta = string.Empty;
            this.AppEtiquetasDiccionarioPeticion = appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorEstatusPeticion.CrearNuevaInstancia();
            this.AppEtiquetasDiccionarioPeticion.DiccionarioId = new Guid(id1);
            
            switch(estatus.ToUpper())
            {
                case "ACTIVO":
                    this.AppEtiquetasDiccionarioPeticion.Estatus = true;
                    break;

                case "INACTIVO":
                    this.AppEtiquetasDiccionarioPeticion.Estatus = false;
                    break;

                default:
                    this.Respuesta = "Estatus proporcionado invalido, solo se permiten los estatus activo e inactivo";
                    break;
            }

		}

		#endregion

        public static ConsultarEtiquetasDeDiccionarioPorEstatusPeticion CrearNuevaPeticion(string id1, string estatus)
        {
            return new ConsultarEtiquetasDeDiccionarioPorEstatusPeticion(id1, estatus);
        }
    }
}
