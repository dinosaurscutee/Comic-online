using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

namespace MangaOnline.Extensions;

public class LogicHandler : ILogicHandler
{
    public async Task<bool> SendEmailAsync(string recipient, string subject, string body)
    {
        using (var client = new SmtpClient("smtp.gmail.com", 587))
        {
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("traicvhe153014@fpt.edu.vn", "0362351671");

            var message = new MailMessage
            {
                From = new MailAddress("traicvhe153014@fpt.edu.vn"),
                To = { recipient },
                Subject = subject,
                Body = body
            };
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress("traicvhe153014@fpt.edu.vn"));
            message.Sender = new MailAddress("traicvhe153014@fpt.edu.vn");

            try
            {
                await client.SendMailAsync(message);
                return true;
            }
            catch (SmtpException ex)
            {
                // Lỗi xảy ra khi gửi email
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }

    public string GeneratePassword(int length)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        Random random = new Random();
        char[] chars = new char[length];

        for (int i = 0; i < length; i++)
        {
            chars[i] = validChars[random.Next(0, validChars.Length)];
        }

        return new string(chars);
    }

    public string CreateImage(IFormFile myFile)
    {
        if (myFile == null || myFile.Length == 0)
        {
            throw new Exception("File not found or empty.");
        }
        //add img
        var newFileName = Guid.NewGuid();
        var extension = Path.GetExtension(myFile.FileName);
        string fileName = newFileName + extension;
        
        string filePath = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot", "image/manga-image", fileName);
        using (var file = new FileStream(filePath, FileMode.Create))
        {
            myFile.CopyTo(file);
        }
        return fileName;
    }

    public string UpdateImage(IFormFile myFile, string oldFile)
    {
        var fileName = CreateImage(myFile);
        // delete img 
        if (!string.IsNullOrEmpty(oldFile))
        {
            string imgPath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "image/manga-image", oldFile);
            FileInfo fileDelete = new FileInfo(imgPath);
            if (fileDelete.Length > 0)
            {
                System.IO.File.Delete(imgPath);
                fileDelete.Delete();
            }
        }
        return fileName;
    }
    
    public string UpdateImageAvatarUser(IFormFile myFile, string? oldFile)
    {
        var fileName = CreateImageAvatarUser(myFile);
        // delete img 
        if (!string.IsNullOrEmpty(oldFile))
        {
            string imgPath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "image/avartar-user", oldFile);
            FileInfo fileDelete = new FileInfo(imgPath);
            if (fileDelete.Length > 0)
            {
                System.IO.File.Delete(imgPath);
                fileDelete.Delete();
            }
        }
        return fileName;
    }

    public string CreateImageAvatarUser(IFormFile myFile)
    {
        if (myFile == null || myFile.Length == 0)
        {
            throw new Exception("File not found or empty.");
        }
        //add img
        var newFileName = Guid.NewGuid();
        var extension = Path.GetExtension(myFile.FileName);
        string fileName = newFileName + extension;
        
        string filePath = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot", "image/avartar-user", fileName);
        using (var file = new FileStream(filePath, FileMode.Create))
        {
            myFile.CopyTo(file);
        }
        return fileName;
    }

    public string CreatePDF(IFormFile myFile)
    {
        if (myFile == null || myFile.Length == 0)
        {
            throw new Exception("File not found or empty.");
        }
        //add pdf
        var newFileName = Guid.NewGuid();
        var extension = Path.GetExtension(myFile.FileName);
        string fileName = newFileName + extension;

        string filePath = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot", "pdf", fileName);
        using (var file = new FileStream(filePath, FileMode.Create))
        {
            myFile.CopyTo(file);
        }
        return fileName;
    }
}