using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using repositoryPattern.Api.Models;
using repositoryPattern.Entities;

namespace repositoryPattern.Api.Services {
    public interface ITokenFactoryService {
        string CreateAccessToken (User user);
    }

    public class TokenFactoryService : ITokenFactoryService {
        private readonly ISecurityService _securityService;
        private readonly IOptionsSnapshot<BearerTokensOptions> _configuration;

        public TokenFactoryService (ISecurityService securityService, IOptionsSnapshot<BearerTokensOptions> configuration) {
            _securityService = securityService;
            _configuration = configuration;
        }

        public string CreateAccessToken (User user) {
            try {

                var claims = new List<Claim> ();

                // Unique Id for all Jwt tokes
                claims.Add (new Claim (JwtRegisteredClaimNames.Jti, _securityService.CreateCryptographicallySecureGuid ().ToString (), ClaimValueTypes.String, _configuration.Value.Issuer));
                //Issuer
                //return(JwtRegisteredClaimNames.Iss+"---"+ _configuration.Value.Issuer+"---"+ ClaimValueTypes.String+"---"+ _configuration.Value.Issuer);
                claims.Add (new Claim (JwtRegisteredClaimNames.Iss, _configuration.Value.Issuer, ClaimValueTypes.String, _configuration.Value.Issuer));
                // Issued at
                claims.Add (new Claim (JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds ().ToString (), ClaimValueTypes.Integer64, _configuration.Value.Issuer));
                claims.Add (new Claim (ClaimTypes.NameIdentifier, user.Id.ToString (), ClaimValueTypes.String, _configuration.Value.Issuer));
                claims.Add (new Claim (ClaimTypes.Name, user.Name, ClaimValueTypes.String, _configuration.Value.Issuer));
                claims.Add (new Claim ("DisplayName", user.Name, ClaimValueTypes.String, _configuration.Value.Issuer));
                // custom data
                claims.Add (new Claim (ClaimTypes.UserData, user.Id.ToString (), ClaimValueTypes.String, _configuration.Value.Issuer));

                // add roles
                if (user.IsAdmin) {
                    claims.Add (new Claim (ClaimTypes.Role, CustomRoles.Admin, ClaimValueTypes.String, _configuration.Value.Issuer));
                }

                var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_configuration.Value.Key));
                var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);
                var now = DateTime.UtcNow;
                var token = new JwtSecurityToken (
                    issuer: _configuration.Value.Issuer,
                    audience: _configuration.Value.Audience,
                    claims: claims,
                    notBefore: now,
                    expires: now.AddMinutes (_configuration.Value.AccessTokenExpirationMinutes),
                    signingCredentials: creds);
                return new JwtSecurityTokenHandler ().WriteToken (token);
            } catch (Exception ex) {
                return ex.Message;
            }
        }
    }
}