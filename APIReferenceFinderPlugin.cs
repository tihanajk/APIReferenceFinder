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
        ExportMetadata("SmallImageBase64", "/9j/4AAQSkZJRgABAQEAlgCWAAD/4QAiRXhpZgAATU0AKgAAAAgAAQESAAMAAAABAAEAAAAAAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcUFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAAgACADASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD0/wAZRxR+H/DOk6NYbdRvo0naS0tA8iRRIrFjgfdMhhRs8bXau28Mpo+veH7DU4tJtIvtMQdomt0DRP0ZDx1VgVPuDVzw/Z2/2DTb3yU+1/YYofNx82zAO36Z5rzzxhr93pxeDQI0t7J9Ra2WJJ/s/nTO7GWR5ArMo37+FAJIJJ5wAD0r+xdL/wCgbZf9+F/wqjr+jaYuhaiRp1kCLaQgiBePlPtXHeC/Fd4Lnyb2bzoo7tbG4j8/7QIZHC7Cku1WYZdAQwz82eNuG7/xB/yAdS/69pf/AEE0AHh//kA6b/17Rf8AoIryHXrfTvEc9wliU1bQodT+0vcW8BuojJlmkgcKDyGYnONuGXnNWfiXqOt6p4N8O6B4Qu7OKPVFjttR1AXaK1nDsG7AznkZGR0xjqwruPBFh4Z8G+G7TRtHvbGO3gXlvPTMjd2PPU0AcR4W0dNItZN6my8L2F1/aHnzW7QCGNWEgjUEAv8AvBncBgJxnIFei3Os6frHh3VX025WdUtX3YBG3KNjII9jWT8Sr+zvvAWu2tlfW0lzLaukaRSqzFj6DuanuLy3tNA1dbvxLHqLSQSFPNeBNnyHgbAufxyaAP/Z"),
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