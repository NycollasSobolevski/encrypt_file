public class BackupedFile(string dbName, string originPath, string destinyPath)
{
    public string DbName = dbName;
    public string OriginPath = originPath;
    public string DestinyPath = destinyPath;


    public override string ToString()
    {
        return $"""
            Database Name: {DbName}
            Origin: {OriginPath}
            Destiny: {DestinyPath}
        """;
    }
}