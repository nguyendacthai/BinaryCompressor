using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface IFileCompressor
    {
        #region Methods

        /// <summary>
        /// Compress File
        /// </summary>
        /// <param name="fi"></param>
        void Compress(FileInfo fi);

        /// <summary>
        /// DecompressFile
        /// </summary>
        /// <param name="fi"></param>
        void Decompress(FileInfo fi);

        #endregion
    }
}
