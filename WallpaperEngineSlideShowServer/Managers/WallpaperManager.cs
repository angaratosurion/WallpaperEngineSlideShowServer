using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
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
        public string GetImageData()
        {
            try
            {
                string ap = null;
                int i = 0;
                string timgpath,tap=null;
                if ( Images== null)
                {
                    Images= GetImages();
                }
                var images = GetImages();
                if (images != null)
                {
                    Random random = new Random();
                    i = random.Next(images.Count);
                    if (i == images.Count)
                    {
                        i = images.Count - 1;
                    }
                  timgpath = images[i];
                    
                    Console.WriteLine(timgpath);
                    if (timgpath != null && File.Exists(timgpath))
                    {
                        ap = this.ImageToBase64(timgpath);
                        
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
                               var met= exif.GetValue(ExifTag.Acceleration);
                            ap.Acceleration = this.RationalToString((int) exif.GetValue(ExifTag.Acceleration).Value.Numerator, 
                                (int)exif.GetValue(ExifTag.Acceleration).Value.Denominator);
                            ap.AmbientTemperature = this.RationalToString(exif.GetValue(ExifTag.AmbientTemperature).Value.Numerator,
                                exif.GetValue(ExifTag.AmbientTemperature).Value.Denominator);
                            ap.Exposure_Time= this.RationalToString((int)exif.GetValue(ExifTag.ExposureTime).Value
                                .Numerator, (int)exif.GetValue(ExifTag.ExposureTime).Value.Denominator);
                            ap.F_number=this.RationalToString((int)exif.GetValue(ExifTag.FNumber).Value.Numerator, 
                                (int)exif.GetValue(ExifTag.FNumber).Value.Denominator) ;
                            ap.Exposure_Program = exif.GetValue(ExifTag.ExposureProgram).Value;
                            ap.Model=exif.GetValue(ExifTag.Model).Value;
                            ap.Orientation= exif.GetValue(ExifTag.Orientation).Value;
                            ap.Resolution_Unit= exif.GetValue(ExifTag.ResolutionUnit).Value;
                            ap.X_resolution= exif.GetValue(ExifTag.XResolution).Value.ToDouble();
                            ap.Y_resolution= exif.GetValue(ExifTag.YResolution).Value.ToDouble();
                            ap.YCbCr_Positioning= exif.GetValue(ExifTag.YCbCrPositioning).Value;
                           ap.DateTaken= exif.GetValue(ExifTag.DateTimeOriginal).Value;
                            ap.Brightness= this.RationalToString(exif.GetValue(ExifTag.BrightnessValue).Value.Numerator,
                                exif.GetValue(ExifTag.BrightnessValue).Value.Denominator);
                            ap.GPSDateStamp= exif.GetValue(ExifTag.GPSDateStamp).Value;
                            ap.GPSAltitude=RationalToString((int)exif.GetValue(ExifTag.GPSAltitude).Value.Numerator,
                                (int)exif.GetValue(ExifTag.GPSAltitude).Value.Denominator);
                            ap.GPSLongitude = RationalToArStringAR(exif.GetValue(ExifTag.GPSLongitude).Value);
                            ap.GPSLatitude=RationalToArStringAR(exif.GetValue(ExifTag.GPSLatitude).Value);
                            ap.ImageWidth = exif.GetValue(ExifTag.ImageWidth).Value.ToString();
                            ap.ImageLength = exif.GetValue(ExifTag.ImageLength).Value.ToString();



                        }

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
            }
                    
                    return base64String;
               
            }
        public string RationalToString(int numerator,int denominator)
        {
            try
            { 
                return String.Format("{0}/{1}", numerator, denominator);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString()); return null;
            }
        }
        public string RationalToArStringAR(SixLabors.ImageSharp.Rational[] rationals)
        {
            try
            {
                string ap = null;

                if( rationals != null )
                {
                    foreach( SixLabors.ImageSharp.Rational r in rationals )
                    {
                        ap += " "+ this.RationalToString((int)r.Numerator, (int)r.Denominator);
                    }
                }


                return ap;
               

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString()); return null;
            }
        }

        private void RationalToString(int numerator, uint denominator)
        {
            throw new NotImplementedException();
        }
    }
 

    }

