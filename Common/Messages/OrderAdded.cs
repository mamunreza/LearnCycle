using System;

namespace Common.Messages
{
    public class OrderAdded
    {
        public Guid Id { get; set; }
        public string OrderNo { get; set; }
        public string OrderDate { get; set; }
    }
}
