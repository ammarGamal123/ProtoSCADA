using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Abstract;
using ProtoSCADA.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Service
{
    public class AlertService : IAlertService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlertService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProcessResult<bool>> AddAlertAsync(Alert alert)
        {
            if (alert == null)
                return ProcessResult<bool>.Failure("Alert cannot be null.", false);

            try
            {
                await _unitOfWork.Alerts.AddAsync(alert);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success("Alert added successfully.", true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error adding alert: {ex.Message}", false);
            }
        }

        public async Task<ProcessResult<bool>> DeleteAlertAsync(int id)
        {
            try
            {
                var alert = await _unitOfWork.Alerts.GetByIdAsync(id);
                if (alert == null)
                    return ProcessResult<bool>.Failure($"No alert found with ID {id}.", false);

                await _unitOfWork.Alerts.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success("Alert deleted successfully.", true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error deleting alert: {ex.Message}", false);
            }
        }

        public async Task<ProcessResult<IEnumerable<Alert>>> GetAllAlertsAsync()
        {
            try
            {
                var alerts = await _unitOfWork.Alerts.GetAllAsync();
                return ProcessResult<IEnumerable<Alert>>.Success("Alerts retrieved successfully.", alerts);
            }
            catch (Exception ex)
            {
                return ProcessResult<IEnumerable<Alert>>.Failure($"Error retrieving alerts: {ex.Message}", null);
            }
        }

        public async Task<ProcessResult<Alert>> GetAlertByIdAsync(int id)
        {
            try
            {
                var alert = await _unitOfWork.Alerts.GetByIdAsync(id);
                if (alert == null)
                    return ProcessResult<Alert>.Failure($"Alert with ID {id} not found.", null);

                return ProcessResult<Alert>.Success("Alert retrieved successfully.", alert);
            }
            catch (Exception ex)
            {
                return ProcessResult<Alert>.Failure($"Error retrieving alert: {ex.Message}", null);
            }
        }

        public async Task<ProcessResult<bool>> UpdateAlertAsync(Alert alert)
        {
            if (alert == null)
                return ProcessResult<bool>.Failure("Alert cannot be null.", false);

            try
            {
                _unitOfWork.Alerts.Update(alert);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success("Alert updated successfully.", true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error updating alert: {ex.Message}", false);
            }
        }
    }
}
