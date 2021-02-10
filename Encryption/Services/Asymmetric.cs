using System;
using System.IO;
using System.Security.Cryptography;

class Assymetric : IEncryptor
{
    protected Assymetric()
    {

    }
    static Assymetric()
    {
        _assymetric = new Assymetric();
    }
    private static Assymetric _assymetric;
    public static Assymetric Instance
    {
        get
        {
            return _assymetric;
        }
    }
    public void Encrypt()
    {

    }

    public void Decrypt()
    {
        try
        {
            /** @TODO: KEY E IV SERÃO DESCRIPTOGRAFADOS ASSIMETRICAMENTE 
            EM SEGUIDA, SERÃO PASSADOS COMO PARAMETRO DA FUNÇAO CREATE DECRYPTOR
            **/

            using (FileStream encryptedStream = new FileStream($"{Path.ENCRYPTED_FILES}/{OutputFile.Assymetric}.txt", FileMode.OpenOrCreate))
            using (Aes aes = Aes.Create())
            {

                byte[] _key = new byte[5];
                byte[] iv = new byte[5];

                var aesDecryptor = aes.CreateDecryptor(_key, iv);

                Core.AesDecryption(encryptedStream, aesDecryptor, OutputFile.Assymetric);
            }
        }
        catch (System.Exception)
        {
            Console.WriteLine("\nFalha ao realizar a descriptografia.");
            throw;
        }

    }
}