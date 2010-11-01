using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using XeroConnector;
using XeroConnector.Interfaces;
using XeroConnector.Model;

namespace Specs_For_XeroSession
{
    public class When_Getting_A_Detailed_TrackingCategory
    {
        [Test]
        public void It_Should_Get_The_Name_Correctly()
        {
            Assert.That(model.Name, Is.EqualTo("Consultant"));
        }

        [Test]
        public void It_Should_Get_The_TrackingCategoryID_Correctly()
        {
            Assert.That(model.TrackingCategoryID, Is.EqualTo(new Guid("a9cb5b5e-398c-4620-8fed-cec3965abba8")));
        }

        [Test]
        public void It_Should_Get_The_Options_Correctly()
        {
            Assert.That(model.Options, Is.Not.Null.And.Not.Empty);
            Assert.That(model.Options, Has.Count.EqualTo(3));
            Assert.That(model.Options.Any(o => o.Name == "Odette Garrison"));
            Assert.That(model.Options.Any(o => o.Name == "Oliver Gray"));
            Assert.That(model.Options.Any(o => o.Name == "Tracy Green"));
        }

        [Test]
        public void It_Should_Set_The_XeroSession_To_The_Session_That_Created_It()
        {
            Assert.That(model.XeroSession, Is.SameAs(session));
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Arrange
            string xml = File.ReadAllText("Data\\SingleTrackingCategory.xml");
            var modelDoc = XDocument.Parse(xml);

            var connection = MockRepository.GenerateMock<IXeroConnection>();
            connection.Stub(c => c.MakeGetTrackingCategoryRequest(new Guid("a9cb5b5e-398c-4620-8fed-cec3965abba8"))).Return(modelDoc);

            // Act
            session = new XeroSession(connection);

            result = session.GetTrackingCategory(new Guid("a9cb5b5e-398c-4620-8fed-cec3965abba8"));
            model = result.Result;
        }

        private Response<TrackingCategory> result;
        private TrackingCategory model;
        private XeroSession session;
    }
}