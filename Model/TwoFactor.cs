using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTools.Model
{
    public class TwoFactor
    {
        public int id { get; set; }
        public string login { get; set; }
        public string Key { get; set; }
        public string responseimage { get; set; }
    }
}