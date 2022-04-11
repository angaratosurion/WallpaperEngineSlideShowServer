namespace WallpaperEngineSlideShowServer.Models
{
    public class ImageData
    {
        public ExifModel Exif { get; set; }
        public string Base64String { get; set; }
        public string FileName { get; set; }
        public string WebUrl { get; set; }
    }
}
