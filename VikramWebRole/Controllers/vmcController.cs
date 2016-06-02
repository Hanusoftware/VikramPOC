using Microsoft.ServiceBus;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.Compute;
using Microsoft.WindowsAzure.Management.Compute.Models;
using Newtonsoft.Json;
//using Org.BouncyCastle.Asn1.X509;
//using Org.BouncyCastle.Crypto;
//using Org.BouncyCastle.Crypto.Generators;
//using Org.BouncyCastle.Crypto.Prng;
//using Org.BouncyCastle.Math;
//using Org.BouncyCastle.Security;
//using Org.BouncyCastle.X509;
//using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using VikramWebRole.Models;

namespace VikramWebRole.Controllers
{
    public class vmcController : Controller
    {
        // GET: vmc
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomVirtualMachine
        public XNamespace ns = "http://schemas.microsoft.com/windowsazure";
        public const string subscriptionId = "4a1c48f7-69e5-453d-8c79-6d5f16cd3d9d";
        public const string base64EncodedCertificate = "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";
        public static CertificateCloudCredentials cloudCredentials = new CertificateCloudCredentials(subscriptionId, new System.Security.Cryptography.X509Certificates.X509Certificate2(Convert.FromBase64String(base64EncodedCertificate)));
        //public static string relatedStorageAccountName = "amit983storage";

        #region Create VM
        public ActionResult VMachineCreator()
        {
            VirtualModel obj = new VirtualModel();
            obj.vmlist = ListVM();
            obj.SubscriptionId = "4a1c48f7-69e5-453d-8c79-6d5f16cd3d9d";

            // var vmResult = VirtualMachineCreator.CreateVirtualMachine(cloudCredentials);// } );
            return View(obj);
        }
        [HttpPost]
        public ActionResult CreateVM(VirtualModel vm)
        {
            QuickCreateVM(vm);
            return RedirectToAction("VMachineCreator");
        }
        public static void QuickCreateVM(VirtualModel vm)
        {
            try
            {
                ComputeManagementClient client = new ComputeManagementClient(cloudCredentials);
                string vmName = vm.VMName;

                //STEP1:Create Hosted Service
                //Azure VM must be hosted in a  hosted cloud service.
                createCloudService(vmName, "South Central US", null);

                //STEP2:Construct VM Role instance
                var vmRole = new Role()
                {
                    RoleType = VirtualMachineRoleType.PersistentVMRole.ToString(),
                    RoleName = vmName,
                    Label = vmName,
                    RoleSize = VirtualMachineRoleSize.Small,
                    ConfigurationSets = new List<ConfigurationSet>(),
                    OSVirtualHardDisk = new OSVirtualHardDisk()
                    {
                        MediaLink = getVhdUri(string.Format("{0}.blob.core.windows.net/vhds", vm.StorageAccount)),
                        SourceImageName = GetSourceImageNameByFamliyName("Windows Server 2012 Datacenter")
                    }
                };

                ConfigurationSet configSet = new ConfigurationSet
                {
                    //ConfigurationSetType = ConfigurationSetTypes.WindowsProvisioningConfiguration,
                    ConfigurationSetType = ConfigurationSetTypes.WindowsProvisioningConfiguration,
                    EnableAutomaticUpdates = true,
                    ResetPasswordOnFirstLogon = false,
                    ComputerName = vmName,
                    AdminUserName = vm.LoginName,
                    AdminPassword = vm.Password

                    //InputEndpoints = new BindingList<InputEndpoint>
                    //                {
                    //                    new InputEndpoint { LocalPort = 3389, Port=3389, Name = "RDP", Protocol = "tcp"  },

                    //new InputEndpoint { LocalPort = 80, Port = 80, Name = "web", Protocol = "tcp" }
                    //                }
                };


                //added by amit but not working to add end point automatically
                ConfigurationSet configSet1 = new ConfigurationSet
                {

                    ConfigurationSetType = ConfigurationSetTypes.NetworkConfiguration,

                    InputEndpoints = new BindingList<InputEndpoint>
                    {
                        new InputEndpoint { LocalPort = 3389, Port = 3389, Name = "RDP", Protocol = "tcp"  },
                        new InputEndpoint { LocalPort = 80, Port = 80, Name = "web", Protocol = "tcp" }
                        //,
                        //new InputEndpoint { LocalPort = 3389, Port = 3389, Name = "Remote Desktop", Protocol = "TCP", EnableDirectServerReturn = true }
                    }
                };
                //end
                vmRole.ConfigurationSets.Add(configSet);
                vmRole.ConfigurationSets.Add(configSet1);
                vmRole.ResourceExtensionReferences = null;

                //STEP3: Add Role instance to Deployment Parmeters
                List<Role> roleList = new List<Role>() { vmRole };
                VirtualMachineCreateDeploymentParameters createDeploymentParams = new VirtualMachineCreateDeploymentParameters
                {

                    Name = vmName,
                    Label = vmName,
                    Roles = roleList,
                    DeploymentSlot = DeploymentSlot.Production
                };

                //STEP4: Create a Deployment with VM Roles.
                client.VirtualMachines.CreateDeployment(vmName, createDeploymentParams);
                Console.WriteLine("Create VM success");
            }
            catch (CloudException e)
            {

                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        private static string EncodeToBase64(string toEncode)
        {
            byte[] toEncodeAsBytes
            = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue
                  = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
        private static Uri getVhdUri(string blobcontainerAddress)
        {
            var now = DateTime.UtcNow;
            string dateString = now.Year + "-" + now.Month + "-" + now.Day + now.Hour + now.Minute + now.Second + now.Millisecond;

            var address = string.Format("http://{0}/{1}-650.vhd", blobcontainerAddress, dateString);
            return new Uri(address);
        }
        private static void createCloudService(string cloudServiceName, string location, string affinityGroupName = null)
        {
            ComputeManagementClient client = new ComputeManagementClient(cloudCredentials);
            HostedServiceCreateParameters hostedServiceCreateParams = new HostedServiceCreateParameters();
            if (location != null)
            {
                hostedServiceCreateParams = new HostedServiceCreateParameters
                {
                    ServiceName = cloudServiceName,
                    Location = location,
                    Label = EncodeToBase64(cloudServiceName),
                };
            }
            else if (affinityGroupName != null)
            {
                hostedServiceCreateParams = new HostedServiceCreateParameters
                {
                    ServiceName = cloudServiceName,
                    AffinityGroup = affinityGroupName,
                    Label = EncodeToBase64(cloudServiceName),
                };
            }
            try
            {
                client.HostedServices.Create(hostedServiceCreateParams);
            }
            catch (CloudException e)
            {
                throw e;
            }

        }
        private static string GetSourceImageNameByFamliyName(string imageFamliyName)
        {
            ComputeManagementClient client = new ComputeManagementClient(cloudCredentials);
            // var operatingSystemImageListResult =  client.VirtualMachineOSImages.ListAsync();
            var results = client.VirtualMachineOSImages.List();
            var disk = results.Where(o => o.ImageFamily == imageFamliyName).FirstOrDefault();

            if (disk != null)
            {
                return disk.Name;
            }
            else
            {
                throw new CloudException(string.Format("Can't find {0} OS image in current subscription"));
            }
        }
        #endregion
        #region List VM
        public List<VirtualModel> ListVM()
        {
            ComputeManagementClient client = new ComputeManagementClient(cloudCredentials);
            var hostedServices = client.HostedServices.List();
            List<VirtualModel> VmList = new List<VirtualModel>();
            VirtualModel objVm = null;
            foreach (var service in hostedServices)
            {
                var deployment = GetAzureDeyployment(service.ServiceName, DeploymentSlot.Production);
                if (deployment != null)
                {
                    if (deployment.Roles.Count > 0)
                    {
                        foreach (var role in deployment.Roles)
                        {
                            objVm = new VirtualModel();
                            if (role.RoleType == VirtualMachineRoleType.PersistentVMRole.ToString())
                            {
                                //  Console.WriteLine(role.RoleName);
                                objVm.VMName = role.RoleName;
                                objVm.SourceImageName = role.OSVirtualHardDisk.SourceImageName;
                                objVm.MediaLoacation = role.VMImageName;
                                objVm.OperatingSystem = role.OSVirtualHardDisk.OperatingSystem;
                                VmList.Add(objVm);
                            }

                        }
                    }
                }
            }

            return VmList;
            // return RedirectToAction("VMachineCreator", objVm);
        }

        private static DeploymentGetResponse GetAzureDeyployment(string serviceName, DeploymentSlot slot)
        {
            ComputeManagementClient client = new ComputeManagementClient(cloudCredentials);
            try
            {
                return client.Deployments.GetBySlot(serviceName, slot);

            }
            catch (CloudException ex)
            {

                if (ex.ErrorCode == "ResourceNotFound")
                {
                    return null;
                }
                else
                {
                    throw ex;
                }
            }
        }

        async public Task<ActionResult> ConnectVM(String roleName, String username)
        {


            roleName = "MxxsagarVM";
            username = "hanu983";
            byte[] rdpFile = await GetRDPFile(username, roleName);
            Response.AppendHeader("Content-Disposition", String.Format("filename={0}.rdp", roleName));
            Response.AppendHeader("Content-Length", rdpFile.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(rdpFile);
            return View("VMachineCreator");
        }
        async public Task<byte[]> GetRDPFile(String ServiceName, String vmName)
        {
            String deploymentName = await GetAzureDeploymentName(ServiceName);
            String uri = String.Format("https://management.core.windows.net/{0}/services/hostedservices/{1}/deployments/{2}/roleinstances/{3}/ModelFile?FileType=RDP", subscriptionId, ServiceName, deploymentName, vmName);
            byte[] RDPFile = null;

            HttpClient http = GetHttpClient();
            Stream responseStream = await http.GetStreamAsync(uri);
            if (responseStream != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    responseStream.CopyTo(ms);
                    RDPFile = ms.ToArray();
                }
            }
            return RDPFile;
        }
        HttpClient GetHttpClient()
        {
            X509Store certificateStore = null;
            X509Certificate2 certificate = null;
            try
            {
                var certificates1 = new X509Certificate2(Convert.FromBase64String(base64EncodedCertificate));
                string thumbprint = certificates1.Thumbprint;
                certificateStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                certificateStore.Open(OpenFlags.ReadOnly);
                // string thumbprint = ConfigurationManager.AppSettings["CertThumbprint"];
                // string thumbprint = "32DD40D58C156F8E92084645F72EEA86EA77AADB";
                //  var certificates = certificateStore.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
                certificate = certificates1;

                //if (certificates.Count > 0)
                //{
                //    certificate = certificates[0];
                //}
            }
            finally
            {
                if (certificateStore != null) certificateStore.Close();
            }

            WebRequestHandler handler = new WebRequestHandler();
            if (certificate != null)
            {
                handler.ClientCertificates.Add(certificate);
                HttpClient httpClient = new HttpClient(handler);
                //And to set required headers lik x-ms-version 
                httpClient.DefaultRequestHeaders.Add("x-ms-version", "2012-03-01");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                return httpClient;
            }
            return null;
        }
        async private Task<String> GetAzureDeploymentName(String ServiceName)
        {
            String uri = String.Format("https://management.core.windows.net/{0}/services/hostedservices/{1}/deploymentslots/{2}", subscriptionId, ServiceName, "Production");
            String DeploymentName = String.Empty;

            HttpClient http = GetHttpClient();

            Stream responseStream = await http.GetStreamAsync(uri);

            if (responseStream != null)
            {
                XDocument xml = XDocument.Load(responseStream);
                var name = xml.Root.Element(ns + "Name");
                DeploymentName = name.Value;
            }

            return DeploymentName;
        }
        #endregion
    }
}