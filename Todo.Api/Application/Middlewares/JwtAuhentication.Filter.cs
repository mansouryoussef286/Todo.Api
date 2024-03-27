using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]

public class JWTAuthenticationFilter : ActionFilterAttribute
{
    public async override void OnActionExecuting(ActionExecutingContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (String.IsNullOrEmpty(token) || !VerifyTokenAndSetEmailClaim(token, context.HttpContext))
        {
            context.Result = new ObjectResult(new { success = false, message = "Unauthorized: User is not authenticated" })
            {
                StatusCode = 401
            };
            return;
        }

        base.OnActionExecuting(context);
    }


    private bool VerifyTokenAndSetEmailClaim(string token, HttpContext context)
    {
        try
        {
            string PublicKeyPath = "publickey.cer";
            X509Certificate2 PublicKey = new X509Certificate2(PublicKeyPath);
            var x509SecurityKey = new X509SecurityKey(PublicKey);

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = x509SecurityKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero // Adjust as needed
            };

            SecurityToken validatedToken;
            var TokenPrincipals = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            var jwtClaims = TokenPrincipals.Claims;

            var emailClaim = jwtClaims.FirstOrDefault(c => c.Type == "Email");
            string Email = emailClaim?.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries)[1];

            var userIdClaim = jwtClaims.FirstOrDefault(c => c.Type == "Id");
            var userIdString = userIdClaim?.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries)[1];
            if (!Int32.TryParse(userIdString, out int userId))
                return false;

            var claims = new[]
            {
                new Claim("Email", Email),
                new Claim("Id", userId.ToString())
            };

            var identity = new ClaimsIdentity(claims, "user");
            var principal = new ClaimsPrincipal(identity);

            context.Items["User"] = principal;
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

}
