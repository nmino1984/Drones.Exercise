using AutoMapper;
using Drones.Application.Commons.Bases;
using Drones.Application.Interfaces;
using Drones.Application.Validators;
using Drones.Application.ViewModels.Drone.Request;
using Drones.Application.ViewModels.Drone.Response;
using Drones.Domain.Entities;
using Drones.Infrastructure.Commons.Bases.Response;
using Drones.Infrastructure.Persistences.Interfaces;
using Drones.Utilities.Statics;

namespace Drones.Application.Services
{
    public class DroneApplication : IDroneApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DroneValidator _validationRules;

        public DroneApplication(IUnitOfWork unitOfWork, IMapper mapper, DroneValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public async Task<BaseResponse<BaseEntityResponse<DroneResponseViewModel>>> ListDrones()
        {
            var response = new BaseResponse<BaseEntityResponse<DroneResponseViewModel>>();
            var drones = await _unitOfWork.Drone.GetAllAsyncAsResponse();

            if (drones is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<DroneResponseViewModel>>(drones);
                response.Message = ReplyMessages.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessages.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<DroneResponseViewModel>> GetDroneById(int id)
        {
            var response = new BaseResponse<DroneResponseViewModel>();
            var drone = await _unitOfWork.Drone.GetByIdAsync(id);

            if (drone is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<DroneResponseViewModel>(drone);
                response.Message = ReplyMessages.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessages.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterDrone(DroneRequestViewModel requestViewModel)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _validationRules.ValidateAsync(requestViewModel);

            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessages.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;

                return response;
            }

            var drone = _mapper.Map<TDrone>(requestViewModel);
            response.Data = await _unitOfWork.Drone.RegisteAsync(drone);

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

        public async Task<BaseResponse<bool>> EditDrone(int droneId, DroneRequestViewModel requestViewModel)
        {
            var response = new BaseResponse<bool>();
            var droneEdit = await GetDroneById(droneId);

            if (droneEdit is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessages.MESSAGE_QUERY_EMPTY;
            }
            else
            {
                var validationResult = await _validationRules.ValidateAsync(requestViewModel);

                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessages.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;

                    return response;
                }

                var drone = _mapper.Map<TDrone>(requestViewModel);
                drone.Id = droneId;
                response.Data = await _unitOfWork.Drone.EditAsync(drone);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessages.MESSAGE_UPDATE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessages.MESSAGE_FAILED;
                }
            }

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteDrone(int droneId)
        {
            var response = new BaseResponse<bool>();
            var droneDelete = await GetDroneById(droneId);

            if (droneDelete is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessages.MESSAGE_QUERY_EMPTY;
            }
            else
            {
                response.Data = await _unitOfWork.Drone.DeleteAsync(droneId);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessages.MESSAGE_DELETE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessages.MESSAGE_FAILED;
                }
            }

            return response;
        }

        public async Task<bool> ChangeStateToDrone(int droneId, StateTypes newState)
        {
            var drone = await _unitOfWork.Drone.GetByIdAsync(droneId);
            drone.State = (int)newState;

            if (drone.State == (int)StateTypes.IDLE)
            {
                var listDroneMedicationsActives = await _unitOfWork.DroneMedication.GetDroneMedications(droneId);

                foreach (var item in listDroneMedicationsActives)
                {
                    item.Active = false;
                    await _unitOfWork.DroneMedication.EditAsync(item);
                }
            }

            return await _unitOfWork.Drone.EditAsync(drone);
        }

        public async Task<bool> ChangeBatteryLevelToDrone(int droneId, double droneBatteryLevel)
        {
            var drone = await _unitOfWork.Drone.GetByIdAsync(droneId);
            if (droneBatteryLevel <= drone.BatteryCapacity)
            {
                drone.BatteryLevel = droneBatteryLevel;
            }

            return await _unitOfWork.Drone.EditAsync(drone);
        }
    }
}
