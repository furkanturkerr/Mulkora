namespace Mulkora.WebApi.Services;

public class ImageFileService
{
    private readonly IWebHostEnvironment _environment;
    //Çalışan WebApi projesinin klasör yollarına ulaşmamızı sağlar.

    public ImageFileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<List<string>> SaveImagesAsync(List<IFormFile> images)
    {
        var imageUrls = new List<string>();

        var allowedExtensions = new List<string>
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
        };

        var webRootPath = _environment.WebRootPath;
        //wwwroot klasörünün yolunu verir.

        if (string.IsNullOrEmpty(webRootPath))
        {
            webRootPath = Path.Combine(_environment.ContentRootPath, "wwwroot");
        }

        var folderPath = Path.Combine(webRootPath, "property-images");
        //İşletim sistemine uygun klasör yolu oluşturur.

        Directory.CreateDirectory(folderPath);
        //Klasör yoksa oluşturur. Klasör zaten varsa hata vermez.

        foreach (var image in images)
        {
            if (image.Length == 0)
            {
                continue;
            }

            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                throw new Exception(
                    "Desteklenmeyen görsel formatı.");
            }

            if (image.Length > 5 * 1024 * 1024)
            {
                throw new Exception(
                    "Bir görsel en fazla 5 MB olabilir.");
            }

            var fileName = $"{Guid.NewGuid()}{extension}";
            //Her dosyaya benzersiz bir isim verir.

            var filePath = Path.Combine(folderPath, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            //Diskte yeni dosya oluşturur

            await image.CopyToAsync(stream);
            //Tarayıcıdan gelen dosyanın içeriğini oluşturulan fiziksel dosyaya yazar.

            var imageUrl = $"/property-images/{fileName}";

            imageUrls.Add(imageUrl);
        }

        return imageUrls;
    }
}