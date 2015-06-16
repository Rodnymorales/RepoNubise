using System.Collections.Generic;
using System.IO;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Modelo;
using Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Persistencia;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;
using System.Xml.Serialization;
using Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Implementacion;
using Should;
using Shouldly;
using Diccionario = Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario.Diccionario;
using Etiqueta = Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Modelo.Etiqueta;
using Traduccion = Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Modelo.Traduccion;

namespace Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.PruebasUnitarias.Repositorio
{
    [TestFixture]
    public class DiccionarioRepositorioTestMock
    {
        #region Atributos

        //private Diccionarios _diccionarioInicialPruebasMock;
        //private Modelo.Diccionario _diccionario;
        //private List<Modelo.Diccionario> listaDiccionarios = new List<Modelo.Diccionario>();

        private const string AmbienteTest = "Prueba";
        private const string AmbienteDesarrollo = "Desarrollo";
        private const string AmbienteCalidad = "Calidad";
        private const string nombreEtiquetaAceptar = "app.aceptar";
        private const string nombreEtiquetaCancelar = "app.cancelar";
        private const string nombreEtiquetaEliminar = "app.test.eliminar";
        private const string nombreEtiquetaEditar = "app.test.editar";
        private const string strAceptar = "aceptar";
        private const string strCancelar = "cancelar";
        private const string strEliminar = "eliminar";
        private const string strEditar = "editar";
        private const string strAcept = "acept";
        private const string strCancel = "canceler";
        private const string strDelete = "delete";
        private const string strEdit = "edit";
        private const string strAccepter = "accepter";
        private const string strCanceler = "canceler";
        private const string strSupprimer = "supprimer";
        private const string strEditer = "editer";
        
        private const string CulturaEspañol = "es-ES";
        private const string CulturaIngles = "en-US";
        private const string CulturaFrances = "fr-FR";

        private readonly Guid diccionarioId1 = new Guid("a47d8e17-b7d5-462a-846d-c9949bb8a21b");
        private readonly Guid diccionarioId2 = new Guid("a45c5755-393f-4642-96e2-cac19deb20f2");
        private readonly Guid diccionarioId3 = new Guid("5e8e86f5-5845-4dd4-998a-0689ae10c8e9");
        private readonly Guid etiquetaAceptarId = new Guid("98342dd9-dc89-4c99-9456-4eafdbdda090");
        private readonly Guid etiquetaCancelarId = new Guid("ab1949f6-a62a-4720-b92a-dc2cce2f8c63");
        private readonly Guid etiquetaEliminarId = new Guid("80716938-d078-4254-9dc1-c09f6f9a9c79");
        private readonly Guid etiquetaEditarId = new Guid("0dd599f0-7078-482a-b98f-f33c3450e5ac");

        private readonly string _directoryStatic =
            Environment.CurrentDirectory.Replace("\\bin\\Debug", "\\DatosPrueba\\") + "diccionario_ok_Existe.xml";

        private XmlSerializer _serializador = new XmlSerializer(typeof(Diccionarios));

        #endregion

        #region Definicion de Diccionarios del Repositorio

        private Diccionarios DiccionariosDelRepositorioConTresDiccionaios()
        {
            ////////////////////////////////////////Diccionario 1
            var diccionario1 = new Modelo.Diccionario
            {
                Id = diccionarioId1,
                Ambiente = AmbienteTest,
                Etiquetas = new Etiquetas()
            };
            ////////////////////////////////////////Diccionario 2
            var diccionario2 = new Modelo.Diccionario
            {
                Id = diccionarioId2,
                Ambiente = AmbienteDesarrollo,
                Etiquetas = new Etiquetas()
            };
            ////////////////////////////////////////Diccionario 3
            var diccionario3 = new Modelo.Diccionario
            {
                Id = diccionarioId3,
                Ambiente = AmbienteCalidad,
                Etiquetas = new Etiquetas()
            };
            ////////////////////////////////////////Etiqueta 1
            var etiquetaAceptar = new Etiqueta
            {
                Id = etiquetaAceptarId,
                Nombre = nombreEtiquetaAceptar,
                Descripcion = strAceptar,
                Traducciones = new Traducciones()
            };
            ////////////////////////////////////////Etiqueta 2
            var etiquetaCancelar = new Etiqueta
            {
                Id = etiquetaCancelarId,
                Nombre = nombreEtiquetaCancelar,
                Descripcion = strCancelar,
                Traducciones = new Traducciones()
            };
            ////////////////////////////////////////Etiqueta 3
            var etiquetaEliminar = new Etiqueta
            {
                Id = etiquetaEliminarId,
                Nombre = nombreEtiquetaEliminar,
                Descripcion = strEliminar,
                Traducciones = new Traducciones()
            };

            ////////////////////////////////////////Traduccion Aceptar
            var traduccionAceptarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strAceptar,
                Value = strAceptar
            };
            ////////////////////////////////////////Traduccion Acept
            var traduccionAceptarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strAcept,
                Value = strAcept
            };
            ////////////////////////////////////////Traduccion Cancelar
            var traduccionCancelarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strCancelar,
                Value = strCancelar
            };
            ////////////////////////////////////////Traduccion Cancel
            var traduccionCancelarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strCancel,
                Value = strCancel
            };
            ////////////////////////////////////////Traduccion Eliminar
            var traduccionEliminarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strEliminar,
                Value = strEliminar
            };
            ////////////////////////////////////////Traduccion Delete
            var traduccionEliminarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strDelete,
                Value = strDelete
            };

            //////////////////////Agregando Traducciones A Etiquetas Aceptar
            etiquetaAceptar.Traducciones.Traducciones1.Add(traduccionAceptarEs);
            etiquetaAceptar.Traducciones.Traducciones1.Add(traduccionAceptarEn);

            //////////////////////Agregando Traducciones A Etiquetas Cancelar
            etiquetaCancelar.Traducciones.Traducciones1.Add(traduccionCancelarEs);
            etiquetaCancelar.Traducciones.Traducciones1.Add(traduccionCancelarEn);

            //////////////////////Agregando Traducciones A Etiquetas Eliminar
            etiquetaEliminar.Traducciones.Traducciones1.Add(traduccionEliminarEs);
            etiquetaEliminar.Traducciones.Traducciones1.Add(traduccionEliminarEn);

            //////////////////////Agregando Etiquetas Al Diccionario 1
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaEliminar);

            //////////////////////Agregando Etiquetas Al Diccionario 2
            diccionario2.Etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            diccionario2.Etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            diccionario2.Etiquetas.ListaEtiquetas.Add(etiquetaEliminar);

            //////////////////////Agregando Etiquetas Al Diccionario 3
            diccionario3.Etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            diccionario3.Etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            diccionario3.Etiquetas.ListaEtiquetas.Add(etiquetaEliminar);

            var traducciones = new Traducciones();
            traducciones.Traducciones1.Add(traduccionAceptarEs);
            traducciones.Traducciones1.Add(traduccionAceptarEn);
            traducciones.Traducciones1.Add(traduccionCancelarEs);
            traducciones.Traducciones1.Add(traduccionCancelarEn);
            traducciones.Traducciones1.Add(traduccionEliminarEs);
            traducciones.Traducciones1.Add(traduccionEliminarEn);

            var etiquetas = new Etiquetas();
            etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            etiquetas.ListaEtiquetas.Add(etiquetaEliminar);

            var diccionarios = new Diccionarios();
            diccionarios.ListaDiccionarios.Add(diccionario1);
            diccionarios.ListaDiccionarios.Add(diccionario2);
            diccionarios.ListaDiccionarios.Add(diccionario3);

            return diccionarios;
        }

        private Diccionarios DiccionariosDelRepositorioConUnDiccionaio()
        {
            ////////////////////////////////////////Diccionario 1
            var diccionario1 = new Modelo.Diccionario
            {
                Id = diccionarioId1,
                Ambiente = AmbienteTest,
                Etiquetas = new Etiquetas()
            };
            ////////////////////////////////////////Etiqueta 1
            var etiquetaAceptar = new Etiqueta
            {
                Id = etiquetaAceptarId,
                Nombre = nombreEtiquetaAceptar,
                Descripcion = strAceptar,
                Traducciones = new Traducciones()
            };
            ////////////////////////////////////////Etiqueta 2
            var etiquetaCancelar = new Etiqueta
            {
                Id = etiquetaCancelarId,
                Nombre = nombreEtiquetaCancelar,
                Descripcion = strCancelar,
                Traducciones = new Traducciones()
            };
            ////////////////////////////////////////Etiqueta 3
            var etiquetaEliminar = new Etiqueta
            {
                Id = etiquetaEliminarId,
                Nombre = nombreEtiquetaEliminar,
                Descripcion = strEliminar,
                Traducciones = new Traducciones()
            };

            ////////////////////////////////////////Traduccion Aceptar
            var traduccionAceptarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strAceptar,
                Value = strAceptar
            };
            ////////////////////////////////////////Traduccion Acept
            var traduccionAceptarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strAcept,
                Value = strAcept
            };
            ////////////////////////////////////////Traduccion Cancelar
            var traduccionCancelarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strCancelar,
                Value = strCancelar
            };
            ////////////////////////////////////////Traduccion Cancel
            var traduccionCancelarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strCancel,
                Value = strCancel
            };
            ////////////////////////////////////////Traduccion Eliminar
            var traduccionEliminarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strEliminar,
                Value = strEliminar
            };
            ////////////////////////////////////////Traduccion Delete
            var traduccionEliminarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strDelete,
                Value = strDelete
            };

            //////////////////////Agregando Traducciones A Etiquetas Aceptar
            etiquetaAceptar.Traducciones.Traducciones1.Add(traduccionAceptarEs);
            etiquetaAceptar.Traducciones.Traducciones1.Add(traduccionAceptarEn);

            //////////////////////Agregando Traducciones A Etiquetas Cancelar
            etiquetaCancelar.Traducciones.Traducciones1.Add(traduccionCancelarEs);
            etiquetaCancelar.Traducciones.Traducciones1.Add(traduccionCancelarEn);

            //////////////////////Agregando Traducciones A Etiquetas Eliminar
            etiquetaEliminar.Traducciones.Traducciones1.Add(traduccionEliminarEs);
            etiquetaEliminar.Traducciones.Traducciones1.Add(traduccionEliminarEn);

            //////////////////////Agregando Etiquetas Al Diccionario 1
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaEliminar);
            
            var traducciones = new Traducciones();
            traducciones.Traducciones1.Add(traduccionAceptarEs);
            traducciones.Traducciones1.Add(traduccionAceptarEn);
            traducciones.Traducciones1.Add(traduccionCancelarEs);
            traducciones.Traducciones1.Add(traduccionCancelarEn);
            traducciones.Traducciones1.Add(traduccionEliminarEs);
            traducciones.Traducciones1.Add(traduccionEliminarEn);

            var etiquetas = new Etiquetas();
            etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            etiquetas.ListaEtiquetas.Add(etiquetaEliminar);

            var diccionarios = new Diccionarios();
            diccionarios.ListaDiccionarios.Add(diccionario1);

            return diccionarios;
        }
        private Diccionarios DiccionariosDelRepositorioConAmbienteModificado()
        {
            ////////////////////////////////////////Diccionario 1
            var diccionario1 = new Modelo.Diccionario
            {
                Id = diccionarioId1,
                Ambiente = AmbienteDesarrollo,
                Etiquetas = new Etiquetas()
            };
            ////////////////////////////////////////Diccionario 2
            var diccionario2 = new Modelo.Diccionario
            {
                Id = diccionarioId2,
                Ambiente = AmbienteDesarrollo,
                Etiquetas = new Etiquetas()
            };
            ////////////////////////////////////////Diccionario 3
            var diccionario3 = new Modelo.Diccionario
            {
                Id = diccionarioId3,
                Ambiente = AmbienteCalidad,
                Etiquetas = new Etiquetas()
            };
            ////////////////////////////////////////Etiqueta 1
            var etiquetaAceptar = new Etiqueta
            {
                Id = etiquetaAceptarId,
                Nombre = nombreEtiquetaAceptar,
                Descripcion = strAceptar,
                Traducciones = new Traducciones()
            };
            ////////////////////////////////////////Etiqueta 2
            var etiquetaCancelar = new Etiqueta
            {
                Id = etiquetaCancelarId,
                Nombre = nombreEtiquetaCancelar,
                Descripcion = strCancelar,
                Traducciones = new Traducciones()
            };
            ////////////////////////////////////////Etiqueta 3
            var etiquetaEliminar = new Etiqueta
            {
                Id = etiquetaEliminarId,
                Nombre = nombreEtiquetaEliminar,
                Descripcion = strEliminar,
                Traducciones = new Traducciones()
            };

            ////////////////////////////////////////Traduccion Aceptar
            var traduccionAceptarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strAceptar,
                Value = strAceptar
            };
            ////////////////////////////////////////Traduccion Acept
            var traduccionAceptarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strAcept,
                Value = strAcept
            };
            ////////////////////////////////////////Traduccion Cancelar
            var traduccionCancelarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strCancelar,
                Value = strCancelar
            };
            ////////////////////////////////////////Traduccion Cancel
            var traduccionCancelarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strCancel,
                Value = strCancel
            };
            ////////////////////////////////////////Traduccion Eliminar
            var traduccionEliminarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strEliminar,
                Value = strEliminar
            };
            ////////////////////////////////////////Traduccion Delete
            var traduccionEliminarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strDelete,
                Value = strDelete
            };

            //////////////////////Agregando Traducciones A Etiquetas Aceptar
            etiquetaAceptar.Traducciones.Traducciones1.Add(traduccionAceptarEs);
            etiquetaAceptar.Traducciones.Traducciones1.Add(traduccionAceptarEn);

            //////////////////////Agregando Traducciones A Etiquetas Cancelar
            etiquetaCancelar.Traducciones.Traducciones1.Add(traduccionCancelarEs);
            etiquetaCancelar.Traducciones.Traducciones1.Add(traduccionCancelarEn);

            //////////////////////Agregando Traducciones A Etiquetas Eliminar
            etiquetaEliminar.Traducciones.Traducciones1.Add(traduccionEliminarEs);
            etiquetaEliminar.Traducciones.Traducciones1.Add(traduccionEliminarEn);

            //////////////////////Agregando Etiquetas Al Diccionario 1
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaEliminar);

            //////////////////////Agregando Etiquetas Al Diccionario 2
            diccionario2.Etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            diccionario2.Etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            diccionario2.Etiquetas.ListaEtiquetas.Add(etiquetaEliminar);

            //////////////////////Agregando Etiquetas Al Diccionario 3
            diccionario3.Etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            diccionario3.Etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            diccionario3.Etiquetas.ListaEtiquetas.Add(etiquetaEliminar);

            var traducciones = new Traducciones();
            traducciones.Traducciones1.Add(traduccionAceptarEs);
            traducciones.Traducciones1.Add(traduccionAceptarEn);
            traducciones.Traducciones1.Add(traduccionCancelarEs);
            traducciones.Traducciones1.Add(traduccionCancelarEn);
            traducciones.Traducciones1.Add(traduccionEliminarEs);
            traducciones.Traducciones1.Add(traduccionEliminarEn);

            var etiquetas = new Etiquetas();
            etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            etiquetas.ListaEtiquetas.Add(etiquetaEliminar);

            var diccionarios = new Diccionarios();
            diccionarios.ListaDiccionarios.Add(diccionario1);
            diccionarios.ListaDiccionarios.Add(diccionario2);
            diccionarios.ListaDiccionarios.Add(diccionario3);

            return diccionarios;
        }

        private Diccionarios DiccionariosDelRepositorioConTresTraducciones()
        {
            ////////////////////////////////////////Diccionario 1
            var diccionario1 = new Modelo.Diccionario
            {
                Id = diccionarioId1,
                Ambiente = AmbienteDesarrollo,
                Etiquetas = new Etiquetas()
            };
            ////////////////////////////////////////Diccionario 2
            var diccionario2 = new Modelo.Diccionario
            {
                Id = diccionarioId2,
                Ambiente = AmbienteDesarrollo,
                Etiquetas = new Etiquetas()
            };
            ////////////////////////////////////////Diccionario 3
            var diccionario3 = new Modelo.Diccionario
            {
                Id = diccionarioId3,
                Ambiente = AmbienteCalidad,
                Etiquetas = new Etiquetas()
            };
            ////////////////////////////////////////Etiqueta 1
            var etiquetaAceptar = new Etiqueta
            {
                Id = etiquetaAceptarId,
                Nombre = nombreEtiquetaAceptar,
                Descripcion = strAceptar,
                Traducciones = new Traducciones()
            };
            ////////////////////////////////////////Etiqueta 2
            var etiquetaCancelar = new Etiqueta
            {
                Id = etiquetaCancelarId,
                Nombre = nombreEtiquetaCancelar,
                Descripcion = strCancelar,
                Traducciones = new Traducciones()
            };
            ////////////////////////////////////////Etiqueta 3
            var etiquetaEliminar = new Etiqueta
            {
                Id = etiquetaEliminarId,
                Nombre = nombreEtiquetaEliminar,
                Descripcion = strEliminar,
                Traducciones = new Traducciones()
            };

            ////////////////////////////////////////Traduccion Aceptar
            var traduccionAceptarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strAceptar,
                Value = strAceptar
            };
            ////////////////////////////////////////Traduccion Acept
            var traduccionAceptarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strAcept,
                Value = strAcept
            };
            ////////////////////////////////////////Traduccion Accepter
            var traduccionAceptarFr = new Traduccion
            {
                Cultura = CulturaFrances,
                Tooltip = strAccepter,
                Value = strAccepter
            };
            ////////////////////////////////////////Traduccion Cancelar
            var traduccionCancelarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strCancelar,
                Value = strCancelar
            };
            ////////////////////////////////////////Traduccion Cancel
            var traduccionCancelarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strCancel,
                Value = strCancel
            };
            ////////////////////////////////////////Traduccion Canceler
            var traduccionCancelarFr = new Traduccion
            {
                Cultura = CulturaFrances,
                Tooltip = strCanceler,
                Value = strCanceler
            };
            ////////////////////////////////////////Traduccion Eliminar
            var traduccionEliminarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strEliminar,
                Value = strEliminar
            };
            ////////////////////////////////////////Traduccion Delete
            var traduccionEliminarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strDelete,
                Value = strDelete
            };
            ////////////////////////////////////////Traduccion Supprimer
            var traduccionEliminarFr = new Traduccion
            {
                Cultura = CulturaFrances,
                Tooltip = strSupprimer,
                Value = strSupprimer
            };


            //////////////////////Agregando Traducciones A Etiquetas Aceptar
            etiquetaAceptar.Traducciones.Traducciones1.Add(traduccionAceptarEs);
            etiquetaAceptar.Traducciones.Traducciones1.Add(traduccionAceptarEn);
            etiquetaAceptar.Traducciones.Traducciones1.Add(traduccionAceptarFr);

            //////////////////////Agregando Traducciones A Etiquetas Cancelar
            etiquetaCancelar.Traducciones.Traducciones1.Add(traduccionCancelarEs);
            etiquetaCancelar.Traducciones.Traducciones1.Add(traduccionCancelarEn);
            etiquetaCancelar.Traducciones.Traducciones1.Add(traduccionCancelarFr);

            //////////////////////Agregando Traducciones A Etiquetas Eliminar
            etiquetaEliminar.Traducciones.Traducciones1.Add(traduccionEliminarEs);
            etiquetaEliminar.Traducciones.Traducciones1.Add(traduccionEliminarEn);
            etiquetaEliminar.Traducciones.Traducciones1.Add(traduccionEliminarFr);

            //////////////////////Agregando Etiquetas Al Diccionario 1
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaEliminar);

            //////////////////////Agregando Etiquetas Al Diccionario 2
            diccionario2.Etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            diccionario2.Etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            diccionario2.Etiquetas.ListaEtiquetas.Add(etiquetaEliminar);

            //////////////////////Agregando Etiquetas Al Diccionario 3
            diccionario3.Etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            diccionario3.Etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            diccionario3.Etiquetas.ListaEtiquetas.Add(etiquetaEliminar);

            var traducciones = new Traducciones();
            traducciones.Traducciones1.Add(traduccionAceptarEs);
            traducciones.Traducciones1.Add(traduccionAceptarEn);
            traducciones.Traducciones1.Add(traduccionAceptarFr);
            traducciones.Traducciones1.Add(traduccionCancelarEs);
            traducciones.Traducciones1.Add(traduccionCancelarEn);
            traducciones.Traducciones1.Add(traduccionCancelarFr);
            traducciones.Traducciones1.Add(traduccionEliminarEs);
            traducciones.Traducciones1.Add(traduccionEliminarEn);
            traducciones.Traducciones1.Add(traduccionEliminarFr);

            var etiquetas = new Etiquetas();
            etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            etiquetas.ListaEtiquetas.Add(etiquetaEliminar);

            var diccionarios = new Diccionarios();
            diccionarios.ListaDiccionarios.Add(diccionario1);
            diccionarios.ListaDiccionarios.Add(diccionario2);
            diccionarios.ListaDiccionarios.Add(diccionario3);

            return diccionarios;
        }

        private Diccionarios DiccionariosDelRepositorioCuatroEtiquetas()

        {
            ////////////////////////////////////////Diccionario 1
            var diccionario1 = new Modelo.Diccionario
            {
                Id = diccionarioId1,
                Ambiente = AmbienteTest,
                Etiquetas = new Etiquetas()
            };
            ////////////////////////////////////////Diccionario 2
            var diccionario2 = new Modelo.Diccionario
            {
                Id = diccionarioId2,
                Ambiente = AmbienteDesarrollo,
                Etiquetas = new Etiquetas()
            };
            ////////////////////////////////////////Diccionario 3
            var diccionario3 = new Modelo.Diccionario
            {
                Id = diccionarioId3,
                Ambiente = AmbienteCalidad,
                Etiquetas = new Etiquetas()
            };
            ////////////////////////////////////////Etiqueta 1
            var etiquetaAceptar = new Etiqueta
            {
                Id = etiquetaAceptarId,
                Nombre = nombreEtiquetaAceptar,
                Descripcion = strAceptar,
                Traducciones = new Traducciones()
            };
            ////////////////////////////////////////Etiqueta 2
            var etiquetaCancelar = new Etiqueta
            {
                Id = etiquetaCancelarId,
                Nombre = nombreEtiquetaCancelar,
                Descripcion = strCancelar,
                Traducciones = new Traducciones()
            };
            ////////////////////////////////////////Etiqueta 3
            var etiquetaEliminar = new Etiqueta
            {
                Id = etiquetaEliminarId,
                Nombre = nombreEtiquetaEliminar,
                Descripcion = strEliminar,
                Traducciones = new Traducciones()
            };
            ////////////////////////////////////////Etiqueta 4
            var etiquetaEditar = new Etiqueta
            {
                Id = etiquetaEditarId,
                Nombre = nombreEtiquetaEditar,
                Descripcion = strEditar,
                Traducciones = new Traducciones()
            };

            ////////////////////////////////////////Traduccion Aceptar
            var traduccionAceptarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strAceptar,
                Value = strAceptar
            };
            ////////////////////////////////////////Traduccion Acept
            var traduccionAceptarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strAcept,
                Value = strAcept
            };
            ////////////////////////////////////////Traduccion Cancelar
            var traduccionCancelarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strCancelar,
                Value = strCancelar
            };
            ////////////////////////////////////////Traduccion Cancel
            var traduccionCancelarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strCancel,
                Value = strCancel
            };
            ////////////////////////////////////////Traduccion Eliminar
            var traduccionEliminarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strEliminar,
                Value = strEliminar
            };
            ////////////////////////////////////////Traduccion Delete
            var traduccionEliminarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strDelete,
                Value = strDelete
            };
            ////////////////////////////////////////Traduccion Editar
            var traduccionEditarEs = new Traduccion
            {
                Cultura = CulturaEspañol,
                Tooltip = strEditar,
                Value = strEditar
            };
            ////////////////////////////////////////Traduccion Edit
            var traduccionEditarEn = new Traduccion
            {
                Cultura = CulturaIngles,
                Tooltip = strEdit,
                Value = strEdit
            };

            //////////////////////Agregando Traducciones A Etiquetas Aceptar
            etiquetaAceptar.Traducciones.Traducciones1.Add(traduccionAceptarEs);
            etiquetaAceptar.Traducciones.Traducciones1.Add(traduccionAceptarEn);

            //////////////////////Agregando Traducciones A Etiquetas Cancelar
            etiquetaCancelar.Traducciones.Traducciones1.Add(traduccionCancelarEs);
            etiquetaCancelar.Traducciones.Traducciones1.Add(traduccionCancelarEn);

            //////////////////////Agregando Traducciones A Etiquetas Eliminar
            etiquetaEliminar.Traducciones.Traducciones1.Add(traduccionEliminarEs);
            etiquetaEliminar.Traducciones.Traducciones1.Add(traduccionEliminarEn);

            //////////////////////Agregando Traducciones A Etiquetas Editar
            etiquetaEditar.Traducciones.Traducciones1.Add(traduccionEditarEs);
            etiquetaEditar.Traducciones.Traducciones1.Add(traduccionEditarEn);

            //////////////////////Agregando Etiquetas Al Diccionario 1
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaEliminar);
            diccionario1.Etiquetas.ListaEtiquetas.Add(etiquetaEditar);

            //////////////////////Agregando Etiquetas Al Diccionario 2
            diccionario2.Etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            diccionario2.Etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            diccionario2.Etiquetas.ListaEtiquetas.Add(etiquetaEliminar);
            diccionario2.Etiquetas.ListaEtiquetas.Add(etiquetaEditar);

            //////////////////////Agregando Etiquetas Al Diccionario 3
            diccionario3.Etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            diccionario3.Etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            diccionario3.Etiquetas.ListaEtiquetas.Add(etiquetaEliminar);
            diccionario3.Etiquetas.ListaEtiquetas.Add(etiquetaEditar);

            var traducciones = new Traducciones();
            traducciones.Traducciones1.Add(traduccionAceptarEs);
            traducciones.Traducciones1.Add(traduccionAceptarEn);
            traducciones.Traducciones1.Add(traduccionCancelarEs);
            traducciones.Traducciones1.Add(traduccionCancelarEn);
            traducciones.Traducciones1.Add(traduccionEliminarEs);
            traducciones.Traducciones1.Add(traduccionEliminarEn);
            traducciones.Traducciones1.Add(traduccionEditarEs);
            traducciones.Traducciones1.Add(traduccionEditarEn);

            var etiquetas = new Etiquetas();
            etiquetas.ListaEtiquetas.Add(etiquetaAceptar);
            etiquetas.ListaEtiquetas.Add(etiquetaCancelar);
            etiquetas.ListaEtiquetas.Add(etiquetaEliminar);
            etiquetas.ListaEtiquetas.Add(etiquetaEditar);

            var diccionarios = new Diccionarios();
            diccionarios.ListaDiccionarios.Add(diccionario1);
            diccionarios.ListaDiccionarios.Add(diccionario2);
            diccionarios.ListaDiccionarios.Add(diccionario3);

            return diccionarios;
        }



        #endregion

        #region Definicion de Diccionarios del Dominio

        private Diccionario UnDiccionarioDominioRespuesta()
        {
            var diccionario = Diccionario.CrearNuevoDiccionario(diccionarioId1, AmbienteTest);

            var etiquetaAceptar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaAceptarId);
            etiquetaAceptar.Nombre = nombreEtiquetaAceptar;
            etiquetaAceptar.Descripcion = strAceptar;
            
            var etiquetaCancelar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaCancelarId);
            etiquetaCancelar.Nombre = nombreEtiquetaCancelar;
            etiquetaCancelar.Descripcion = strCancelar;

            var etiquetaEliminar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaEliminarId);
            etiquetaEliminar.Nombre = nombreEtiquetaEliminar;
            etiquetaEliminar.Descripcion = strEliminar;
            
            var culturaEspañol = Cultura.CrearNuevaCultura(CulturaEspañol);
            var culturaIngles = Cultura.CrearNuevaCultura(CulturaIngles);

            var traduccionAceptarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strAceptar);
            traduccionAceptarEs.ToolTip = strAceptar;
            traduccionAceptarEs.Texto = strAceptar;

            var traduccionAceptarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(culturaIngles,
                strAcept);
            traduccionAceptarEn.ToolTip = strAcept;
            traduccionAceptarEn.Texto = strAcept;

            var traduccionCancelarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strCancelar);
            traduccionCancelarEs.ToolTip = strCancelar;
            traduccionCancelarEs.Texto = strCancelar;

            var traduccionCancelarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaIngles, strCancel);
            traduccionCancelarEn.ToolTip = strCancel;
            traduccionCancelarEn.Texto = strCancel;

            var traduccionEliminarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strEliminar);
            traduccionEliminarEs.ToolTip = strEliminar;
            traduccionEliminarEs.Texto = strEliminar;

            var traduccionEliminarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaIngles, strDelete);
            traduccionEliminarEn.ToolTip = strDelete;
            traduccionEliminarEn.Texto = strDelete;

            etiquetaAceptar.AgregarTraduccion(traduccionAceptarEs);
            etiquetaAceptar.AgregarTraduccion(traduccionAceptarEn);
            etiquetaCancelar.AgregarTraduccion(traduccionCancelarEs);
            etiquetaCancelar.AgregarTraduccion(traduccionCancelarEn);
            etiquetaEliminar.AgregarTraduccion(traduccionEliminarEs);
            etiquetaEliminar.AgregarTraduccion(traduccionEliminarEn);

            diccionario.AgregarEtiqueta(etiquetaAceptar);
            diccionario.AgregarEtiqueta(etiquetaCancelar);
            diccionario.AgregarEtiqueta(etiquetaEliminar);

            return diccionario;
        }
        
        private Diccionario UnDiccionarioDominioRespuestaConAmbienteModificado()
        {
            var diccionario = Diccionario.CrearNuevoDiccionario(diccionarioId1, AmbienteDesarrollo);

            var etiquetaAceptar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaAceptarId);
            etiquetaAceptar.Nombre = nombreEtiquetaAceptar;
            etiquetaAceptar.Descripcion = strAceptar;

            var etiquetaCancelar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaCancelarId);
            etiquetaCancelar.Nombre = nombreEtiquetaCancelar;
            etiquetaCancelar.Descripcion = strCancelar;

            var etiquetaEliminar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaEliminarId);
            etiquetaEliminar.Nombre = nombreEtiquetaEliminar;
            etiquetaEliminar.Descripcion = strEliminar;

            var culturaEspañol = Cultura.CrearNuevaCultura(CulturaEspañol);
            var culturaIngles = Cultura.CrearNuevaCultura(CulturaIngles);

            var traduccionAceptarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strAceptar);
            traduccionAceptarEs.ToolTip = strAceptar;
            traduccionAceptarEs.Texto = strAceptar;

            var traduccionAceptarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(culturaIngles,
                strAcept);
            traduccionAceptarEn.ToolTip = strAcept;
            traduccionAceptarEn.Texto = strAcept;

            var traduccionCancelarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strCancelar);
            traduccionCancelarEs.ToolTip = strCancelar;
            traduccionCancelarEs.Texto = strCancelar;

            var traduccionCancelarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaIngles, strCancel);
            traduccionCancelarEn.ToolTip = strCancel;
            traduccionCancelarEn.Texto = strCancel;

            var traduccionEliminarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strEliminar);
            traduccionEliminarEs.ToolTip = strEliminar;
            traduccionEliminarEs.Texto = strEliminar;

            var traduccionEliminarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaIngles, strDelete);
            traduccionEliminarEn.ToolTip = strDelete;
            traduccionEliminarEn.Texto = strDelete;

            etiquetaAceptar.AgregarTraduccion(traduccionAceptarEs);
            etiquetaAceptar.AgregarTraduccion(traduccionAceptarEn);
            etiquetaCancelar.AgregarTraduccion(traduccionCancelarEs);
            etiquetaCancelar.AgregarTraduccion(traduccionCancelarEn);
            etiquetaEliminar.AgregarTraduccion(traduccionEliminarEs);
            etiquetaEliminar.AgregarTraduccion(traduccionEliminarEn);

            diccionario.AgregarEtiqueta(etiquetaAceptar);
            diccionario.AgregarEtiqueta(etiquetaCancelar);
            diccionario.AgregarEtiqueta(etiquetaEliminar);

            return diccionario;
        }

        private Diccionario UnDiccionarioDominioRespuestaConTresTraducciones()
        {
            var diccionario = Diccionario.CrearNuevoDiccionario(diccionarioId1, AmbienteDesarrollo);

            var etiquetaAceptar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaAceptarId);
            etiquetaAceptar.Nombre = nombreEtiquetaAceptar;
            etiquetaAceptar.Descripcion = strAceptar;

            var etiquetaCancelar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaCancelarId);
            etiquetaCancelar.Nombre = nombreEtiquetaCancelar;
            etiquetaCancelar.Descripcion = strCancelar;

            var etiquetaEliminar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaEliminarId);
            etiquetaEliminar.Nombre = nombreEtiquetaEliminar;
            etiquetaEliminar.Descripcion = strEliminar;

            var culturaEspañol = Cultura.CrearNuevaCultura(CulturaEspañol);
            var culturaIngles = Cultura.CrearNuevaCultura(CulturaIngles);
            var culturaFrances = Cultura.CrearNuevaCultura(CulturaFrances);

            var traduccionAceptarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strAceptar);
            traduccionAceptarEs.ToolTip = strAceptar;
            traduccionAceptarEs.Texto = strAceptar;

            var traduccionAceptarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(culturaIngles,
                strAcept);
            traduccionAceptarEn.ToolTip = strAcept;
            traduccionAceptarEn.Texto = strAcept;

            var traduccionAceptarFr = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(culturaFrances,
                strAccepter);
            traduccionAceptarEn.ToolTip = strAccepter;
            traduccionAceptarEn.Texto = strAccepter;

            var traduccionCancelarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strCancelar);
            traduccionCancelarEs.ToolTip = strCancelar;
            traduccionCancelarEs.Texto = strCancelar;

            var traduccionCancelarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaIngles, strCancel);
            traduccionCancelarEn.ToolTip = strCancel;
            traduccionCancelarEn.Texto = strCancel;

            var traduccionCancelarFr = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaFrances, strCanceler);
            traduccionCancelarEn.ToolTip = strCanceler;
            traduccionCancelarEn.Texto = strCanceler;

            var traduccionEliminarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strEliminar);
            traduccionEliminarEs.ToolTip = strEliminar;
            traduccionEliminarEs.Texto = strEliminar;

            var traduccionEliminarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaIngles, strDelete);
            traduccionEliminarEn.ToolTip = strDelete;
            traduccionEliminarEn.Texto = strDelete;

            var traduccionEliminarFr = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaFrances, strSupprimer);
            traduccionEliminarEn.ToolTip = strSupprimer;
            traduccionEliminarEn.Texto = strSupprimer;

            etiquetaAceptar.AgregarTraduccion(traduccionAceptarEs);
            etiquetaAceptar.AgregarTraduccion(traduccionAceptarEn);
            etiquetaAceptar.AgregarTraduccion(traduccionAceptarFr);
            etiquetaCancelar.AgregarTraduccion(traduccionCancelarEs);
            etiquetaCancelar.AgregarTraduccion(traduccionCancelarEn);
            etiquetaCancelar.AgregarTraduccion(traduccionCancelarFr);
            etiquetaEliminar.AgregarTraduccion(traduccionEliminarEs);
            etiquetaEliminar.AgregarTraduccion(traduccionEliminarEn);
            etiquetaEliminar.AgregarTraduccion(traduccionEliminarFr);

            diccionario.AgregarEtiqueta(etiquetaAceptar);
            diccionario.AgregarEtiqueta(etiquetaCancelar);
            diccionario.AgregarEtiqueta(etiquetaEliminar);

            return diccionario;
        }

        private Diccionario UnDiccionarioDominioRespuestaConCuatroEtiquetas()
        {
            var diccionario = Diccionario.CrearNuevoDiccionario(diccionarioId1, AmbienteTest);

            var culturaEspañol = Cultura.CrearNuevaCultura(CulturaEspañol);
            var culturaIngles = Cultura.CrearNuevaCultura(CulturaIngles);

            var etiquetaAceptar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaAceptarId);
            etiquetaAceptar.Nombre = nombreEtiquetaAceptar;
            etiquetaAceptar.Descripcion = strAceptar;

            var etiquetaCancelar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaCancelarId);
            etiquetaCancelar.Nombre = nombreEtiquetaCancelar;
            etiquetaCancelar.Descripcion = strCancelar;

            var etiquetaEliminar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaEliminarId);
            etiquetaEliminar.Nombre = nombreEtiquetaEliminar;
            etiquetaEliminar.Descripcion = strEliminar;

            var etiquetaEditar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaEditarId);
            etiquetaEditar.Nombre = nombreEtiquetaEditar;
            etiquetaEditar.Descripcion = strEditar;

            var traduccionAceptarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strAceptar);
            traduccionAceptarEs.ToolTip = strAceptar;
            traduccionAceptarEs.Texto = strAceptar;

            var traduccionAceptarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(culturaIngles,
                strAcept);
            traduccionAceptarEn.ToolTip = strAcept;
            traduccionAceptarEn.Texto = strAcept;

            var traduccionCancelarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strCancelar);
            traduccionCancelarEs.ToolTip = strCancelar;
            traduccionCancelarEs.Texto = strCancelar;

            var traduccionCancelarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaIngles, strCancel);
            traduccionCancelarEn.ToolTip = strCancel;
            traduccionCancelarEn.Texto = strCancel;

            var traduccionEliminarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strEliminar);
            traduccionEliminarEs.ToolTip = strEliminar;
            traduccionEliminarEs.Texto = strEliminar;

            var traduccionEliminarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaIngles, strDelete);
            traduccionEliminarEn.ToolTip = strDelete;
            traduccionEliminarEn.Texto = strDelete;

            var traduccionEditarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strEditar);
            traduccionEliminarEs.ToolTip = strEditar;
            traduccionEliminarEs.Texto = strEditar;

            var traduccionEditarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaIngles, strEdit);
            traduccionEliminarEn.ToolTip = strEdit;
            traduccionEliminarEn.Texto = strEdit;

            etiquetaAceptar.AgregarTraduccion(traduccionAceptarEs);
            etiquetaAceptar.AgregarTraduccion(traduccionAceptarEn);
            etiquetaCancelar.AgregarTraduccion(traduccionCancelarEs);
            etiquetaCancelar.AgregarTraduccion(traduccionCancelarEn);
            etiquetaEliminar.AgregarTraduccion(traduccionEliminarEs);
            etiquetaEliminar.AgregarTraduccion(traduccionEliminarEn);
            etiquetaEditar.AgregarTraduccion(traduccionEditarEs);
            etiquetaEditar.AgregarTraduccion(traduccionEditarEn);

            diccionario.AgregarEtiqueta(etiquetaAceptar);
            diccionario.AgregarEtiqueta(etiquetaCancelar);
            diccionario.AgregarEtiqueta(etiquetaEliminar);
            diccionario.AgregarEtiqueta(etiquetaEditar);

            return diccionario;
        }

        private IEnumerable<Diccionario> DiccionarioDominioRespuestaCompleto()
        {
            var culturaEspañol = Cultura.CrearNuevaCultura(CulturaEspañol);
            var culturaIngles = Cultura.CrearNuevaCultura(CulturaIngles);
            
            var etiquetaAceptar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaAceptarId);
            etiquetaAceptar.Nombre = nombreEtiquetaAceptar;
            etiquetaAceptar.Descripcion = strAceptar;

            var etiquetaCancelar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaCancelarId);
            etiquetaCancelar.Nombre = nombreEtiquetaCancelar;
            etiquetaCancelar.Descripcion = strCancelar;

            var etiquetaEliminar = Nucleo.Dominio.Entidades.Etiquetas.Etiqueta.CrearNuevaEtiqueta(etiquetaEliminarId);
            etiquetaEliminar.Nombre = nombreEtiquetaEliminar;
            etiquetaEliminar.Descripcion = strEliminar;

            var traduccionAceptarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strAceptar);
            traduccionAceptarEs.ToolTip = strAceptar;
            traduccionAceptarEs.Texto = strAceptar;

            var traduccionAceptarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(culturaIngles,
                strAcept);
            traduccionAceptarEn.ToolTip = strAcept;
            traduccionAceptarEn.Texto = strAcept;

            var traduccionCancelarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strCancelar);
            traduccionCancelarEs.ToolTip = strCancelar;
            traduccionCancelarEs.Texto = strCancelar;

            var traduccionCancelarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaIngles, strCancel);
            traduccionCancelarEn.ToolTip = strCancel;
            traduccionCancelarEn.Texto = strCancel;

            var traduccionEliminarEs = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaEspañol, strEliminar);
            traduccionEliminarEs.ToolTip = strEliminar;
            traduccionEliminarEs.Texto = strEliminar;

            var traduccionEliminarEn = Nucleo.Dominio.Entidades.Etiquetas.Traduccion.CrearNuevaTraduccion(
                culturaIngles, strDelete);
            traduccionEliminarEn.ToolTip = strDelete;
            traduccionEliminarEn.Texto = strDelete;

            etiquetaAceptar.AgregarTraduccion(traduccionAceptarEs);
            etiquetaAceptar.AgregarTraduccion(traduccionAceptarEn);
            etiquetaCancelar.AgregarTraduccion(traduccionCancelarEs);
            etiquetaCancelar.AgregarTraduccion(traduccionCancelarEn);
            etiquetaEliminar.AgregarTraduccion(traduccionEliminarEs);
            etiquetaEliminar.AgregarTraduccion(traduccionEliminarEn);

            //////////////////////Diccionario 1
            var diccionario1 = Diccionario.CrearNuevoDiccionario(diccionarioId1, AmbienteTest);
            diccionario1.AgregarEtiqueta(etiquetaAceptar);
            diccionario1.AgregarEtiqueta(etiquetaCancelar);
            diccionario1.AgregarEtiqueta(etiquetaEliminar);

            ////////////////////Diccionario 2
            var diccionario2 = Diccionario.CrearNuevoDiccionario(diccionarioId2, AmbienteDesarrollo);
            diccionario2.AgregarEtiqueta(etiquetaAceptar);
            diccionario2.AgregarEtiqueta(etiquetaCancelar);
            diccionario2.AgregarEtiqueta(etiquetaEliminar);

            ////////////////////Diccionario 2
            var diccionario3 = Diccionario.CrearNuevoDiccionario(diccionarioId3, AmbienteCalidad);
            diccionario3.AgregarEtiqueta(etiquetaAceptar);
            diccionario3.AgregarEtiqueta(etiquetaCancelar);
            diccionario3.AgregarEtiqueta(etiquetaEliminar);

            var diccionarios = new List<Diccionario> {diccionario1, diccionario2, diccionario3};

            return new List<Diccionario>(diccionarios);
        }

        private Diccionario DiccionarioDominioRespuestaNull()
        {
            return null;
        }

        #endregion

        #region Obtener Diccionario

        //[Category("Obtener Diccionario")]
        //[Test]
        //public void DeberiaPoderObtenerUnDiccionarioDelXml()
        //{
        //    //Arrange
        //    var persistenciaMock = Substitute.For<IPersistencia>();
        //    persistenciaMock.LeerXml(_directoryStatic, _serializador).Returns(DiccionariosDelRepositorioConTresDiccionaios());
        //    var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, persistenciaMock, _serializador);
        //    var diccionarioEsperado = UnDiccionarioDominioRespuesta();
            
        //    //Act
        //    var diccionarioReal = repositorio.ObtenerUnDiccionario(diccionarioId1);

        //    //Assert
        //    diccionarioEsperado.Id.ShouldBe(diccionarioReal.Id);
        //    diccionarioEsperado.Ambiente.ShouldBe(diccionarioReal.Ambiente); 
        //    //CollectionAssert.AreEquivalent(diccionarioEsperado.Etiquetas, diccionarioReal.Etiquetas);
        //    diccionarioEsperado.Etiquetas.Count().ShouldBe(diccionarioReal.Etiquetas.Count());

        //    var indice = 0;
        //    foreach (var etiquetaReal in diccionarioReal.Etiquetas)
        //    {
        //        etiquetaReal.Id.ShouldBe(diccionarioEsperado.Etiquetas[indice].Id);
        //        etiquetaReal.Nombre.ShouldBe(diccionarioEsperado.Etiquetas[indice].Nombre);
        //        etiquetaReal.Descripcion.ShouldBe(diccionarioEsperado.Etiquetas[indice].Descripcion);
        //        etiquetaReal.Textos.ShouldBe(diccionarioEsperado.Etiquetas[indice].Textos);
        //        indice++;
        //    }
        //}

        [Category("Obtener Diccionario")]
        [Test]
        public void DeberiaPoderDevolverUnaExcepcionDeTipoFileNotFoundExceptionAlbuscarElDiccionarioCuandoelXmlNoExiste()
        {
            //Arrange
            var persistenciaMock = Substitute.For<IPersistencia>();
            persistenciaMock.When(x => x.LeerXml(_directoryStatic, _serializador))
                .Do(obj => { throw new FileNotFoundException(); });
            var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, persistenciaMock, _serializador);
            var idDiccionario = new Guid("25849869-2551-4b80-9dd7-2aaafccf8bfa");

            //Act y Assert
            Shouldly.Should.Throw<FileNotFoundException>(() => repositorio.ObtenerUnDiccionario(idDiccionario));
        }

        [Category("Obtener Diccionario")]
        [Test]
        public void DeberiaPoderObtenerTodosLosDiccionariosDelXml()
        {
            //Arrange
            var persistenciaMock = Substitute.For<IPersistencia>();
            persistenciaMock.LeerXml(_directoryStatic, _serializador).Returns(DiccionariosDelRepositorioConTresDiccionaios());
            var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, persistenciaMock, _serializador);
            var diccionarioEsperado = DiccionarioDominioRespuestaCompleto();

            //Act
            var diccionarioReal = repositorio.ObtenerDiccionarios();

            //Assert
            CollectionAssert.AreEquivalent(diccionarioEsperado, diccionarioReal);
        }

        [Category("Obtener Diccionario")]
        [Test]
        public void DeberiaPoderRetornarNullAlBuscarUnDiccionarioQueNoExisteEnElXml()
        {
            //Arrange
            var persistenciaMock = Substitute.For<IPersistencia>();
            persistenciaMock.LeerXml(_directoryStatic, _serializador).Returns(DiccionariosDelRepositorioConTresDiccionaios());
            var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, persistenciaMock, _serializador);
            var diccionarioEsperado = DiccionarioDominioRespuestaNull();
            var idDiccionario = new Guid("25849869-2551-4b80-9dd7-2aaafccf8bfa");

            //Act
            var diccionarioReal = repositorio.ObtenerUnDiccionario(idDiccionario);

            //Assert
            diccionarioReal.ShouldBe(diccionarioEsperado);
        }

        [Category("Obtener Diccionario")]
        [Test]
        public void DeberiaPoderRetornarUnaExcepcionDeTipoFormatExceptionCuandoElIdDelDiccionarioEsInvalido()
        {
            //Arrange
            var persistenciaMock = Substitute.For<IPersistencia>();
            persistenciaMock.When(x => x.LeerXml(_directoryStatic, _serializador))
                .Do(obj => { throw new FormatException(); });
            var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, persistenciaMock, _serializador);
            const string idDiccionario = "2582986";

            //Act y Assert
            Shouldly.Should.Throw<FormatException>(() => repositorio.ObtenerUnDiccionario(new Guid(idDiccionario)));
        }

        [Category("Obtener Diccionario")]
        [Test]
        public void DeberiaPoderRetornarUnaListaDeDiccionariosAlBuscarLaListaEnElXml()
        {
            //Arrange
            var persistenciaMock = Substitute.For<IPersistencia>();
            persistenciaMock.LeerXml(_directoryStatic, _serializador).Returns(DiccionariosDelRepositorioConTresDiccionaios());
            var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, persistenciaMock, _serializador);
            var diccionarioEsperado = DiccionarioDominioRespuestaCompleto();

            //Act
            var diccionarioReal = repositorio.ObtenerDiccionarios();

            //Assert
            diccionarioReal.ShouldEqual(diccionarioEsperado);
            //CollectionAssert.AreEquivalent(diccionarioEsperado, diccionarioReal);
        }

        [Category("Obtener Diccionario")]
        [Test]
        public void DeberiaPoderDevolverUnaExcepcionDeTipoInvalidOperationExceptionCuandoSeBuscaUnDiccionarioEnUnXmlVacio()
        {
            //Arrange
            var persistenciaMock = Substitute.For<IPersistencia>();
            persistenciaMock.When(x => x.LeerXml(_directoryStatic, _serializador))
                .Do(obj => { throw new InvalidOperationException(); });
            var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, persistenciaMock, _serializador);

            //Act y Assert
            Shouldly.Should.Throw<InvalidOperationException>(() => repositorio.ObtenerUnDiccionario(diccionarioId1));
        }

        #endregion

        #region Modificar Diccionario

        //[Category("Modificar Diccionario")]
        //[Test]
        //public void DeberiaPoderActualizarElAmbienteDelDiccionario()
        //{
        //    //Arrange
        //    var persistenciaMock = Substitute.For<IPersistencia>();
        //    persistenciaMock.LeerXml(_directoryStatic, _serializador).Returns(DiccionariosDelRepositorioConTresDiccionaios());
        //    persistenciaMock.EscribirXml(_directoryStatic, _serializador, DiccionariosDelRepositorioConTresDiccionaios())
        //        .Returns(DiccionariosDelRepositorioConAmbienteModificado());
        //    var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, persistenciaMock, _serializador);
        //    var diccionarioEsperado = UnDiccionarioDominioRespuestaConAmbienteModificado();
            
        //    //Act
        //    var diccionarioReal = repositorio.SalvarUnDiccionario(diccionarioEsperado);

        //    //Assert
        //    diccionarioEsperado.Ambiente.ShouldEqual(diccionarioReal.Ambiente);
        //}

        //[Category("Modificar Diccionario")]
        //[Test]
        //public void DeberiaPoderActualizarElDiccionarioConEtiquetasNuevas()
        //{
        //    //Arrange
        //    var persistenciaMock = Substitute.For<IPersistencia>();
        //    persistenciaMock.LeerXml(_directoryStatic, _serializador).Returns(DiccionariosDelRepositorioConTresDiccionaios());
        //    persistenciaMock.EscribirXml(_directoryStatic, _serializador, DiccionariosDelRepositorioConTresDiccionaios())
        //        .Returns(DiccionariosDelRepositorioCuatroEtiquetas());
        //    var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, persistenciaMock, _serializador);
        //    var diccionarioEsperado = UnDiccionarioDominioRespuestaConCuatroEtiquetas();

        //    //Act
        //    var diccionarioReal = repositorio.SalvarUnDiccionario(diccionarioEsperado);

        //    //Assert
        //    diccionarioReal.Etiquetas.ShouldEqual(diccionarioEsperado.Etiquetas);
        //    CollectionAssert.AreEquivalent(diccionarioEsperado.Etiquetas, diccionarioReal.Etiquetas);

        //}

        //[Category("Modificar Diccionario")]
        //[Test]
        //public void DeberiaPoderActualizarElDiccionarioConTraduccionesNuevas()
        //{
        //    //Arrange
        //    var persistenciaMock = Substitute.For<IPersistencia>();
        //    persistenciaMock.LeerXml(_directoryStatic, _serializador).Returns(DiccionariosDelRepositorioConTresDiccionaios());
        //    persistenciaMock.EscribirXml(_directoryStatic, _serializador, DiccionariosDelRepositorioConTresDiccionaios())
        //        .Returns(DiccionariosDelRepositorioConTresTraducciones());
        //    var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, persistenciaMock, _serializador);
        //    var diccionarioEsperado = UnDiccionarioDominioRespuestaConTresTraducciones();

        //    //Act
        //    var diccionarioReal = repositorio.SalvarUnDiccionario(diccionarioEsperado);

        //    //Assert
        //    diccionarioEsperado.Id.ShouldBe(diccionarioReal.Id);
        //    diccionarioEsperado.Ambiente.ShouldBe(diccionarioReal.Ambiente);
        //    //CollectionAssert.AreEquivalent(diccionarioEsperado.Etiquetas, diccionarioReal.Etiquetas);
        //    diccionarioEsperado.Etiquetas.Count().ShouldBe(diccionarioReal.Etiquetas.Count());

        //    var indice = 0;
        //    foreach (var etiquetaReal in diccionarioReal.Etiquetas)
        //    {
        //        etiquetaReal.Id.ShouldBe(diccionarioEsperado.Etiquetas[indice].Id);
        //        etiquetaReal.Nombre.ShouldBe(diccionarioEsperado.Etiquetas[indice].Nombre);
        //        etiquetaReal.Descripcion.ShouldBe(diccionarioEsperado.Etiquetas[indice].Descripcion);
        //        etiquetaReal.Textos.ShouldBe(diccionarioEsperado.Etiquetas[indice].Textos);
        //        indice++;
        //    }
        //}
        
        #endregion

        #region Eliminar Diccionario

        //[Category("Eliminar Diccionario")]
        //[Test]
        //public void DeberiaPoderEliminarUnDiccionarioCuandoSeLePasaElId()
        //{
        //    //Arrange
        //    var persistenciaMock = Substitute.For<IPersistencia>();
        //    persistenciaMock.LeerXml(_directoryStatic, _serializador).Returns(DiccionariosDelRepositorioConTresDiccionaios());
        //    persistenciaMock.EscribirXml(_directoryStatic, _serializador, DiccionariosDelRepositorioConTresDiccionaios())
        //        .Returns(DiccionariosDelRepositorioConUnDiccionaio());
        //    var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, persistenciaMock, _serializador);
        //    var diccionarioEsperado = UnDiccionarioDominioRespuesta();

        //    //Act
        //    var diccionarioReal = repositorio.SalvarUnDiccionario(diccionarioEsperado);

        //    //Assert
        //    //TODO verificar que esta prueba de
        //    diccionarioEsperado.ShouldEqual(diccionarioReal.Ambiente);
        //}

        [Category("Eliminar Diccionario")]
        [Test]
        public void DeberiaPoderEliminarUnaListaDeDiccionariosCuandoSeLesPasaElId()
        {
            //// Se tiene que buscar un ID de diccionario en el Xml del repositorio.

            //var lista = new List<Guid>();

            ////Arrange
            //var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, _persistencia, _serializador);

            ////Act

            //var diccionarios = repositorio.ObtenerDiccionarios();

            //var diccionario1 = diccionarios.ToList().First();
            //var diccionario2 = diccionarios.ToList().Last();
            //lista.Add(diccionario1.Id);
            //lista.Add(diccionario2.Id);

            //var diccionario = repositorio.EliminarDiccionarios(lista);

            ////Assert
            //diccionario.Find(e => e.Id == diccionario1.Id && e.Id == diccionario2.Id).ShouldBeNull();
        }

        [Category("Eliminar Diccionario")]
        [Test]
        public void DeberiaPoderRetornarUnErrorDeTipoNullReferenceExceptionAlEliminarUnDiccionarioNoExistenteEnElXml()
        {
            ////Arrange
            //var repositorio = new DiccionarioRepositorioXmlImpl(_directory, _persistencia, _serializador);

            ////Act y Assert
            //Assert.Throws<NullReferenceException>(
            //    () => repositorio.EliminarUnDiccionario(new Guid("835944df-3bc0-46b3-8508-cb1aed001bc4")));
        }

        #endregion
    }
}
