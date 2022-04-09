using Microsoft.AspNetCore.Cors;

namespace WallpaperEngineSlideShowServer.Managers
{
    [EnableCors("MyAllowSpecificOrigins")]
    public class WallpaperManager
    {

        public List<string> GetImages()
        {
            try
            {
                List<string> ap = new List<string>();
                string path = AppSettingsManager.GeWallPaperPath();
                if (path != null && Directory.Exists(path))
                {
                    var ext = AppSettingsManager.GetExtentions();
                    if ( ext == null)
                    {
                        ext = "*.*";
                    }
                    var files = Directory.GetFiles(path,ext);
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
                var images = this.GetImages();
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
                var images = this.GetImages();
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
                        tap = this.ImageToBase64(timgpath);
                        string fileext = Path.GetExtension(timgpath)
;
                        fileext=fileext.Replace(".", "");
                        ap = String.Format("data:image/{0};base64,{1}",fileext,tap);
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
        public string ImageToBase64(string path)
        {
            String base64String = null;

            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {  
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
    }
}
