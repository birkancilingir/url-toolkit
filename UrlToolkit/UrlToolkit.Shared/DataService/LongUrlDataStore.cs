using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UrlToolkit.DataService.Entities;
using Windows.Storage;
using Windows.Storage.Streams;

namespace UrlToolkit.DataService
{
    public class LongUrlDataStore
    {
        private const String SERVICES_FILE = "_services.xml";

        public static async Task WriteSupportedServicesToDataStore(IList<Service> services)
        {
            MemoryStream servicesData = new MemoryStream();

            DataContractSerializer serializer = new DataContractSerializer(typeof(IList<Service>));
            serializer.WriteObject(servicesData, services);

            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(SERVICES_FILE, CreationCollisionOption.ReplaceExisting);
            using (Stream fileStream = await file.OpenStreamForWriteAsync())
            {
                servicesData.Seek(0, SeekOrigin.Begin);
                await servicesData.CopyToAsync(fileStream);
            }
        }

        public static async Task<IList<Service>> ReadSupportedServicesFromDataStore()
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(SERVICES_FILE);
                using (IInputStream inStream = await file.OpenSequentialReadAsync())
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(IList<Service>));
                    IList<Service> _servicesData = (IList<Service>)serializer.ReadObject(inStream.AsStreamForRead());

                    return _servicesData;
                }
            }
            catch(FileNotFoundException e)
            {
                return null;
            }
        }
    }
}
