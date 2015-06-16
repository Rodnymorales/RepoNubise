using System;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using appModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
    public class ModificarEtiquetasAUnDiccionarioRespuesta : comunes.RespuestaWeb<ModificarEtiquetasAUnDiccionarioRespuesta>
	{
        #region Propiedades
        
        public Comunes.Etiquetas Etiquetas { get; set; }

        #endregion

        #region Constructores

        private ModificarEtiquetasAUnDiccionarioRespuesta(appModelosRespuesta.ModificarEtiquetasAUnDiccionarioRespuesta respuestaApp)
	    {
            this.Etiquetas = new comunes.Etiquetas();
            this.Etiquetas.ListaEtiquetas = utilitario.MapeoDominioAWebApiComunes.MapearEtiquetas(respuestaApp.ListaDeEtiquetas);
	    }

        public static ModificarEtiquetasAUnDiccionarioRespuesta CrearNuevaRespuesta(appModelosRespuesta.ModificarEtiquetasAUnDiccionarioRespuesta respuestaApp)
	    {
	        return new ModificarEtiquetasAUnDiccionarioRespuesta(respuestaApp);
	    }

        #endregion

    }
}