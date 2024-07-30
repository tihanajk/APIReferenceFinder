using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace APIReferenceFinder
{

    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "API Reference Finder"),
        ExportMetadata("Description", "Custom API reference finder in power automate flows and webresources"),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "/9j/4AAQSkZJRgABAQEAYABgAAD/4QAiRXhpZgAATU0AKgAAAAgAAQESAAMAAAABAAEAAAAAAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcUFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAAgACADASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD3n4mavdafpFpZ6ZLcw6jqV0lvHLbW7TyRIPnkcIoJOEUjOOCwra8J6wNe8O2Wo+W0MkqFZonGGilUlZEPurqy/hV6Szt5L6G8eJWuoY3ijkPVFcqWA+uxfyryzx14iudJluotDEdpaG/WB9svkieaQjzHaTaxVQTj5QCWDHPNaU6bqOyInNQV2et0V5d4E8X3UtwsN5cC5gF0LKbEwnWKRgpUpLtDMCWVSGBILdRtOfUaVSm6bswhNTV0FeJasun+ML6/k0Bjq2iwagk08tvD5qpMjfPFs+86kru3qGHznsK7T4v6N4n8Q+GY9I8J3UFmLydYb+d3KyJbHh/LOOvr7ZxXQeD/AA3p3hLw9aaPo8KxWluuBgcu3dj6k06dR03zIKkFUVmeceHdGGgW8l5qCT2XhmxuftiLJbMJBlgViSNQXIEp3cqOMKARyPU9K1ODU4Wlt47uNVOCLm0lt2/KRVNZnj7TbnWPCd9Y2Ks1xN5YUK4U8SKSQTwCACa09J0/+zoZI/td5dl3Ll7qTewzjgcDA46UVKjqO7CnTVNWR//Z"),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "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"),
        ExportMetadata("BackgroundColor", "Lavender"),
        ExportMetadata("PrimaryFontColor", "Black"),
        ExportMetadata("SecondaryFontColor", "Gray")]
    public class APIReferenceFinderPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new APIReferenceFinderPluginControl();
        }



        /// <summary>
        /// Constructor 
        /// </summary>
        public APIReferenceFinderPlugin()
        {
            // If you have external assemblies that you need to load, uncomment the following to 
            // hook into the event that will fire when an Assembly fails to resolve
            // AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEventHandler);
        }

        /// <summary>
        /// Event fired by CLR when an assembly reference fails to load
        /// Assumes that related assemblies will be loaded from a subfolder named the same as the Plugin
        /// For example, a folder named Sample.XrmToolBox.MyPlugin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly AssemblyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Assembly loadAssembly = null;
            Assembly currAssembly = Assembly.GetExecutingAssembly();

            // base name of the assembly that failed to resolve
            var argName = args.Name.Substring(0, args.Name.IndexOf(","));

            // check to see if the failing assembly is one that we reference.
            List<AssemblyName> refAssemblies = currAssembly.GetReferencedAssemblies().ToList();
            var refAssembly = refAssemblies.Where(a => a.Name == argName).FirstOrDefault();

            // if the current unresolved assembly is referenced by our plugin, attempt to load
            if (refAssembly != null)
            {
                // load from the path to this plugin assembly, not host executable
                string dir = Path.GetDirectoryName(currAssembly.Location).ToLower();
                string folder = Path.GetFileNameWithoutExtension(currAssembly.Location);
                dir = Path.Combine(dir, folder);

                var assmbPath = Path.Combine(dir, $"{argName}.dll");

                if (File.Exists(assmbPath))
                {
                    loadAssembly = Assembly.LoadFrom(assmbPath);
                }
                else
                {
                    throw new FileNotFoundException($"Unable to locate dependency: {assmbPath}");
                }
            }

            return loadAssembly;
        }
    }
}