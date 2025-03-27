
if(args.Length != 3)
{
    System.Console.WriteLine("""
        Invalid params!
        try with the following structure params
        "encrypt_file.exe <16_bytes_key_file_path> <input_path> <output_path>"
    """);
    return;
}

string key = args[0];
var crypto = new Crypto(key);

string inPath = args[1];
string outPath = args[2];

crypto.Encrypt(inPath, outPath);

//Examples
// crypto.Encrypt(path, "./encrypted_dbskillhub_backup-20-03-2025_15-28");
// crypto.Decrypt("./encrypted_dbskillhub_backup-20-03-2025_15-28", "./desencrypted_dbskillhub_backup-20-03-2025_15-28.bak");


