using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BitcoinBrainWalletSweeper
{
    public class Crypto
    {
        public string ComputeHash(string input, HashAlgorithm algorithm)
        {
            if (input != null)
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                if (algorithm != null)
                {
                    var hashedBytes = algorithm.ComputeHash(inputBytes);

                    return BitConverter.ToString(hashedBytes);
                }
            }
            return null;
        }
    }
}
