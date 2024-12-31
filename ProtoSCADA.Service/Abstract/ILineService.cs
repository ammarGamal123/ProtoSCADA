using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Service.Abstract
{
    public interface ILineService
    {
        Task<ProcessResult<Line>> GetLineByIdAsync(int id);
        Task<ProcessResult<IEnumerable<LineDto>>> GetAllLinesAsync(int pageNumber , int pageSize);
        Task<ProcessResult<bool>> AddLineAsync(Line line);
        Task<ProcessResult<bool>> UpdateLineAsync(Line line);
        Task<ProcessResult<bool>> DeleteLineAsync(int id);
    }
}