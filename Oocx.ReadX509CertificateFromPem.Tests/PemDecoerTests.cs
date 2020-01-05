using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oocx.ReadX509CertificateFromPem;
using System.Text;

namespace Oocx.ReadX509CertificateFromPem.Tests
{
    [TestClass]
    public class PemDecoerTests
    {
        [TestMethod]
        public void PemWithoutMatchingSectionShouldReturnEmtpyByteArray()
        {
            var pem = @"-----BEGIN Foo-----
data...
-----END Foo-----";

            var result = PemDecoder.DecodeSection(pem, "Bar");

            result.Length.Should().Be(0);
        }

        [TestMethod]
        public void EmptyPemShouldReturnEmtpyByteArray()
        {
            var pem = "";

            var result = PemDecoder.DecodeSection(pem, "Bar");

            result.Length.Should().Be(0);
        }

        [TestMethod]
        public void PemWithSingleSectionThatMatchesShouldReturnDecodedData()
        {
            var pem = @"-----BEGIN Foo-----
QmFy
-----END Foo-----";

            var result = PemDecoder.DecodeSection(pem, "Foo");

            result.Length.Should().Be(3);
            Encoding.UTF8.GetString(result).Should().Be("Bar");
        }



        [TestMethod]
        public void PemWithMultipleSectionsOneMatchesShouldReturnDecodedData()
        {
            var pem = @"-----BEGIN Bar-----
data...
-----END Bar-----
-----BEGIN Foo-----
QmFy
-----END Foo-----";

            var result = PemDecoder.DecodeSection(pem, "Foo");

            result.Length.Should().Be(3);
            Encoding.UTF8.GetString(result).Should().Be("Bar");
        }
    }
}
