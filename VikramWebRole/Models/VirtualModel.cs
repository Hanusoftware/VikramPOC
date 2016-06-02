using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VikramWebRole.Models
{
    public class VirtualModel
    {
        public string SubscriptionId { get; set; }
        public string StorageAccount { get; set; }
        public string VMName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string MediaLoacation { get; set; }
        public string SourceImageName { get; set; }
        public string OperatingSystem { get; set; }

        public List<VirtualModel> vmlist { get; set; }
    }
}