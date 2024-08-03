using static ToyStore.Api.Helpers.FileHelper;

namespace ToyStore.Api.Helpers
{
    public class FileHelper
    {

        private readonly IWebHostEnvironment environment;
        public FileHelper(IWebHostEnvironment env)
        {
            this.environment = env;
        }

        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            
                var contentPath = this.environment.WebRootPath;
                // path = "c://projects/productminiapi/uploads" ,not exactly something like that
                var path = Path.Combine(contentPath, "Resources","Images");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Check the allowed extenstions
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    return new Tuple<int, string>(0, msg);
                }
                string uniqueString = Guid.NewGuid().ToString();
                // we are trying to create a unique filename here
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, "Resources/Images/" + newFileName);
            
          
        }

        public void DeleteImage(string imageFileName)
        {
            var path = getFullPath(imageFileName);
            if (File.Exists(path))
                File.Delete(path);
        }

        public string getFullPath(string imageFileName)
        {
            var contentPath = this.environment.WebRootPath;
            return Path.Combine(contentPath, imageFileName);
        }

    }
}

