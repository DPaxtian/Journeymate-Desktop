using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using ImageService;

namespace ImageService
{
    public class ImageClient
    {
        public static void UploadProfileImage(string name, string path)
        {
            var channel = new Channel("localhost", 9050, ChannelCredentials.Insecure);
            var client = new ImageServices.ImageServicesClient(channel);

            try
            {
                string imageName = name;
                byte[] imageData = GetImageData(path);

                var request = new Image
                {
                    Name = imageName,
                    Data = Google.Protobuf.ByteString.CopyFrom(imageData)
                };

                Google.Protobuf.WellKnownTypes.Empty response = client.SaveImage(request);

                Console.WriteLine("La imagen se guardó correctamente.");
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Status.Detail}");
            }

            channel.ShutdownAsync().Wait();
        }


        
        public static byte[] DownloadProfileImage(string imageName)
        {
            var channel = new Channel("localhost", 9050, ChannelCredentials.Insecure);
            var client = new ImageServices.ImageServicesClient(channel);

            byte[] imageData = null;

            var request = new DownloadImageRequest
            {
                ImageName = imageName
            };

            try
            {
                var response = client.DownloadImage(request);
                imageData = response.ImageData.ToByteArray();

                // Guardar o procesar los datos de la imagen descargada según tus necesidades
                // ...
            }
            catch (RpcException ex)
            {
                if (ex.StatusCode == StatusCode.NotFound)
                {
                    Console.WriteLine($"La imagen '{imageName}' no se encontró.");
                }
                else
                {
                    Console.WriteLine($"Ocurrió un error al descargar la imagen: {ex.Status.Detail}");
                }
            }

            return imageData;
        }



        private static byte[] GetImageData(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException($"La imagen '{imagePath}' no existe.");
            }

            return File.ReadAllBytes(imagePath);
        }
    }
}
