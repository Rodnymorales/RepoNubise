using System;
using System.Linq;
using NUnit.Framework;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using Should;
using System.Collections.Generic;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.PruebasUnitarias.Entidades
{
	[TestFixture]
	public class DiccionarioTest
	{
        private const string ambienteTestPrueba = "Prueba";
		private Diccionario diccionarioPrueba;
		private readonly Dictionary<string, Etiqueta> listaEtiquetas;
        private readonly Etiqueta etiqueta1;
        private readonly Etiqueta etiqueta2;

		public DiccionarioTest ()
		{

			this.listaEtiquetas = new Dictionary<string, Etiqueta> ();
			this.etiqueta1 = Etiqueta.CrearNuevaEtiqueta ("1 test de mi aplicación");
			this.etiqueta2 = Etiqueta.CrearNuevaEtiqueta ("2 test de mi aplicación");
			this.listaEtiquetas.Add (this.etiqueta1.Nombre, this.etiqueta1);
			this.listaEtiquetas.Add (this.etiqueta2.Nombre, this.etiqueta2);

		}

		[SetUp]
		public void SetUp ()
		{
            this.diccionarioPrueba = Diccionario.CrearNuevoDiccionario(ambienteTestPrueba);
		}

		#region creacion

		[Test]
		public void PruebaCrearDiciconarioNuevo ()
		{
			//Arrange
			//Act
			//Assert
			this.diccionarioPrueba.ShouldBeType (typeof(Diccionario));

		}

		[Test]
		public void PruebaObtenerGuidDiccionario ()
		{
			//Arrange

			//Act
			//Assert
			this.diccionarioPrueba.Id.ShouldBeType (typeof(Guid));

		}

		#endregion

		#region agregar etiquetas

		[Test]
		public void PruebaAgregarUnaEtiquetaNoExistenteAlDiccionarioVacio ()
		{
			//Arrange

			//Act
			this.diccionarioPrueba.AgregarEtiqueta (this.etiqueta1);
			//Assert
			this.diccionarioPrueba.Etiquetas.Count ().ShouldEqual (1);
			this.diccionarioPrueba.Etiquetas.ShouldContain (this.etiqueta1);
		}

		[Test]
		public void PruebaAgregarUnaEtiquetaNoExistenteAlDiccionarioConValores ()
		{
			//Arrange

			//Act
			this.diccionarioPrueba.AgregarEtiqueta (this.etiqueta1);
			this.diccionarioPrueba.AgregarEtiqueta (this.etiqueta2);
			//Assert
			//this.diccionarioPrueba.Etiquetas.ShouldBeType(typeof(IReadOnlyCollection<Etiqueta>));
			this.diccionarioPrueba.Etiquetas.Count ().ShouldBeGreaterThan (0);
			this.diccionarioPrueba.Etiquetas.ShouldContain (this.etiqueta1);
			this.diccionarioPrueba.Etiquetas.ShouldContain (this.etiqueta2);

		}

		[Test]
		public void PruebaAgregarUnaEtiquetaExistenteAlDiccionarioConValores ()
		{
			//Arrange

			//Act
			this.diccionarioPrueba.AgregarEtiqueta (this.etiqueta1);

			//Assert
			Assert.Throws<ArgumentException> (delegate {
				this.diccionarioPrueba.AgregarEtiqueta (this.etiqueta1);
			});

		}

		[Test]
		public void PruebaAgregarUnaEtiquetaNullAlDiccionarioVacio ()
		{
			//Arrange
			//Act
			//Assert
			Assert.Throws<ArgumentNullException> (delegate {
				this.diccionarioPrueba.AgregarEtiqueta (null);
			});

		}

		[Test]
		public void PruebaAgregarUnaEtiquetaNullAlDiccionarioConValores ()
		{
			//Arrange

			//Act
			this.diccionarioPrueba.AgregarEtiqueta (etiqueta1);

			//Assert
			Assert.Throws<ArgumentNullException> (delegate {
				this.diccionarioPrueba.AgregarEtiqueta (null);
			});

		}

		[Test]
		public void PruebaAgregarUnaEtiquetaNullAlDiccionario ()
		{
			//Arrange
			Etiqueta prueba = null;
			//Act
			//Assert
			Assert.Throws<ArgumentNullException> (delegate {
				this.diccionarioPrueba.AgregarEtiqueta (prueba);
			});

		}

		#endregion

		#region eliminar etiquetas

		[Test]
		public void PruebaEliminarDiccionarioCompleto ()
		{
			//Arrange

			//Act
			this.diccionarioPrueba.EliminarTodoElDiccionario ();
			//Assert
			this.diccionarioPrueba.Etiquetas.Count ().ShouldEqual (0);
		}

		[Test]
		public void PruebaEliminarUnaEtiquetaExistenteAlDiccionarioConValores ()
		{
			//Arrange
			this.diccionarioPrueba.AgregarEtiqueta (this.etiqueta1);
			//Act
			this.diccionarioPrueba.EliminarEtiqueta (this.etiqueta1);
			//Assert
			this.diccionarioPrueba.Etiquetas.ShouldNotContain (this.etiqueta1);
		}

		[Test]
		public void PruebaEliminarUnaEtiquetaNoExistenteAlDiccionarioConValores ()
		{
			//Arrange
			this.diccionarioPrueba.AgregarEtiqueta (this.etiqueta2);
			//Act		
			this.diccionarioPrueba.EliminarEtiqueta (this.etiqueta1);
			//Assert
			this.diccionarioPrueba.Etiquetas.Count ().ShouldEqual (1);
		}


		[Test]
		public void PruebaEliminarUnaEtiquetaNullAlDiccionarioConValores ()
		{
			//Arrange

			//Act
			this.diccionarioPrueba.AgregarEtiqueta (etiqueta1);

			//Assert
			Assert.Throws<ArgumentNullException> (delegate {
				this.diccionarioPrueba.EliminarEtiqueta (null);
			});

		}

		[Test]
		public void PruebaEliminarUnaEtiquetaAlDiccionarioVacio ()
		{
			//Arrange
            Diccionario prueba = Diccionario.CrearNuevoDiccionario(ambienteTestPrueba);
			//Act
			prueba.EliminarEtiqueta (this.etiqueta1);
			//Assert
			prueba.Etiquetas.Count ().ShouldEqual (0);
		}

		[Test]
		public void PruebaEliminarUnaEtiquetaNullAlDiccionarioVacio ()
		{
			//Arrange

			//Act
            Diccionario prueba = Diccionario.CrearNuevoDiccionario(ambienteTestPrueba);
			//Assert
			Assert.Throws<ArgumentNullException> (delegate {
				prueba.EliminarEtiqueta (null);
			});

		}

		#endregion

		#region Igualdad

		[Test]
		public void PruebaIgualdadDeDiccionariosTrueCuandoEsElMismoDiccionario ()
		{
			//Arrange
			//Act
			//Assert
			this.diccionarioPrueba.Equals (this.diccionarioPrueba).ShouldBeTrue ();
		}

		[Test]
		public void PruebaIgualdadDeDiccionariosFalseCuandoSonDiccionariosDistintos ()
		{
			//Arrange
            Diccionario prueba = Diccionario.CrearNuevoDiccionario(ambienteTestPrueba);
			//Act
			//Assert
			this.diccionarioPrueba.Equals (prueba).ShouldBeFalse ();
		}

		[Test]
		public void PruebaIgualdadDeDiccionariosFalseCuandoComparoConNull ()
		{
			//Arrange
			Diccionario prueba = null;
			//Act
			//Assert
			this.diccionarioPrueba.Equals (prueba).ShouldBeFalse ();
		}

		#endregion
	}
}
