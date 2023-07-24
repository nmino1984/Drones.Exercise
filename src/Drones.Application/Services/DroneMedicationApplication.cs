using AutoMapper;
using Drones.Application.Commons.Bases;
using Drones.Application.Interfaces;
using Drones.Application.Validators.Category;
using Drones.Application.ViewModels.Drone.Request;
using Drones.Application.ViewModels.Drone.Response;
using Drones.Application.ViewModels.DroneMedication.Request;
using Drones.Application.ViewModels.Response;
using Drones.Domain.Entities;
using Drones.Infrastructure.Persistences.Interfaces;
using Drones.Utilities.Statics;
using FluentValidation;

namespace Drones.Application.Services
{
    public class DroneMedicationApplication : IDroneMedicationApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DroneValidator _droneValidationRules;

        public DroneMedicationApplication(IUnitOfWork unitOfWork, IMapper mapper, DroneValidator droneValidationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _droneValidationRules = droneValidationRules;
        }

        public async Task<BaseResponse<bool>> RegisterDrone(DroneRequestViewModel requestViewModel)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _droneValidationRules.ValidateAsync(requestViewModel);

            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessages.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;

                return response;
            }

            var category = _mapper.Map<TDrone>(requestViewModel);
            response.Data = await _unitOfWork.Drone.RegisteAsync(category);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessages.MESSAGE_SAVE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessages.MESSAGE_FAILED;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> LoadDroneWithMeditionItems(DispatchRequestViewModel requestViewModel)
        {
            var response = new BaseResponse<bool>();
            var drone = await _unitOfWork.Drone.GetByIdAsync(requestViewModel.droneId);
            var listMedications = requestViewModel.listMedications!;

            if (drone is not null)
            {
                response.IsSuccess = true;
                response.Data = await _unitOfWork.DroneMedication.LoadDroneWithMedicationItems(requestViewModel.droneId, requestViewModel.listMedications!);
                response.Message = ReplyMessages.MESSAGE_QUERY;
            }
            else if (listMedications.Count > 0)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessages.MESSAGE_WRONG_DRONE_ID;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessages.MESSAGE_WRONG_DRONE_ID;
            }

            return response;
        }

        public async Task<BaseResponse<List<MedicationResponseViewModel>>> CheckingLoadedMedicationItemsByDroneGiven(int droneId)
        {
            var response = new BaseResponse<List<MedicationResponseViewModel>>();
            var returnList = new List<MedicationResponseViewModel>();
            var listMedication = await _unitOfWork.DroneMedication.CheckLoadedMedicationItemsByDroneGiven(droneId);

            if (listMedication.Count > 0)
            {
                foreach (var item in listMedication)
                {
                    var medication = await _unitOfWork.Medication.GetByIdAsync(item);

                    returnList.Add(new MedicationResponseViewModel
                    {
                        Id = medication.Id,
                        Name = medication.Name,
                        Code = medication.Code,
                        Image = medication.Image,
                        Weight = medication.Weight,
                    });
                }

                response.IsSuccess = true;
                response.Data = returnList;
                response.Message = ReplyMessages.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = ReplyMessages.MESSAGE_DRONE_NOT_LOADED;
            }

            return response;
        }

        public async Task<BaseResponse<List<DroneResponseViewModel>>> CheckingAvailableDronesForLoaded()
        {
            var response = new BaseResponse<List<DroneResponseViewModel>>();
            var returnList = new List<DroneResponseViewModel>();
            var drones = await _unitOfWork.Drone.GetAllAsync();

            drones = drones.AsQueryable().Where(w => w.State == (int)StateTypes.IDLE).ToList();

            if (drones.Count() > 0)
            {
                foreach (var item in drones)
                {
                    returnList.Add(new DroneResponseViewModel
                    {
                        Id = item.Id,
                        SerialNumber = item.Name,
                        Model = Enum.GetName(typeof(ModelTypes), item.Model),
                        BatteryCapacity = item.BatteryCapacity,
                        State = Enum.GetName(typeof(StateTypes), item.State),
                        WeightLimit = item.WeightLimit
                    });
                }
                response.IsSuccess = true;
                response.Data = returnList;
                response.Message = ReplyMessages.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = ReplyMessages.MESSAGE_NOT_DRONE_AVAILABLE;
            }

            return response;
        }

        public async Task<BaseResponse<int>> CheckDroneBatteryLevelByDroneGiven(int droneId)
        {
            var response = new BaseResponse<int>();
            var drone = await _unitOfWork.Drone.GetByIdAsync(droneId);

            if (drone is not null)
            {
                response.IsSuccess = true;
                response.Data = drone.BatteryCapacity;
                response.Message = ReplyMessages.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessages.MESSAGE_WRONG_DRONE_ID;
            }

            return response;
        }
    }
}
