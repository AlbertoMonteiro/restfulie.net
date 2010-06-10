﻿using NUnit.Framework;
using Restfulie.Server.Configuration;
using Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Unmarshalling.Deserializers.Xml;
using System.Linq;

namespace Restfulie.Server.Tests.Configuration
{
    [TestFixture]
    public class RestfulieConfigurationTests
    {
        [Test]
        public void ShouldRegisterSerializerAndDeserializerForAMediaType()
        {
            var config = new RestfulieConfiguration();

            config.Register<XmlAndHypermedia>(new XmlSerializer(), new XmlHypermediaInserter(), new XmlDeserializer());
            
            var mediaType = config.MediaTypes.Find("application/xml");
            
            Assert.IsTrue(mediaType.Serializer is XmlSerializer);
            Assert.IsTrue(mediaType.Deserializer is XmlDeserializer);
            Assert.IsTrue(mediaType.Hypermedia is XmlHypermediaInserter);
        }

        [Test]
        public void ShouldRegisterAVendorizedMediaType()
        {
            var config = new RestfulieConfiguration();
            config.RegisterVendorized("some-vendor-media-type", new XmlSerializer(), new XmlHypermediaInserter(), new XmlDeserializer());

            var mediaType = config.MediaTypes.Find("some-vendor-media-type");

            Assert.IsTrue(mediaType is Vendorized);
            Assert.AreEqual("some-vendor-media-type", mediaType.Synonyms.First());
        }
    }
}