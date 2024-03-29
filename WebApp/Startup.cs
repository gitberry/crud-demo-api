﻿using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;


[assembly: OwinStartup(typeof(WebApp.Startup))]

namespace WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //cargo cult coding from here: https://www.c-sharpcorner.com/article/asp-net-web-api-2-creating-and-validating-jwt-json-web-token/
            app.UseJwtBearerAuthentication(
                            new JwtBearerAuthenticationOptions
                            {
                                AuthenticationMode = AuthenticationMode.Active,
                                TokenValidationParameters = new TokenValidationParameters()
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = AppConfig.Setting.JWTapiSite,
                                    ValidAudience = AppConfig.Setting.JWTclientSite,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.Setting.JWTsigningSecret))
                                }
                            });
        }
    }
}
