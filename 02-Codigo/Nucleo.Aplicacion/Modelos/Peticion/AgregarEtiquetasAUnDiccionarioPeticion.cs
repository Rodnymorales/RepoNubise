﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion
{
    public class AgregarEtiquetasAUnDiccionarioPeticion : PeticionApp<AgregarEtiquetasAUnDiccionarioPeticion>
	{
		[Required]
		public List<Etiqueta> ListaDeEtiquetas { get; set; }

		[Required]
		public Guid DiccionarioId { get; set; }

		#region constructores

		private AgregarEtiquetasAUnDiccionarioPeticion()
		{
			ListaDeEtiquetas = new List<Etiqueta>();
			DiccionarioId = Guid.Empty;
		}
        
        #endregion
	}
}