using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.DTOs
{
    public class ColorSection
    {
        public string Main { get; set; }
        public string ContrastText { get; set; }
    }

    public class BackgroundSection
    {
        public string Default { get; set; }
        public string Paper { get; set; }
    }

    public class TextSection
    {
        public string Primary { get; set; }
        public string Secondary { get; set; }
        public string Disabled { get; set; }
    }

    public class ModeTheme
    {
        public string Mode { get; set; }
        public ColorSection Primary { get; set; }
        public ColorSection Secondary { get; set; }
        public BackgroundSection Background { get; set; }
        public TextSection Text { get; set; }
        public ColorSection Border { get; set; }
        public ColorSection Success { get; set; }
        public ColorSection Warning { get; set; }
        public ColorSection Error { get; set; }
    }

    public class ColorThemeResponse
    {
        public ModeTheme Light { get; set; }
        public ModeTheme Dark { get; set; }
    }
}
