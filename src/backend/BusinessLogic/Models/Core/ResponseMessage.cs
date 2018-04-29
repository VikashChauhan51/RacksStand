using System;
using System.Runtime.Serialization;
namespace Models.Core
{
    [Serializable]
    [DataContract]
    public class ResponseMessage<T> where T : class, new()
    {
        #region ctor
        public ResponseMessage()
        {
        }
        public ResponseMessage(T data)
        {
            this.Data = data;
        }
        public ResponseMessage(bool isSucceed, string message, T data)
        {
            this.IsSucceed = isSucceed;
            this.Message = message;
            this.Data = data;
        }
        #endregion
        /// <summary>
        /// Status of request result.
        /// </summary>
        [DataMember]
        public bool IsSucceed { get; set; }
        /// <summary>
        /// Message of request result.
        /// </summary>
        [DataMember]
        public string Message { get; set; }
        /// <summary>
        /// Data of request result.
        /// </summary>
        [DataMember]
        public T Data { get; set; }
    }
}
