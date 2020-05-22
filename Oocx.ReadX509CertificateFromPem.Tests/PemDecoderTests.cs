using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oocx.ReadX509CertificateFromPem;
using System.Text;

namespace Oocx.ReadX509CertificateFromPem.Tests
{
    [TestClass]
    public class PemDecoderTests
    {
        [TestMethod]
        public void PemWithoutMatchingSectionShouldReturnEmptyByteArray()
        {
            var pem = @"-----BEGIN Foo-----
data...
-----END Foo-----";

            var result = PemDecoder.DecodeSection(pem, "Bar");

            result.Length.Should().Be(0);
        }

        [TestMethod]
        public void EmptyPemShouldReturnEmptyByteArray()
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
