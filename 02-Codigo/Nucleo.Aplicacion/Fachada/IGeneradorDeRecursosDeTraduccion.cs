using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada
{
	public interface IGeneradorDeRecursosDeTraduccion
	{
		GenerarRecursosPorIdiomaRespuesta GenerarRecursos(GenerarRecursosPorIdiomaPeticion peticion);
	}
}