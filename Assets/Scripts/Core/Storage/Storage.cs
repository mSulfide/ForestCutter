using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Storage
{
    public Storage()
    {
        string path = GetPath("", type: EFileType.Directory);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public void Save(string fileName, object data, EPathOption option = EPathOption.LocalSave)
    {
        //Debug.Log($"Save: {fileName}");
        string path = GetPath(fileName, option: option);

        if (!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path));

        string json = JsonConvert.SerializeObject(data);
        //Debug.Log(json);
        File.WriteAllText(path, json);
    }

    public T Load<T>(string fileName, T defaultValue = default, EPathOption option = EPathOption.LocalSave)
    {
        //Debug.Log($"Load: {fileName}");
        string path = GetPath(fileName, option: option);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            //Debug.Log(json);
            return JsonConvert.DeserializeObject<T>(json);
        }

        return defaultValue;
    }

    public void Delete(string fileName, EFileType type, EPathOption option = EPathOption.LocalSave)
    {
        string path = GetPath(fileName, type, option);
        if (Directory.Exists(path))
            Directory.Delete(path, true);
    }

    public IEnumerable<string> GetDirectories(string fileName, EPathOption option = EPathOption.LocalSave)
    {
        string path = GetPath(fileName, EFileType.Directory, option);

        return Directory.GetDirectories(path).Select(directory => new DirectoryInfo(directory).Name);
    }

    private string GetPath(string fileName, EFileType type = EFileType.Json, EPathOption option = EPathOption.LocalSave)
    {
        return Path.Combine(
            option switch
            {
                EPathOption.LocalSave => Application.persistentDataPath,
                EPathOption.Full => "",
                _ => "",
            },
            type switch
            {
                EFileType.Json => $"{fileName}.json",
                EFileType.Directory => fileName,
                _ => fileName
            });
    }
}
