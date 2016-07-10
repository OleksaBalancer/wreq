using AutoMapper;
using wreq.Models.Entities;
using wreq.Models.ViewModels;

namespace wreq.Models
{
    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<SoilType, SoilTypeViewModel>().ReverseMap();
            CreateMap<SoilType, SoilTypeListViewModel>();
            CreateMap<IrrigationType, IrrigationTypeViewModel>().ReverseMap();
            CreateMap<IrrigationType, IrrigationTypeListViewModel>();
            CreateMap<Culture, CultureViewModel>().ReverseMap();
            CreateMap<Culture, CultureListViewModel>();

            CreateMap<Field, FieldViewModel>()
                .ForMember(
                dest => dest.SoilTypeName,
                opts => opts.MapFrom(src => src.SoilType.Name))
                .ForMember(
                dest => dest.IrrigationTypeName,
                opts => opts.MapFrom(src => src.IrrigationType.Name));
            CreateMap<FieldViewModel, Field>();
            CreateMap<Field, FieldListViewModel>();

            CreateMap<Crop, CropViewModel>()
                .ForMember(
                dest => dest.CultureName,
                opts => opts.MapFrom(src => src.Culture.Name))
                .ForMember(
                dest => dest.FieldName,
                opts => opts.MapFrom(src => src.Field.Name));
            CreateMap<CropViewModel, Crop>();
            CreateMap<Crop, CropListViewModel>();

            CreateMap<ApplicationUser, ApplicationUserViewModel>();

        }
    }
}