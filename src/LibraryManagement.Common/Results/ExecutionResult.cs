using LibraryManagement.Common.Infrastructure;
using System.Collections.Generic;

namespace LibraryManagement.Common.Results
{
    public class ExecutionResult
    {
        private bool? _success;

        public bool IsSuccess
        {
            get => _success ?? Errors.Count == 0;
            set => _success = value;
        }

        public IList<ErrorInfo> Errors { get; set; }
    }
}
