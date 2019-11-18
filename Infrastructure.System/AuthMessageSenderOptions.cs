using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.System
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
        public string SendEmail { get; set; }
    }
}
