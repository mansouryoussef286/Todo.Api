using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Security.Claims;

public class JWTAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public JWTAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var headers = context.Request.Headers;
        if (context.Request.Headers.TryGetValue("Authorization", out var headerValue))
        {
            string[] AccessToken = headerValue.ToString().Split(' ', StringSplitOptions.TrimEntries);

            if((AccessToken.Length != 2) || AccessToken[0] != "AccessToken" || !VerifyTokenAndSetEmailClaim(AccessToken[1], context)){
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized: User is not authenticated");
                return;
            }
            await _next(context);
            return;
        }
    }

    private bool VerifyTokenAndSetEmailClaim(string Token, HttpContext context){
        string PublicKeyPath = "publickey.cer";
        X509Certificate2 PublicKey = new X509Certificate2(PublicKeyPath);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new X509SecurityKey(PublicKey)
        };
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var TokenPrincipals = handler.ValidateToken(Token, validationParameters, out var validatedToken);

            var jwtClaims = TokenPrincipals.Claims;
            var emailClaim = jwtClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            string Email = emailClaim?.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries)[1];

            if(Email == null)
                return false;
            
            var claims = new[]
            {
                new Claim("Email", Email),
                new Claim("Token", Token),
            };

            var identity = new ClaimsIdentity(claims, "user");
            var principal = new ClaimsPrincipal(identity);

            context.Items["User"] = principal;

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}