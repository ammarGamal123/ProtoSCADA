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
    public class FactoryService : IFactoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFactoryRepository _factoryRepository;

        public FactoryService(IUnitOfWork unitOfWork, IFactoryRepository factoryRepository)
        {
            _unitOfWork = unitOfWork;
            _factoryRepository = factoryRepository ?? throw new ArgumentNullException(nameof(factoryRepository));
        }

        public async Task<ProcessResult<bool>> AddFactoryAsync(Factory factory)
        {
            try
            {
                if (factory == null)
                    return ProcessResult<bool>.Failure("Factory cannot be null.");

                await _unitOfWork.Factories.AddAsync(factory);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error adding factory: {ex.Message}");
            }
        }

        public async Task<ProcessResult<bool>> DeleteFactoryAsync(int id)
        {
            try
            {
                var factory = await _unitOfWork.Factories.GetByIdAsync(id);
                if (factory == null)
                    return ProcessResult<bool>.Failure($"Factory with ID {id} not found.");

                await _unitOfWork.Factories.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error deleting factory: {ex.Message}");
            }
        }

        public async Task<ProcessResult<IEnumerable<FactoryDto>>> GetAllFactoriesAsync(int pageNumber, int pageSize)
        {
            try
            {
                // Validate pagination parameters
                var validationResult = ValidatePagination.Validate(pageNumber, pageSize);
                if (!validationResult.IsSuccess)
                {
                    return ProcessResult<IEnumerable<FactoryDto>>.Failure(validationResult.Message);
                }

                // Fetch paginated DTOs directly from the repository
                var factoryDtos = await _factoryRepository.GetAllFactoriesAsync(pageNumber, pageSize);

                return ProcessResult<IEnumerable<FactoryDto>>.Success(factoryDtos);
            }
            catch (Exception ex)
            {
                return ProcessResult<IEnumerable<FactoryDto>>.Failure($"Error retrieving factories: {ex.Message}");
            }
        }


        public async Task<ProcessResult<Factory>> GetFactoryByIdAsync(int id)
        {
            try
            {
                var factory = await _factoryRepository.GetByIdAsync(id);
                if (factory == null)
                    return ProcessResult<Factory>.Failure($"Factory with ID {id} not found.");

                return ProcessResult<Factory>.Success(factory);
            }
            catch (Exception ex)
            {
                return ProcessResult<Factory>.Failure($"Error retrieving factory: {ex.Message}");
            }
        }

        public async Task<ProcessResult<bool>> UpdateFactoryAsync(Factory factory)
        {
            try
            {
                if (factory == null)
                    return ProcessResult<bool>.Failure("Factory cannot be null.");

                _unitOfWork.Factories.Update(factory);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error updating factory: {ex.Message}");
            }
        }
    }
}