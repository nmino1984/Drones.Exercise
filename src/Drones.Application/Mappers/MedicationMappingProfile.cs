using AutoMapper;
using Drones.Application.ViewModels.Request;
using Drones.Application.ViewModels.Response;
using Drones.Domain.Entities;
using Drones.Infrastructure.Commons.Bases.Response;

namespace POS.Application.Mappers
{
    public class MedicationMappingsProfile : Profile
    {
        public MedicationMappingsProfile()
        {
            CreateMap<TMedication, MedicationResponseViewModel>()
                .ReverseMap();

            CreateMap<BaseEntityResponse<TMedication>, BaseEntityResponse<MedicationResponseViewModel>>()
                .ReverseMap();

            CreateMap<MedicationRequestViewModel, TMedication>()
                .ReverseMap();
        }
    }
}