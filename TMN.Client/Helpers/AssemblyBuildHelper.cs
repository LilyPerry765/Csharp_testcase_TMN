using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.Windows.Media.Imaging;

namespace TMN
{
    public class AssemblyBuildHelper
    {
        public void CreateAssembly(string AssemblyName, System.Drawing.Image image)
        {

            if (File.Exists("TempImages.dll"))
                File.Delete("TempImages.dll");

            if (File.Exists("Images.dll"))
                File.Move("Images.dll", "TempImages.dll");

            AppDomain domain = AppDomain.CurrentDomain;
            AssemblyName name = new AssemblyName("Images");
            AssemblyBuilder builder = domain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Save);
            ModuleBuilder moduleBuilder = builder.DefineDynamicModule(AssemblyName, AssemblyName, false);

            IResourceWriter writer = moduleBuilder.DefineResource("MyResource.resources", "This DLL saves a image for background image .");
            writer.AddResource("img", image);

            builder.Save(AssemblyName);
        }

        private BitmapSource ConvertGDI_To_WPF(System.Drawing.Bitmap bm)
        {
            BitmapSource bms = null;
            if (bm != null)
            {
                IntPtr h_bm = bm.GetHbitmap();
                bms = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(h_bm, IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                return bms;
            }
            else
            {
                return null;
            }
        }

        public BitmapSource GetImage(string AssemblyName)
        {
            try
            {
                ResourceManager manager = new ResourceManager(@"MyResource", Assembly.LoadFrom(AssemblyName));
                return ConvertGDI_To_WPF((System.Drawing.Bitmap)manager.GetObject("img"));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
