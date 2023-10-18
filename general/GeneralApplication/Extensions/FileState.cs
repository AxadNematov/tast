using GeneralDomain.Configs;
using Microsoft.AspNetCore.Http;

namespace GeneralApplication.Extensions;

public static class FileState
{
    public static string AddFile(IFormFile newFile)
    {
        var path = Directory.GetCurrentDirectory();
        var name = Guid.NewGuid().ToString();
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        byte[] bytes = null;
        using (var binaryReader = new BinaryReader(newFile.OpenReadStream()))
        {
            bytes = binaryReader.ReadBytes((int)newFile.Length);
        }

        var fileTip = newFile.FileName.Split('.').Last();

        name = name + '.' + fileTip;
        path = Path.Combine(path, name);
        if (bytes.Length == 0)
        {

        }
        Console.WriteLine(path);
        File.WriteAllBytes(path, bytes);
        return name;

    }
}