using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UploadFileBackend.Api.Controllers
{
    [Route("api/upload")]
    public class UploadController : Controller
    {
        //POST
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            return Ok();
        }
    }
}