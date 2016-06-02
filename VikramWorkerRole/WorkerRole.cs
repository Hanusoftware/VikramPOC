using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;

namespace VikramWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
       
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("VikramWorkerRole is running");

            try
            {
                #region Queue
                //this.RunAsync(this.cancellationTokenSource.Token).Wait();
                CloudStorageAccount storage = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConStr"].ToString());

                CloudQueueClient queueClient = storage.CreateCloudQueueClient();
                CloudQueue queue = queueClient.GetQueueReference(ConfigurationManager.AppSettings["queue"]);
                queue.CreateIfNotExists();
                while (true)
                {
                    Thread.Sleep(10000);
                    if (queue.Exists())
                    {
                        CloudQueueMessage msg = queue.GetMessage();
                        if (msg != null)
                        {
                            string message = msg.AsString;
                            CloudBlobClient blobClient = storage.CreateCloudBlobClient();
                            CloudBlobContainer container = blobClient.GetContainerReference(ConfigurationManager.AppSettings["container"]);
                            container.CreateIfNotExists();
                            CloudAppendBlob appendBlob = container.GetAppendBlobReference("VikramQueue.txt");
                            if (appendBlob.Exists())
                            {
                                appendBlob.AppendText(" " + message);
                            }
                            else
                            {
                                appendBlob.UploadText(message);
                            }
                            queue.DeleteMessage(msg);
                        }
                    }
                }
                #endregion queue
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }
        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("VikramWorkerRole has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("VikramWorkerRole is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("VikramWorkerRole has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Working");
                await Task.Delay(1000);
            }
        }
    }
}
