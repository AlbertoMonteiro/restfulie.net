﻿using Moq;
using NUnit.Framework;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Results;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class CreatedTests
    {
        private Created result;

        [SetUp]
        public void SetUp()
        {
            var mediaType = new Mock<IMediaType>();
            mediaType.SetupGet(mt => mt.Synonyms).Returns(new[] { "media-type" });

            result = new Created
            {
                MediaType = mediaType.Object
            };
        }

        [Test]
        public void ShouldSetStatusCode()
        {
            Assert.That(result.GetDecorators().Contains(typeof(StatusCode)));
        }

        [Test]
        public void ShouldSetLocation()
        {
            Assert.That(result.GetDecorators().Contains(typeof(Location)));
        }

        [Test]
        public void ShouldSetContent()
        {
            Assert.That(result.GetDecorators().Contains(typeof(Content)));
        }

        [Test]
        public void ShouldSetContentType()
        {
            Assert.That(result.GetDecorators().Contains(typeof(ContentType)));
        }
    }
}
