using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Abstract;
using ProtoSCADA.Service.Utilities;
using ProtoSCADA.Service.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Service
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportRepository _reportRepository;

        public ReportService(IUnitOfWork unitOfWork, IReportRepository reportRepository)
        {
            _unitOfWork = unitOfWork;
            _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
        }

        public async Task<ProcessResult<bool>> AddReportAsync(Report report)
        {
            try
            {
                if (report == null)
                    return ProcessResult<bool>.Failure("Report cannot be null.");

                await _unitOfWork.Reports.AddAsync(report);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error adding report: {ex.Message}");
            }
        }

        public async Task<ProcessResult<bool>> DeleteReportAsync(int id)
        {
            try
            {
                var report = await _unitOfWork.Reports.GetByIdAsync(id);
                if (report == null)
                    return ProcessResult<bool>.Failure($"Report with ID {id} not found.");

                await _unitOfWork.Reports.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error deleting report: {ex.Message}");
            }
        }

        public async Task<ProcessResult<IEnumerable<ReportDto>>> GetAllReportsAsync(int pageNumber, int pageSize)
        {
            try
            {
                // Validate pagination parameters
                var validationResult = ValidatePagination.Validate(pageNumber, pageSize);
                if (!validationResult.IsSuccess)
                {
                    return ProcessResult<IEnumerable<ReportDto>>.Failure(validationResult.Message);
                }

                // Fetch paginated DTOs directly from the repository
                var reportDtos = await _reportRepository.GetAllReportsAsync(pageNumber, pageSize);

                return ProcessResult<IEnumerable<ReportDto>>.Success(reportDtos);
            }
            catch (Exception ex)
            {
                return ProcessResult<IEnumerable<ReportDto>>.Failure($"Error retrieving reports: {ex.Message}");
            }
        }
        public async Task<ProcessResult<Report>> GetReportByIdAsync(int id)
        {
            try
            {
                var report = await _reportRepository.GetByIdAsync(id);
                if (report == null)
                    return ProcessResult<Report>.Failure($"Report with ID {id} not found.");

                return ProcessResult<Report>.Success(report);
            }
            catch (Exception ex)
            {
                return ProcessResult<Report>.Failure($"Error retrieving report: {ex.Message}");
            }
        }

        public async Task<ProcessResult<bool>> UpdateReportAsync(Report report)
        {
            try
            {
                if (report == null)
                    return ProcessResult<bool>.Failure("Report cannot be null.");

                _unitOfWork.Reports.Update(report);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error updating report: {ex.Message}");
            }
        }
    }
}