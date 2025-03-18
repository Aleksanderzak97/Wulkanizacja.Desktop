using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wulkanizacja.Desktop.Services
{
    public class TokenService
    {
        private string _jwtToken;

        public void SetToken(string token)
        {
            _jwtToken = token;
        }

        public string GetToken()
        {
            return _jwtToken;
        }
    }
}
