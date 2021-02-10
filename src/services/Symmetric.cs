
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
            using (FileStream myStream = new FileStream($"{Path.ENCRYPTED_FILES}/symmetric.txt", FileMode.OpenOrCreate))
            using (Aes aes = Aes.Create())
            {
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
            using (FileStream encryptedStream = new FileStream($"{Path.ENCRYPTED_FILES}/symmetric.txt", FileMode.OpenOrCreate))
            using (Aes aes = Aes.Create())
            {
                //Lê o vetor de inicializaçao do inicio do arquivo.
                byte[] iv = new byte[aes.IV.Length];
                encryptedStream.Read(iv, 0, iv.Length);

                using (CryptoStream cryptStream = new CryptoStream(encryptedStream, aes.CreateDecryptor(_key, iv), CryptoStreamMode.Read))
                using (StreamReader streamReader = new StreamReader(cryptStream))
                {
                    var decryptedText = streamReader.ReadToEnd();
                    Console.WriteLine("\nArquivo descriptografado com sucesso!");
                    Console.WriteLine($"\nMensagem criptografada originalmente: {decryptedText}");
                    SaveDecryptedFile(decryptedText);
                }

            }
        }
        catch (System.Exception)
        {
            Console.WriteLine("\nFalha ao realizar a descriptografia.");
            throw;
        }

    }

    private void SaveDecryptedFile(string text)
    {
        using (FileStream decryptedStream = new FileStream($"{Path.DECRYPTED_FILES}/symmetric.txt", FileMode.OpenOrCreate))
        using (StreamWriter streamWriter = new StreamWriter(decryptedStream))
        {
            streamWriter.WriteLine(text);
        }
    }
}
