using AutoMapper;
using Drones.Application.ViewModels.Drone.Request;
using Drones.Application.ViewModels.Drone.Response;
using Drones.Infrastructure.Commons.Bases.Response;
using Drones.Domain.Entities;
using Drones.Utilities.Statics;

namespace POS.Application.Mappers
{
    public class DroneMedicationMappingsProfile : Profile
    {
        public DroneMedicationMappingsProfile()
        {
            CreateMap<RDroneMedication, DroneMedicationResponseViewModel>()
                .ForMember(x => x.MedicationName, x => x.MapFrom(y => y.IdMedicationNavigation.Name))
                .ForMember(x => x.DroneSerialNumber, x => x.MapFrom(y => y.IdDroneNavigation.Name))
                .ReverseMap();

            CreateMap<BaseEntityResponse<RDroneMedication>, BaseEntityResponse<DroneMedicationResponseViewModel>>()
                .ReverseMap();

            CreateMap<DroneMedicationRequestViewModel, RDroneMedication>()
                .ReverseMap();
        }
    }
}