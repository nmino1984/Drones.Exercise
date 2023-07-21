using Drones.Application.Commons.Bases;
using Drones.Application.ViewModels.Drone.Response;
using Drones.Application.ViewModels.Request;
using Drones.Application.ViewModels.Response;
using Drones.Infrastructure.Commons.Bases.Response;

namespace Drones.Application.Interfaces
{
    public interface IMedicationApplication
    {
        Task<BaseResponse<BaseEntityResponse<MedicationResponseViewModel>>> ListMedications();
        Task<BaseResponse<DroneResponseViewModel>> GetMedicationById(int id);
        Task<BaseResponse<bool>> RegisterMedication(MedicationRequestViewModel requestViewModel);
        Task<BaseResponse<bool>> EditMedication(int droneId, MedicationRequestViewModel requestViewModel);
        Task<BaseResponse<bool>> DeleteMedication(int id);

    }
}
