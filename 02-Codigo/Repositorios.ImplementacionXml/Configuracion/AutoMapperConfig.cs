using System;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

using ED = Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades;
using ER = Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Modelo;

namespace Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Configuracion
{
	internal static class AutoMapperConfig
	{

		private static bool autoMapperConfigured = false;
		private static readonly object autoMapperLock = new object ();

		static AutoMapperConfig ()
		{
			
		
		}

		public static void SetAutoMapperConfiguration ()
		{
			if (!autoMapperConfigured) {
				lock (autoMapperLock) {
					if (!autoMapperConfigured) {
						autoMapperConfigured = true;
						SetAutoMapperConfigurationPrivate ();
					}
				}
			}
		}

		[SuppressMessage ("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This code is required to configure AutoMapper correctly.  It could potentially be broken down into smaller methods later."),
			SuppressMessage ("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This code is required to configuring AutoMapper correctly.  It could potentially be broken down into smaller methods later.")]
		private static void SetAutoMapperConfigurationPrivate ()
		{
			// Define el mapeo de Etiqueta de Repositorio a Etiqueta Dominio
			Mapper.CreateMap<ED.Etiquetas.Etiqueta,ER.Etiqueta> ()
				.ForMember (dest => dest.Activo, src => src.MapFrom (val => val.Activo))
				.ForMember (dest => dest.Descripcion, src => src.MapFrom (val => val.Descripcion))
				.ForMember (dest => dest.IdiomaPorDefecto, src => src.MapFrom (val => val.IdiomaPorDefecto))
				.ForMember (dest => dest.Nombre, src => src.MapFrom (val => val.Nombre))
				.ForMember (dest => dest.NombreEtiqueta, src => src.MapFrom (val => val.Nombre))
				.ForMember (dest => dest.Traducciones, src => src.Ignore ());

			// Define el mapeo de Etiqueta de Dominio a Etiqueta de Repositorio
			Mapper.CreateMap<ER.Etiqueta,ED.Etiquetas.Etiqueta> ()
				.ForMember (dest => dest.Activo, src => src.MapFrom (val => val.Activo))
				.ForMember (dest => dest.Descripcion, src => src.MapFrom (val => val.Descripcion))
				.ForMember (dest => dest.IdiomaPorDefecto, src => src.MapFrom (val => val.IdiomaPorDefecto))
				.ForMember (dest => dest.Nombre, src => src.MapFrom (val => val.Nombre))
				.ForMember (dest => dest.Textos, src => src.Ignore ());

			// Define el mapeo de Traduccion de Dominio a Traduccion de Repositoriow
			Mapper.CreateMap<ER.Traduccion,ED.Etiquetas.Traduccion> ()
				.ForMember (dest => dest.Cultura, src => src.Ignore ())
				.ForMember (dest => dest.Texto, src => src.MapFrom (val => val.Value))
				.ForMember (dest => dest.ToolTip, src => src.MapFrom (val => val.Tooltip));
			
			// Define el mapeo de Traduccion de Dominio a Traduccion de Repositorio
			Mapper.CreateMap<ED.Etiquetas.Traduccion,ER.Traduccion> ()
				.ForMember (dest => dest.Cultura, src => src.Ignore ())
				.ForMember (dest => dest.Value, src => src.MapFrom (val => val.Texto))
				.ForMember (dest => dest.Tooltip, src => src.MapFrom (val => val.ToolTip));

			//Define el mapeo de Cultura de Domino a Cultura de Repositorio
			Mapper.CreateMap<ED.Etiquetas.Cultura, ER.Traduccion> ()
				.ForMember (dest => dest.Cultura, src => src.MapFrom (val => val.CodigoIso));

			//Define el mapeo de Cultura de Repositorio a Cultura de Dominio
			Mapper.CreateMap< ER.Traduccion, ED.Etiquetas.Cultura > ()
				.ForMember (dest => dest.CodigoIso, src => src.MapFrom (val => val.Cultura));
			

		}
	}
}

