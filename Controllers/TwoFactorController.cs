using Microsoft.AspNetCore.Mvc;
using Google.Authenticator;
using Microsoft.AspNetCore.Authorization;
using ApiTools.Context;
using ApiTools.Model;
using ApiTools.Services;
using Microsoft.EntityFrameworkCore;

namespace ApiTools.Controllers;

[ApiController]
[Route("[controller]")]
public class TwoFactorController : ControllerBase
{

    private readonly AppDbContext _appDbcontext;
    private readonly TwoFactorAuthenticator _tfa;
    private readonly ILogger<TwoFactorController> _logger;

    public TwoFactorController(ILogger<TwoFactorController> logger, AppDbContext appDbcontext)
    {
        _logger = logger;
        _tfa = new TwoFactorAuthenticator();
        _appDbcontext = appDbcontext;
    }

    [HttpGet("generateqr")]
    //[Authorize]
    public async Task<ActionResult<string>> GenerateQR(string login)
    {

        string responseimage = VerificaLogin(login).Result.Value;

        if (responseimage != "false")
            return responseimage;
        else
        {
            string key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
            SetupCode setupInfo = _tfa.GenerateSetupCode("Rede Gazeta (2FA)", login, key, false, 3);

            Console.WriteLine($"Email: {login} - Key: {key}"); //se possuir banco de dados conferir a chave


            _appDbcontext.TwoFactor.Add(new TwoFactor
            {
                login = login,
                Key = key,
                responseimage = setupInfo.QrCodeSetupImageUrl
            });
            await _appDbcontext.SaveChangesAsync();

            return setupInfo.QrCodeSetupImageUrl.ToString();
        }
    }
    [HttpGet("VerificaLogin")]
    //[Authorize]
    public async Task<ActionResult<string>> VerificaLogin(string login)
    {

        List<TwoFactor> twofactorModels = await _appDbcontext.TwoFactor
               .Where(x => x.login.Equals(login))
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

            return twofactorModels[0].responseimage;

        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }
    // [HttpPost("validate")]
    // // [Authorize]
    // public ActionResult<bool> Validate(string code)
    // {

    //     RetornaCode auth = new RetornaCode(_appDbcontext);
    //     string key = auth.Authenticate(code).Result;


    //     return _tfa.ValidateTwoFactorPIN(key, code);
    // }

    //VERIFICA QRCODE E A CHAVE SECRETA
    [HttpPost("validatecode")]
    // [Authorize]
    public ActionResult<bool> ValidateCode(string code, string key)
    {
        return _tfa.ValidateTwoFactorPIN(key, code);
    }

    //VERIFICA BUSCA A CHAVE SECRETA NO BANCO E VALIDA O QRCODE
    [HttpPost("validate")]
    // [Authorize]
    public async Task<ActionResult<bool>> Validate(string login, string key)
    {
        string codesecret = "";
        List<TwoFactor> twofactorModels = await _appDbcontext.TwoFactor
              .Where(x => x.login.Equals(login))
              .ToListAsync();
        // List<TwoFactor> twofactorModels = new List<TwoFactor>();

        // var data = await _appDbcontext.Contact.Where(x => x.Username.Contains(model)).ToListAsync();
        try
        {
            if (twofactorModels.Count == 0)
            {
                // Não foram encontrados resultados, retorne uma mensagem apropriada
                // return false;
            }
            codesecret = twofactorModels[0].Key;
            // return twofactorModels[0].Key;

        }
        catch (Exception ex)
        {
            return false;
        }

        return _tfa.ValidateTwoFactorPIN(codesecret, key);
    }



}
