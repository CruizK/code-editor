using System;
using System.Collections.Generic;

namespace CodeEditorApi.Services
{
    public class VerifyAccountTokenService
    {
        private static readonly Dictionary<string, string> _accountTokens = new Dictionary<string, string>();
        public static string GenerateToken(string email)
        {

            string guid = Guid.NewGuid().ToString();

            lock (_accountTokens)
            {
                _accountTokens.Add(guid, email);
            }

            return guid;
        }

        public static string? CheckToken(string token)
        {
            lock (_accountTokens)
            {
                if (_accountTokens.ContainsKey(token))
                {
                    string email = _accountTokens[token];
                    _accountTokens.Remove(token);
                    return email;
                }
                return null;
            }
        }


        public static void ClearCodes()
        {
            lock (_accountTokens)
            {
                _accountTokens.Clear();
            }
        }
    }
}
