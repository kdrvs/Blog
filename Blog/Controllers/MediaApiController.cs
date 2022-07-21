using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaApiController : ControllerBase
    {
        public string folder = "wwwroot/media/";

        [HttpGet]
        public List<string> Get()
        {
            var folderInfo = new DirectoryInfo(folder);
            var filesInfo = folderInfo.GetFiles();
            var filesName = filesInfo.Select(f => f.Name).OrderByDescending(t => t).ToList();
            return filesName;
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<bool> Post(IFormFile iForm)
        {
            if(iForm == null)
            {
                return false;
            }

            bool status;
            try
            {
                var filename = folder + DateTime.Now.Ticks.ToString() + iForm.FileName;
                using(var stream = System.IO.File.Create(filename))
                {
                    await iForm.CopyToAsync(stream);
                }

                status = true;
            }
            catch
            {
                status = false;
            }
            return status;
        }
    }
}
