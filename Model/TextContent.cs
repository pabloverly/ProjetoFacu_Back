using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTools.Model
{
    public interface TextContent
    {
        public string Text { get; set; }
        public string? TypeSudio { get; set; }
        public string? AudioDevice { get; set; }
        public float? Pitch { get; set; }
        public float? Speed { get; set; }

    }
}