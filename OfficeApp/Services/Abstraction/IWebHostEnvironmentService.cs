
namespace OfficeApp.Services.Abstraction;

public interface IWebHostEnvironmentService
{
    (string AbsolutePath, string RelativePath) GetFilePath(string FileName);
    void AddEmployeeProfilePhoto(IFormFile image, string FilePath);
    void DeleteEmployeeProfilePhoto(string PhotoPath);
}
