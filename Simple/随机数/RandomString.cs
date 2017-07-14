using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple
{
    public class RandomString
    {
        static System.Random random = new System.Random();
        private static char[] _CHAR_ARRAY = new char[] { 
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 
                'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 
                'W', 'X', 'Y', 'Z'
             };
        private static char[] _LETTER_ARRAY = new char[] { 
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 
                'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
             };
        private static char[] _NUM_ARRAY = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public static string GetRandomCharacter(int pLen)
        {
            var builder = new StringBuilder(100);
            for (int i = 0; i < pLen; i++)
            {
                builder.Append(_CHAR_ARRAY[random.Next(36)]);
            }
            return builder.ToString();
        }
        public static string GetRandomLetter(int pLen)
        {
            var builder = new StringBuilder(100);
            for (var i = 0; i < pLen; i++)
            {
                builder.Append(_LETTER_ARRAY[random.Next(26)]);
            }
            return builder.ToString();
        }
        public static string GetRandomNumber(int pLen)
        {
            var builder = new StringBuilder(100);
            for (var i = 0; i < pLen; i++)
            {
                builder.Append(_NUM_ARRAY[random.Next(10)]);
            }
            return builder.ToString();
        }
        public static int RandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public static byte[] RandomByte(int len)
        {
            var result = new List<byte>();
            for (var i = 0; i < len; i++)
            {
                result.Add(Convert.ToByte(RandomInt(0, 255)));
            }
            return result.ToArray();
        }
    }
}
