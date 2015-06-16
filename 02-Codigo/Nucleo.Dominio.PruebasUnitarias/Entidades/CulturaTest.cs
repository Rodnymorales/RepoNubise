using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Should;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using System.ComponentModel.DataAnnotations;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.PruebasUnitarias.Entidades
{
	[TestFixture]
	public class CulturaTest
	{
        private readonly List<string> codigosIsoCorrectos;
        private readonly List<string> codigosIsoErrados;

		public CulturaTest ()
		{
			codigosIsoCorrectos = new List<string> () { "es", "es-VE" };
			codigosIsoErrados = new List<string> () { "esp", "es-ve", "es.VE", "ES-VE", "es-VE." };
		}

		[Category ("Creación")]
		[Test]
		public void PruebaCreacionDeNuevaCulturaConCodigoIsoCorrectos ()
		{
			//Arrange
			//Act
			foreach (var item in codigosIsoCorrectos) {
				Cultura cultura = Cultura.CrearNuevaCultura (item);

				//Assert
				cultura.CodigoIso.ShouldBeSameAs (item);
			}
		}

		[Category ("Creación")]
		[Test]
		public void PruebaCreacionDeNuevaCulturaConCodigoIsoErrados ()
		{
			//Arrange
			//Act
			foreach (var item in codigosIsoErrados) {
				//Assert
				Assert.Throws<ValidationException> (delegate {
					Cultura.CrearNuevaCultura (item);
				});
			}
		}
	}
}
