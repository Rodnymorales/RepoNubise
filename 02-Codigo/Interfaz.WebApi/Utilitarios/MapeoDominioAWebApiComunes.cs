using System;
using System.Collections.Generic;
using System.Linq;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using dominio = Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios
{
    public class MapeoDominioAWebApiComunes
    {

        public static comunes.Diccionario MapearDiccionario(dominio.Diccionario.Diccionario diccionarioDom)
        {
            var dicctionarioRepo = new comunes.Diccionario() { Id = diccionarioDom.Id };

            dicctionarioRepo.Etiquetas = new List<comunes.Etiqueta>();

            foreach (var etiqueta in diccionarioDom.Etiquetas)
            {

                var etiquetaMapper = new comunes.Etiqueta()
                {

                    Activo = etiqueta.Activo,
                    Descripcion = etiqueta.Descripcion,
                    Id = etiqueta.Id,
                    IdiomaPorDefecto = etiqueta.IdiomaPorDefecto,
                    Nombre = etiqueta.Nombre,
                    Traducciones = new List<comunes.Traduccion>()

                };


                foreach (var texto in etiqueta.Textos)
                {

                    var textoMapper = new comunes.Traduccion()
                    {
                        Cultura = texto.Cultura.CodigoIso,
                        Tooltip = texto.ToolTip,
                        Value = texto.Texto
                    };

                    etiquetaMapper.Traducciones.Add(textoMapper);
                }

                dicctionarioRepo.Etiquetas.Add(etiquetaMapper);

            }

            return dicctionarioRepo;
        }

        public static List<comunes.Diccionario> MapearDiccionarios(List<dominio.Diccionario.Diccionario> diccionarios)
        {
            var listaDiccionariosRespuestaWebApi = new List<comunes.Diccionario>();


            foreach (dominio.Diccionario.Diccionario diccionario in diccionarios)
            {
                var diccionarioRespuesta = new comunes.Diccionario();

                diccionarioRespuesta.Ambiente = diccionario.Ambiente;
                diccionarioRespuesta.Id = diccionario.Id;
                diccionarioRespuesta.Etiquetas = MapearEtiquetas(diccionario.Etiquetas);

                listaDiccionariosRespuestaWebApi.Add(diccionarioRespuesta);
            }


            return listaDiccionariosRespuestaWebApi;
        }

        public static List<comunes.Etiqueta> MapearEtiquetas(IReadOnlyList<dominio.Etiquetas.Etiqueta> etiquetas)
        {
            var listaParaRespuestaWebApi = new List<comunes.Etiqueta>();


            foreach (dominio.Etiquetas.Etiqueta etiqueta in etiquetas)
            {
                var etiquetaRespuesta = new comunes.Etiqueta();

                etiquetaRespuesta.Id = etiqueta.Id;
                etiquetaRespuesta.Activo = etiqueta.Activo;
                etiquetaRespuesta.Descripcion = etiqueta.Descripcion;
                etiquetaRespuesta.IdiomaPorDefecto = etiqueta.IdiomaPorDefecto;
                etiquetaRespuesta.Nombre = etiqueta.Nombre;
                etiquetaRespuesta.Traducciones = MapearTraducciones(etiqueta.Textos);

                listaParaRespuestaWebApi.Add(etiquetaRespuesta);
            }

            return listaParaRespuestaWebApi;
        }

        public static List<comunes.Traduccion> MapearTraducciones(IReadOnlyList<dominio.Etiquetas.Traduccion> textos)
        {
            var listaTextosParaRespuestaWebApi = new List<comunes.Traduccion>();

            foreach (var traduccion in textos)
            {
                var traduccionRespuesta = new comunes.Traduccion();

                traduccionRespuesta.Cultura = traduccion.Cultura.CodigoIso;
                traduccionRespuesta.Value = traduccion.Texto;
                traduccionRespuesta.Tooltip = traduccion.ToolTip;

                listaTextosParaRespuestaWebApi.Add(traduccionRespuesta);
            }

            return listaTextosParaRespuestaWebApi;
        }
    }
}