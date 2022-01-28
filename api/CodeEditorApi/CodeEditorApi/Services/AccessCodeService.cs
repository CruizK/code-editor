using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeEditorApiDataAccess.StaticData;

namespace CodeEditorApi.Services
{
    // TODO: Replace this with redis with expirations
    public static class AccessCodeService
    {

        private static readonly Dictionary<string, Roles> _accessCodes = new Dictionary<string, Roles>();
        public static string GenerateAccessCode(Roles role)
        {
           
            string guid = Guid.NewGuid().ToString();
            
            lock(_accessCodes)
            {
                _accessCodes.Add(guid, role);
            }

            return guid;
        }

        public static Roles? CheckAccessCode(string accessCode)
        {
            lock(_accessCodes)
            {
                if (_accessCodes.ContainsKey(accessCode))
                {
                    Roles role = _accessCodes[accessCode];
                    _accessCodes.Remove(accessCode);
                    return role;
                }
                return null;
            }
        }
    }
}
