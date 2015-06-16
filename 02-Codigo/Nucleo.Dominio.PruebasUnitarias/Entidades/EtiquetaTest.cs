using System;
using System.Linq;
using NUnit.Framework;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using Should;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;
using System.Collections.Generic;



namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.PruebasUnitarias.Entidades
{
	[TestFixture]
	public class EtiquetaTest
	{
		private readonly Etiqueta etiqueta;
		private readonly Cultura cultura;
		private readonly string texto;
        private readonly Diccionario Diccionario;
               
        

		public EtiquetaTest ()
		{
			this.etiqueta = Etiqueta.CrearNuevaEtiqueta ("app.common.aceptar2");
			this.cultura = Cultura.CrearNuevaCultura ("en-US");
			this.texto = "accept2";
		}

		#region creacion

		[Category ("Creación")]
		[Test]
		public void PruebaCreacionDeNuevaEtiqueta ()
		{
			//Arrange
			Etiqueta prueba = Etiqueta.CrearNuevaEtiqueta ("en");
			//Act
			//Assert
			prueba.ShouldBeType (typeof(Etiqueta));
		}

		#endregion

		#region igualdad

		[Test]
		public void PruebaEqualsEsFalsoCuandoNoEsLaMismaEtiqueta ()
		{
			//Arrange	
			Etiqueta prueba = Etiqueta.CrearNuevaEtiqueta ("es");
			//Act
			Boolean resultado = this.etiqueta.Equals (prueba);
			//Assert
			resultado.ShouldBeFalse ();

		}

		[Test]
		public void PruebaEqualsEsFalsoCuandoEsUnaEtiquetaConLosMismosValores ()
		{
			//Arrange	
			Etiqueta prueba = Etiqueta.CrearNuevaEtiqueta ("en");
			//Act			
			Boolean resultado = this.etiqueta.Equals (prueba);
			//Assert
			resultado.ShouldBeFalse ();

		}

		[Test]
		public void PruebaEqualsVerdaderoCuandoEsElMismoObjetoDeEtiqueta ()
		{
			//Arrange				
			//Act
			Boolean resultado = this.etiqueta.Equals (this.etiqueta);
			//Assert
			resultado.ShouldBeTrue ();

		}

		[Test]
		public void PruebaEqualsFalsoCuandoElObjetoEsNulo ()
		{
			//Arrange				
			//Act
			Boolean resultado = this.etiqueta.Equals (null);
			//Assert
			resultado.ShouldBeFalse ();
		}

		#endregion

		#region hashcode

		[Test]
		public void PruebaHashCodeEsEntero ()
		{
			//Arrange
			Etiqueta prueba = Etiqueta.CrearNuevaEtiqueta ("prueba");
			//Act
			int resultado = prueba.GetHashCode ();
			//Assert
			resultado.ShouldBeType (typeof(int));
		}

		#endregion

		#region agregar traducciones

		[Test]
		public void PruebaAgregarTraduccionExitenteAEtiquetaConTraducciones ()
		{
			//Arrange
			Etiqueta prueba = Etiqueta.CrearNuevaEtiqueta ("app.common.aceptar");
			Traduccion traduccion = Traduccion.CrearNuevaTraduccion (this.cultura, this.texto);
			Traduccion traduccion2 = Traduccion.CrearNuevaTraduccion (this.cultura, this.texto);
			//Act
			prueba.AgregarTraduccion (traduccion);
			//Assert
			Assert.Throws<ArgumentException> (delegate {
				prueba.AgregarTraduccion (traduccion2);
			});


            
           
		}

		[Test]
		public void PruebaAgregarTraduccionAEtiquetaSinTraducciones ()
		{
			//Arrange
			Etiqueta prueba = Etiqueta.CrearNuevaEtiqueta ("app.common.aceptar");
			//Act
			Traduccion traduccion = Traduccion.CrearNuevaTraduccion (this.cultura, this.texto);
			prueba.AgregarTraduccion (traduccion);
			//Assert
			prueba.ShouldBeType<Etiqueta> ();
			prueba.Textos.Count.ShouldEqual (1);
			//prueba.Textos.ShouldContain(new KeyValuePair<string, Traduccion>(this.cultura, this.valor));
			prueba.Textos.ShouldContain (traduccion);

		}

		[Test]
		public void PruebaAgregarTraduccionAEtiquetaConTraducciones ()
		{
			//Arrange
			Etiqueta prueba = Etiqueta.CrearNuevaEtiqueta ("app.common.aceptar");
			Traduccion traduccion = Traduccion.CrearNuevaTraduccion (this.cultura, this.texto);
			prueba.AgregarTraduccion (traduccion);
			//Act
			Traduccion traprueba = Traduccion.CrearNuevaTraduccion (Cultura.CrearNuevaCultura ("en"), this.texto);
			prueba.AgregarTraduccion (traprueba);
			//Assert
			//prueba.ShouldBeType<Etiqueta>();
			prueba.Textos.Count.ShouldEqual (2);
			//prueba.Textos.ShouldContain (new KeyValuePair<Cultura, Valor> (this.cultura, this.valor));
			//prueba.Textos.ShouldContain (new KeyValuePair<Cultura, Valor> (Cultura.CrearNuevoValorDeCultura ("en"), this.valor));
			prueba.Textos.ShouldContain (traduccion);
			prueba.Textos.ShouldContain (traprueba);
		}



		#endregion

		#region eliminar traducciones

		[Test]
		public void PruebaEliminarTraduccionAEtiquetaSinTraducciones ()
		{
			//Arrange
			Etiqueta prueba = Etiqueta.CrearNuevaEtiqueta ("app.common.aceptar");
			//Act
            prueba.EliminarTraduccion(Traduccion.CrearNuevaTraduccion(this.cultura, this.texto));
			//Assert
			prueba.Textos.Count.ShouldEqual (0);
		}

		[Test]
		public void PruebaEliminarTraduccionExistenteAEtiquetaConTraducciones ()
		{
			//Arrange
			Etiqueta prueba = Etiqueta.CrearNuevaEtiqueta ("app.common.aceptar");
			prueba.AgregarTraduccion (Traduccion.CrearNuevaTraduccion (this.cultura, this.texto));
			//Act
            prueba.EliminarTraduccion(Traduccion.CrearNuevaTraduccion(this.cultura, this.texto));
			//Assert
			prueba.Textos.Count.ShouldEqual (0);
		}

		[Test]
		public void PruebaEliminarTraduccionNoExistenteAEtiquetaConTraducciones ()
		{
			//Arrange
			Etiqueta prueba = Etiqueta.CrearNuevaEtiqueta ("app.common.aceptar");
			prueba.AgregarTraduccion (Traduccion.CrearNuevaTraduccion (this.cultura, this.texto));
			//Act
            prueba.EliminarTraduccion(Traduccion.CrearNuevaTraduccion(this.cultura, "TextoNoExiste"));
			//Assert
			prueba.Textos.Count.ShouldEqual (1);
		}

		#endregion

		#region modificar traducciones

		[Test]
		public void PruebaModificarTraduccionAEtiquetaSinTraduccionesAgregaNuevaTraduccion ()
		{
			//Arrange
			Etiqueta prueba = Etiqueta.CrearNuevaEtiqueta ("app.common.aceptar");
			Traduccion traduccion = Traduccion.CrearNuevaTraduccion (Cultura.CrearNuevaCultura ("es"), "aceptar");
			//Act
			prueba.ModificarTraduccion (traduccion);
			//Assert
			prueba.Textos.Count.ShouldEqual (1);
			//prueba.Textos.ContainsKey(Cultura.CrearNuevoValorDeCultura("es")).ShouldEqual(true);
			prueba.Textos.ShouldContain (traduccion);
		}

		[Test]
		public void PruebaModificarTraduccionExistenteAEtiquetaConTraduccionesCambiaValorDeLaTraduccion ()
		{
			//Arrange
			Etiqueta prueba = Etiqueta.CrearNuevaEtiqueta ("app.common.aceptar");
			prueba.AgregarTraduccion (Traduccion.CrearNuevaTraduccion (Cultura.CrearNuevaCultura ("it"), "hola"));
			Traduccion traduccion = Traduccion.CrearNuevaTraduccion (Cultura.CrearNuevaCultura ("it"), "ciao");
			//Act
			prueba.ModificarTraduccion (traduccion);
			//Assert
			prueba.Textos.Count.ShouldEqual (1);
			//prueba.Textos.ContainsKey(Cultura.CrearNuevoValorDeCultura("it")).ShouldEqual(true);
			//prueba.Textos.[Cultura.CrearNuevoValorDeCultura("it")].ShouldEqual(Valor.CrearNuevoValorDeTraduccion("ciao"));
			prueba.Textos.ShouldContain (traduccion);
		      
        
        }


		#endregion
	}
}
