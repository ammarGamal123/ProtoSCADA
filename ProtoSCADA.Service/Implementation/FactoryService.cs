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

    public class FactoryService : IFactoryService
        {
            private readonly IUnitOfWork _unitOfWork;

            public FactoryService(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ProcessResult<bool>> AddFactoryAsync(Factory factory)
            {
                if (factory == null)
                    return ProcessResult<bool>.Failure("Factory cannot be null.", false);

                try
                {
                    await _unitOfWork.Factorys.AddAsync(factory);
                    await _unitOfWork.SaveAsync();
                    return ProcessResult<bool>.Success("Factory added successfully.", true);
                }
                catch (Exception ex)
                {
                    return ProcessResult<bool>.Failure($"Error adding factory: {ex.Message}", false);
                }
            }

            public async Task<ProcessResult<bool>> DeleteFactoryAsync(int id)
            {
                try
                {
                    var factory = await _unitOfWork.Factorys.GetByIdAsync(id);
                    if (factory == null)
                        return ProcessResult<bool>.Failure($"No factory found with ID {id}.", false);

                    await _unitOfWork.Factorys.DeleteAsync(id);
                    await _unitOfWork.SaveAsync();
                    return ProcessResult<bool>.Success("Factory deleted successfully.", true);
                }
                catch (Exception ex)
                {
                    return ProcessResult<bool>.Failure($"Error deleting factory: {ex.Message}", false);
                }
            }

            public async Task<ProcessResult<IEnumerable<Factory>>> GetAllFactorysAsync()
            {
                try
                {
                    var factorys = await _unitOfWork.Factorys.GetAllAsync();
                    return ProcessResult<IEnumerable<Factory>>.Success("Factors retrieved successfully.", factorys);
                }
                catch (Exception ex)
                {
                    return ProcessResult<IEnumerable<Factory>>.Failure($"Error retrieving factorys: {ex.Message}", null);
                }
            }

            public async Task<ProcessResult<Factory>> GetFactorByIdAsync(int id)
            {
                try
                {
                    var factory = await _unitOfWork.Factorys.GetByIdAsync(id);
                    if (factory == null)
                        return ProcessResult<Factory>.Failure($"Factor with ID {id} not found.", null);

                    return ProcessResult<Factory>.Success("Factor retrieved successfully.", factory);
                }
                catch (Exception ex)
                {
                    return ProcessResult<Factory>.Failure($"Error retrieving factory: {ex.Message}", null);
                }
            }

            public async Task<ProcessResult<bool>> UpdateFactoryAsync(Factory factory)
            {
                if (factory == null)
                    return ProcessResult<bool>.Failure("Factory cannot be null.", false);

                try
                {
                    _unitOfWork.Factorys.Update(factory);
                    await _unitOfWork.SaveAsync();
                    return ProcessResult<bool>.Success("Factory updated successfully.", true);
                }
                catch (Exception ex)
                {
                    return ProcessResult<bool>.Failure($"Error updating factory: {ex.Message}", false);
                }
            }
        }
    }

