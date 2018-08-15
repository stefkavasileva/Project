using System;

namespace Landmarks.Web.Common.Helpers.Messages
{
    [Serializable]
    public class MessageModel
    {
        public MessageType Type { get; set; }

        public string Message { get; set; }
    }
}
