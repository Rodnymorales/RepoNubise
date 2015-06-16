using System;
using System.Linq;
using appModelosPeticion = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using dominio = Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion
{
	public class ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion
	{

        public appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion AppEtiquetasDiccionarioPeticion { get; set; }

        public string Respuesta{get;set;}

        private ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion(string id1,string idioma)
        {
            this.Respuesta = string.Empty;
            this.AppEtiquetasDiccionarioPeticion = appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion.CrearNuevaInstancia();

            AppEtiquetasDiccionarioPeticion.DiccionarioId = new Guid(id1);

            try
            {
                dominio.Etiquetas.Cultura cultura = dominio.Etiquetas.Cultura.CrearNuevaCultura(idioma);
                this.AppEtiquetasDiccionarioPeticion.Idioma = cultura.CodigoIso;
            }
            catch (Exception ex)
            {
                Respuesta = "El idioma proporcionado no es correcto. Detalles: " + ex.Message;
            }

        }

        public static ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion CrearNuevaPeticion(string id1, string idioma)
        {
            return new ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion(id1, idioma);
        }
    }
}
