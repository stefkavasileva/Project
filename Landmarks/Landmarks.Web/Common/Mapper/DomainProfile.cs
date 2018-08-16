using AutoMapper;
using Landmarks.Models;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Common.Models.Admin.ViewModels;
using Landmarks.Common.Models.Main.ViewModel;

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
            CreateMap<AddEditLandmarkBindingModel, Landmark>()
                .ForMember(entity => entity.Name,
                    opt => opt.MapFrom(viewModel => viewModel.Name))
                .ForMember(entity => entity.CategoryId,
                    opt => opt.MapFrom(viewModel => viewModel.CategoryId))
                .ForMember(entity => entity.RegionId,
                    opt => opt.MapFrom(viewModel => viewModel.RegionId));

        }
    }
}
