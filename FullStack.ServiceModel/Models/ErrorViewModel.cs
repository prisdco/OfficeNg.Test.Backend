using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStack.ServiceModel
{
    public class ErrorViewModel
    {
        public List<Error> Errors { get; set; } = new List<Error>();
    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
