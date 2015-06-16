using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using System;
using appModelosPeticion = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion
{
    public class ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion : PeticionWeb<ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion>
    {
        public appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion AppEtiquetasDicionarioPeticion { get; set; }

        #region constructores

        private ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion(string id,string idioma)
        {
            this.AppEtiquetasDicionarioPeticion = appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion.CrearNuevaInstancia();
            this.AppEtiquetasDicionarioPeticion.DiccionarioId = new Guid(id);
            this.AppEtiquetasDicionarioPeticion.IdiomaPorDefecto = idioma;

        }

        public static ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion CrearUnaNuevaPeticion(string id,string idioma)
        {
            return new ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion(id,idioma);
        }

        #endregion

    }
}