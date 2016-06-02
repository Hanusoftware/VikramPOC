using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VikramWebRole.Models
{
    public class TableModel : TableEntity
    {
        public TableModel(string ContactNo, string Email)
        {
            this.PartitionKey = ContactNo;
            this.RowKey = Email;
        }
        public TableModel() { }
        public string FName { get; set; }
        public string LName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
    public class QueueModel
    {
        public string Message { get; set; }
    }
    public class ServiceBusQueueModel
    {
        public string Message { get; set; }
        public List<ServiceBusQueueModel> qlist { get; set; }
    }
}