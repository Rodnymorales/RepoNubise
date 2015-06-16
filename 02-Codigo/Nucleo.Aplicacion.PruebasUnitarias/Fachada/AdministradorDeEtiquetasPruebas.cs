using System;
using System.Collections.Generic;
using System.Linq;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada.Implementacion;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Repositorios;
using NUnit.Framework;
using Shouldly;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using NSubstitute;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.PruebasUnitarias.Fachada
{
    [TestFixture]
    public class AdministradorDeEtiquetasPruebas
    {
        #region Constantes
        private const string AmbienteTestPrueba = "desarrollo";
        private const string AmbienteModificado = "ambiente modificado de desarrollo";
        private const string ConsultarDiccionarioPorIdioma = "en-US";
        private const string ConsultarEtiquetasDeDiccionariosPorNombre = "editar";
        private const string ConsultarEtiquetasPorNombre = "cancelar";
        private const string ConsultarEtiquetaPorDescripcion = "aceptar";
        private const string ConsultarEtiquetaPorIdiomaPorDefecto = "es-VE";
        private const bool ConsultarEtiquetaPorEstatus = true;
        #endregion
        private readonly Guid diccionarioBaseId = new Guid("a1fa3369-bc3f-4ebc-9cac-5677cbaa8114");
        private readonly Guid diccionarioAdicionalId = new Guid("05609dcd-d579-4f6e-8be2-8127373e5416");

        private readonly Guid etiquetaAceptarId = new Guid("8a87f8a7-3df9-4d90-9478-350b964fc888");
        private readonly Guid etiquetaCancelarId = new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");
        private readonly Guid etiquetaEditarId = new Guid("0260b80b-4ac6-40a6-b5eb-b57916eaab2b");
        private readonly Guid etiquetaEliminarId = new Guid("e2850768-35df-46bb-8f79-48b06ba45528");

        #region Definiciones de diccionarios Mocks

        private Diccionario DiccionarioNull()
        {
            return null;
        }

        private Diccionario DiccionarioBaseSinEtiquetas()
        {
            return Diccionario.CrearNuevoDiccionario(diccionarioBaseId, AmbienteTestPrueba);
        }

        private Diccionario DiccionarioAdicionalSinEtiquetas()
        {
            return Diccionario.CrearNuevoDiccionario(diccionarioAdicionalId, "Otro diccionario");
        }

        private Diccionario DiccionarioBaseConDosEtiquetas()
        {
            var diccionario = DiccionarioBaseSinEtiquetas();

            diccionario.AgregarEtiquetas(new List<Etiqueta>() { EtiquetaAceptar(), EtiquetaCancelar() });

            return diccionario;
        }

        private Diccionario DiccionarioAdicionalConDosEtiquetas()
        {
            var listaDeEtiquetas = new List<Etiqueta>();

            var diccionario = DiccionarioAdicionalSinEtiquetas();

            listaDeEtiquetas.Add(EtiquetaEditar());
            listaDeEtiquetas.Add(EtiquetaEliminar());

            diccionario.AgregarEtiquetas(listaDeEtiquetas);

            return diccionario;
        }

        private List<Diccionario> ListaConDosDiccionarios()
        {
            return new List<Diccionario> { DiccionarioBaseConDosEtiquetas(), DiccionarioAdicionalConDosEtiquetas() };
        }

        private Diccionario DiccionarioConElAmbienteModificado()
        {
            var diccionario = Diccionario.CrearNuevoDiccionario(diccionarioBaseId, AmbienteModificado);

            diccionario.AgregarEtiquetas(new List<Etiqueta>() { EtiquetaAceptar(), EtiquetaCancelar() });

            return diccionario;
        }

        private Diccionario DiccionarioBaseConCuatroEtiquetas()
        {
            var diccionario = DiccionarioBaseConDosEtiquetas();

            return diccionario.AgregarEtiquetas(EtiquetasAAgregar());
        }

        private Diccionario DiccionarioConLaEtiquetaAceptarModificada()
        {
            var diccionario = DiccionarioBaseSinEtiquetas();

            diccionario.AgregarEtiquetas(new List<Etiqueta>() { EtiquetaAceptarModificada(), EtiquetaCancelar() });

            return diccionario;
        }

        private Diccionario DiccionarioConLaEtiquetaAceptarModificadaYDosEtiquetasAgregadas()
        {
            var diccionario = DiccionarioBaseSinEtiquetas();

            diccionario.AgregarEtiquetas(EtiquetasAModificarYAgregar());

            return diccionario;
        }

        private Diccionario DiccionarioConLaEtiquetaCancelarEliminada()
        {
            var diccionario = DiccionarioBaseSinEtiquetas();

            return diccionario.AgregarEtiquetas(new List<Etiqueta>() { EtiquetaAceptar() });
        }

        private Diccionario DiccionarioConTraduccionesDeLaEtiquetaAceptarAgregadas()
        {
            var diccionario = DiccionarioBaseSinEtiquetas();

            var etiquetaAceptarConDosTraduccionesAgregadas = EtiquetaAceptar();

            etiquetaAceptarConDosTraduccionesAgregadas = etiquetaAceptarConDosTraduccionesAgregadas.AgregarTraducciones(TraduccionesAAgregar());

            return diccionario.AgregarEtiquetas(new List<Etiqueta>() { etiquetaAceptarConDosTraduccionesAgregadas, EtiquetaCancelar() });
        }

        private Diccionario DiccionarioConTraduccionesDeLaEtiquetaAceptarModificadas()
        {
            var diccionario = DiccionarioBaseSinEtiquetas();

            return diccionario.AgregarEtiquetas(new List<Etiqueta>() { EtiquetaAceptarConTraduccionesModificadas(), EtiquetaCancelar() });
        }

        private Diccionario DiccionarioConTraduccionesDeLaEtiquetaAceptarModificadasYNuevas()
        {
            var diccionario = DiccionarioBaseSinEtiquetas();

            return diccionario.AgregarEtiquetas(new List<Etiqueta>() { EtiquetaAceptarConTraduccionesModificadasYNuenvas(), EtiquetaCancelar() });
        }

        private Diccionario DiccionarioConTodasLasTraduccionesDeLaEtiquetaAceptarEliminadas()
        {
            var diccionario = DiccionarioBaseSinEtiquetas();

            return diccionario.AgregarEtiquetas(new List<Etiqueta>() { EtiquetaAceptarSinTraducciones(), EtiquetaCancelar() });
        }

        private Diccionario DiccionarioConDosTraduccionesDeLaEtiquetaCancelarEliminadas()
        {
            var diccionario = DiccionarioBaseSinEtiquetas();

            return diccionario.AgregarEtiquetas(new List<Etiqueta>() { EtiquetaAceptar(), EtiquetaCancelarConDosTraducciones() });
        }



        private List<Diccionario> DiccionarioRespuestaConsultarEtiquetasPorNombre()
        {
            var listaDeDiccionarios = new List<Diccionario>();

            // Definición primer diccionario
            var listaDeEtiquetas1 = new List<Etiqueta>();

            var diccionario1 = DiccionarioBaseSinEtiquetas();

            listaDeEtiquetas1.Add(EtiquetaEditar());

            diccionario1.AgregarEtiquetas(listaDeEtiquetas1);

            listaDeDiccionarios.Add(diccionario1);

            // Definición segundo diccionario
            var listaDeEtiquetas2 = new List<Etiqueta>();

            var diccionario2 = DiccionarioAdicionalSinEtiquetas();

            listaDeEtiquetas2.Add(EtiquetaEditar());

            diccionario2.AgregarEtiquetas(listaDeEtiquetas2);

            listaDeDiccionarios.Add(diccionario2);

            return listaDeDiccionarios;
        }

        #endregion

        #region Definiciones de etiquetas Mocks

        private Etiqueta EtiquetaAceptar()
        {
            var etiquetaAceptar = Etiqueta.CrearNuevaEtiqueta(etiquetaAceptarId);

            etiquetaAceptar.IdiomaPorDefecto = "es-VE";
            etiquetaAceptar.Nombre = "app.common.aceptar";
            etiquetaAceptar.Descripcion = "Traducciones del botón aceptar.";
            etiquetaAceptar.AgregarTraducciones(TraduccionesDeLaEtiquetaAceptar());
            etiquetaAceptar.Activo = true;

            return etiquetaAceptar;
        }

        private Etiqueta EtiquetaCancelar()
        {
            var etiquetaCancelar = Etiqueta.CrearNuevaEtiqueta(etiquetaCancelarId);

            etiquetaCancelar.IdiomaPorDefecto = "en-US";
            etiquetaCancelar.Nombre = "app.common.cancelar";
            etiquetaCancelar.Descripcion = "Traducciones del botón cancelar.";
            etiquetaCancelar.AgregarTraducciones(TraduccionesDeLaEtiquetaCancelar());
            etiquetaCancelar.Activo = true;

            return etiquetaCancelar;
        }

        private Etiqueta EtiquetaEditar()
        {
            var etiquetaEditar = Etiqueta.CrearNuevaEtiqueta(etiquetaEditarId);

            etiquetaEditar.IdiomaPorDefecto = "en-US";
            etiquetaEditar.Nombre = "app.common.editar";
            etiquetaEditar.Descripcion = "Traducciones del botón editar.";
            etiquetaEditar.AgregarTraducciones(TraduccionesDeLaEtiquetaEditar());
            etiquetaEditar.Activo = true;

            return etiquetaEditar;
        }

        private Etiqueta EtiquetaEliminar()
        {
            var etiquetaEliminar = Etiqueta.CrearNuevaEtiqueta(etiquetaEliminarId);

            etiquetaEliminar.IdiomaPorDefecto = "es-VE";
            etiquetaEliminar.Nombre = "app.common.eliminar";
            etiquetaEliminar.Descripcion = "Traducciones del botón eliminar.";
            etiquetaEliminar.AgregarTraducciones(TraduccionesDeLaEtiquetaEliminar());
            etiquetaEliminar.Activo = true;

            return etiquetaEliminar;
        }

        private Etiqueta EtiquetaAceptarModificada()
        {
            var etiquetaAceptar = Etiqueta.CrearNuevaEtiqueta(etiquetaAceptarId);

            etiquetaAceptar.IdiomaPorDefecto = "es-VE";
            etiquetaAceptar.Nombre = "app.common.aceptar modificada";
            etiquetaAceptar.Descripcion = "Traducciones del botón aceptar. Modificado.";
            etiquetaAceptar.AgregarTraducciones(TraduccionesDeLaEtiquetaAceptar());
            etiquetaAceptar.Activo = true;

            return etiquetaAceptar;
        }

        private Etiqueta EtiquetaAceptarConTraduccionesModificadas()
        {
            var etiquetaAceptar = Etiqueta.CrearNuevaEtiqueta(etiquetaAceptarId);

            etiquetaAceptar.IdiomaPorDefecto = "es-VE";
            etiquetaAceptar.Nombre = "app.common.aceptar";
            etiquetaAceptar.Descripcion = "Traducciones del botón aceptar.";
            etiquetaAceptar.AgregarTraducciones(TraduccionesDeLaEtiquetaAceptarModificadas());
            etiquetaAceptar.Activo = true;

            return etiquetaAceptar;
        }

        private Etiqueta EtiquetaAceptarConTraduccionesModificadasYNuenvas()
        {
            var etiquetaAceptar = Etiqueta.CrearNuevaEtiqueta(etiquetaAceptarId);

            etiquetaAceptar.IdiomaPorDefecto = "es-VE";
            etiquetaAceptar.Nombre = "app.common.aceptar";
            etiquetaAceptar.Descripcion = "Traducciones del botón aceptar.";
            etiquetaAceptar.AgregarTraducciones(TraduccionesDeLaEtiquetaAceptarModificadasYNuevas());
            etiquetaAceptar.Activo = true;

            return etiquetaAceptar;
        }

        private Etiqueta EtiquetaAceptarSinTraducciones()
        {
            var etiquetaAceptar = Etiqueta.CrearNuevaEtiqueta(etiquetaAceptarId);

            etiquetaAceptar.IdiomaPorDefecto = "es-VE";
            etiquetaAceptar.Nombre = "app.common.aceptar";
            etiquetaAceptar.Descripcion = "Traducciones del botón aceptar.";
            etiquetaAceptar.Activo = true;

            return etiquetaAceptar;
        }

        private Etiqueta EtiquetaCancelarConDosTraducciones()
        {
            var etiquetaCancelar = Etiqueta.CrearNuevaEtiqueta(etiquetaCancelarId);

            etiquetaCancelar.IdiomaPorDefecto = "es-VE";
            etiquetaCancelar.Nombre = "app.common.cancelar";
            etiquetaCancelar.Descripcion = "Traducciones del botón cancelar.";
            etiquetaCancelar.AgregarTraducciones(DosTraduccionesDeLaEtiquetaCancelarEliminadas());
            etiquetaCancelar.Activo = true;

            return etiquetaCancelar;
        }

        private List<Etiqueta> EtiquetasAAgregar()
        {
            return new List<Etiqueta> { EtiquetaEditar(), EtiquetaEliminar() };
        }

        private List<Etiqueta> EtiquetasAModificar()
        {
            return new List<Etiqueta> { EtiquetaAceptarModificada() };
        }

        private List<Etiqueta> EtiquetasAModificarYAgregar()
        {
            return new List<Etiqueta> { EtiquetaAceptarModificada(), EtiquetaEditar(), EtiquetaEliminar() };
        }

        private List<Etiqueta> EtiquetasAEliminar()
        {
            return new List<Etiqueta> { EtiquetaCancelar() };
        }

        private List<Etiqueta> ListaDeEtiquetasPorIdioma()
        {
            var listaDeEtiquetas = new List<Etiqueta>();

            var etiquetaAceptar = Etiqueta.CrearNuevaEtiqueta(etiquetaAceptarId);
            var etiquetaCancelar = Etiqueta.CrearNuevaEtiqueta(etiquetaCancelarId);
            var etiquetaEditar = Etiqueta.CrearNuevaEtiqueta(etiquetaEditarId);
            var etiquetaEliminar = Etiqueta.CrearNuevaEtiqueta(etiquetaEliminarId);

            etiquetaAceptar.IdiomaPorDefecto = "es-VE";
            etiquetaAceptar.Nombre = "app.common.aceptar";
            etiquetaAceptar.Descripcion = "Traducciones del botón aceptar.";
            etiquetaAceptar.AgregarTraducciones(new List<Traduccion> { Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "accept") });
            etiquetaAceptar.Activo = true;

            etiquetaCancelar.IdiomaPorDefecto = "es-VE";
            etiquetaCancelar.Nombre = "app.common.cancelar";
            etiquetaCancelar.Descripcion = "Traducciones del botón cancelar.";
            etiquetaCancelar.AgregarTraducciones(new List<Traduccion> { Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "cancel") });
            etiquetaCancelar.Activo = true;

            etiquetaEditar.IdiomaPorDefecto = "es-VE";
            etiquetaEditar.Nombre = "app.common.editar";
            etiquetaEditar.Descripcion = "Traducciones del botón editar.";
            etiquetaEditar.AgregarTraducciones(new List<Traduccion> { Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "edit") });
            etiquetaEditar.Activo = true;

            etiquetaEliminar.IdiomaPorDefecto = "es-VE";
            etiquetaEliminar.Nombre = "app.common.eliminar";
            etiquetaEliminar.Descripcion = "Traducciones del botón eliminar.";
            etiquetaEliminar.AgregarTraducciones(new List<Traduccion> { Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "delete") });
            etiquetaEliminar.Activo = true;

            listaDeEtiquetas.Add(etiquetaAceptar);
            listaDeEtiquetas.Add(etiquetaCancelar);
            listaDeEtiquetas.Add(etiquetaEditar);
            listaDeEtiquetas.Add(etiquetaEliminar);

            return listaDeEtiquetas;
        }

        private List<Etiqueta> ListaDeEtiquetasPorNombre()
        {
            return new List<Etiqueta> { EtiquetaCancelar() };
        }

        #endregion

        #region Definiciones de traducciones Mocks

        private List<Traduccion> TraduccionesDeLaEtiquetaAceptar()
        {
            var listaDeTraduccionesAceptar = new List<Traduccion>();

            var traduccionAceptarEs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es"), "aceptar");
            var traduccionAceptarEsVe = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es-VE"), "aceptar");
            var traduccionAceptarEn = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en"), "accept");
            var traduccionAceptarEnUs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "accept");

            listaDeTraduccionesAceptar.Add(traduccionAceptarEs);
            listaDeTraduccionesAceptar.Add(traduccionAceptarEsVe);
            listaDeTraduccionesAceptar.Add(traduccionAceptarEn);
            listaDeTraduccionesAceptar.Add(traduccionAceptarEnUs);

            return listaDeTraduccionesAceptar;
        }

        private List<Traduccion> TraduccionesDeLaEtiquetaCancelar()
        {
            var listaDeTraduccionesCancelar = new List<Traduccion>();

            var traduccionCancelarEs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es"), "cancelar");
            var traduccionCancelarEsVe = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es-VE"), "cancelar");
            var traduccionCancelarEn = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en"), "cancel");
            var traduccionCancelarEnUs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "cancel");

            listaDeTraduccionesCancelar.Add(traduccionCancelarEs);
            listaDeTraduccionesCancelar.Add(traduccionCancelarEsVe);
            listaDeTraduccionesCancelar.Add(traduccionCancelarEn);
            listaDeTraduccionesCancelar.Add(traduccionCancelarEnUs);

            return listaDeTraduccionesCancelar;
        }

        private List<Traduccion> TraduccionesDeLaEtiquetaEditar()
        {
            var listaDeTraduccionesEditar = new List<Traduccion>();

            var traduccionEditarEs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es"), "Editar");
            var traduccionEditarEsVe = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es-VE"), "Editar");
            var traduccionEditarEn = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en"), "Edit");
            var traduccionEditarEnUs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "Edit");

            listaDeTraduccionesEditar.Add(traduccionEditarEs);
            listaDeTraduccionesEditar.Add(traduccionEditarEsVe);
            listaDeTraduccionesEditar.Add(traduccionEditarEn);
            listaDeTraduccionesEditar.Add(traduccionEditarEnUs);

            return listaDeTraduccionesEditar;
        }

        private List<Traduccion> TraduccionesDeLaEtiquetaEliminar()
        {
            var listaDeTraduccionesEliminar = new List<Traduccion>();

            var traduccionEliminarEs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es"), "Eliminar");
            var traduccionEliminarEsVe = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es-VE"), "Eliminar");
            var traduccionEliminarEn = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en"), "Delete");
            var traduccionEliminarEnUs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "Delete");

            listaDeTraduccionesEliminar.Add(traduccionEliminarEs);
            listaDeTraduccionesEliminar.Add(traduccionEliminarEsVe);
            listaDeTraduccionesEliminar.Add(traduccionEliminarEn);
            listaDeTraduccionesEliminar.Add(traduccionEliminarEnUs);

            return listaDeTraduccionesEliminar;
        }

        private List<Traduccion> TraduccionesDeLaEtiquetaAceptarModificadas()
        {
            var listaDeTraduccionesAceptar = new List<Traduccion>();

            var traduccionAceptarEn = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en"), "accept in english neutral");
            var traduccionAceptarEnUs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "accept in english US");

            listaDeTraduccionesAceptar.Add(traduccionAceptarEn);
            listaDeTraduccionesAceptar.Add(traduccionAceptarEnUs);

            return listaDeTraduccionesAceptar;
        }

        private List<Traduccion> TraduccionesDeLaEtiquetaAceptarModificadasYNuevas()
        {
            var listaDeTraduccionesAceptar = new List<Traduccion>();

            var traduccionAceptarEn = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en"), "accept in english neutral");
            var traduccionAceptarEnUs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "accept in english US");
            var traduccionAceptarFr = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("fr"), "francés en francés neutral");
            var traduccionAceptarFrFr = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("fr-FR"), "francés en francés de Francia");

            listaDeTraduccionesAceptar.Add(traduccionAceptarEn);
            listaDeTraduccionesAceptar.Add(traduccionAceptarEnUs);
            listaDeTraduccionesAceptar.Add(traduccionAceptarFr);
            listaDeTraduccionesAceptar.Add(traduccionAceptarFrFr);

            return listaDeTraduccionesAceptar;
        }

        private List<Traduccion> DosTraduccionesDeLaEtiquetaCancelarEliminadas()
        {
            var listaDeTraduccionesCancelar = new List<Traduccion>();

            var traduccionCancelarEs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es"), "cancelar");
            var traduccionCancelarEsVe = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es-VE"), "cancelar");

            listaDeTraduccionesCancelar.Add(traduccionCancelarEs);
            listaDeTraduccionesCancelar.Add(traduccionCancelarEsVe);

            return listaDeTraduccionesCancelar;
        }

        private List<Traduccion> TraduccionesAAgregar()
        {
            var listaDeTraducciones = new List<Traduccion>();

            var traduccionAceptarFr = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("fr"), "francés en francés neutral");
            var traduccionAceptarFrFr = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("fr-FR"), "francés en francés de Francia");

            listaDeTraducciones.Add(traduccionAceptarFr);
            listaDeTraducciones.Add(traduccionAceptarFrFr);

            return listaDeTraducciones;
        }

        private List<Traduccion> TraduccionesAModificar()
        {
            var listaDeTraducciones = new List<Traduccion>();

            var traduccionAceptarEn = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en"), "accept in english neutral");
            var traduccionAceptarEnUs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "accept in english US");
            var traduccionAceptarFr = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("fr"), "francés en francés neutral");
            var traduccionAceptarFrFr = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("fr-FR"), "francés en francés de Francia");

            listaDeTraducciones.Add(traduccionAceptarEn);
            listaDeTraducciones.Add(traduccionAceptarEnUs);
            listaDeTraducciones.Add(traduccionAceptarFr);
            listaDeTraducciones.Add(traduccionAceptarFrFr);

            return listaDeTraducciones;
        }

        private List<Traduccion> TraduccionesAModificarYAgregar()
        {
            var listaDeTraducciones = new List<Traduccion>();

            var traduccionAceptarEn = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en"), "accept in english neutral");
            var traduccionAceptarEnUs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "accept in english US");
            var traduccionAceptarFr = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("fr"), "francés en francés neutral");
            var traduccionAceptarFrFr = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("fr-FR"), "francés en francés de Francia");

            listaDeTraducciones.Add(traduccionAceptarEn);
            listaDeTraducciones.Add(traduccionAceptarEnUs);
            listaDeTraducciones.Add(traduccionAceptarFr);
            listaDeTraducciones.Add(traduccionAceptarFrFr);

            return listaDeTraducciones;
        }

        private List<Traduccion> TraduccionesAEliminar()
        {
            var listaDeTraducciones = new List<Traduccion>();

            var traduccionCancelarEn = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en"), "cancel");
            var traduccionCancelarEnUs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "cancel");

            listaDeTraducciones.Add(traduccionCancelarEn);
            listaDeTraducciones.Add(traduccionCancelarEnUs);

            return listaDeTraducciones;
        }

        #endregion

        #region Definiciones de las respuestas esperadas

        private ConsultarDiccionariosRespuesta RespuestaEsperadaConsultarDiccionarios()
        {
            var respuesta = ConsultarDiccionariosRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeDiccionarios = ListaConDosDiccionarios();

            return respuesta;
        }

        private ConsultarUnDiccionarioarioRespuesta RespuestaEsperadaConsultarUnDiccionario()
        {
            var diccionarioRespuesta = DiccionarioAdicionalConDosEtiquetas();
            var respuesta = ConsultarUnDiccionarioarioRespuesta.CrearNuevaInstancia(String.Empty);

            respuesta.Diccionario = diccionarioRespuesta;
            respuesta.Relaciones["diccionario"] = diccionarioRespuesta.Id;

            return respuesta;
        }

        private ConsultarEtiquetasPorNombreRespuesta RespuestaEsperadaConsultarEtiquetasPorNombre()
        {
            var listaDeDiccionarios = DiccionarioRespuestaConsultarEtiquetasPorNombre();

            var respuesta = ConsultarEtiquetasPorNombreRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeDiccionarios = listaDeDiccionarios;

            return respuesta;
        }

        private ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta RespuestaEsperadaConsultarEtiquetasPorIdioma()
        {
            var listaDeEtiquetas = ListaDeEtiquetasPorIdioma();

            var respuesta = ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeEtiquetas = listaDeEtiquetas;
            respuesta.Relaciones["diccionario"] = diccionarioBaseId;

            return respuesta;
        }

        private ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta RespuestaEsperadaConsultarEtiquetasPorIdiomaVacia()
        {
            var respuesta = ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeEtiquetas = new List<Etiqueta>();
            respuesta.Relaciones["diccionario"] = diccionarioBaseId;

            return respuesta;
        }

        private ConsultarEtiquetasDeDiccionarioPorNombreRespuesta RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorNombre()
        {
            var listaDeEtiquetas = ListaDeEtiquetasPorNombre();

            var respuesta = ConsultarEtiquetasDeDiccionarioPorNombreRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeEtiquetas = listaDeEtiquetas;
            respuesta.Relaciones["diccionario"] = diccionarioBaseId;

            return respuesta;
        }

        private ConsultarEtiquetasDeDiccionarioPorNombreRespuesta RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorNombreVacia()
        {
            var respuesta = ConsultarEtiquetasDeDiccionarioPorNombreRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeEtiquetas = new List<Etiqueta>();
            respuesta.Relaciones["diccionario"] = diccionarioBaseId;

            return respuesta;
        }

        private ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorDescripcion()
        {
            var respuesta = ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeEtiquetas = new List<Etiqueta> { EtiquetaAceptar() };
            respuesta.Relaciones["diccionario"] = diccionarioBaseId;

            return respuesta;
        }

        private ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorDescripcionVacia()
        {
            var respuesta = ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeEtiquetas = new List<Etiqueta>();
            respuesta.Relaciones["diccionario"] = diccionarioBaseId;

            return respuesta;
        }

        private ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorEstatus()
        {
            var respuesta = ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeEtiquetas = new List<Etiqueta> { EtiquetaAceptar(), EtiquetaCancelar(), EtiquetaEditar(), EtiquetaEliminar() };
            respuesta.Relaciones["diccionario"] = diccionarioBaseId;

            return respuesta;
        }

        private ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorEstatusVacia()
        {
            var respuesta = ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeEtiquetas = new List<Etiqueta>();
            respuesta.Relaciones["diccionario"] = diccionarioBaseId;

            return respuesta;
        }

        private ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorIdiomaPorDefecto()
        {
            var respuesta = ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeEtiquetas = new List<Etiqueta> { EtiquetaAceptar(), EtiquetaEliminar() };
            respuesta.Relaciones["diccionario"] = diccionarioBaseId;

            return respuesta;
        }

        private ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorIdiomaPorDefectoVacia()
        {
            var respuesta = ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeEtiquetas = new List<Etiqueta>();
            respuesta.Relaciones["diccionario"] = diccionarioBaseId;

            return respuesta;
        }

        private ModificarUnDiccionarioRespuesta RespuestaEsperadaModificarUnDiccionario()
        {
            var diccionarioRespuesta = DiccionarioConElAmbienteModificado();

            var respuesta = ModificarUnDiccionarioRespuesta.CrearNuevaInstancia();

            respuesta.Diccionario = diccionarioRespuesta;

            return respuesta;
        }

        private EliminarUnDiccionarioRespuesta RespuestaEsperadaEliminarUnDiccionario()
        {
            var respuesta = EliminarUnDiccionarioRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeDiccionarios = new List<Diccionario> { DiccionarioAdicionalConDosEtiquetas() };

            return respuesta;
        }

        private AgregarEtiquetasAUnDiccionarioRespuesta RespuestaEsperadaAgregarEtiquetas()
        {
            var diccionarioRespuesta = DiccionarioBaseConCuatroEtiquetas();

            var respuesta = AgregarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeEtiquetas = diccionarioRespuesta.Etiquetas.ToList();
            respuesta.Relaciones["diccionario"] = diccionarioRespuesta.Id;

            return respuesta;
        }

        private ModificarEtiquetasAUnDiccionarioRespuesta RespuestaEsperadaModificarEtiquetas()
        {
            var diccionarioRespuesta = DiccionarioConLaEtiquetaAceptarModificada();
            //var diccionarioRespuesta = DiccionarioConLaEtiquetaAceptarModificadaPrueba();

            var respuesta = ModificarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeEtiquetas = diccionarioRespuesta.Etiquetas.ToList();
            respuesta.Relaciones["diccionario"] = diccionarioRespuesta.Id;

            return respuesta;
        }

        private ModificarEtiquetasAUnDiccionarioRespuesta RespuestaEsperadaModificarYAgregarEtiquetas()
        {
            var diccionarioRespuesta = DiccionarioConLaEtiquetaAceptarModificadaYDosEtiquetasAgregadas();

            var respuesta = ModificarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeEtiquetas = diccionarioRespuesta.Etiquetas.ToList();
            respuesta.Relaciones["diccionario"] = diccionarioRespuesta.Id;

            return respuesta;
        }

        private EliminarEtiquetasAUnDiccionarioRespuesta RespuestaEsperadaEliminarEtiquetas()
        {
            var diccionarioRespuesta = DiccionarioConLaEtiquetaCancelarEliminada();

            var respuesta = EliminarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeEtiquetas = diccionarioRespuesta.Etiquetas.ToList();
            respuesta.Relaciones["diccionario"] = diccionarioRespuesta.Id;

            return respuesta;
        }

        private EliminarEtiquetasAUnDiccionarioRespuesta RespuestaEsperadaEliminarTodasLasEtiquetas()
        {
            var diccionarioRespuesta = DiccionarioBaseSinEtiquetas();

            var respuesta = EliminarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeEtiquetas = diccionarioRespuesta.Etiquetas.ToList();
            respuesta.Relaciones["diccionario"] = diccionarioRespuesta.Id;

            return respuesta;
        }

        private AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta RespuestaEsperadaAgregarTraducciones()
        {
            var diccionarioRespuesta = DiccionarioConTraduccionesDeLaEtiquetaAceptarAgregadas();

            var respuesta = AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeTraducciones = diccionarioRespuesta.Etiquetas.Where(e => e.Id == etiquetaAceptarId).ToList().FirstOrDefault().Textos.ToList();
            respuesta.Relaciones["diccionario"] = diccionarioRespuesta.Id;
            respuesta.Relaciones["etiqueta"] = diccionarioRespuesta.Etiquetas.Where(e => e.Id == etiquetaAceptarId).ToList().FirstOrDefault().Id;

            return respuesta;
        }

        private ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta RespuestaEsperadaModificarTraducciones()
        {
            var diccionarioRespuesta = DiccionarioConTraduccionesDeLaEtiquetaAceptarModificadas();

            var respuesta = ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeTraducciones = diccionarioRespuesta.Etiquetas.Where(e => e.Id == etiquetaAceptarId).ToList().FirstOrDefault().Textos.ToList();
            respuesta.Relaciones["diccionario"] = diccionarioRespuesta.Id;
            respuesta.Relaciones["etiqueta"] = diccionarioRespuesta.Etiquetas.Where(e => e.Id == etiquetaAceptarId).ToList().FirstOrDefault().Id;

            return respuesta;
        }

        private ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta RespuestaEsperadaModificarYAgregarTraducciones()
        {
            var diccionarioRespuesta = DiccionarioConTraduccionesDeLaEtiquetaAceptarModificadasYNuevas();

            var respuesta = ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeTraducciones = diccionarioRespuesta.Etiquetas.Where(e => e.Id == etiquetaAceptarId).ToList().FirstOrDefault().Textos.ToList();
            respuesta.Relaciones["diccionario"] = diccionarioRespuesta.Id;
            respuesta.Relaciones["etiqueta"] = diccionarioRespuesta.Etiquetas.Where(e => e.Id == etiquetaAceptarId).ToList().FirstOrDefault().Id;

            return respuesta;
        }

        private EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta RespuestaEsperadaEliminarDosTraducciones()
        {
            var diccionarioRespuesta = DiccionarioConDosTraduccionesDeLaEtiquetaCancelarEliminadas();

            var respuesta = EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeTraducciones = diccionarioRespuesta.Etiquetas.Where(e => e.Id == etiquetaCancelarId).ToList().FirstOrDefault().Textos.ToList();
            respuesta.Relaciones["diccionario"] = diccionarioRespuesta.Id;
            respuesta.Relaciones["etiqueta"] = diccionarioRespuesta.Etiquetas.Where(e => e.Id == etiquetaCancelarId).ToList().FirstOrDefault().Id;

            return respuesta;
        }

        private EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta RespuestaEsperadaEliminarTodasLasTraducciones()
        {
            var diccionarioRespuesta = DiccionarioConTodasLasTraduccionesDeLaEtiquetaAceptarEliminadas();

            var respuesta = EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            respuesta.ListaDeTraducciones = diccionarioRespuesta.Etiquetas.Where(e => e.Id == etiquetaAceptarId).ToList().FirstOrDefault().Textos.ToList();
            respuesta.Relaciones["diccionario"] = diccionarioRespuesta.Id;
            respuesta.Relaciones["etiqueta"] = diccionarioRespuesta.Etiquetas.Where(e => e.Id == etiquetaAceptarId).ToList().FirstOrDefault().Id;

            return respuesta;
        }

        #endregion

        #region Consultas
        #region ConsultarEtiquetasPorNombre

        [Test]
        [Category("Consultar etiquetas por nombre")]
        public void DeberiaPoderObtenerUnErrorAlConsultarLasEtiquetasPorNombreSiNoSeEncuentraNingunDiccionarioEnElRepositorio()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.When(x => x.ObtenerDiccionarios()).Do(obj => { throw new Exception(); });

            var peticion = ConsultarEtiquetasPorNombrePeticion.CrearNuevaInstancia();
            peticion.Nombre = ConsultarEtiquetasDeDiccionariosPorNombre;

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            Should.Throw<Exception>(
                () => serviciosApi.ConsultarEtiquetasPorNombre(peticion)
            );
        }

        [Test]
        [Category("Consultar etiquetas por nombre")]
        public void DeberiaPoderConsultarLasEtiquetasPorNombreEnTodosLosDicccionarios()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerDiccionarios().Returns(new List<Diccionario> { DiccionarioBaseConCuatroEtiquetas(), DiccionarioAdicionalConDosEtiquetas() });

            var peticion = ConsultarEtiquetasPorNombrePeticion.CrearNuevaInstancia();
            peticion.Nombre = ConsultarEtiquetasDeDiccionariosPorNombre; // "sadfsad dfsad";

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaConsultarEtiquetasPorNombre();

            //Act
            var respuestaReal = serviciosApi.ConsultarEtiquetasPorNombre(peticion);

            //Assert
            foreach (var item in respuestaReal.ListaDeDiccionarios)
            {
                respuestaEsperada.ListaDeDiccionarios.ShouldContain(item);
            }
        }

        #endregion


        #region ConsultarEtiquetasDeDiccionarioPorIdioma

        [Test]
        [Category("Consultar las etiquetas de un diccionario por idioma")]
        public void DeberiaPoderObtenerUnErrorCuandoNoSeEncuentraElDiccionarioSolicitadoParaConsultarLasEtiquetasPorIdioma()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioAdicionalId;
            peticion.Idioma = ConsultarDiccionarioPorIdioma;

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            //Act y Assert
            Should.Throw<Exception>(
                () => serviciosApi.ConsultarEtiquetasDeDiccionarioPorIdioma(peticion)
            );
        }

        [Test]
        [Category("Consultar las etiquetas de un diccionario por idioma")]
        public void DeberiaPoderObtenerUnaListaDeEtiquetasVaciaCuandoSeEnvíaUnIdiomaDeTraduccionDeEtiquetaQueNoExiste()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.Idioma = "pt";

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaConsultarEtiquetasPorIdiomaVacia();

            //Act
            var respuestaReal = serviciosApi.ConsultarEtiquetasDeDiccionarioPorIdioma(peticion);

            //Assert
            respuestaReal.ListaDeEtiquetas.Count().ShouldBe(0);

            respuestaEsperada.ShouldSatisfyAllConditions(
                () => respuestaEsperada.ListaDeEtiquetas.ShouldBe(respuestaReal.ListaDeEtiquetas),
                () => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
            );
        }

        [Test]
        [Category("Consultar las etiquetas de un diccionario por idioma")]
        public void DeberiaPoderConsultarLasEtiquetasDeUnDiccionarioPorIdioma()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.Idioma = ConsultarDiccionarioPorIdioma;

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaConsultarEtiquetasPorIdioma();

            //Act
            var respuestaReal = serviciosApi.ConsultarEtiquetasDeDiccionarioPorIdioma(peticion);

            //Assert
            CollectionAssert.AreEqual(respuestaEsperada.ListaDeEtiquetas.ToList<Etiqueta>(), respuestaReal.ListaDeEtiquetas.ToList<Etiqueta>());
            CollectionAssert.AreEqual(respuestaEsperada.Relaciones, respuestaReal.Relaciones);

            //respuestaEsperada.ShouldSatisfyAllConditions(
            //	() => respuestaEsperada.ListaDeEtiquetas.ShouldBe(respuestaReal.ListaDeEtiquetas),
            //	() => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
            //);
        }

        #endregion


        #region ConsultarEtiquetasDeDiccionarioPorNombre

        [Test]
        [Category("Consultar las etiquetas de un diccionario por nombre")]
        public void DeberiaPoderObtenerUnErrorCuandoNoSeEncuentraElDiccionarioSolicitadoParaConsultarLasEtiquetasPorNombre()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = ConsultarEtiquetasDeDiccionarioPorNombrePeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioAdicionalId;
            peticion.Nombre = ConsultarEtiquetasPorNombre;

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            //Act y Assert
            Should.Throw<Exception>(
                () => serviciosApi.ConsultarEtiquetasDeDiccionarioPorNombre(peticion)
            );
        }

        [Test]
        [Category("Consultar las etiquetas de un diccionario por nombre")]
        public void DeberiaPoderObtenerUnaListaDeEtiquetasVaciaCuandoSeEnvíaUnNombreDeEtiquetaQueNoExiste()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = ConsultarEtiquetasDeDiccionarioPorNombrePeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.Nombre = "buscar";

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorNombreVacia();

            //Act
            var respuestaReal = serviciosApi.ConsultarEtiquetasDeDiccionarioPorNombre(peticion);

            //Assert
            respuestaReal.ListaDeEtiquetas.Count().ShouldBe(0);

            respuestaEsperada.ShouldSatisfyAllConditions(
                () => respuestaEsperada.ListaDeEtiquetas.ShouldBe(respuestaReal.ListaDeEtiquetas),
                () => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
            );
        }

        [Test]
        [Category("Consultar las etiquetas de un diccionario por nombre")]
        public void DeberiaPoderConsultarLasEtiquetasDeUnDiccionarioPorNombre()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = ConsultarEtiquetasDeDiccionarioPorNombrePeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.Nombre = ConsultarEtiquetasPorNombre;

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorNombre();

            //Act
            var respuestaReal = serviciosApi.ConsultarEtiquetasDeDiccionarioPorNombre(peticion);

            //Assert
            CollectionAssert.AreEqual(respuestaEsperada.ListaDeEtiquetas.ToList<Etiqueta>(), respuestaReal.ListaDeEtiquetas.ToList<Etiqueta>());
            CollectionAssert.AreEqual(respuestaEsperada.Relaciones, respuestaReal.Relaciones);

            //respuestaEsperada.ShouldSatisfyAllConditions(
            //	() => respuestaEsperada.ListaDeEtiquetas.ShouldBe(respuestaReal.ListaDeEtiquetas),
            //	() => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
            //);
        }

        #endregion


        #region ConsultarEtiquetasDeDiccionarioPorDescripcion

        [Test]
        [Category("Consultar las etiquetas de un diccionario por descripción")]
        public void DeberiaPoderObtenerUnErrorCuandoNoSeEncuentraElDiccionarioSolicitadoParaConsultarLasEtiquetasPorDescripcion()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioAdicionalId;
            peticion.Descripcion = ConsultarEtiquetaPorDescripcion;

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            //Act y Assert
            Should.Throw<Exception>(
                () => serviciosApi.ConsultarEtiquetasDeDiccionarioPorDescripcion(peticion)
            );
        }

        [Test]
        [Category("Consultar las etiquetas de un diccionario por descripción")]
        public void DeberiaPoderObtenerUnaListaDeEtiquetasVaciaCuandoSeEnvíaUnaDescripcionDeEtiquetaQueNoExiste()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.Descripcion = "buscar";

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorDescripcionVacia();

            //Act
            var respuestaReal = serviciosApi.ConsultarEtiquetasDeDiccionarioPorDescripcion(peticion);

            //Assert
            respuestaReal.ListaDeEtiquetas.Count().ShouldBe(0);

            respuestaEsperada.ShouldSatisfyAllConditions(
                () => respuestaEsperada.ListaDeEtiquetas.ShouldBe(respuestaReal.ListaDeEtiquetas),
                () => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
            );
        }

        [Test]
        [Category("Consultar las etiquetas de un diccionario por descripción")]
        public void DeberiaPoderConsultarLasEtiquetasDeUnDiccionarioPorDescripcion()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.Descripcion = ConsultarEtiquetaPorDescripcion;

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorDescripcion();

            //Act
            var respuestaReal = serviciosApi.ConsultarEtiquetasDeDiccionarioPorDescripcion(peticion);

            //Assert
            CollectionAssert.AreEqual(respuestaEsperada.ListaDeEtiquetas.ToList<Etiqueta>(), respuestaReal.ListaDeEtiquetas.ToList<Etiqueta>());
            CollectionAssert.AreEqual(respuestaEsperada.Relaciones, respuestaReal.Relaciones);

            //respuestaEsperada.ShouldSatisfyAllConditions(
            //	() => respuestaEsperada.ListaDeEtiquetas.ShouldBe(respuestaReal.ListaDeEtiquetas),
            //	() => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
            //);
        }

        #endregion


        #region ConsultarEtiquetasDeDiccionarioPorEstatus

        [Test]
        [Category("Consultar las etiquetas de un diccionario por estatus")]
        public void DeberiaPoderObtenerUnErrorCuandoNoSeEncuentraElDiccionarioSolicitadoParaConsultarLasEtiquetasPorEstatus()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = ConsultarEtiquetasDeDiccionarioPorEstatusPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioAdicionalId;
            peticion.Estatus = ConsultarEtiquetaPorEstatus;

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            //Act y Assert
            Should.Throw<Exception>(
                () => serviciosApi.ConsultarEtiquetasDeDiccionarioPorEstatus(peticion)
            );
        }

        [Test]
        [Category("Consultar las etiquetas de un diccionario por estatus")]
        public void DeberiaPoderObtenerUnaListaDeEtiquetasVaciaCuandoSeEnvíaUnEstatusDeEtiquetaQueNoExiste()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = ConsultarEtiquetasDeDiccionarioPorEstatusPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.Estatus = false;

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorEstatusVacia();

            //Act
            var respuestaReal = serviciosApi.ConsultarEtiquetasDeDiccionarioPorEstatus(peticion);

            //Assert
            respuestaReal.ListaDeEtiquetas.Count().ShouldBe(0);

            respuestaEsperada.ShouldSatisfyAllConditions(
                () => respuestaEsperada.ListaDeEtiquetas.ShouldBe(respuestaReal.ListaDeEtiquetas),
                () => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
            );
        }

        [Test]
        [Category("Consultar las etiquetas de un diccionario por estatus")]
        public void DeberiaPoderConsultarLasEtiquetasDeUnDiccionarioPorEstatus()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = ConsultarEtiquetasDeDiccionarioPorEstatusPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.Estatus = ConsultarEtiquetaPorEstatus;

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorEstatus();

            //Act
            var respuestaReal = serviciosApi.ConsultarEtiquetasDeDiccionarioPorEstatus(peticion);

            //Assert
            respuestaEsperada.ShouldSatisfyAllConditions(
                () => respuestaEsperada.ListaDeEtiquetas.ShouldBe(respuestaReal.ListaDeEtiquetas),
                () => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
            );
        }

        #endregion


        #region ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefecto

        [Test]
        [Category("Consultar las etiquetas de un diccionario por idioma por defecto")]
        public void DeberiaPoderObtenerUnErrorCuandoNoSeEncuentraElDiccionarioSolicitadoParaConsultarLasEtiquetasPorIdiomaPorDefecto()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioAdicionalId;
            peticion.IdiomaPorDefecto = ConsultarEtiquetaPorIdiomaPorDefecto;

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            //Act y Assert
            Should.Throw<Exception>(
                () => serviciosApi.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefecto(peticion)
            );
        }

        [Test]
        [Category("Consultar las etiquetas de un diccionario por idioma por defecto")]
        public void DeberiaPoderObtenerUnaListaDeEtiquetasVaciaCuandoSeEnvíaUnIdiomaPorDefectoDeEtiquetaQueNoExiste()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.IdiomaPorDefecto = "pt";

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorIdiomaPorDefectoVacia();

            //Act
            var respuestaReal = serviciosApi.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefecto(peticion);

            //Assert
            respuestaReal.ListaDeEtiquetas.Count().ShouldBe(0);

            respuestaEsperada.ShouldSatisfyAllConditions(
                () => respuestaEsperada.ListaDeEtiquetas.ShouldBe(respuestaReal.ListaDeEtiquetas),
                () => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
            );
        }

        [Test]
        [Category("Consultar las etiquetas de un diccionario por idioma por defecto")]
        public void DeberiaPoderConsultarLasEtiquetasDeUnDiccionarioPorIdiomaPorDefecto()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.IdiomaPorDefecto = ConsultarEtiquetaPorIdiomaPorDefecto;

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaConsultarEtiquetasDeUnDiccionarioPorIdiomaPorDefecto();

            //Act
            var respuestaReal = serviciosApi.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefecto(peticion);

            //Assert
            CollectionAssert.AreEqual(respuestaEsperada.ListaDeEtiquetas.ToList<Etiqueta>(), respuestaReal.ListaDeEtiquetas.ToList<Etiqueta>());
            CollectionAssert.AreEqual(respuestaEsperada.Relaciones, respuestaReal.Relaciones);

            //respuestaEsperada.ShouldSatisfyAllConditions(
            //	() => respuestaEsperada.ListaDeEtiquetas.ShouldBe(respuestaReal.ListaDeEtiquetas),
            //	() => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
            //);
        }

        #endregion

        #endregion

        #region AgregarEtiquetasAUnDiccionario

        [Test]
        [Category("Agregar etiquetas a un diccionario")]
        public void DeberiaPoderObtenerUnErrorDeTipoNullReferenceExceptionCuandoNoSeEncontroElDiccionarioSolicitadoParaAgregarEtiquetas()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            var diccionarioInicialDeLasPruebas = DiccionarioBaseConDosEtiquetas();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);
            diccionarioRepositorio.SalvarUnDiccionario(diccionarioInicialDeLasPruebas).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = AgregarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioAdicionalId;
            peticion.ListaDeEtiquetas = EtiquetasAAgregar();

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            //Act y Assert
            Should.Throw<NullReferenceException>(
                () => serviciosApi.AgregarEtiquetasAUnDiccionario(peticion)
            );
        }

        [Test]
        [Category("Agregar etiquetas a un diccionario")]
        public void DeberiaPoderObtenerUnErrorDeTipoExceptionCuandoSeEnviaUnaListaVaciaDeEtiquetasParaAgregar()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            var diccionarioInicialDeLasPruebas = DiccionarioBaseConDosEtiquetas();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);
            diccionarioRepositorio.SalvarUnDiccionario(diccionarioInicialDeLasPruebas).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = AgregarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            //Act y Assert
            Should.Throw<Exception>(
                () => serviciosApi.AgregarEtiquetasAUnDiccionario(peticion)
            );
        }

        [Test]
        [Category("Agregar etiquetas a un diccionario")]
        public void DeberiaPoderObtenerUnErrorDeTipoArgumentExceptionSiSeEnvianEtiquetasRepetidas()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            var diccionarioInicialDeLasPruebas = DiccionarioBaseConDosEtiquetas();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);
            diccionarioRepositorio.SalvarUnDiccionario(diccionarioInicialDeLasPruebas).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = AgregarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.ListaDeEtiquetas = new List<Etiqueta> { EtiquetaAceptar() };

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            //Act y Assert
            Should.Throw<ArgumentException>(
                () => serviciosApi.AgregarEtiquetasAUnDiccionario(peticion)
            );
        }

        [Test]
        [Category("Agregar etiquetas a un diccionario")]
        public void DeberiaPoderObtenerUnErrorSiAlSalvarLasNuevasEtiquetasEnElDiccionarioSeRetornaNull()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            var diccionarioInicialDeLasPruebas = DiccionarioBaseConDosEtiquetas();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);
            diccionarioRepositorio.SalvarUnDiccionario(diccionarioInicialDeLasPruebas).Returns(DiccionarioNull());

            var peticion = AgregarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.ListaDeEtiquetas = EtiquetasAAgregar();

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            //Act y Assert
            Should.Throw<Exception>(
                () => serviciosApi.AgregarEtiquetasAUnDiccionario(peticion)
            );
        }

        [Test]
        [Category("Agregar etiquetas a un diccionario")]
        public void DeberiaPoderAgregaEtiquetasAUnDiccionarioExistente()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            var diccionarioInicialDeLasPruebas = DiccionarioBaseConDosEtiquetas();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);
            diccionarioRepositorio.SalvarUnDiccionario(diccionarioInicialDeLasPruebas).Returns(DiccionarioBaseConCuatroEtiquetas());

            var peticion = AgregarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.ListaDeEtiquetas = EtiquetasAAgregar();

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaAgregarEtiquetas();

            //Act
            var respuestaReal = serviciosApi.AgregarEtiquetasAUnDiccionario(peticion);

            //Assert
            respuestaEsperada.ShouldSatisfyAllConditions(
                () => respuestaEsperada.ListaDeEtiquetas.ShouldBe(respuestaReal.ListaDeEtiquetas),
                () => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
            );
        }

        #endregion

        #region ModificarEtiquetasAUnDiccionario

        [Test]
        [Category("Modificar etiquetas a un diccionario")]
        public void DeberiaPoderObtenerUnErrorDeTipoNullReferenceExceptionCuandoNoSeEncontroElDiccionarioSolicitadoParaModificarEtiquetas()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            var diccionarioInicialDeLasPruebas = DiccionarioBaseConDosEtiquetas();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);
            diccionarioRepositorio.SalvarUnDiccionario(diccionarioInicialDeLasPruebas).Returns(DiccionarioConLaEtiquetaAceptarModificada());

            var peticion = ModificarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioAdicionalId;
            peticion.ListaDeEtiquetas = EtiquetasAModificar();

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            //Act y Assert
            Should.Throw<NullReferenceException>(
                () => serviciosApi.ModificarEtiquetasAUnDiccionario(peticion)
            );
        }

        [Test]
        [Category("Modificar etiquetas a un diccionario")]
        public void DeberiaPoderObtenerUnErrorDeTipoExceptionCuandoSeEnviaUnaListaVaciaDeEtiquetasParaModificar()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            var diccionarioInicialDeLasPruebas = DiccionarioBaseConDosEtiquetas();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);
            diccionarioRepositorio.SalvarUnDiccionario(diccionarioInicialDeLasPruebas).Returns(DiccionarioConLaEtiquetaAceptarModificada());

            var peticion = ModificarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            //Act y Assert
            Should.Throw<Exception>(
                () => serviciosApi.ModificarEtiquetasAUnDiccionario(peticion)
            );
        }

        [Test]
        [Category("Modificar etiquetas a un diccionario")]
        public void DeberiaPoderObtenerUnErrorSiAlSalvarLasEtiquetasModificadasEnElDiccionarioSeRetornaNull()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            var diccionarioInicialDeLasPruebas = DiccionarioBaseConDosEtiquetas();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);
            diccionarioRepositorio.SalvarUnDiccionario(diccionarioInicialDeLasPruebas).Returns(DiccionarioNull());

            var peticion = ModificarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.ListaDeEtiquetas = EtiquetasAModificar();

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);


            //Act y Assert
            Should.Throw<Exception>(
                () => serviciosApi.ModificarEtiquetasAUnDiccionario(peticion)
            );
        }

        [Test]
        [Category("Modificar etiquetas a un diccionario")]
        public void DeberiaPoderModificarEtiquetasAUnDiccionarioExistente()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            var diccionarioInicialDeLasPruebas = DiccionarioBaseConDosEtiquetas();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);
            diccionarioRepositorio.SalvarUnDiccionario(diccionarioInicialDeLasPruebas).Returns(DiccionarioConLaEtiquetaAceptarModificada());

            var peticion = ModificarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.ListaDeEtiquetas = EtiquetasAModificar();

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaModificarEtiquetas();

            //Act
            var respuestaReal = serviciosApi.ModificarEtiquetasAUnDiccionario(peticion);

            //Assert
            respuestaEsperada.ShouldSatisfyAllConditions(
                () => respuestaEsperada.ListaDeEtiquetas.ShouldBe(respuestaReal.ListaDeEtiquetas),
                () => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
            );
        }

        [Test]
        [Category("Modificar etiquetas a un diccionario")]
        public void DeberiaPoderModificarYAgregarEtiquetasAUnDiccionarioExistente()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            var diccionarioInicialDeLasPruebas = DiccionarioBaseConDosEtiquetas();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);
            diccionarioRepositorio.SalvarUnDiccionario(diccionarioInicialDeLasPruebas).Returns(DiccionarioConLaEtiquetaAceptarModificadaYDosEtiquetasAgregadas());

            var peticion = ModificarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.ListaDeEtiquetas = EtiquetasAModificarYAgregar();

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaModificarYAgregarEtiquetas();

            //Act
            var respuestaReal = serviciosApi.ModificarEtiquetasAUnDiccionario(peticion);

            //Assert
            respuestaEsperada.ShouldSatisfyAllConditions(
                () => respuestaEsperada.ListaDeEtiquetas.ShouldBe(respuestaReal.ListaDeEtiquetas),
                () => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
            );
        }

        #endregion

        #region EliminarEtiquetasAUnDiccionario

        [Test]
        [Category("Eliminar etiquetas a un diccionario")]
        public void DeberiaPoderObtenerUnErrorDeTipoNullReferenceExceptionCuandoNoSeEncontroElDiccionarioSolicitadoParaEliminarEtiquetas()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            var diccionarioInicialDeLasPruebas = DiccionarioBaseConDosEtiquetas();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);
            diccionarioRepositorio.SalvarUnDiccionario(diccionarioInicialDeLasPruebas).Returns(DiccionarioConLaEtiquetaCancelarEliminada());

            var peticion = EliminarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioAdicionalId;
            peticion.ListaDeEtiquetas = EtiquetasAEliminar();

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            //Act y Assert
            Should.Throw<NullReferenceException>(
                () => serviciosApi.EliminarEtiquetasAUnDiccionario(peticion)
            );
        }

        [Test]
        [Category("Eliminar etiquetas a un diccionario")]
        public void DeberiaPoderObtenerUnErrorDeTipoExceptionCuandoSeEnviaUnaListaVaciaDeEtiquetasParaEliminar()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            var diccionarioInicialDeLasPruebas = DiccionarioBaseConDosEtiquetas();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);
            diccionarioRepositorio.SalvarUnDiccionario(diccionarioInicialDeLasPruebas).Returns(DiccionarioConLaEtiquetaCancelarEliminada());

            var peticion = EliminarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            //Act y Assert
            Should.Throw<Exception>(
                () => serviciosApi.EliminarEtiquetasAUnDiccionario(peticion)
            );
        }

        [Test]
        [Category("Eliminar etiquetas a un diccionario")]
        public void DeberiaPoderObtenerUnErrorSiAlSalvarLasEtiquetasEliminadasEnElDiccionarioSeRetornaNull()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            var diccionarioInicialDeLasPruebas = DiccionarioBaseConDosEtiquetas();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);
            diccionarioRepositorio.SalvarUnDiccionario(diccionarioInicialDeLasPruebas).Returns(DiccionarioNull());

            var peticion = EliminarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.ListaDeEtiquetas = EtiquetasAEliminar();

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            //Act y Assert
            Should.Throw<Exception>(
                () => serviciosApi.EliminarEtiquetasAUnDiccionario(peticion)
            );
        }

        [Test]
        [Category("Eliminar etiquetas a un diccionario")]
        public void DeberiaPoderEliminarEtiquetasAUnDiccionarioExistente()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            var diccionarioInicialDeLasPruebas = DiccionarioBaseConDosEtiquetas();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);
            diccionarioRepositorio.SalvarUnDiccionario(diccionarioInicialDeLasPruebas).Returns(DiccionarioConLaEtiquetaCancelarEliminada());

            var peticion = EliminarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.ListaDeEtiquetas = EtiquetasAEliminar();

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaEliminarEtiquetas();

            //Act
            var respuestaReal = serviciosApi.EliminarEtiquetasAUnDiccionario(peticion);

            //Assert
            respuestaEsperada.ShouldSatisfyAllConditions(
                () => respuestaEsperada.ListaDeEtiquetas.ShouldBe(respuestaReal.ListaDeEtiquetas),
                () => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
            );
        }

        [Test]
        [Category("Eliminar etiquetas a un diccionario")]
        public void DeberiaPoderEliminarTodasLasEtiquetasAUnDiccionarioExistente()
        {
            //Arrange
            var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
            var diccionarioInicialDeLasPruebas = DiccionarioBaseConCuatroEtiquetas();
            diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);
            diccionarioRepositorio.SalvarUnDiccionario(diccionarioInicialDeLasPruebas).Returns(DiccionarioBaseSinEtiquetas());

            var peticion = ModificarEtiquetasAUnDiccionarioPeticion.CrearNuevaInstancia();
            peticion.DiccionarioId = diccionarioBaseId;
            peticion.ListaDeEtiquetas = new List<Etiqueta> { EtiquetaEliminar(), EtiquetaCancelar(), EtiquetaEditar(), EtiquetaAceptar() };

            var serviciosApi = new AdministradorDeEtiquetas(diccionarioRepositorio);

            var respuestaEsperada = RespuestaEsperadaEliminarTodasLasEtiquetas();

            //Act
            var respuestaReal = serviciosApi.ModificarEtiquetasAUnDiccionario(peticion);

            //Assert
            respuestaEsperada.ShouldSatisfyAllConditions(
                () => respuestaEsperada.ListaDeEtiquetas.ShouldBe(respuestaReal.ListaDeEtiquetas),
                () => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
            );
        }

        #endregion
    }
}
