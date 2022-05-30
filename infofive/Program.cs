using System;
using System.Numerics;
using System.Text;

namespace infofive
{
    class Program
    {
        static void Main(string[] args)
        {
            int port1 = 8000;
            int port2 = 8001;


            Console.WriteLine("Iveskite teksta");
            String input = Console.ReadLine();
            Console.WriteLine("Iveskite p: ");
            int p = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Iveskite q: ");
            int q = Convert.ToInt32(Console.Read());

            string numbers = "";
            string ciphNumbers = "";

            var Encrypted = Rsa.Sign(input, p, q);
            var plainBytes = Encoding.ASCII.GetBytes(input);

            char[] cipherText = new char[Encrypted.Length];
            int[] EncryptedNumber = new int[Encrypted.Length];

            for (int i = 0; i < Encrypted.Length; i++)
            {
                numbers += int.Parse(plainBytes[i].ToString()) + " ";
                ciphNumbers += Encrypted[i] + " ";
                cipherText[i] = (char)Encrypted[i];
                EncryptedNumber[i] = Encrypted[i];
            }


            Console.WriteLine("Message value: " + numbers);
            Console.WriteLine("Signature value: " + ciphNumbers);

            BigInteger priKey = Rsa.GetPrivateKey();
            BigInteger[] pubKey = Rsa.GetPublicKey();
            Console.WriteLine("Private key: ( " + priKey + " )");
            Console.WriteLine("Public key: ( " + pubKey[0] + "; " + pubKey[1] + " )");

            Clien.Send(port1, pubKey, numbers, ciphNumbers);

        }
    }
}
