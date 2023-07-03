using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiTools.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApiTools.Controllers
{
    [Route("api/[controller]")]
    public class Stopword : ControllerBase
    {
        private readonly AppDbContext _appDbcontext;
        public Stopword(AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }

        [HttpGet]
        public async Task<IActionResult> GetStopword()
        {

            return Ok
            (new
            {
                sucess = true,
                data = await _appDbcontext.Stopwords.ToListAsync()
            }
            );
        }


    }
}