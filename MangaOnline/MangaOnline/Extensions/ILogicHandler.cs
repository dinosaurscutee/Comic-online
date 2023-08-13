namespace MangaOnline.Extensions;

public interface ILogicHandler
{
    Task<bool> SendEmailAsync(string recipient, string subject, string body);

    string GeneratePassword(int length);

    string CreateImage(IFormFile myFile);
    
    string UpdateImage(IFormFile myFile,string oldFile);
    
    string UpdateImageAvatarUser(IFormFile myFile,string? oldFile);

    string CreatePDF(IFormFile myFile);
}