using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTools.Helper
{
    public class ContentHeper
    {
        public StringContent ContentString(string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }
    }
}