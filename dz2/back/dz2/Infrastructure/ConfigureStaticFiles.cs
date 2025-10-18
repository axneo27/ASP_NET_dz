using Microsoft.Extensions.FileProviders;
using System.IO;

namespace spr421_spotify_clone.Infrastructure
{
    public static class ConfigureStaticFiles
    {
        public static void AddStaticFiles(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            string rootPath = environment.ContentRootPath;
            string storagePath = Path.Combine(rootPath, "storage");

            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/storage",
                FileProvider = new PhysicalFileProvider(storagePath)
            });

            string[] folders = ["audio", "images"];

            foreach (var item in folders)
            {
                string path = Path.Combine(storagePath, item);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                app.UseStaticFiles(new StaticFileOptions
                {
                    RequestPath = $"/{item}",
                    FileProvider = new PhysicalFileProvider(path)
                });
            }
        }
    }
}
