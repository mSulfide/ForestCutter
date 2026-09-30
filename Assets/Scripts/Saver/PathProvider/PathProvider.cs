using System.IO;
using UnityEngine;

public class PathProvider
{
    private readonly string _path = "";

    public PathProvider(string path)
    {
        _path = path;
    }

    public PathProvider(IPathHierarchy current)
    {
        while (current != null)
        {
            _path = Path.Combine(current.Name, _path);

            current = current.Parent;
        }
    }

    public string GetPath(params string[] names) => Path.Combine(_path, Path.Combine(names));

    public string GetPath(string fileName) => Path.Combine(_path, fileName);

    public string GetPath() => _path;

    public static string Combine(params string[] names) => Path.Combine(names);
}