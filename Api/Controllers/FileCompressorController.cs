using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

            using (FileStream outFile = File.Create(info.Photos.Name))
            {
                using (GZipStream decompress = new GZipStream(outFile,
                    CompressionMode.Decompress))
                {
                    // Copy the decompression stream 
                    // into the output file.
                    decompress.CopyTo(outFile);

                    //Console.WriteLine("Decompressed: {0}", info.Photos.Name);

                }
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}