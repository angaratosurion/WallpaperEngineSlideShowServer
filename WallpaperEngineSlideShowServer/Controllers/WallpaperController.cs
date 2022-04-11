using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WallpaperEngineSlideShowServer.Managers;
using WallpaperEngineSlideShowServer.Models;

namespace WallpaperEngineSlideShowServer.Controllers
{
    //[EnableCors()]
    public class WallpaperController : Controller
    {
        // GET: WallpaperController

        static WallpaperManager wallpaperManager = new WallpaperManager();
       
        public ActionResult GetImages()
        {

            try
            {
                List<string> images = new List<string>();
                images=WallpaperManager.GetImages();

                return new OkObjectResult(images);
                

            }
            catch (Exception ex)
            {

                 Console.WriteLine(ex.ToString()); return null;
                
            }


          
        }
        public ActionResult GetImage()
        {
            try
            {
                string ap = null;
             
                ap= wallpaperManager.GetImage();
                

                return new OkObjectResult(ap);
            }
            catch (Exception ex)
            {

                 Console.WriteLine(ex.ToString()); return null;
            }
        }
        public ActionResult GetImageData()
        {
            try
            {
              ImageData ap = null;
                int i = 0;
              ap =wallpaperManager.GetImageData();
               

                return new OkObjectResult(ap);
            }
            catch (Exception ex)
            {

                 Console.WriteLine(ex.ToString()); return null;
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
            catch (Exception ex)
            {

                 Console.WriteLine(ex.ToString()); return null;
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
            catch (Exception ex)
            {
               
                 Console.WriteLine(ex.ToString()); return null;
            }
        }



    }
}
