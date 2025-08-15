using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IGeoImportService
    {
        Task ImportAsync(string folderPath, CancellationToken ct = default);
    }
}
