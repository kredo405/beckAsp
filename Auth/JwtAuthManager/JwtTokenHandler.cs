using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Data;
using Auth.JwtAuthManager.Models;
using Microsoft.IdentityModel.Tokens;

namespace Auth.JwtAuthManager;

public class JwtTokenHandler
{
      public const string JWT_SECURITY_KEY = "5d+f4ZdXvShHk3wBUZSd5DbTGivwVEwSiE7sX3YxT6M";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly List<UserAccount> _userAccountsList;
        private readonly AuthContext _ctx;

        public JwtTokenHandler(AuthContext ctx)
        {
            _ctx = ctx;
        }

        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrWhiteSpace(authenticationRequest.UserName) ||
                string.IsNullOrWhiteSpace(authenticationRequest.Password))
                return null;
            /*Validation*/
            var userAccount = _ctx.Users.FirstOrDefault(x =>
                x.Name == authenticationRequest.UserName /*&& x.PasswordHash == authenticationRequest.Password*/);
            if (userAccount == null) return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.UserName),
                //new Claim(ClaimTypes.Role, userAccount.Role)
            });

            var signingCredentials = new SigningCredentials
            (
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature
            );

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials

            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                UserName = userAccount.Name,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token
            };
        }
}