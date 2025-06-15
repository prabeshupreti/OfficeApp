
using OfficeApp.Services.Abstraction;

namespace OfficeApp.Services.Implementation;

public class WebHostEnvironmentService : IWebHostEnvironmentService
{

    private readonly IWebHostEnvironment _webHostEnvironment;

    public WebHostEnvironmentService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public void AddEmployeeProfilePhoto(IFormFile image, string FilePath)
    {
        using (var fileStream = new FileStream(FilePath, FileMode.Create))
        {
            image.CopyTo(fileStream);
        }
    }

    public void DeleteEmployeeProfilePhoto(string PhotoPath)
    {

        var fileName = @$"{_webHostEnvironment.WebRootPath}{PhotoPath}";

        if (File.Exists(fileName))

            File.Delete(fileName);
    }

    public (string AbsolutePath, string RelativePath) GetFilePath(string FileName)
    {
        var uploadsFolder = $@"{_webHostEnvironment.WebRootPath}\ProfilePhotos\";
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        string AbsoluteFilePath = @$"{uploadsFolder}{FileName}";

        return (AbsoluteFilePath, AbsoluteFilePath.Split("wwwroot")[1]);
    }
}
