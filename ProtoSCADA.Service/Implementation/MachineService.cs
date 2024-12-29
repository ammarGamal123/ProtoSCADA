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
    public class MachineService : IMachineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMachineRepository _machineRepository;

        public MachineService(IUnitOfWork unitOfWork, IMachineRepository machineRepository = null)
        {
            _unitOfWork = unitOfWork;
            _machineRepository = machineRepository;
        }

        public async Task<ProcessResult<bool>> AddMachineAsync(Machine machine)
        {
            try
            {
                if (machine == null)
                    return ProcessResult<bool>.Failure("Machine cannot be null.");

                await _unitOfWork.Machines.AddAsync(machine);
                await _unitOfWork.SaveAsync(); // Ensure changes are saved
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error adding machine: {ex.Message}");
            }
        }

        public async Task<ProcessResult<bool>> DeleteMachineAsync(int id)
        {
            try
            {
                var machine = await _unitOfWork.Machines.GetByIdAsync(id);
                if (machine == null)
                    return ProcessResult<bool>.Failure($"Machine with ID {id} not found.");

                await _unitOfWork.Machines.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error deleting machine: {ex.Message}");
            }
        }

        public async Task<ProcessResult<IEnumerable<Machine>>> GetAllMachinesAsync()
        {
            try
            {
                var machines = await _machineRepository.GetAllAsync();
                return ProcessResult<IEnumerable<Machine>>.Success(machines);
            }
            catch (Exception ex)
            {
                return ProcessResult<IEnumerable<Machine>>.Failure($"Error retrieving machines: {ex.Message}");
            }
        }

        public async Task<ProcessResult<Machine>> GetMachineByIdAsync(int id)
        {
            try
            {
                var machine = await _machineRepository.GetByIdAsync(id);
                if (machine == null)
                    return ProcessResult<Machine>.Failure($"Machine with ID {id} not found.");

                return ProcessResult<Machine>.Success(machine);
            }
            catch (Exception ex)
            {
                return ProcessResult<Machine>.Failure($"Error retrieving machine: {ex.Message}");
            }
        }

        public async Task<ProcessResult<bool>> UpdateMachineAsync(Machine machine)
        {
            try
            {
                if (machine == null)
                    return ProcessResult<bool>.Failure("Machine cannot be null.");

                _unitOfWork.Machines.Update(machine);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error updating machine: {ex.Message}");
            }
        }
    }
}
