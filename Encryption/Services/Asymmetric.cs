using System;
using System.IO;
using System.Security.Cryptography;

public class RetrievedKeys
{
    public byte[] AesKey { get; private set; }
    public byte[] Iv { get; private set; }
    public string RSAXmlString { get; private set; }

    public RetrievedKeys(byte[] aesKey, byte[] iv, string rsaXmlString)
    {
        AesKey = aesKey;
        RSAXmlString = rsaXmlString;
        Iv = iv;
    }
}


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

    private static byte[] _modulus = {214,46,220,83,160,73,40,39,201,155,19,202,3,11,191,178,56,
                                        74,90,36,248,103,18,144,170,163,145,87,54,61,34,220,222,
                                        207,137,149,173,14,92,120,206,222,158,28,40,24,30,16,175,
                                        108,128,35,230,118,40,121,113,125,216,130,11,24,90,48,194,
                                        240,105,44,76,34,57,249,228,125,80,38,9,136,29,117,207,139,
                                        168,181,85,137,126,10,126,242,120,247,121,8,100,12,201,171,
                                        38,226,193,180,190,117,177,87,143,242,213,11,44,180,113,93,
                                        106,99,179,68,175,211,164,116,64,148,226,254,172,147}; // Representa uma public key gerada por terceiros.
    private static byte[] _exponent = { 1, 0, 1 };
    public static Assymetric Instance
    {
        get
        {
            return _assymetric;
        }
    }
    public void Encrypt()
    {
        Console.WriteLine("Digite o texto a ser criptografado:");
        string text = Console.ReadLine();

        string outputPath = $"{Path.ENCRYPTED_FILES}/{OutputFile.Assymetric}.txt";
        using (FileStream myStream = new FileStream(outputPath, FileMode.OpenOrCreate))
        using (RSA rsa = RSA.Create(1024))
        {
            byte[] encryptedSymmetricKey;
            byte[] encryptedSymmetricIV;


            // RSAParameters rsaKeyInfo = new RSAParameters();
            /** RSA PUBLIC KEY = = modulus + exponent **/
            // rsaKeyInfo.Modulus = _modulus;
            // rsaKeyInfo.Exponent = _exponent;
            // rsa.ImportParameters(rsaKeyInfo);

            //Create a new instance of the default Aes implementation class.  
            Aes aes = Aes.Create();

            using (CryptoStream cryptStream = new CryptoStream(myStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            using (StreamWriter streamWriter = new StreamWriter(cryptStream))
            {
                streamWriter.WriteLine(text);
            }

            //Encrypt the symmetric key and IV.  
            encryptedSymmetricKey = rsa.Encrypt(aes.Key, RSAEncryptionPadding.Pkcs1);
            encryptedSymmetricIV = rsa.Encrypt(aes.IV, RSAEncryptionPadding.Pkcs1);

            var rsaXmlString = rsa.ToXmlString(true);

            SaveKeys(encryptedSymmetricKey, encryptedSymmetricIV, rsaXmlString);

        }


    }

    public void Decrypt()
    {
        try
        {
            var retrievedKeys = GetKeys();

            RSA rsa = RSA.Create();
            rsa.FromXmlString(retrievedKeys.RSAXmlString);

            var key = rsa.Decrypt(retrievedKeys.AesKey, RSAEncryptionPadding.Pkcs1);
            var iv = rsa.Decrypt(retrievedKeys.Iv, RSAEncryptionPadding.Pkcs1);

            using (FileStream encryptedStream = new FileStream($"{Path.ENCRYPTED_FILES}/{OutputFile.Assymetric}.txt", FileMode.OpenOrCreate))
            using (Aes aes = Aes.Create())
            {
                var aesDecryptor = aes.CreateDecryptor(key, iv);

                Core.AesDecryption(encryptedStream, aesDecryptor, OutputFile.Assymetric);
            }
        }
        catch (System.Exception)
        {
            Console.WriteLine("\nFalha ao realizar a descriptografia.");
            throw;
        }

    }

    private void SaveKeys(byte[] encryptedSymmetricKey, byte[] encryptedSymmetricIV, string privateKey)
    {
        File.WriteAllBytes($"{Path.KEYS}/{Key.AES_KEY}.txt", encryptedSymmetricKey);
        File.WriteAllBytes($"{Path.KEYS}/{Key.IV}.txt", encryptedSymmetricIV);
        File.WriteAllText($"{Path.KEYS}/{Key.PRIVATE_KEY}.txt", privateKey);
    }

    private RetrievedKeys GetKeys()
    {
        try
        {
            var aesKey = File.ReadAllBytes($"{Path.KEYS}/{Key.AES_KEY}.txt");
            var iv = File.ReadAllBytes($"{Path.KEYS}/{Key.IV}.txt");
            var privateKey = File.ReadAllText($"{Path.KEYS}/{Key.PRIVATE_KEY}.txt");

            return new RetrievedKeys(aesKey, iv, privateKey);
        }
        catch (System.Exception)
        {
            Console.WriteLine("Erro ao recuperar as chaves.");
            throw;
        }
    }

}