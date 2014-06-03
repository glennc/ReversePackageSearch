using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.StaticFiles;
using Microsoft.AspNet.FileSystems;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.OptionsModel;

namespace ReversePackageSearch
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
            var configuration = new Configuration();
            configuration.AddEnvironmentVariables();

            // Set up application services
            app.UseServices(services =>
            {
                services.Add(OptionsServices.GetDefaultServices());
                services.SetupOptions<BlobStorageOptions>(options =>
                {
                    options.ConnectionString = configuration.Get("ReversePackageSearch:BlobStorageConnection");
                });
                // Add MVC services to the services container
                services.AddMvc();
            });

            // Add static files
            app.UseStaticFiles(new StaticFileOptions { FileSystem = new PhysicalFileSystem("content") });
            // Add MVC to the request pipeline
            app.UseMvc();
        }
    }
}
;