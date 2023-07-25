using Microsoft.AspNetCore.Mvc;
using Google.Authenticator;
using Microsoft.AspNetCore.Authorization;
using ApiTools.Context;
using ApiTools.Model;
using ApiTools.Services;

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
    // [Authorize]
    public async Task<ActionResult<string>> GenerateQR(string login)
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
    // [HttpPost("validate")]
    // // [Authorize]
    // public ActionResult<bool> Validate(string code)
    // {

    //     RetornaCode auth = new RetornaCode(_appDbcontext);
    //     string key = auth.Authenticate(code).Result;


    //     return _tfa.ValidateTwoFactorPIN(key, code);
    // }

    [HttpPost("validatecode")]
    // [Authorize]
    public ActionResult<bool> ValidateCode(string code, string key)
    {

        return _tfa.ValidateTwoFactorPIN(key, code);
    }



}
