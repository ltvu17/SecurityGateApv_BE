using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using SecurityGateApv.Domain.Enums;
using SecurityGateApv.Domain.Interfaces.Repositories;
using SecurityGateApv.Domain.Models;
using SecurityGateApv.Infras.DBContext;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace SecurityGateApv.Infras.Repositories
{
    public class CardRepo : RepoBase<Card>, ICardRepo
    {
        private readonly SecurityGateApvDbContext _context;
        private readonly DbSet<Visit> _dbSet;

        public CardRepo(SecurityGateApvDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Visit>();
        }

        public async Task<Card> GenerateQRCard(string cardIdGuid, IFormFile file, string cardTypeName)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file), "File parameter is null.");
            }

            // Generate QR code
            var qrCodeGenerator = new QRCodeGenerator();
            var qrCodeData = qrCodeGenerator.CreateQrCode(cardIdGuid, QRCoder.QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qrCode.GetGraphic(5, new byte[] { 0, 0, 0, 255 }, new byte[] { 0xf5, 0xf5, 0xf7, 255 });

            // Create a card image with the QR code embedded
            using (var qrCodeImage = Image.Load<Rgba32>(qrCodeBytes))
            {
                int cardWidth = 250;
                int cardHeight = 400;
                int qrCodeSize = 150; // Fixed size for the QR code

                // Resize the QR code to the fixed size
                qrCodeImage.Mutate(x => x.Resize(qrCodeSize, qrCodeSize));

                using (var cardImage = new Image<Rgba32>(cardWidth, cardHeight))
                {
                    cardImage.Mutate(ctx =>
                    {
                        ctx.Clear(Color.ParseHex("#34495e"));

                        // Draw rounded rectangle
                        var rect = new RectangularPolygon(0, 0, cardWidth, cardHeight);
                        ctx.Fill(Color.ParseHex("#34495e"), rect);
                        ctx.Draw(Color.White, 10, rect);

                        // Add logo
                        using (var memoryStream = new MemoryStream())
                        {
                            file.CopyTo(memoryStream);
                            memoryStream.Seek(0, SeekOrigin.Begin); // Reset stream position
                            using (var logo = Image.Load<Rgba32>(memoryStream.ToArray()))
                            {
                                int logoWidth = 100;
                                int logoX = (cardWidth - logoWidth) / 2;
                                int logoY = 20;
                                logo.Mutate(x => x.Resize(logoWidth, logoWidth)); // Resize logo to 100x100
                                ctx.DrawImage(logo, new Point(logoX, logoY), 1);
                            }
                        }

                        // Add title text
                        var titleFont = SixLabors.Fonts.SystemFonts.CreateFont("Arial", 24, SixLabors.Fonts.FontStyle.Bold);
                        var titleText = "Security Gate APV";
                        var titleSize = TextMeasurer.MeasureSize(titleText, new TextOptions(titleFont));
                        var titleX = (cardWidth - titleSize.Width) / 2;
                        ctx.DrawText(titleText, titleFont, Color.White, new PointF(titleX, 110));

                        // Draw the QR code on the card
                        int qrCodeX = (cardWidth - qrCodeSize) / 2;
                        int qrCodeY = 160;
                        ctx.DrawImage(qrCodeImage, new Point(qrCodeX, qrCodeY), 1);

                        // Add footer text
                        var footerFont = SixLabors.Fonts.SystemFonts.CreateFont("Arial", 12, SixLabors.Fonts.FontStyle.Bold);
                        var footerColor = cardTypeName == CardTypeEnum.ShotTermCard.ToString() ? Color.White : Color.Yellow;
                        string footerText = cardTypeName == CardTypeEnum.ShotTermCard.ToString() ? "Thẻ ra vào hàng ngày" : "Thẻ ra vào theo lịch trình";
                        var footerSize = TextMeasurer.MeasureSize(footerText, new TextOptions(footerFont));
                        var footerX = (cardWidth - footerSize.Width) / 2;
                        ctx.DrawText(footerText, footerFont, footerColor, new PointF(footerX, cardHeight - 30));
                    });

                    // Convert the card image to a base64 string
                    using (var ms = new MemoryStream())
                    {
                        cardImage.Save(ms, new PngEncoder());
                        var cardImageBase64 = Convert.ToBase64String(ms.ToArray());

                        // Create the Card object
                        var card = Card.GenerateCard(cardIdGuid, cardImageBase64);
                        return card.Value;
                    }
                }
            }
        }
    }
}
