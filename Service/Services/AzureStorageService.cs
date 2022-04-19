using Azure.Storage.Blobs;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        public string SendFileToAzureStorage(int id, string filename)
        {
            BlobContainerClient container = new BlobContainerClient(ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString, "container-qrcode");
            try
            {
                BlobClient blob = container.GetBlobClient($"animal-{id}.png");
                var bytes = Convert.FromBase64String(String.Format(@"{0}", filename));
                using (Stream stream = new MemoryStream(bytes))
                {
                    blob.Upload(stream);
                }

                return blob.Uri.AbsoluteUri;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
