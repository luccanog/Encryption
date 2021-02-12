
using System;
using System.IO;
using System.Security.Cryptography;

class Symmetric : IEncryptor
{
    protected Symmetric()
    {

    }
    static Symmetric()
    {
        _symmetric = new Symmetric();
    }
    private static Symmetric _symmetric;

    private static byte[] _key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

    public static Symmetric Instance
    {
        get
        {
            return _symmetric;
        }
    }
    public void Encrypt()
    {
        Console.WriteLine("Digite o texto a ser criptografado:");
        string text = Console.ReadLine();

        try
        {
            string outputPath = $"{Path.ENCRYPTED_FILES}/{OutputFile.Symmetric}.txt";
            using (FileStream myStream = new FileStream(outputPath, FileMode.OpenOrCreate))
            using (Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = _key;

                //Salva o vetor de inicializaçao no inicio do arquivo 
                //Este dado será usado para a descriptografia.
                byte[] iv = aes.IV;
                myStream.Write(iv, 0, iv.Length);

                using (CryptoStream cryptStream = new CryptoStream(myStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (StreamWriter streamWriter = new StreamWriter(cryptStream))
                {
                    streamWriter.WriteLine(text);
                }
            }

            Console.WriteLine("\nArquivo criptografado com sucesso!\n");
            Console.WriteLine($"Arquivo salvo em: {outputPath}\n");

        }
        catch
        {
            Console.WriteLine("\nFalha ao realizar a criptografia.");
            throw;
        }

    }

    public void Decrypt()
    {
        try
        {
            using (FileStream encryptedStream = new FileStream($"{Path.ENCRYPTED_FILES}/{OutputFile.Symmetric}.txt", FileMode.OpenOrCreate))
            using (Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.PKCS7;
                //Lê o vetor de inicializaçao do inicio do arquivo.
                byte[] iv = new byte[aes.IV.Length];
                encryptedStream.Read(iv, 0, iv.Length);
                var aesDecryptor = aes.CreateDecryptor(_key, iv);
                Core.AesDecryption(encryptedStream, aesDecryptor, OutputFile.Symmetric);
            }
        }
        catch (System.Exception)
        {
            Console.WriteLine("\nFalha ao realizar a descriptografia.");
            throw;
        }

    }
}
