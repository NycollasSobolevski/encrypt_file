

string key = "iTnCPKiVmpBagmQu";
var crypto = new Crypto(key);

string inPath = args[0];
string outPath = args[1];

crypto.Encrypt(inPath, outPath);

// crypto.Encrypt(path, "./encrypted_dbskillhub_backup-20-03-2025_15-28");
// crypto.Decrypt("./encrypted_dbskillhub_backup-20-03-2025_15-28", "./desencrypted_dbskillhub_backup-20-03-2025_15-28.bak");


