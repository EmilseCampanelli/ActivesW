using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Repository;
using Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class ColorThemeService : IColorThemeService
    {
        private readonly IColorThemeRepository _repository;

        public ColorThemeService(IColorThemeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ColorThemeResponse> GetColorsAsync()
        {
            var data = await _repository.GetAllAsync();

            ModeTheme BuildMode(string mode) => new ModeTheme
            {
                Mode = mode,
                Primary = new ColorSection
                {
                    Main = data.FirstOrDefault(x => x.Mode == mode && x.Section == "primary" && x.Property == "main")?.Value,
                    ContrastText = data.FirstOrDefault(x => x.Mode == mode && x.Section == "primary" && x.Property == "contrastText")?.Value,
                },
                Secondary = new ColorSection
                {
                    Main = data.FirstOrDefault(x => x.Mode == mode && x.Section == "secondary" && x.Property == "main")?.Value,
                    ContrastText = data.FirstOrDefault(x => x.Mode == mode && x.Section == "secondary" && x.Property == "contrastText")?.Value,
                },
                Background = new BackgroundSection
                {
                    Default = data.FirstOrDefault(x => x.Mode == mode && x.Section == "background" && x.Property == "default")?.Value,
                    Paper = data.FirstOrDefault(x => x.Mode == mode && x.Section == "background" && x.Property == "paper")?.Value,
                },
                Text = new TextSection
                {
                    Primary = data.FirstOrDefault(x => x.Mode == mode && x.Section == "text" && x.Property == "primary")?.Value,
                    Secondary = data.FirstOrDefault(x => x.Mode == mode && x.Section == "text" && x.Property == "secondary")?.Value,
                    Disabled = data.FirstOrDefault(x => x.Mode == mode && x.Section == "text" && x.Property == "disabled")?.Value,
                },
                Border = new ColorSection
                {
                    Main = data.FirstOrDefault(x => x.Mode == mode && x.Section == "border" && x.Property == "main")?.Value,
                },
                Success = new ColorSection
                {
                    Main = data.FirstOrDefault(x => x.Mode == mode && x.Section == "success" && x.Property == "main")?.Value,
                    ContrastText = data.FirstOrDefault(x => x.Mode == mode && x.Section == "success" && x.Property == "contrastText")?.Value,
                },
                Warning = new ColorSection
                {
                    Main = data.FirstOrDefault(x => x.Mode == mode && x.Section == "warning" && x.Property == "main")?.Value,
                    ContrastText = data.FirstOrDefault(x => x.Mode == mode && x.Section == "warning" && x.Property == "contrastText")?.Value,
                },
                Error = new ColorSection
                {
                    Main = data.FirstOrDefault(x => x.Mode == mode && x.Section == "error" && x.Property == "main")?.Value,
                    ContrastText = data.FirstOrDefault(x => x.Mode == mode && x.Section == "error" && x.Property == "contrastText")?.Value,
                }
            };

            return new ColorThemeResponse
            {
                Light = BuildMode("light"),
                Dark = BuildMode("dark")
            };
        }
    }
}
