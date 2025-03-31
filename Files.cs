public class Files
{
    public readonly static string BaseBackupPath = "C:\\data\\backupDatabases";
    public readonly static string BaseServerPath = "S:\\COM\\Human_Resources\\01.Engineering_Tech_School\\02.Internal\\8 - Meio oficiais\\BACKUP";
    public readonly static string KeyPath = "C:\\data\\encrypt_file\\key.txt";
    public static IEnumerable<BackupedFile> GetFilesPath()
    {
        var folders = Directory.GetDirectories(BaseBackupPath);
        List<BackupedFile> backups = [];

        foreach (var databasePath in folders)
        {
            var files = Directory.GetFiles(databasePath);
            var currentFilename = files
                .Select(f => {
                    var filePath = f.Split("\\");
                    string filename = filePath[^1];
                    return filename;
                })
                .Where(f => {
                    DateTime today = DateTime.Today;
                    var date = f.Split("_")[2].Split("-");
                    bool isCurrentDay = Int32.Parse(date[0]) == today.Day;
                    bool isCurrentMonth = Int32.Parse(date[1]) == today.Month;
                    bool isCurrentYear = Int32.Parse(date[2]) == today.Year;

                    return isCurrentDay && isCurrentMonth && isCurrentYear;
                })
                .Last();
            string dbName = currentFilename.Split("_")[0];
            string dbLocalBackup = $"{BaseBackupPath}\\{dbName}\\{currentFilename}";
            string dbServerBackup = $"{BaseServerPath}\\{dbName}\\{currentFilename.Split(".")[0]}";
            BackupedFile backuped = new(dbName, dbLocalBackup, dbServerBackup);
            backups.Add(backuped);
        }

        return backups;
    }
}