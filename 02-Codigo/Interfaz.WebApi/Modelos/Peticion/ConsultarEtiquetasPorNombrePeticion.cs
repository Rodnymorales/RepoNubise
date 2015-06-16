using System;
using System.Linq;
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using appModelosPeticion = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion
{
    public class ConsultarEtiquetasPorNombrePeticion : PeticionWeb<ConsultarEtiquetasPorNombrePeticion>
    {
        public appModelosPeticion.ConsultarEtiquetasPorNombrePeticion AppEtiquetaPeticion { get; set; }

        private ConsultarEtiquetasPorNombrePeticion(string nombreEtiqueta)
        {
            this.AppEtiquetaPeticion = appModelosPeticion.ConsultarEtiquetasPorNombrePeticion.CrearNuevaInstancia();
            this.AppEtiquetaPeticion.Nombre = nombreEtiqueta;
        }

        public static ConsultarEtiquetasPorNombrePeticion CrearNuevaPeticion(string nombreEtiqueta)
        {
            return new ConsultarEtiquetasPorNombrePeticion(nombreEtiqueta);
        }

    }
}