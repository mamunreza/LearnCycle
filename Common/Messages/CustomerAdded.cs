using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messages
{
    public class CustomerAdded
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CustomerNumber { get; set; }
    }
}
