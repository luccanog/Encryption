
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
        Console.WriteLine("1 - CRIPTOGRAFANDO...\n");
        _encryptor.Encrypt();

        Console.WriteLine("2 - DESCRIPTOGRAFANDO...\n");
        _encryptor.Decrypt();
    }
}