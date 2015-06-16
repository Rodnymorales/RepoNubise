using System;
using System.Collections.Generic;
using System.Linq;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using dominio = Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios
{
    public class MapeoWebApiComunesADominio
    {
        public static List<dominio.Etiquetas.Etiqueta> MapearEtiquetas(List<comunes.Etiqueta> etiquetas)
        {
            var listaEtiquetasParaApp = new List<dominio.Etiquetas.Etiqueta>();

            foreach (comunes.Etiqueta etiqueta in etiquetas)
            {
                var etiquetaDominio = dominio.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiqueta.Id);

                etiquetaDominio.Activo = etiqueta.Activo;
                etiquetaDominio.Descripcion = etiqueta.Descripcion;
                etiquetaDominio.IdiomaPorDefecto = etiqueta.IdiomaPorDefecto;
                etiquetaDominio.Nombre = etiqueta.Nombre;
                etiquetaDominio.AgregarTraducciones(MapearTraducciones(etiqueta.Traducciones));

                listaEtiquetasParaApp.Add(etiquetaDominio);
            }

            return listaEtiquetasParaApp;
        }

        public static List<dominio.Etiquetas.Traduccion> MapearTraducciones(List<comunes.Traduccion> textos)
        {
            var listaTextosParaApp = new List<dominio.Etiquetas.Traduccion>();

            foreach (var traduccion in textos)
            {
                var cultura = dominio.Etiquetas.Cultura.CrearNuevaCultura(traduccion.Cultura);
                var traduccionesDominio = dominio.Etiquetas.Traduccion.CrearNuevaTraduccion(cultura, traduccion.Value, traduccion.Tooltip);

                listaTextosParaApp.Add(traduccionesDominio);
            }

            return listaTextosParaApp;
        }
    }
}