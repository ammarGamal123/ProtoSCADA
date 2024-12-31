using ProtoSCADA.Service.Utilities;
using System;

namespace ProtoSCADA.Service.Validation
{
    public static class ValidatePagination
    {
        /// <summary>
        /// Validates the pagination parameters (pageNumber and pageSize).
        /// </summary>
        /// <param name="pageNumber">The page number to validate.</param>
        /// <param name="pageSize">The page size to validate.</param>
        /// <returns>A ProcessResult indicating whether the pagination parameters are valid.</returns>
        public static ProcessResult<bool> Validate(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                return ProcessResult<bool>.Failure("Page number must be greater than 0.");
            }

            if (pageSize < 1)
            {
                return ProcessResult<bool>.Failure("Page size must be greater than 0.");
            }

            return ProcessResult<bool>.Success(true);
        }
    }
}