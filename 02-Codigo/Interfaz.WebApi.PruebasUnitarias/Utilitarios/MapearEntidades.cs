using System;
using System.Collections.Generic;
using System.Linq;
using dominio = Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.PruebasUnitarias.Utilitarios
{
     public static class MapearEntidades
    {
        private const string AmbienteTestPrueba = "Prueba";

        public static List<dominio.Diccionario.Diccionario> MapearDiccionariosTipoComunesConTipoAplicacionParaMock(comunes.Diccionarios diccionarios)
        {
            var listadiccionariosParaApp = new List<dominio.Diccionario.Diccionario>();


            foreach (comunes.Diccionario diccionario in diccionarios.ListaDiccionarios)
            {
                var diccionarioDominio = dominio.Diccionario.Diccionario.CrearNuevoDiccionario(diccionario.Id, AmbienteTestPrueba);

                diccionarioDominio.Ambiente = diccionario.Ambiente;


                if (diccionario.Etiquetas.Count > 0)
                    diccionarioDominio.AgregarEtiquetas(MapearEtiquetasTipoComunesConTipoAplicacionParaMock(diccionario.Etiquetas));

                listadiccionariosParaApp.Add(diccionarioDominio);
            }

            return listadiccionariosParaApp;
        }

        public static List<dominio.Etiquetas.Etiqueta> MapearEtiquetasTipoComunesConTipoAplicacionParaMock(List<comunes.Etiqueta> etiquetas)
        {
            var listaEtiquetasParaApp = new List<dominio.Etiquetas.Etiqueta>();

            foreach (comunes.Etiqueta etiqueta in etiquetas)
            {
                var etiquetaDominio = dominio.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiqueta.Id);

                etiquetaDominio.Activo = etiqueta.Activo;
                etiquetaDominio.Descripcion = etiqueta.Descripcion;
                etiquetaDominio.IdiomaPorDefecto = etiqueta.IdiomaPorDefecto;
                etiquetaDominio.Nombre = etiqueta.Nombre;
                etiquetaDominio.AgregarTraducciones(MapearTraduccionesTipoComunesConTipoAplicacionParaMock(etiqueta.Traducciones));

                listaEtiquetasParaApp.Add(etiquetaDominio);
            }

            return listaEtiquetasParaApp;
        }

        public static List<dominio.Etiquetas.Traduccion> MapearTraduccionesTipoComunesConTipoAplicacionParaMock(List<comunes.Traduccion> textos)
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
