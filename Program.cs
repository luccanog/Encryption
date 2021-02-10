using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryption
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Digite o texto a ser criptografado:");
            string text = Console.ReadLine();

            Encryptor encryptor = new Encryptor(Symmetric.Instance);

            encryptor.Run();
            Console.Read();

        }
    }
}
