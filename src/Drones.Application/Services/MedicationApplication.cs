using AutoMapper;
using Drones.Application.Commons.Bases;
using Drones.Application.Interfaces;
using Drones.Application.Validators.Category;
using Drones.Application.ViewModels.Drone.Response;
using Drones.Application.ViewModels.Request;
using Drones.Application.ViewModels.Response;
using Drones.Domain.Entities;
using Drones.Infrastructure.Commons.Bases.Response;
using Drones.Infrastructure.Persistences.Interfaces;
using Drones.Utilities.Statics;

namespace Drones.Application.Services
{
    public class MedicationApplication : IMedicationApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MedicationValidator _validationRules;

        public MedicationApplication(IUnitOfWork unitOfWork, IMapper mapper, MedicationValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public async Task<BaseResponse<BaseEntityResponse<MedicationResponseViewModel>>> ListMedications()
        {
            var response = new BaseResponse<BaseEntityResponse<MedicationResponseViewModel>>();
            var medications = await _unitOfWork.Medication.GetAllAsync();

            if (medications is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<MedicationResponseViewModel>>(medications);
                response.Message = ReplyMessages.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessages.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<DroneResponseViewModel>> GetMedicationById(int id)
        {
            var response = new BaseResponse<DroneResponseViewModel>();
            var medication = await _unitOfWork.Medication.GetByIdAsync(id);

            if (medication is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<DroneResponseViewModel>(medication);
                response.Message = ReplyMessages.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessages.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterMedication(MedicationRequestViewModel requestViewModel)
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

            var medication = _mapper.Map<TMedication>(requestViewModel);
            response.Data = await _unitOfWork.Medication.RegisteAsync(medication);

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

        public async Task<BaseResponse<bool>> EditMedication(int medicationId, MedicationRequestViewModel requestViewModel)
        {
            var response = new BaseResponse<bool>();
            var medicationEdit = await GetMedicationById(medicationId);

            if (medicationEdit is null)
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

                var medication = _mapper.Map<TMedication>(requestViewModel);
                medication.Id = medicationId;
                response.Data = await _unitOfWork.Medication.EditAsync(medication);

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

        public async Task<BaseResponse<bool>> DeleteMedication(int medicationId)
        {
            var response = new BaseResponse<bool>();
            var medicationDelete = await GetMedicationById(medicationId);

            if (medicationDelete is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessages.MESSAGE_QUERY_EMPTY;
            }
            else
            {
                response.Data = await _unitOfWork.Medication.DeleteAsync(medicationId);

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
    }
}
