using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Day4
{
    class Program
    {
        // from: http://blogs.msdn.com/b/csharpfaq/archive/2006/10/09/how-do-i-calculate-a-md5-hash-from-a-string_3f00_.aspx
        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        static void Main(string[] args)
        {
            string input = "bgvyzdsv";
            string hash;

            int x = 0;
            do {
                x++;
                hash = CalculateMD5Hash(input + x.ToString());
            } while (!hash.StartsWith("00000"));

            Console.WriteLine("Answer 1: {0}", x);


            x = 0;
            do
            {
                x++;
                hash = CalculateMD5Hash(input + x.ToString());
            } while (!hash.StartsWith("000000"));

            Console.WriteLine("Answer 2: {0}", x);

            Console.ReadKey();
        }
    }
}
