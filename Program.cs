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

            Encryptor encryptor = new Encryptor(Symmetric.Instance);
            encryptor.Run();

        }
    }
}
