using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTools.Context;
using ApiTools.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiTools.Services
{
    public class RetornaCode
    {
        private readonly AppDbContext _appDbcontext;

        public RetornaCode(AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }
        public async Task<string> Authenticate([FromQuery] string username)
        {
            List<TwoFactor> twofactorModels = new List<TwoFactor>();

            // var data = await _appDbcontext.Contact.Where(x => x.Username.Contains(model)).ToListAsync();
            try
            {
                twofactorModels = _appDbcontext.TwoFactor.Where(x => x.login.Equals(username)).ToList();

                if (twofactorModels[0].login == null)
                    return null;

                return twofactorModels[0].login;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}