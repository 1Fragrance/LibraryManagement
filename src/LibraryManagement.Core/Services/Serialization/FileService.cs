using LibraryManagement.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibraryManagement.Core.Services.Serialization
{
    public class FileService : ServiceBase
    {
        public FileService(DbDataSource context) : base(context)
        {
        }

        public IDictionary<int, string> GetCurrentFiles()
        {
            return Directory.GetFiles(Directory.GetCurrentDirectory()).Select((value, index) => new { index, value }).ToDictionary(w => w.index, w => w.value);
        }
    }
}
