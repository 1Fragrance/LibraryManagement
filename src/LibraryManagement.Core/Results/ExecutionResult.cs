using System.Collections.Generic;

namespace LibraryManagement.Core.Results
{
    public class ExecutionResult : ResultBase
    {
        public IList<string> ErrorList { get; set; }
    }
}
