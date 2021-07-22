using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UploadFileBackend.Api.Models;
using UploadFileBackend.Core.Helpers;
using UploadFileBackend.Core.Services;
using UploadFileBackend.Models.DTOs;

namespace UploadFileBackend.Api.Controllers
{
    [Route("api/upload")]
    public class UploadController : Controller
    {
        private readonly ILogger<UploadController> _logger;
        private readonly IFileService _fileService;
        public UploadController(ILogger<UploadController> logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }
        //POST
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            var response = new BaseResponse<FileItem>();
            try
            {
                if (file == null)
                {
                    response.FriendlyMessage = new FriendlyMessage("No file to upload", "", true);
                    return BadRequest(response);
                }
                await _fileService.CreateAsync(file);
                return Ok();
            }
            catch(Exception ex)
            {
                response.FriendlyMessage = new FriendlyMessage(ex.Message, null,true);
                return StatusCode(StatusCodes.Status500InternalServerError,response);
            }
        }
        //GET
        [HttpGet]
        public async Task<IActionResult> GetUploadedFiles()
        {
            var response = new BaseResponse<FileItem>();
            try
            {
                response.DataList = await _fileService.GetFileListAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.FriendlyMessage = new FriendlyMessage(ex.Message, null,true);
                return StatusCode(StatusCodes.Status500InternalServerError,response);
            }
        }
        //GET
        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetUploadedFileByName(string fileName)
        {
            var response = new BaseResponse<FileItem>();
            try
            {
                var stream = await _fileService.GetFileByNameAsync(fileName);
                if (stream == null)
                    return NotFound();
                return File(stream,MimeHelper.GetMimeTypeForFileExtension(fileName),fileName);
            }
            catch (Exception ex)
            {
                response.FriendlyMessage = new FriendlyMessage(ex.Message, null,true);
                return StatusCode(StatusCodes.Status500InternalServerError,response);
            }
        }
    }
}