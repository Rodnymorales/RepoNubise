using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using Telerik.JustMock.AutoMock;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada;

namespace Interfaz.WebApi.PruebasUnitariasJustMock
{
    #region Atributos adicionales de pruebas
    //
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //
    #endregion

    [TestClass]
    public class RecursoControladorTest
    {
        public TestContext ContextoDeLaPrueba { get; set; }

        public app.IAplicacionGenerarRecursos AppGenerarRecurso { get; set; }

        [TestMethod]
        public void TestMethod()
        {
            //
            // TODO: Add test logic here
            //
        }
    }
}
