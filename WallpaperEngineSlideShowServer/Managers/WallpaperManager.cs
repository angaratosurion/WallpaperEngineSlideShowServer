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
                        ap = files.ToList();
                    }




                }


                return ap;
            }
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
            }

        }
    }
}
