using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Infrastructure
{
    public class NotificationDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateNotify { get; set; }
        public string Uri { get; set; }
    }
}
