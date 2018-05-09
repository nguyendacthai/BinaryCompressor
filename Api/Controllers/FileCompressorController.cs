using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using Api.ViewModels;

namespace Api.Controllers
{
    [RoutePrefix("api/file-compressor")]
    public class FileCompressorController : ApiController
    {
        [Route("decompress")]
        [HttpPost]
        public HttpResponseMessage DecompressFile([FromBody] FileCompressorViewModel info)
        {
            #region parameters validation

            if (info == null)
            {
                info = new FileCompressorViewModel();
                Validate(info);
            }

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            #endregion

            var fileName = Guid.NewGuid().ToString("D");

            var path = HttpContext.Current.Server.MapPath($"~/{fileName}");

            using (var ms = new MemoryStream(info.Photos.Buffer))
            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            using (var gZipStream = new GZipStream(ms, CompressionMode.Decompress))
            {
                gZipStream.CopyTo(fileStream);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}