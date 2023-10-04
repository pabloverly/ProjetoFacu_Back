using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTools.Context;
using ApiTools.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTools.Services
{
    public class RetornaKey
    {
        private readonly AppDbContext _appDbcontext;

        public RetornaKey(AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }

        public async Task<string> VerificaKey(string username)
        {
            List<TwoFactor> twofactorModels = await _appDbcontext.TwoFactor
              .Where(x => x.login.Equals(username))
              .ToListAsync();
            // List<TwoFactor> twofactorModels = new List<TwoFactor>();

            // var data = await _appDbcontext.Contact.Where(x => x.Username.Contains(model)).ToListAsync();
            try
            {
                if (twofactorModels.Count == 0)
                {
                    // Não foram encontrados resultados, retorne uma mensagem apropriada
                    return "false";
                }

                return twofactorModels[0].Key;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

    }
}