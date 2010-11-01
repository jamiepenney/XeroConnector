using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace XeroConnector
{
    [XmlRoot]
    public class Response<TModel>
    {
        [XmlElement]
        public Guid Id { get; set; }
        [XmlElement]
        public string Status { get; set; }
        [XmlElement]
        public DateTime? DateTimeUTC { get; set; }
        [XmlElement]
        public string ProviderName { get; set; }
        [XmlArray]
        public Collection<Error> Errors { get; set; }

        [XmlIgnore]
        public TModel Result { get; set; }
    }

    public class Error
    {
        [XmlElement]
        public int ErrorNumber { get; set; }
        [XmlElement]
        public string Type { get; set; }
        [XmlElement]
        public string Message { get; set; }
    }
}