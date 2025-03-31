
var files = Files.GetFilesPath();
var crypto = new Crypto(Files.KeyPath);
foreach (var item in files)
    crypto.Encrypt(item.OriginPath, item.DestinyPath);