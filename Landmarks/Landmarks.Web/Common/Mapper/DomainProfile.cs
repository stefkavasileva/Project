using AutoMapper;
using Landmarks.Models;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Common.Models.Main.ViewModels;

namespace Landmarks.Web.Common.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<AddEditRegionBindingModel, Region>()
                .ForMember(entity => entity.Name,
                    opt => opt.MapFrom(viewModel => viewModel.Name))
                .ForMember(entity => entity.Area,
                    opt => opt.MapFrom(viewModel => viewModel.Area))
                .ForMember(entity => entity.Population,
                    opt => opt.MapFrom(viewModel => viewModel.Population));

            CreateMap<AddEditLandmarkBindingModel, Landmark>()
                .ForMember(entity => entity.Name,
                    opt => opt.MapFrom(viewModel => viewModel.Name))
                .ForMember(entity => entity.CategoryId,
                    opt => opt.MapFrom(viewModel => viewModel.CategoryId))
                .ForMember(entity => entity.RegionId,
                    opt => opt.MapFrom(viewModel => viewModel.RegionId));

            CreateMap<Landmark, Landmarks.Common.Models.Admin.ViewModels.LandmarkConciseViewModel>()
                .ForMember(viewModel => viewModel.Name,
                    (IMemberConfigurationExpression<Landmark, Landmarks.Common.Models.Admin.ViewModels.LandmarkConciseViewModel, string> opt) => opt.MapFrom(entity => entity.Name))
                .ForMember(viewModel => viewModel.CategoryName,
                    (IMemberConfigurationExpression<Landmark, Landmarks.Common.Models.Admin.ViewModels.LandmarkConciseViewModel, string> opt) => opt.MapFrom(entity => entity.Category.Name))
                .ForMember(viewModel => viewModel.RegionName,
                    (IMemberConfigurationExpression<Landmark, Landmarks.Common.Models.Admin.ViewModels.LandmarkConciseViewModel, string> opt) => opt.MapFrom(entity => entity.Region.Name));

            CreateMap<Landmark, AddEditLandmarkBindingModel>()
                .ForMember(viewModel => viewModel.Name,
                    opt => opt.MapFrom(entity => entity.Name))
                .ForMember(viewModel => viewModel.CategoryId,
                    opt => opt.MapFrom(entity => entity.CategoryId))
                .ForMember(viewModel => viewModel.RegionId,
                    opt => opt.MapFrom(entity => entity.RegionId));

             CreateMap<Landmark, LandmarkRankingViewModel>()
                .ForMember(viewModel => viewModel.Name,
                    opt => opt.MapFrom(entity => entity.Name))
                .ForMember(viewModel => viewModel.CategoryName,
                    opt => opt.MapFrom(entity => entity.Category.Name))
                .ForMember(viewModel => viewModel.RegionName,
                    opt => opt.MapFrom(entity => entity.Region.Name))
                    .ForMember(viewModel => viewModel.Id,
                     opt => opt.MapFrom(entity => entity.Id))
                 .ForMember(viewModel => viewModel.Description,
                     opt => opt.MapFrom(entity => entity.Description))
                 .ForMember(viewModel => viewModel.DesireToVisitCount,
                     opt => opt.MapFrom(entity => entity.DesiredPlaces.Count))
                 .ForMember(viewModel => viewModel.PostedDate,
                     opt => opt.MapFrom(entity => entity.PostedDate))
                 .ForMember(viewModel => viewModel.VisitorsCount,
                     opt => opt.MapFrom(entity => entity.Visitors.Count)).ReverseMap();

            CreateMap<AddEditLandmarkBindingModel, Landmark>()
                .ForMember(entity => entity.Name,
                    opt => opt.MapFrom(viewModel => viewModel.Name))
                .ForMember(entity => entity.CategoryId,
                    opt => opt.MapFrom(viewModel => viewModel.CategoryId))
                .ForMember(entity => entity.RegionId,
                    opt => opt.MapFrom(viewModel => viewModel.RegionId));

            CreateMap<Region, RegionDetailsViewModel>()
                .ForMember(entity => entity.Id,
                    opt => opt.MapFrom(viewModel => viewModel.Id))
                .ForMember(entity => entity.Name,
                    opt => opt.MapFrom(viewModel => viewModel.Name))
                .ForMember(entity => entity.Area,
                    opt => opt.MapFrom(viewModel => viewModel.Area))
                .ForMember(entity => entity.Population,
                    opt => opt.MapFrom(viewModel => viewModel.Population));

            CreateMap<ListCategoriesViewModel, Category>()
                .ForMember(entity => entity.Id,
                    opt => opt.MapFrom(viewModel => viewModel.Id))
                .ForMember(entity => entity.Name,
                    opt => opt.MapFrom(viewModel => viewModel.Name))
                    .ReverseMap();
        }
    }
}
