using CookiesCookbook.FileAccess;

public class FileMetadata
{
    public string Name { get; set; }
    public FileFormat Format { get; set; }

    public FileMetadata(string name, FileFormat format)
    {
        Name = name;
        Format = format;
    }

    public string ToPath() => $"{Name}.{Format.AsFileExtension()}";
}
