using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WallpaperEngineSlideShowServer.Managers;

namespace WallpaperEngineSlideShowServer.Controllers
{
    //[EnableCors()]
    public class WallpaperController : Controller
    {
        // GET: WallpaperController

        WallpaperManager wallpaperManager = new WallpaperManager();
       
        public ActionResult GetImages()
        {

            try
            {
                List<string> images = new List<string>();
                images=wallpaperManager.GetImages();

                return new OkObjectResult(images);
                

            }
            catch (Exception)
            {

                throw;
            }


          
        }
        public ActionResult GetImage()
        {
            try
            {
                string ap = null;
                int i = 0;
                var images = this.wallpaperManager.GetImages();
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

                return new OkObjectResult(ap);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult GetInterval()
        {
            try
            {
                int ap = 0;
                ap = wallpaperManager.GetInterval();



                return new OkObjectResult(ap);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult GetBackgroundColor()
        {
            try
            {
               string ap = null;
                ap = wallpaperManager.GetBackgroundColor();



                return new OkObjectResult(ap);
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
