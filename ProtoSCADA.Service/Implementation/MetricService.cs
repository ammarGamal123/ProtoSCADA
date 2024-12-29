using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Abstract;
using ProtoSCADA.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProtoSCADA.Service
{
    public class MetricService : IMetricService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MetricService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Add a new metric
        public async Task<ProcessResult<bool>> AddMetricAsync(Metric metric)
        {
            if (metric == null)
                return ProcessResult<bool>.Failure("Metric cannot be null.", false);

            try
            {
                await _unitOfWork.Metrics.AddAsync(metric);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success("Metric added successfully.", true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error adding metric: {ex.Message}", false);
            }
        }

        // Delete a metric by ID
        public async Task<ProcessResult<bool>> DeleteMetricAsync(int id)
        {
            try
            {
                var metric = await _unitOfWork.Metrics.GetByIdAsync(id);
                if (metric == null)
                    return ProcessResult<bool>.Failure($"No metric found with ID {id}.", false);

                await _unitOfWork.Metrics.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success("Metric deleted successfully.", true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error deleting metric: {ex.Message}", false);
            }
        }

        // Retrieve all metrics
        public async Task<ProcessResult<IEnumerable<Metric>>> GetAllMetricsAsync()
        {
            try
            {
                var metrics = await _unitOfWork.Metrics.GetAllAsync();
                return ProcessResult<IEnumerable<Metric>>.Success("Metrics retrieved successfully.", metrics);
            }
            catch (Exception ex)
            {
                return ProcessResult<IEnumerable<Metric>>.Failure($"Error retrieving metrics: {ex.Message}", null);
            }
        }

        // Retrieve a metric by ID
        public async Task<ProcessResult<Metric>> GetMetricByIdAsync(int id)
        {
            try
            {
                var metric = await _unitOfWork.Metrics.GetByIdAsync(id);
                if (metric == null)
                    return ProcessResult<Metric>.Failure($"Metric with ID {id} not found.", null);

                return ProcessResult<Metric>.Success("Metric retrieved successfully.", metric);
            }
            catch (Exception ex)
            {
                return ProcessResult<Metric>.Failure($"Error retrieving metric: {ex.Message}", null);
            }
        }

        // Update a metric's details
        public async Task<ProcessResult<bool>> UpdateMetricAsync(Metric metric)
        {
            if (metric == null)
                return ProcessResult<bool>.Failure("Metric cannot be null.", false);

            try
            {
                _unitOfWork.Metrics.Update(metric);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success("Metric updated successfully.", true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error updating metric: {ex.Message}", false);
            }
        }
    }
}
