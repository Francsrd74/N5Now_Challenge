using AccessControl.Domain.Common.Enums;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AccessControl.Application.Common.DTOs
{
    public class PermissionTopicMessageDto
    {
        public PermissionTopicMessageDto(string topicOperation)
        {
            TopicOperation = topicOperation;
        }

        public Guid Guid { get; set; }  = Guid.NewGuid(); 
        public string TopicOperation { get; set; }
    }
}
