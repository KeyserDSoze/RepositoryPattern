using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Api
{
    public static class WebApplicationExtensions
    {
        internal static List<RepositoryPatternService> Services = new();
        public static WebApplication AddApi(WebApplication app)
        {
            //foreach (var populationServiceSetting in Services)
            //{
            //    app.MapGet($"api/{populationServiceSetting.EntityType.Name.ToLower()}", (x) =>
            //    {
            //        var keyValue = x.Request.Query[populationServiceSetting.KeyName!.ToLower()];
            //        x.RequestServices.GetService(populationServiceSetting.InternalSettingsType)
            //    });
            //}
            return app;
        }
    }
}
