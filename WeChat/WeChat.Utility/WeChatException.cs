using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WeChat.Utility
{
    [Serializable]
    public class WeChatException : Exception
    {
        public List<string> Errors { get; set; }
        public string ErrorCode { get; set; }
        
        public WeChatException()
        {
            Errors = new List<string>();
        }

    
        public WeChatException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
            Errors = new List<string>();
        }

     
        public WeChatException(string message)
            : base(message)
        {
            ErrorCode = message;
            Errors = new List<string>();
        }

       
        public WeChatException(string message, Exception innerException)
            : base(message, innerException)
        {
            Errors = new List<string>();
        }

        public WeChatException(string errorcode, List<String> errors)
            : base(errorcode)
        {
            Errors = errors;
            ErrorCode = errorcode;
        }

        public WeChatException(string errorcode, string message)
            : base(errorcode)
        {
            Errors = new List<string> { message};
            ErrorCode = errorcode;
        }
    }
}
