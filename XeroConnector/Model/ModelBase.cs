using System.Xml.Serialization;
using XeroConnector.Interfaces;

namespace XeroConnector.Model
{
    public interface IModel : ILazyLoad
    {
        [XmlIgnore]
        IXeroSession XeroSession { get; set; }

        void LoadDetailedInformation();
    }

    public abstract class ModelBase : IModel
    {
        [XmlIgnore]
        public IXeroSession XeroSession { get; set; }

        protected ModelBase(IXeroSession session)
        {
            XeroSession = session;
        }

        public virtual void LoadDetailedInformation() {}

        public bool IsLoaded { get; set; }
    }

    public interface ILazyLoad
    {
        bool IsLoaded { get; set; }
    }

    public class LazyLoadMixin : ILazyLoad
    {
        public bool IsLoaded { get; set; }
    }
}