
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
    public static Symmetric Instance
    {
        get
        {
            return _symmetric;
        }
    }
    public void Run()
    {
        Console.WriteLine("Digite o texto a ser criptografado:");
        string text = Console.ReadLine();

        try
        {
            using (FileStream myStream = new FileStream("exemplo.txt", FileMode.OpenOrCreate))
            using (Aes aes = Aes.Create())
            {
                aes.Key = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

                //Stores IV at the beginning of the file.
                //This information will be used for decryption.
                byte[] iv = aes.IV;
                myStream.Write(iv, 0, iv.Length);

                //Create a StreamWriter for easy writing to the file stream.
                //Write to the stream.
                using (CryptoStream cryptStream = new CryptoStream(myStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (StreamWriter streamWriter = new StreamWriter(cryptStream))
                {
                    streamWriter.WriteLine(aes.Key);
                }
            }
            Console.WriteLine("Arquivo criptografado com sucesso.");
        }
        catch
        {
            Console.WriteLine("Falha ao realizar a criptografia.");
            throw;
        }

    }
}
