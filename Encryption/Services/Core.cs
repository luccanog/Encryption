

using System;
using System.IO;
using System.Security.Cryptography;

public static class Core
{
    static Core()
    {
    }

    public static void AesDecryption(FileStream encryptedStream, ICryptoTransform decryptor, OutputFile outputType)
    {
        using (CryptoStream cryptStream = new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read))
        using (StreamReader streamReader = new StreamReader(cryptStream))
        {
            var decryptedText = streamReader.ReadToEnd();
            Console.WriteLine("Arquivo descriptografado com sucesso!");
            Console.WriteLine($"\nMensagem criptografada originalmente: {decryptedText}");
            SaveDecryptedFile(decryptedText, outputType);
        }
    }
    private static void SaveDecryptedFile(string text, OutputFile outputType)
    {
        string outputPath = $"{Path.DECRYPTED_FILES}/{outputType}.txt";
        using (FileStream decryptedStream = new FileStream(outputPath, FileMode.OpenOrCreate))
        using (StreamWriter streamWriter = new StreamWriter(decryptedStream))
        {
            streamWriter.WriteLine(text);
        }
        Console.WriteLine($"Arquivo salvo em: {outputPath}\n");
        Console.WriteLine("##### FIM #####\n");

    }

}