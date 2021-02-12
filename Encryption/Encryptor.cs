
using System;

public class Encryptor
{
    IEncryptor _encryptor;

    public Encryptor(IEncryptor encryptor)
    {
        _encryptor = encryptor;
    }

    public void Run()
    {
        Console.WriteLine("##### CRIPTOGRAFANDO #####\n");
        _encryptor.Encrypt();

        Console.WriteLine("##### DESCRIPTOGRAFANDO #####\n");
        _encryptor.Decrypt();
    }
}