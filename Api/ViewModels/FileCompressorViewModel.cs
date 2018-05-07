using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class FileCompressorViewModel
    {
        #region Properties
        
        public string Name { get; set; }

        /// <summary>
        /// File photo.
        /// </summary>
        [Required]
        public HttpFile Photos { get; set; }

        #endregion
    }
}