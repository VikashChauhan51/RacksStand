using ServiceStack.DataAnnotations;
using System;
 

namespace Domain.Core
{
    [Alias("ActivityLog")]
    public  class ActivityLog
    {
        public string Id { get; set; }
        public byte ActivityType { get; set; }
        public string TargetObjectId { get; set; }
        public string CreatedBy { get; set; }
        public string Message { get; set; }
        public string Browser { get; set; }
        public string Platform { get; set; }
        public string Version { get; set; }
        public string HostName { get; set; }
        public string HostAddress { get; set; }
        public string URI { get; set; }
        public bool? IsMobileDevice { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
