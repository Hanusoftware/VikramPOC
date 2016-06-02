using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;

namespace VikramWebRole.Models
{
    public class ConnectionString
    {
       public CloudStorageAccount StorageAccount;
       public CloudBlobContainer Container;
       public CloudBlobContainer PContainer;
        public CloudBlobClient BlobClient;
        public CloudTableClient TableClient;
        public CloudTable Table;
        public CloudQueueClient QueueClient;
        public CloudQueue Queue;
        public string SubscriptionID = string.Empty;
        public ConnectionString()
        {
            StorageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConStr"].ToString());
            BlobClient = StorageAccount.CreateCloudBlobClient();

            Container = BlobClient.GetContainerReference(ConfigurationManager.AppSettings["container"].ToString());
            Container.CreateIfNotExists();

            PContainer = BlobClient.GetContainerReference(ConfigurationManager.AppSettings["Pcontainer"].ToString());
            PContainer.CreateIfNotExists();

            TableClient = StorageAccount.CreateCloudTableClient();
            Table = TableClient.GetTableReference(ConfigurationManager.AppSettings["table"].ToString());
            Table.CreateIfNotExists();

            QueueClient = StorageAccount.CreateCloudQueueClient();
            Queue = QueueClient.GetQueueReference(ConfigurationManager.AppSettings["queue"].ToString());
            Queue.CreateIfNotExists();

            SubscriptionID = ConfigurationManager.AppSettings["SubscriptionID"].ToString();
        }
    }
}