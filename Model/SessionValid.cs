using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTools.Model
{
    public class SessionValid
    {


        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime Valid { get; set; }
        public int sn_ativo { get; set; }
    }
}