using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VikramWebRole.Models
{
    public class BlobModel
    {
        public string DocUrl { get; set; }
        public string FileName { get; set; }
        public List<BlobModel> Urls { get; set; }

        public string urlText { get; set; }
    }
}