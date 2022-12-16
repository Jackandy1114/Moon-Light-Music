using System.Text;

using Moon_Light_Music.Core.Contracts.Services;

using Newtonsoft.Json;

namespace Moon_Light_Music.Core.Services;

public class FileService : IFileService
{
    public T Read<T>(string folderPath, string fileName)
    {
        var path = Path.Combine(folderPath, fileName);
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json);
        }

        return default;
    }

    public void Save<T>(string folderPath, string fileName, T content)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var fileContent = JsonConvert.SerializeObject(content);

        //Xử lý việc multi Writefile
        while (true)
        {
            try
            {
                File.WriteAllText(Path.Combine(folderPath, fileName), fileContent, Encoding.UTF8);
                break;
            }
            catch (Exception)
            {
                Thread.Sleep(500);
            }
        }


    }

    public void Delete(string folderPath, string fileName)
    {
        if (fileName != null && File.Exists(Path.Combine(folderPath, fileName)))
        {
            File.Delete(Path.Combine(folderPath, fileName));
        }
    }
}
