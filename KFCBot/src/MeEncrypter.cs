using System;
using System.Collections.Generic;
using System.Text;

/*
 * TODO: 
 *      -> Key should be string
 */

namespace KFCBot.src
{
    public static class MeEncrypter
    {
        public static string CryptXOR(ref string text, int key)
        {
            byte[] mass = Encoding.Unicode.GetBytes(text);
            for (int i = 0; i < mass.Length; i++)
            {
                mass[i] = (byte)(mass[i] ^ key);
            }
            text = Encoding.Unicode.GetString(mass);
            return text;
        }
        public static string DecryptXOR(ref string text, int key)
        {
            return CryptXOR(ref text, key);
        }
    }
}
