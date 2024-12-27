using Microsoft.AspNetCore.Http;
using SecurityGateApv.Domain.Interfaces.ExtractImage;
using System.Drawing;
namespace SecurityGateApv.Infras.Helpers
{
    public class ExtractQRCode : IExtractQRCode
    {
        public string ExtractQrCodeFromImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
                throw new ArgumentException("No image provided");

            using (var stream = image.OpenReadStream())
            {
                var bitmap = new Bitmap(stream);


                /*var barcodereader = new zxing.windows.compatibility.barcodereader();
                var result = barcodereader.decode(bitmap);

                if (result == null)
                {
                    Console.WriteLine("No QR code found in the image.");
                }
                else
                {
                    Console.WriteLine($"QR code found: {result.Text}");
                }
*/
                return null; /*result?.Text ?? "No QR code found";*/
            }
        }
    }
}
