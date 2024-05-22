public abstract class StringsRepository : IStringsRepository
{
    public List<string> Read(string filePath)
    {
        if (!File.Exists(filePath)) return new List<string>();
        var fileContents = File.ReadAllText(filePath);
        return TextToStrings(fileContents);
    }
    protected abstract List<string> TextToStrings(string fileContents);
    public void Write(string filePath, List<string> strings)
    {
        File.WriteAllText(filePath, StringToText(strings));
    }
    protected abstract string StringToText(List<string> strings);
}
