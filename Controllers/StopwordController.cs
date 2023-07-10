using ApiTools.Context;
using ApiTools.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ApiTools.Services;
using ApiTools.Repositories;

namespace ApiTools.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StopwordController : ControllerBase
    {
        private readonly AppDbContext _appDbcontext;
        public StopwordController(AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }

        [HttpGet]
        [Route("select")]
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

        [HttpGet]
        public async Task<IActionResult> GetStopword([FromQuery] int? id, [FromQuery] string? description)
        {
            if (description != null)
            {
                return Ok
                (new
                {
                    sucess = true,
                    data = await _appDbcontext.Stopwords.Where(x => x.Description.Contains(description)).ToListAsync()
                }
                );
            }
            else
            {
                var stopword = await _appDbcontext.Stopwords.FindAsync(id);
                if (stopword == null)
                {
                    return NotFound
                    (new
                    {
                        sucess = false,
                        message = "Item não encontrado"
                    }
                    );
                }
                return Ok
                (new
                {
                    sucess = true,
                    data = stopword
                }
                );
            }

        }
        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> PostStopword([FromBody] Stopword stopword)
        {
            try
            {
                _appDbcontext.Stopwords.Add(stopword);
                await _appDbcontext.SaveChangesAsync();
                return Ok
                (new
                {
                    sucess = true,
                    data = stopword
                }
                );
            }
            catch (Exception ex)
            {
                return BadRequest
                (new
                {
                    sucess = false,
                    message = ex.Message
                }
                );
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStopword(int id, [FromBody] Stopword stopword)
        {
            try
            {
                if (id != stopword.Id)
                {
                    return NotFound
                    (new
                    {
                        sucess = false,
                        message = "Id não encontrado"
                    }
                    );
                }
                _appDbcontext.Entry(stopword).State = EntityState.Modified;
                await _appDbcontext.SaveChangesAsync();
                return Ok
                (new
                {
                    sucess = true,
                    data = stopword
                }
                );
            }
            catch (Exception ex)
            {
                return BadRequest
                (new
                {
                    sucess = false,
                    message = ex.Message
                }
                );
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStopword(int id)
        {
            try
            {
                var stopword = await _appDbcontext.Stopwords.FindAsync(id);
                if (stopword == null)
                {
                    return NotFound
                    (new
                    {
                        sucess = false,
                        message = "Id não encontrado"
                    }
                    );
                }
                _appDbcontext.Stopwords.Remove(stopword);
                await _appDbcontext.SaveChangesAsync();
                return Ok
                (new
                {
                    sucess = true,
                    data = stopword
                }
                );
            }
            catch (Exception ex)
            {
                return BadRequest
                (new
                {
                    sucess = false,
                    message = ex.Message
                }
                );
            }
        }
    }
}