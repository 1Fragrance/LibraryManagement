using System.Collections.Generic;
using LibraryManagement.Common.Infrastructure;
using LibraryManagement.Common.Results;
using LibraryManagement.Data;

namespace LibraryManagement.Core.Services
{
    public abstract class ServiceBase
    {
        protected DbDataSource Context { get; }

        protected ServiceBase(DbDataSource context)
        {
            this.Context = context;
        }

        public ExecutionResult BadResult(string message)
        {
            return new ExecutionResult()
            {
                IsSuccess = false,
                Errors = new List<ErrorInfo>
                {
                    new ErrorInfo {Message = message}            
                }
            };
        }

        public ExecutionResult SuccessResult()
        {
            return new ExecutionResult()
            {
                IsSuccess = true
            };
        }

    }
}
