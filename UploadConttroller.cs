using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Mvc;
using YouTubeUploader.Helpers;

namespace YouTubeUploader.Controllers
{
    public class UploadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly YouTubeServiceHelper _youTubeServiceHelper;

        public UploadController(YouTubeServiceHelper youTubeServiceHelper)
        {
            _youTubeServiceHelper = youTubeServiceHelper;
        }

       
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var youtubeService = await _youTubeServiceHelper.GetYouTubeServiceAsync();
                
                var video = new Google.Apis.YouTube.v3.Data.Video
                {
                    Snippet = new VideoSnippet
                    {
                        Title = "Sample Video",
                        Description = "This is a sample video upload.",
                        Tags = new[] { "sample", "video" },
                        CategoryId = "22" 
                    },
                    Status = new VideoStatus
                    {
                        PrivacyStatus = "private"
                    }
                };

                using (var fileStream = file.OpenReadStream())
                {
                    var request = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                    await request.UploadAsync();
                    var result = request.ResponseBody;

                    ViewBag.Message = $"Video uploaded successfully. Video ID: {result.Id}";
                }
            }
            else
            {
                ViewBag.Message = "Please select a file to upload.";
            }

            return View("Index");
        }
    }
}
