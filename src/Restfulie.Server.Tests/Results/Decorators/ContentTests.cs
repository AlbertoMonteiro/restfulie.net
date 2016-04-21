﻿using System.IO;
using System.Web;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Tests.Results.Decorators
{
    [TestFixture]
    public class ContentDecoratorTests
    {
        private Mock<ControllerContext> context;
        private Mock<HttpContextBase> httpContext;
        private Mock<HttpResponseBase> httpResponseBase;
        private Mock<TextWriter> output;

        [SetUp]
        public void SetUp()
        {
            output = new Mock<TextWriter>();
            httpResponseBase = new Mock<HttpResponseBase>();
            httpContext = new Mock<HttpContextBase>();
            context = new Mock<ControllerContext>();

            context.Setup(c => c.HttpContext).Returns(httpContext.Object);
            httpContext.Setup(h => h.Response).Returns(httpResponseBase.Object);
            httpResponseBase.Setup(h => h.Output).Returns(output.Object);
        }

        [Test]
        public void ShouldSetContent()
        {
            new Content("some content here").Execute(context.Object);

            output.Verify(o => o.Write("some content here"));
            output.Verify(o => o.Flush());
        }

        [Test]
        public void ShouldNotSetContentWhenIsEmpty()
        {
            new Content(string.Empty).Execute(context.Object);

            output.Verify(o => o.Write(string.Empty), Times.Never());
            output.Verify(o => o.Flush(), Times.Never());
        }

        [Test]
        public void ShouldCallNextDecorator()
        {
            var nextDecorator = new Mock<ResultDecorator>();

            new Content("some content here", nextDecorator.Object).Execute(context.Object);

            nextDecorator.Verify(nd => nd.Execute(context.Object));
        }
    }
}