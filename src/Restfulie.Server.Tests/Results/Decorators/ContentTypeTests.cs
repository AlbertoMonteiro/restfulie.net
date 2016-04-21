﻿using System.Web;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Tests.Results.Decorators
{
    [TestFixture]
    public class ContentTypeDecoratorTests
    {
        private Mock<ControllerContext> context;
        private Mock<HttpContextBase> httpContext;
        private Mock<HttpResponseBase> httpResponseBase;

        [SetUp]
        public void SetUp()
        {
            httpResponseBase = new Mock<HttpResponseBase>();
            httpContext = new Mock<HttpContextBase>();
            context = new Mock<ControllerContext>();

            context.Setup(c => c.HttpContext).Returns(httpContext.Object);
            httpContext.Setup(h => h.Response).Returns(httpResponseBase.Object);
        }

        [Test]
        public void ShouldSetContentType()
        {
            new ContentType("application/xml").Execute(context.Object);

            httpResponseBase.VerifySet(h => h.ContentType = "application/xml");
        }

        [Test]
        public void ShouldNotSetContentTypeWhenIsEmpty()
        {
            new ContentType(string.Empty).Execute(context.Object);

            httpResponseBase.VerifySet(h => h.ContentType = string.Empty, Times.Never());
        }

        [Test]
        public void ShouldCallNextDecorator()
        {
            var nextDecorator = new Mock<ResultDecorator>();

            new ContentType("application/xml", nextDecorator.Object).Execute(context.Object);

            nextDecorator.Verify(nd => nd.Execute(context.Object));
        }
    }
}