using System;

namespace Landmarks.Web.Common.Messages
{
    [Serializable]
    public class MessageModel
    {
        public MessageType Type { get; set; }

        public string Message { get; set; }
    }
}
