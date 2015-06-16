using System;
using System.Collections.Generic;
using System.Linq;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Comunes
{
	public interface  IRepositorio<T>
	{
		List<T> ObtenerTodosLosElementos ();

		T ObtenerUnElemento ();

		Boolean EliminarUnElemento (Guid id);

		T ModificarUnElemento (T elementoAModificar);

		T CrearUnElemento (T elementoACrear);
	}
}
