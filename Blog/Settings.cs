using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using System.IO;

namespace Blog
{
    public static class Settings
    {
        public static bool Pre_moderation_comments { get; set; } = false;
        public static string ConnectionString { get; private set; }


        private static string hashedPassData = "private/HashedPassword.txt";
        private static string connectionData = "private/Connection.txt";

        public static void readConnectionString()
        {
            try
            {
                ConnectionString = File.ReadAllText(connectionData);
            }
            catch
            {
                ConnectionString = "";
            }
        }
        public static async Task<bool> checkPassword(string password)
        {
            bool valid = false;
            try
            {
                if(getHash(password) == await readHash())
                {
                    valid = true;
                }
            }
            catch
            {
                valid = false;
            }
            return valid;

        }

        private static string getHash(string _password)
        {
            byte[] arr = Encoding.ASCII.GetBytes("1Z20f45e"); //to settings
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: _password,
                salt: arr,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));
            return hashed;
        }

        private static async Task<string> readHash()
        {
            string hash = "";
            try
            {
                hash = await File.ReadAllTextAsync(hashedPassData);
                
            }
            catch
            {
                hash = "////";
            }
            return hash;
        }
    }
}
