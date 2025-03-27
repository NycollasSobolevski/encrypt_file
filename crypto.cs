using System.Security.Cryptography;
using System.Text;

public class Crypto
{
    private string passKey;

    public Crypto (string key) 
        => passKey = ReadKeyFile(key);

    public void Encrypt (string inputFile, string outputFile)
    {
        byte[] key = Encoding.UTF8.GetBytes(this.passKey);

        using Aes aes = Aes.Create();

        aes.Key = key;
        aes.GenerateIV();

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        CheckIfDirExists(outputFile);

        using FileStream fsInput = new (inputFile, FileMode.Open, FileAccess.Read);
        using FileStream fsOutput = new (outputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);

        fsOutput.Write(aes.IV, 0, aes.IV.Length);

        using CryptoStream csEncrypt = new (fsOutput, encryptor, CryptoStreamMode.Write);

        fsInput.CopyTo(csEncrypt);
    }
    public void Decrypt (string inputFile, string outputFile)
    {
        byte[] key = Encoding.UTF8.GetBytes(this.passKey);

        using Aes aes = Aes.Create();

        aes.Key = key;

        using FileStream fsInput = new(inputFile, FileMode.Open, FileAccess.Read);

        byte[] iv = new byte[aes.IV.Length];
        fsInput.Read(iv, 0, iv.Length);
        aes.IV = iv;

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using FileStream fsOutput = new(outputFile, FileMode.Create, FileAccess.Write);
        using CryptoStream csDecrypt = new(fsInput, decryptor, CryptoStreamMode.Read);
        csDecrypt.CopyTo(fsOutput);
    }

    /// <summary>
    /// Check if directory exists, if not, create.
    /// </summary>
    /// <param name="filePath">This param must have filename</param>
    private void CheckIfDirExists(string filePath)
    {
        string res = "";
        string[] dirs = filePath.Split("/");
        if(dirs.Length <= 1)
            dirs = filePath.Split("\\");

        for(int i = 0; i < dirs.Length - 1; i++)
            res += $@"{dirs[i]}\";

        if(!Directory.Exists(res))
            Directory.CreateDirectory(res);
    }

    private string ReadKeyFile(string filePath)
    {
        if(!File.Exists(filePath))
            throw new Exception("Key File not found!");

        var result = File.ReadAllText(filePath)
            ?? throw new Exception("Not found key in file!");

        if(result.Length != 16)
            throw new Exception("invalid key format! The key must have 16 bytes/characters");

        return result;
    }
}