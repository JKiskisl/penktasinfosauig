using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace infofive
{
    class Rsa
    {
        private static BigInteger n, fn, e, d, s, t;

        public static int[] Sign(string plainText, BigInteger p, BigInteger q)
        {
            byte[] textBytes = Encoding.ASCII.GetBytes(plainText);
            // n = p * q
            n = p * q;
            // Φ(n) = (p − 1)(q − 1)
            fn = (p - 1) * (q - 1);
            // e:   1 < e < Φ(n)  ir  DBD (e; Φ(n)) = 1
            Random random = new Random();
            s = 1;
            t = 1;
            do
            {
                e = RandomInteger(2, fn, random);
            }
            while (Gcd(fn, e, ref s, ref t) != 1);
            // d:   d * e (mod Φ(n)) = 1
            if (t >= 0)
                d = t;
            else
                d = fn + t;
            // viesasis raktas (n,e), privatus raktas d

            // parasas: s = x ^ d (mod(n))
            var signature = new int[textBytes.Length];
            for (int i = 0; i < textBytes.Length; i++)
            {
                signature[i] = (int)BigInteger.ModPow(textBytes[i], d, n);
            }
            return signature;
        }

        public static int[] Unsign(int[] signature, BigInteger[] pubKey)
        {
            n = pubKey[0];
            e = pubKey[1];

            // message: x' = s ^ e (mod(n))
            var decrypted = new int[signature.Length];
            for (int i = 0; i < signature.Length; i++)
            {
                decrypted[i] = (int)BigInteger.ModPow(signature[i], e, n);
            }
            return decrypted;
        }

        public static BigInteger[] GetPublicKey()
        {
            return new BigInteger[] { n, e };
        }

        public static BigInteger GetPrivateKey()
        {
            return d;
        }

        private static BigInteger RandomInteger(BigInteger bottomLimit, BigInteger topLimit, Random seed)
        {
            byte[] bytes = topLimit.ToByteArray();
            BigInteger res;
            do
            {
                seed.NextBytes(bytes);
                bytes[bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
                res = new BigInteger(bytes);
            } while (res >= topLimit || res < bottomLimit);
            return res;
        }

        private static BigInteger Gcd(BigInteger r0, BigInteger r1, ref BigInteger s, ref BigInteger t)
        {
            if (r0 == 0)
            {
                s = 0;
                t = 1;
                return r1;
            }
            BigInteger s1 = 1, t1 = 1;
            BigInteger g = Gcd(r1 % r0, r0, ref s1, ref t1);
            s = t1 - (r1 / r0) * s1;
            t = s1;
            return g;
        }

        public static bool IsPrime(BigInteger number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            var boundary = BigInteger.Pow(number, 2);
            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }
    }
}
