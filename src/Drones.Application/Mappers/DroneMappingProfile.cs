using AutoMapper;
using Drones.Application.ViewModels.Drone.Request;
using Drones.Application.ViewModels.Drone.Response;
using Drones.Infrastructure.Commons.Bases.Response;
using Drones.Domain.Entities;
using Drones.Utilities.Statics;

namespace POS.Application.Mappers
{
    public class DroneMappingsProfile : Profile
    {
        public DroneMappingsProfile()
        {
            CreateMap<TDrone, DroneResponseViewModel>()
                .ForMember(x => x.Model, x => x.MapFrom(y => (ModelTypes)(y.Model!)))
                .ForMember(x => x.State, x => x.MapFrom(y => (StateTypes)(y.State!)))
                .ReverseMap();

            CreateMap<BaseEntityResponse<TDrone>, BaseEntityResponse<DroneResponseViewModel>>()
                .ReverseMap();

            CreateMap<DroneRequestViewModel, TDrone>()
                .ReverseMap();

            CreateMap<TDrone, DroneSelectResponseViewModel>()
                .ReverseMap();
        }
    }
}