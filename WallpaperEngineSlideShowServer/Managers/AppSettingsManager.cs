using System.Runtime.InteropServices;

namespace WallpaperEngineSlideShowServer
{
    public static class AppSettingsManager
    {
        static string pathwithextention;//= System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                                        // static string path;//= System.IO.Path.GetDirectoryName(pathwithextention).Replace("file:\\", "");
                                        //return View();

        static ConfigurationBuilder builder;
        static IConfigurationRoot config;//= builder.Build();//
        public static void Init()
        {
            pathwithextention = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

            string path = "";//= System.IO.Path.GetDirectoryName(pathwithextention).Replace("file:\\", "");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = System.IO.Path.GetDirectoryName(pathwithextention).Replace("file:\\", "");
            }
            else
            {
                path = System.IO.Path.GetDirectoryName(pathwithextention).Replace("file:", "");
            }
            //return View();
            builder = (ConfigurationBuilder)new ConfigurationBuilder()
                      .SetBasePath(path)
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //  var config = builder.Build();//


            config = builder.Build();//
        }

        
        public static string GeWallPaperPath()
        {
            try
            {
                Init();
                return config.GetValue<string>("ApppSettings:WallPaperPath");
            }
            catch (Exception ex )
            {

                 
                return null;
            }
        }
        public static int GeInterval()
        {
            try
            {
                Init();
                return config.GetValue<int>("ApppSettings:Interval");
            }
            catch (Exception ex )
            {


                return 0;
            }
        }
        public static string GetExtentions()
        {
            try
            {
                Init();
                return config.GetValue<string>("ApppSettings:Extentions");
            }
            catch (Exception ex )
            {


                return null;
            }
        }
        public static string GetBackgroundColor()
        {
            try
            {
                Init();
                return config.GetValue<string>("ApppSettings:Background-Color");
            }
            catch (Exception ex )
            {


                return null;
            }
        }
        public static  Boolean GetRandomBackgroundColor()
        {
            try
            {
                Init();
                return config.GetValue<Boolean>("ApppSettings:Random-Background-Color");
            }
            catch (Exception ex )
            {


                return false;
            }
        }
    }
}
