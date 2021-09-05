using System.Runtime.Serialization;

namespace AcidLabsTest.Service.Models.Exceptions
{
    [DataContract]
    public sealed class ExceptionModel
    {
        public ExceptionModel(string title, string exceptionMessage, int statusCode)
        {
            Title = title;
            ExceptionMessage = exceptionMessage;
            StatusCode = statusCode;
        }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "exceptionMessage")]
        public string ExceptionMessage { get; set; }

        [DataMember(Name = "statusCode")]
        public int StatusCode { get; set; }
    }
}
