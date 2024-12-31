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
    public class LineService : ILineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILineRepository _lineRepository;

        public LineService(IUnitOfWork unitOfWork, ILineRepository lineRepository = null)
        {
            _unitOfWork = unitOfWork;
            _lineRepository = lineRepository ?? throw new ArgumentNullException(nameof(lineRepository));
        }

        public async Task<ProcessResult<bool>> AddLineAsync(Line line)
        {
            try
            {
                if (line == null)
                    return ProcessResult<bool>.Failure("Line cannot be null.");

                await _unitOfWork.Lines.AddAsync(line);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error adding line: {ex.Message}");
            }
        }

        public async Task<ProcessResult<bool>> DeleteLineAsync(int id)
        {
            try
            {
                var line = await _unitOfWork.Lines.GetByIdAsync(id);
                if (line == null)
                    return ProcessResult<bool>.Failure($"Line with ID {id} not found.");

                await _unitOfWork.Lines.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error deleting line: {ex.Message}");
            }
        }

        public async Task<ProcessResult<IEnumerable<LineDto>>> GetAllLinesAsync(int pageNumber, int pageSize)
        {
            try
            {
                // Validate pagination parameters
                var validationResult = ValidatePagination.Validate(pageNumber, pageSize);
                if (!validationResult.IsSuccess)
                {
                    return ProcessResult<IEnumerable<LineDto>>.Failure(validationResult.Message);
                }

                // Fetch paginated DTOs directly from the repository
                var lineDtos = await _lineRepository.GetAllLinesAsync(pageNumber, pageSize);

                return ProcessResult<IEnumerable<LineDto>>.Success(lineDtos);
            }
            catch (Exception ex)
            {
                return ProcessResult<IEnumerable<LineDto>>.Failure($"Error retrieving lines: {ex.Message}");
            }
        }

        public async Task<ProcessResult<Line>> GetLineByIdAsync(int id)
        {
            try
            {
                var line = await _lineRepository.GetByIdAsync(id);
                if (line == null)
                    return ProcessResult<Line>.Failure($"Line with ID {id} not found.");

                return ProcessResult<Line>.Success(line);
            }
            catch (Exception ex)
            {
                return ProcessResult<Line>.Failure($"Error retrieving line: {ex.Message}");
            }
        }

        public async Task<ProcessResult<bool>> UpdateLineAsync(Line line)
        {
            try
            {
                if (line == null)
                    return ProcessResult<bool>.Failure("Line cannot be null.");

                _unitOfWork.Lines.Update(line);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error updating line: {ex.Message}");
            }
        }
    }
}