using System;
using BiilkLogin.Extensions;
using BiilkLogin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BiilkLogin.Infrastructure
{
    public class SessionVariables
    {
        public static SessionLayer GetSession(IServiceProvider serviceProvider)
        {
            var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var sessionlayer = accessor.HttpContext.Session.Get<SessionLayer>("session");
            if (sessionlayer == null)
            {
                sessionlayer = new SessionLayer
                {
                    Id=0,
                    Name=null,
                    Mail=null,
                    Username=null,
                    Passwordd=null,
                    Addresskey=0
                };
                accessor.HttpContext.Session.Set<SessionLayer>("session", sessionlayer);
            }
            return sessionlayer;
        }
    }
}
