using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using MahCoadz.Kryptow;
using MahCoadz.Uzer;

namespace MahCoadz.JWT
{
    public class JwtTool
    {
        public static Object ValidateAndGenerateToken(
            string givenUserID,
            string givenUserPassword,
            List<IUzer> givenUsers,
            string givenAPISecret,
            string givenIssuerSite,
            string givenClientSite,
            string givenTokenClaimName,
            DateTime givenTokenExpires
            )
        {
            IUzer thisUzer = givenUsers.Find(z => z.uzerId.ToLower() == givenUserID.ToLower());
            if (thisUzer != null)
            {
                if (thisUzer.hashedPassword == Kryptow.KryptowTool.HashPassword(givenUserPassword, thisUzer.hashseed))
                {
                    // the user is authenticated - generate a token and return it..
                    return GenerateToken(
                        givenAPISecret
                        , givenIssuerSite
                        , givenClientSite
                        , givenTokenClaimName
                        , givenTokenExpires);
                }
            }
            return null;
        }

        public static Object GenerateToken(
            string givenAPISecret,
            string givenIssuerSite,
            string givenClientSite,
            string givenTokenClaimName,
            DateTime givenTokenExpires
            )
        {
            var thisSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(givenAPISecret));
            var thisCredentials = new SigningCredentials(thisSecurityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            //observation: I'm PRESUMING that a GUID adds a bunch of randomness to the token - thus the signature won't betray the validating secret...
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            //permClaims.Add(new Claim("userid", "1")); // redacted to keep api minimal
            permClaims.Add(new Claim("name", givenTokenClaimName)); // our key to a user table somewhere

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(
                            givenIssuerSite,
                            givenClientSite,
                            permClaims,
                            expires: givenTokenExpires,
                            signingCredentials: thisCredentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };
        }

        public static bool TryRetrieveToken(HttpRequestMessage givenRequest, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!givenRequest.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            // next two lines are a kludge because the client WAS sending JSON instead of encoded string...
            var badhead = "{\"data\":\"";
            if (token.Substring(0, badhead.Length) == badhead) { token = token.Substring(badhead.Length); token = token.Substring(0, token.Length - 2); }
            return true; 
        }

        public IUzer ValidateToken(HttpRequestMessage givenRequest, List<IUzer> givenUsers, string givenSecret, string givenJwtClientSite, string givenJwtApiSite )
        {
            TokenValidationParameters theseParams = generateTokenValidationParameters(
                givenSecret,
                givenJwtClientSite,
                givenJwtApiSite,
                true,
                true);

            string requestHeaderJwtTokenJSON;
            if (!TryRetrieveToken(givenRequest, out requestHeaderJwtTokenJSON)) { return null; } // false; }

            SecurityToken unusedSecurityToken;
            JwtSecurityTokenHandler tmpJwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                ClaimsPrincipal result = tmpJwtSecurityTokenHandler.ValidateToken(requestHeaderJwtTokenJSON, theseParams, out unusedSecurityToken);
                if (result.Identity.IsAuthenticated)
                {
                    // testing indicates that is also properly fails expired tokens so..
                    // the following is the belt accompanying the suspenders:
                    string claimExpiryText = result.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
                    DateTime claimExpiry = ConvertFromUnixTimestamp(claimExpiryText); // new DateTime(0);
                    if (claimExpiry >= DateTime.UtcNow)
                    {
                        // And of course - we need to be sure that the Uzer exists in OUR context - not just the MS stack'S JWT validation
                        string claimUzerID = result.Claims.First(cc => cc.Type == "name")?.Value;
                        if (!(claimUzerID is null))
                        {
                            IUzer theUzer = givenUsers.Find(uu => uu.uzerId.ToLower() == claimUzerID.ToLower());
                            if (!(theUzer is null))
                            {
                                return theUzer;
                            }
                        }                        
                    }
                }
            }
#if DEBUG
            catch (Exception ex)
            {
                var thisEX = ex; // Only needed for debugging..
#else
                    catch 
                    {
#endif
                return null; 
            }
            return null; 
        }

        public TokenValidationParameters generateTokenValidationParameters(
            string givenSecret,
            string givenAudience,
            string givenIssuer,
            bool givenLifetime,
            bool givenIssuerSigningKey
            )
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(givenSecret));
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidAudience            = givenAudience, 
                ValidIssuer              = givenIssuer, 
                ValidateLifetime         = givenLifetime, 
                ValidateIssuerSigningKey = givenIssuerSigningKey, 
                LifetimeValidator        = this.LifetimeValidator,
                IssuerSigningKey         = securityKey
            };
            return validationParameters;
        }

        public static DateTime ConvertFromUnixTimestamp(string givenTimestampText)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            int timestamp = 0;
            int.TryParse(givenTimestampText, out timestamp);
            return origin.AddSeconds(timestamp); 
        }

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }

    }
}
