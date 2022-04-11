 
using Microsoft.AspNetCore.Cors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using WallpaperEngineSlideShowServer.Models;

namespace WallpaperEngineSlideShowServer.Managers
{
    [EnableCors("MyAllowSpecificOrigins")]
    public class WallpaperManager
    {
       static  List<string> Images;
        public   WallpaperManager()
        {
            Images=GetImages();
        }
        public static  List<string> GetImages()
        {
            try
            {
                List<string> ap = new List<string>();
                string path = AppSettingsManager.GeWallPaperPath();
                if (path != null && System.IO.Directory.Exists(path))
                {
                    var ext = AppSettingsManager.GetExtentions();
                    if ( ext == null)
                    {
                        ext = "*.*";
                    }
                    var files = System.IO.Directory.GetFiles(path,ext);
                    if (files != null)
                    {
                        //foreach(var file in files)
                        //{
                        //    ap.Add(file.Replace("\\","/"));
                        //}
                        ap=files.ToList();
                    }
                   




                }


                return ap;
            }
            catch (Exception ex)
            {

                 Console.WriteLine(ex.ToString()); return null;
            }
        }

        public string GetImage()
        {
            try
            {
                string ap = null;
                int i = 0;
                if ( Images== null)
                {
                    Images = GetImages();
                }
                var images = Images; ;
                if (images != null)
                {
                    Random random = new Random();
                    i = random.Next(images.Count);
                    if (i == images.Count)
                    {
                        i = images.Count - 1;
                    }
                    ap = images[i];


                }

                return ap;
            }
            catch (Exception ex)
            {

                 Console.WriteLine(ex.ToString()); return null;
            }


        }
        public ImageData GetImageData()
        {
            try
            {
                ImageData ap = null;
                int i = 0;
                string timgpath, tap=null; ;
                if ( Images== null)
                {
                    Images= GetImages();
                }
                var images = Images;
                if (images != null)
                {
                    Random random = new Random();
                    i = random.Next(images.Count);
                    if (i == images.Count)
                    {
                        i = images.Count - 1;
                    }
                  timgpath = images[i];
                    
                    
                    if (timgpath != null && File.Exists(timgpath))
                    {
                      tap = this.ImageToBase64(timgpath);
                        ap = new ImageData();
                        ap.Base64String = tap;
                        ap.FileName=timgpath;
                        ap.Exif = this.GetMetadata(timgpath);

                        
                    }


                }
                return ap;


            }
            catch (Exception ex)
            {

                 Console.WriteLine(ex.ToString()); return null;
            }


        }


        public int GetInterval()
        {
            try
            {
                int ap = 0;
                ap = AppSettingsManager.GeInterval();

                return ap;
            }
            catch (Exception ex)
            {

                 Console.WriteLine(ex.ToString()); return -1;
            }

        }
        public string GetBackgroundColor()
        {
            try
            {
                string ap=null ;
                Boolean rndcolor= AppSettingsManager.GetRandomBackgroundColor();
                Byte[] rgb = new Byte[3];
                if (rndcolor)
                {
                  
                    Random random = new Random();
                    rgb[0] =(byte) random.Next(256);
                    rgb[1] = (byte)random.Next(256);
                    rgb[2] = (byte)random.Next(256);
                    ap = Convert.ToString(rgb[0]) + " , " + Convert.ToString(rgb[1]) + " , " + Convert.ToString(rgb[2]);
                }
                else
                {
                    ap = AppSettingsManager.GetBackgroundColor();
                }
                

                return ap;
            }
            catch (Exception ex)
            {

                 Console.WriteLine(ex.ToString()); return null;
            }

        }


        public ExifModel GetMetadata(string path)
        {
            try
            {
                ExifModel ap = null; ;
                if (path != null && File.Exists(path))
                {
                    using (Image image = Image.Load(path))
                    {
                        var exif = image.Metadata.ExifProfile ; 
                        if (exif != null)
                        {
                             ap= new ExifModel();
                              
                            ap.Exposure_Time= this.RationalToString ( (IExifValue<Rational>)exif.GetValue(ExifTag.ExposureTime));
                            ap.F_number = this.RationalToString(exif.GetValue(ExifTag.FNumber));
                            if (exif.GetValue(ExifTag.ExposureProgram) != null)
                                {
                                ap.Exposure_Program = exif.GetValue(ExifTag.ExposureProgram).Value;
                            }
                            if (exif.GetValue(ExifTag.Model) != null)
                            {
                                ap.Model = exif.GetValue(ExifTag.Model).Value;
                            }
                            ap.Orientation= this.RationalToString(exif.GetValue(ExifTag.Orientation));
                            ap.Resolution_Unit= this.RationalToString(exif.GetValue(ExifTag.ResolutionUnit));
                            ap.X_resolution = RationalToDouble(exif.GetValue(ExifTag.XResolution));
                            ap.Y_resolution= RationalToDouble(exif.GetValue(ExifTag.YResolution));
                            ap.YCbCr_Positioning = this.RationalToString(exif.GetValue(ExifTag.YCbCrPositioning));
                           ap.DateTaken= CheckifStringValueisNull(exif.GetValue(ExifTag.DateTimeOriginal));
                            ap.Brightness= this.RationalToString(exif.GetValue(ExifTag.BrightnessValue));
                            ap.GPSDateStamp = CheckifStringValueisNull(exif.GetValue(ExifTag.GPSDateStamp));
                            ap.GPSAltitude=RationalToString(exif.GetValue(ExifTag.GPSAltitude));
                            ap.GPSLongitude = RationalToArStringAR(exif.GetValue(ExifTag.GPSLongitude));
                            ap.GPSLatitude=RationalToArStringAR(exif.GetValue(ExifTag.GPSLatitude));
                            ap.ImageWidth = this.RationalToString(exif.GetValue(ExifTag.ImageWidth));
                            ap.ImageLength = this.RationalToString(exif.GetValue(ExifTag.ImageLength));
                         





                        }
                        image.Dispose();

                    }
                }
                    return ap;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString()); return null;
            }
        }

       


        public string ImageToBase64(string path)
        {
            String base64String = null;

            using (Image image = Image.Load(path)) 
            {

                

                var imgformat = Image.DetectFormat(path);
                base64String = image.ToBase64String(imgformat);
                image.Dispose();    
            }
                    
                    return base64String;

        }

       private string RationalToString(int numerator, int denominator)
        {
            try
            {
                return String.Format("{0}/{1}", numerator, denominator);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString()); return "";
            }
        }
        public string RationalToString(IExifValue<Number> exif)
        {
            try
            {
                string num = "";
                if (exif != null)
                {

                    num = exif.Value.ToString();

                }
                return String.Format("{0}", num);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString()); return "";
            }
        }
        public string CheckifStringValueisNull(IExifValue<string> exif)
        {
            try
            {
               string num="";
                if (exif != null)
                {

                    num = exif.Value;

                }
                return String.Format("{0}", num);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString()); return "";
            }
        }
        public string RationalToString(IExifValue<ushort> exif)
        {
            try
            {
                string num = "";
                if (exif != null)
                {

                    num = exif.Value.ToString();

                }
                return String.Format("{0}", num);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString()); return "";
            }
        }
        public string RationalToString(IExifValue<Rational> exif)
         {
            try
            {
                int numerator= 0, denominator = 0;
                if (exif != null)
                {
                    numerator = (int)exif.Value.Numerator;
                    denominator = (int)exif.Value.Denominator;


                }
                return String.Format("{0}/{1}", numerator, denominator);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString()); return "";
            }
        }
        public double RationalToDouble(IExifValue<Rational> exif)
        {
            try
            {
               double num = 0;
                if (exif != null)
                {
                    num=exif.Value.ToDouble();


                }
                return num;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString()); return -1;
            }
        }
        public string RationalToString(IExifValue<SignedRational> exif)
        {
            try
            {
                int numerator = 0, denominator = 0;
                if (exif != null)
                {
                    numerator = (int)exif.Value.Numerator;
                    denominator = (int)exif.Value.Denominator;


                }
                return String.Format("{0}/{1}", numerator, denominator);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString()); return "";
            }
        }
        public string RationalToArStringAR(IExifValue<Rational[]>rationals)
        {
            try
            {
                string ap = null;

                if( rationals != null )
                {
                    
                    
                    foreach (var r in rationals.Value)
                    {

                        ap += " " + this.RationalToString((int)r.Numerator, (int)r.Denominator);
                    }
                }


                return ap;
               

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString()); return null;
            }
        }

        
    }
 

    }

