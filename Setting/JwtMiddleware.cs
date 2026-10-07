using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using shoppingapi2.Dtos.Objects;

namespace shoppingapi2.Setting;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, AppSettings appSettings)
    {
        _next = next;
        _appSettings = appSettings;
    }
    public async Task InvokeAsync(HttpContext context)
    {

        var token = context.Request.Headers.Authorization
                       .FirstOrDefault()?
                       .Split(" ")[^1]
                   ?? context.Request.Query["token"].ToString();
        if (string.IsNullOrWhiteSpace(token))
        {
            await _next(context);
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret!);
        tokenHandler.ValidateToken(token,
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            },
            out SecurityToken validatedToken);
        var jwtToken = (JwtSecurityToken)validatedToken;
        var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

        context.Items["user"] = userId;
        context.Items["token"] = token;

        await _next(context);

    }
}