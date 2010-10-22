using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using XeroConnector.Interfaces;

namespace XeroConnector.Model
{
    public class TrackingCategory : ModelBase
    {
        internal TrackingCategory() : base(null) {}

        public TrackingCategory(IXeroSession session) : base(session)
        {
        }

        [XmlElement]
        public Guid TrackingCategoryID { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [XmlArray]
        public Collection<Option> Options { get; set; }
    }

    public class Option
    {
        [XmlElement]
        public string Name { get; set; }
    }
}