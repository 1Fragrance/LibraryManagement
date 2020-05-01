using System.Collections.Generic;

namespace LibraryManagement.Common.Results
{
    public class ExecutionResult : ResultBase
    {
        public IList<string> ErrorList { get; set; }
    }
}
