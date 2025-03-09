using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wulkanizacja.Desktop.Exceptions
{
    public class ApiResponseException : Exception
    {
        public ApiResponseException(string message)
            : base(message)
        {
        }
    }
}
