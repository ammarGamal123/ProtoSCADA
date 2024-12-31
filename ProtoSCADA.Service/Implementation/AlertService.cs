using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Abstract;
using ProtoSCADA.Service.Utilities;
using ProtoSCADA.Service.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Service
{
    public class AlertService : IAlertService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAlertRepository _alertRepository;

        public AlertService(IUnitOfWork unitOfWork, IAlertRepository alertRepository = null)
        {
            _unitOfWork = unitOfWork;
            _alertRepository = alertRepository;
        }

        public async Task<ProcessResult<bool>> AddAlertAsync(Alert alert)
        {
            try
            {
                if (alert == null)
                    return ProcessResult<bool>.Failure("Alert cannot be null.");

                await _unitOfWork.Alerts.AddAsync(alert);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error adding alert: {ex.Message}");
            }
        }

        public async Task<ProcessResult<bool>> DeleteAlertAsync(int id)
        {
            try
            {
                var alert = await _unitOfWork.Alerts.GetByIdAsync(id);
                if (alert == null)
                    return ProcessResult<bool>.Failure($"Alert with ID {id} not found.");

                await _unitOfWork.Alerts.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error deleting alert: {ex.Message}");
            }
        }

        public async Task<ProcessResult<IEnumerable<Alert>>> GetAllAlertsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var validationResult = ValidatePagination.Validate(pageNumber,pageSize);
                if (!validationResult.IsSuccess)
                {
                    return ProcessResult<IEnumerable<Alert>>.Failure(validationResult.Message);
                }

                var alerts = await _alertRepository.GetAllAsync();
                var paginatedAlerts = alerts
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return ProcessResult<IEnumerable<Alert>>.Success(paginatedAlerts);
            }
            catch (Exception ex)
            {
                return ProcessResult<IEnumerable<Alert>>.Failure($"Error retrieving alerts: {ex.Message}");
            }
        }

        public async Task<ProcessResult<Alert>> GetAlertByIdAsync(int id)
        {
            try
            {
                var alert = await _alertRepository.GetByIdAsync(id);
                if (alert == null)
                    return ProcessResult<Alert>.Failure($"Alert with ID {id} not found.");

                return ProcessResult<Alert>.Success(alert);
            }
            catch (Exception ex)
            {
                return ProcessResult<Alert>.Failure($"Error retrieving alert: {ex.Message}");
            }
        }

        public async Task<ProcessResult<bool>> UpdateAlertAsync(Alert alert)
        {
            try
            {
                if (alert == null)
                    return ProcessResult<bool>.Failure("Alert cannot be null.");

                _unitOfWork.Alerts.Update(alert);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error updating alert: {ex.Message}");
            }
        }
    }
}