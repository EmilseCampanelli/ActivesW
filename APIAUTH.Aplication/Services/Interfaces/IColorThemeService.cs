using APIAUTH.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IColorThemeService
    {
        Task<ColorThemeResponse> GetColorsAsync();
    }
}
