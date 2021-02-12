using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryption
{
    class Program
    {
        static IEncryptor _instance;

        static void Main(string[] args)
        {
            int flow = Start();

            switch (flow)
            {
                case 1: _instance = Symmetric.Instance; break;
                case 2: _instance = Assymetric.Instance; break;
                case 0: Environment.Exit(0); break;
            }

            Encryptor encryptor = new Encryptor(_instance);
            encryptor.Run();

        }

        static private int Start()
        {
            List<int> options = new List<int>() { 0, 1, 2 };
            int flow = 0;
            try
            {


                Console.WriteLine("Bem Vindo - Digite uma opção abaixo.\n");
                Console.WriteLine("1 - Criptografia Simétrica\n2 - Criptografia Assimétrica\n0 - Sair");
                flow = Convert.ToInt16(Console.ReadLine());

                if (!options.Contains(flow))
                {
                    Console.WriteLine("\nOpção inválida, tente novamente!");
                    Start();
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("\nOps, ocorreu um problema tente novamente!");
                Start();
            }

            return flow;
        }
    }
}
