using Microsoft.AspNetCore.Mvc;
using Google.Authenticator;
using Microsoft.AspNetCore.Authorization;

namespace ApiTools.Controllers;

[ApiController]
[Route("[controller]")]
public class TwoFactorController : ControllerBase
{

    private readonly TwoFactorAuthenticator _tfa;
    private readonly ILogger<TwoFactorController> _logger;

    public TwoFactorController(ILogger<TwoFactorController> logger)
    {
        _logger = logger;
        _tfa = new TwoFactorAuthenticator();
    }

    [HttpGet("generateqr")]
    [Authorize]
    public ActionResult<string> GenerateQR(string email)
    {

        string key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
        SetupCode setupInfo = _tfa.GenerateSetupCode("Rede Gazeta (2FA)", email, key, false, 3);

        Console.WriteLine($"Email: {email} - Key: {key}"); //se possuir banco de dados conferir a chave

        return setupInfo.QrCodeSetupImageUrl;
    }

    [HttpPost("validatecode")]
    [Authorize]
    public ActionResult<bool> ValidateCode(string code, string key)
    {

        return _tfa.ValidateTwoFactorPIN(key, code);
    }
}
