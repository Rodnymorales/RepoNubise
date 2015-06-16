using System.IO;
using System.Linq;
using Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Implementacion;
using Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Persistencia;
using NUnit.Framework;
using Should;
using System;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using System.Xml.Serialization;

namespace Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.PruebasDeIntegracion.Repositorio
{
    [TestFixture]
    public class DiccionarioRepositorioTest
    {
        private const string AmbienteTestPrueba = "Prueba";
        private const string AmbienteTestDesarrollo = "Desarrollo";

        private readonly XmlSerializer _serializador = new XmlSerializer(typeof(Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Modelo.Diccionarios));
        private readonly IPersistencia _persistencia = new PersistenciaArchivo();
        
        #region "DirectoriosXML"

        private readonly string _directory = Environment.CurrentDirectory.Replace("\\bin\\Debug", "\\DatosPrueba\\") +
                                             "diccionario_ok.xml";

        private readonly string _directoryStatic =
            Environment.CurrentDirectory.Replace("\\bin\\Debug", "\\DatosPrueba\\") + "diccionario_ok_Existe.xml";

        private readonly string _directoryVacio =
            Environment.CurrentDirectory.Replace("\\bin\\Debug", "\\DatosPrueba\\") + "diccionario_ok_Vacio.xml";

        #endregion
        
        private Diccionario DiccionarioDominio { get; set; }

        private Diccionario DiccionarioDominio2 { get; set; }

        #region Crear Diccionario

        [Category("Crear Diccionario")]
        [Test]
        public void DeberiaPoderCrearUnNuevoDiccionarioEnUnXmlExistente()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directory,_persistencia,_serializador);
            var etqDom = Etiqueta.CrearNuevaEtiqueta("app.testNuevo1");
            var traduccionDom2 = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "accept", "accept");
            etqDom.AgregarTraduccion(traduccionDom2);
            var etqDom2 = Etiqueta.CrearNuevaEtiqueta("app.testNuevo2");
            var traduccionDom22 = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "accept", "accept");
            etqDom2.AgregarTraduccion(traduccionDom22);

            //Act
            DiccionarioDominio = Diccionario.CrearNuevoDiccionario(Guid.NewGuid(), "Yomaira");
            DiccionarioDominio.AgregarEtiqueta(etqDom);
            DiccionarioDominio.AgregarEtiqueta(etqDom2);

            //Assert
            repositorio.SalvarUnDiccionario(DiccionarioDominio).ShouldEqual(DiccionarioDominio);
        }

        [Category("Crear Diccionario")]
        [Test]
        public void DeberiaPoderProbarQueAlCrearListaDeDiccionariosEnElXmlRetorneElTipoDeObjetoCorrecto()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directory, _persistencia, _serializador);

            var listaDiccionarios = new List<Diccionario>();

            var etqDom = Etiqueta.CrearNuevaEtiqueta("app.test");

            var traduccionDom2 = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "accept", "accept");

            etqDom.AgregarTraduccion(traduccionDom2);

            var etqDom2 = Etiqueta.CrearNuevaEtiqueta("app.test2");

            var traduccionDom22 = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "accept", "accept");

            etqDom2.AgregarTraduccion(traduccionDom22);

            DiccionarioDominio = Diccionario.CrearNuevoDiccionario(Guid.NewGuid(), AmbienteTestPrueba);

            DiccionarioDominio.AgregarEtiqueta(etqDom);
            DiccionarioDominio.AgregarEtiqueta(etqDom2);

            DiccionarioDominio2 = Diccionario.CrearNuevoDiccionario(Guid.NewGuid(), AmbienteTestDesarrollo);

            DiccionarioDominio2.AgregarEtiqueta(etqDom);
            DiccionarioDominio2.AgregarEtiqueta(etqDom2);

            listaDiccionarios.Add(DiccionarioDominio);
            listaDiccionarios.Add(DiccionarioDominio2);

            //Act y Assert
            var diccionario = repositorio.SalvarDiccionarios(listaDiccionarios);

            //Assert
            diccionario.ShouldBeType<List<Diccionario>>();
        }

        [Category("Crear Diccionario")]
        [Test]
        public void DeberiaPoderRetornarUnaExceptionTipoExceptionCuandoSeIntentaCrearUnDiccionarioConUnAmbienteExistente()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directory, _persistencia, _serializador);
            DiccionarioDominio = Diccionario.CrearNuevoDiccionario(Guid.NewGuid(), "Ambiente_Uno");

            //Act
          
            Assert.Throws<Exception>(() => repositorio.SalvarUnDiccionario(DiccionarioDominio));
        }

        [Category("Crear Diccionario")]
        [Test]
        public void DeberiaPoderCrearUnaListadeDiccionariosEnElXml()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directory,_persistencia,_serializador);
            var listaDiccionarios = new List<Diccionario>
            {
                Diccionario.CrearNuevoDiccionario(Guid.NewGuid(), "Ambiente_Uno"),
                Diccionario.CrearNuevoDiccionario(Guid.NewGuid(), "Ambiente_Dos")
            };

            //Act
            var diccionario = repositorio.SalvarDiccionarios(listaDiccionarios);

            //Assert
            diccionario.ShouldEqual(listaDiccionarios);
        }

        [Category("Crear Diccionario")]
        [Test]
        public void DeberiaPoderProbarQueAlCrerUnaListaDeDiccionariosRetorneUnaListaDeDiccionarios()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directory,_persistencia,_serializador);
            var listaDiccionarios = new List<Diccionario>
            {
                Diccionario.CrearNuevoDiccionario(Guid.NewGuid(), "Ambiente_Uno"),
                Diccionario.CrearNuevoDiccionario(Guid.NewGuid(), "Ambiente_Dos")
            };

            //Act
            var diccionario = repositorio.SalvarDiccionarios(listaDiccionarios);

            //Assert
            diccionario.ShouldBeType<List<Diccionario>>();
        }

        #endregion

        #region Obtener Diccionario

        [Category("Obtener Diccionario")]
        [Test]
        public void DeberiaPoderDevolverUnaExcepcionDeTipoFileNotFoundExceptionAlbuscarElDiccionarioCuandoelXmlNoExiste()
        {
            //Para que ejecute la excepción modificar el ID del diccionario por uno que no exista (Verificar Diccionario)

            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl("Repositorio", _persistencia, _serializador);
            const string idDiccionario = "25829869-2551-4b80-9dd7-2aaafccf8bfa";

            //Act y Assert
            Assert.Throws<FileNotFoundException>(() => repositorio.ObtenerUnDiccionario(new Guid(idDiccionario)));
        }

        [Category("Obtener Diccionario")]
        [Test]
        public void DeberiaPoderObtenerUnDiccionarioDelXml()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, _persistencia, _serializador);

            var diccionarios = repositorio.ObtenerDiccionarios();

            var diccionarioObtener = diccionarios.ToList().FirstOrDefault();
            //Act
            var diccionario = repositorio.ObtenerUnDiccionario(Guid.Parse(diccionarioObtener.Id.ToString()));

            //Assert
            diccionarioObtener.Id.ShouldEqual(diccionario.Id);
        }

        [Category("Obtener Diccionario")]
        [Test]
        public void DeberiaPoderObtenerUnDiccionarioDelXmlYRetornarElTipoCorrecto()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, _persistencia, _serializador);
     
            //Act
            var diccionarios = repositorio.ObtenerDiccionarios();
            var diccionarioBuscar = diccionarios.ToList().FirstOrDefault();
            var diccionario = repositorio.ObtenerUnDiccionario(new Guid(diccionarioBuscar.Id.ToString()));

            //Assert
            diccionario.ShouldBeType<Diccionario>();
        }
        
        [Category("Obtener Diccionario")]
        [Test]

        public void DeberiaPoderObtenerTodosLosDiccionariosDelXml()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directory,_persistencia,_serializador);

            //Act
            var diccionario = repositorio.ObtenerDiccionarios();

            //Assert
            diccionario.ShouldBeType<List<Diccionario>>();
        }

        [Category("Obtener Diccionario")]
        [Test]
        public void DeberiaPoderRetornarNullAlBuscarUnDiccionarioQueNoExisteEnElXml()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, _persistencia, _serializador);
            const string idDiccionario = "b6110594-cfc0-4cbb-a283-de5d52d753e1";
            
            //Act
            var diccionario = repositorio.ObtenerUnDiccionario(Guid.Parse(idDiccionario));

            //Assert
            diccionario.ShouldBeNull();
        }
        
        [Category("Obtener Diccionario")]
        [Test]
        public void DeberiaPoderRetornarUnaExcepcionDeTipoFormatExceptionCuandoElIdDelDiccionarioEsInvalido()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directory,_persistencia,_serializador);
            const string idDiccionario = "2582986";

            //Act y Assert
            Assert.Throws<FormatException>(() => repositorio.ObtenerUnDiccionario(new Guid(idDiccionario)));
        }

        [Category("Obtener Diccionario")]
        [Test]
        public void DeberiaPoderRetornarUnaListaDeDiccionariosAlBuscarLaListaEnElXml()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directory,_persistencia,_serializador);

            //Act
            var diccionarios = repositorio.ObtenerDiccionarios();

            //Assert
            diccionarios.ShouldBeType<List<Diccionario>>();
        }

        [Category("Obtener Diccionario")]
        [Test]
        public void
            DeberiaPoderDevolverUnaExcepcionDeTipoInvalidOperationExceptionCuandoSeBuscaUnDiccionarioEnUnXmlVacio()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directoryVacio,_persistencia,_serializador);
            const string idDiccionario = "7a0c67d5-a4e3-4171-8277-6bfd30b4ead7";

            //Act y Assert

            Assert.Throws<InvalidOperationException>(
                () => repositorio.ObtenerUnDiccionario(new Guid(idDiccionario)));
        }

        #endregion

        #region Modificar Diccionario

        [Category("Modificar Diccionario")]
        [Test]
        public void DeberiaPoderActualizarElAmbienteDelDiccionario()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directory,_persistencia,_serializador);

            var etqDom = Etiqueta.CrearNuevaEtiqueta("app.test");
            var traduccionDom2 = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-ES"), "aceptar",
                "aceptar");
            etqDom.AgregarTraduccion(traduccionDom2);

            var etqDom2 = Etiqueta.CrearNuevaEtiqueta("app.test2");
            var traduccionDom22 = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("fr-FR"), "accepter",
                "accepter");
            etqDom2.AgregarTraduccion(traduccionDom22);

            DiccionarioDominio = Diccionario.CrearNuevoDiccionario(Guid.Parse("7a0c67d5-a4e3-4171-8277-6bfd30b4ead7"),
                "NUEVO AMBIENTE");

            DiccionarioDominio.AgregarEtiqueta(etqDom);
            DiccionarioDominio.AgregarEtiqueta(etqDom2);

            //Act
            var diccionario = repositorio.SalvarUnDiccionario(DiccionarioDominio);

            //Assert
            diccionario.Ambiente.ShouldEqual(DiccionarioDominio.Ambiente);
        }
        
        [Category("Modificar Diccionario")]
        [Test]
        public void DeberiaPoderModificarElDiccionarioConEtiquetasNuevas()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, _persistencia, _serializador);

            var a = DateTime.Now.AddSeconds(1);
            var etqDom = Etiqueta.CrearNuevaEtiqueta("app.testNuevo1_NUEVA" + a);
            var traduccionDom = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "accept", "accept");
            etqDom.AgregarTraduccion(traduccionDom);

            var etqDom2 = Etiqueta.CrearNuevaEtiqueta("app.testNuevo2_NUEVA" + a);
            var traduccionDom2 = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "accept", "accept");
            etqDom2.AgregarTraduccion(traduccionDom2);


            var diccionarios = repositorio.ObtenerDiccionarios();

            var diccionarioModificar = diccionarios.ToList().FirstOrDefault();



            DiccionarioDominio = Diccionario.CrearNuevoDiccionario(Guid.Parse(diccionarioModificar.Id.ToString()), "Prueba");

            DiccionarioDominio.AgregarEtiqueta(etqDom);
            DiccionarioDominio.AgregarEtiqueta(etqDom2);

            //Act
            var diccionario = repositorio.SalvarUnDiccionario(DiccionarioDominio);
            //Assert
            diccionario.ShouldEqual(DiccionarioDominio);
        }

        [Category("Modificar Diccionario")]
        [Test]
        public void DeberiaPoderModificarElDiccionarioConTraduccionesNuevas()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, _persistencia, _serializador);
            var a = DateTime.Now.AddSeconds(1);

            var etqDom = Etiqueta.CrearNuevaEtiqueta("app.testNuevo1_" + a);

            var traduccionDom2 = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-ES"), "aceptar",
                "aceptar");

            etqDom.AgregarTraduccion(traduccionDom2);

            var etqDom2 = Etiqueta.CrearNuevaEtiqueta("app.testNuevo2_" + a);

            var traduccionDom22 = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("fr-FR"), "accepter",
                "accepter");

            etqDom2.AgregarTraduccion(traduccionDom22);

            var diccionarios = repositorio.ObtenerDiccionarios();

            var diccionarioSalvar= diccionarios.ToList().FirstOrDefault();

            DiccionarioDominio = Diccionario.CrearNuevoDiccionario(Guid.Parse(diccionarioSalvar.Id.ToString()), "Prueba");

            DiccionarioDominio.AgregarEtiqueta(etqDom);
            DiccionarioDominio.AgregarEtiqueta(etqDom2);

            //Act
            var diccionario = repositorio.SalvarUnDiccionario(DiccionarioDominio);
            //Assert
            diccionario.ShouldEqual(DiccionarioDominio);
        }

        #endregion

        #region Eliminar Diccionario

        [Category("Eliminar Diccionario")]
        [Test]
        public void DeberiaPoderEliminarUnDiccionarioCuandoSeLePasaElId()
        {
            // Se tiene que buscar un ID de diccionario en el Xml del repositorio.

            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, _persistencia, _serializador);
        
            //Act

            var diccionarios = repositorio.ObtenerDiccionarios();
            
            var diccionarioElminar = diccionarios.ToList().FirstOrDefault();

            var diccionario = repositorio.EliminarUnDiccionario(new Guid(diccionarioElminar.Id.ToString()));

            //Assert
            diccionario.Find(e => e.Id == new Guid(diccionarioElminar.Id.ToString())).ShouldBeNull();
        }

        [Category("Eliminar Diccionario")]
        [Test]
        public void DeberiaPoderEliminarUnaListaDeDiccionariosCuandoSeLesPasaElId()
        {
            // Se tiene que buscar un ID de diccionario en el Xml del repositorio.

            var lista = new List<Guid>();

            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directoryStatic, _persistencia, _serializador);

            //Act

            var diccionarios = repositorio.ObtenerDiccionarios();

            var diccionario1 = diccionarios.ToList().First();
            var diccionario2 = diccionarios.ToList().Last();
            lista.Add(diccionario1.Id);
            lista.Add(diccionario2.Id);

            var diccionario = repositorio.EliminarDiccionarios(lista);

            //Assert
            diccionario.Find(e => e.Id == diccionario1.Id && e.Id == diccionario2.Id).ShouldBeNull();
        }

        [Category("Eliminar Diccionario")]
        [Test]
        public void DeberiaPoderRetornarUnErrorDeTipoNullReferenceExceptionAlEliminarUnDiccionarioNoExistenteEnElXml()
        {
            //Arrange
            var repositorio = new DiccionarioRepositorioXmlImpl(_directory,_persistencia,_serializador);

            //Act y Assert
            Assert.Throws<NullReferenceException>(
                () => repositorio.EliminarUnDiccionario(new Guid("835944df-3bc0-46b3-8508-cb1aed001bc4")));
        }

        #endregion
    }
}

