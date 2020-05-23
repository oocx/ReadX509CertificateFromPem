using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oocx.ReadX509CertificateFromPem;
using System.IO;

namespace Oocx.ReadX509CertificateFromPem.Tests
{
    [TestClass]
    public class CertificateFromPemReaderTests
    {
        [TestMethod]
        [DeploymentItem("rsa")]
        public void ShouldLoadCertificateWithPkcs8RSAKey()
        {
            var sut = new CertificateFromPemReader();

            var certificate = sut.LoadCertificateWithPrivateKey("rsa/certificate.pem", "rsa/key.pem");

            certificate.SerialNumber.Should().Be("009F8967503EECCE07");
            certificate.HasPrivateKey.Should().BeTrue();
            certificate.PrivateKey.KeyExchangeAlgorithm.Should().Be("RSA");
        }

        [TestMethod]
        [DeploymentItem("ecdh")]
        [Ignore]
        public void ShouldLoadCertificateWithPkcs8ECDHKey()
        {
            var sut = new CertificateFromPemReader();

            var certificate = sut.LoadCertificateWithPrivateKey("ecdh/certificate.pem", "ecdh/key.pem");

            certificate.SerialNumber.Should().Be("009F8967503EECCE07");
            certificate.HasPrivateKey.Should().BeTrue();
        }

        [TestMethod]
        [DeploymentItem("ecdh2")]
        public void ShouldLoadCertificateWithECPrivateKey()
        {
            var sut = new CertificateFromPemReader();

            var certificate = sut.LoadCertificateWithPrivateKey("ecdh2/certificate.pem", "ecdh2/key.pem");

            certificate.SerialNumber.Should().Be("00DBE1B57BA78B7A78");
            certificate.HasPrivateKey.Should().BeTrue();
        }

        [TestMethod]
        [DeploymentItem("rsa")]
        public void ShouldLoadMatchingCertificatesFromFileOrString()
        {
            var sut = new CertificateFromPemReader();
            var certificateFile = sut.LoadCertificateWithPrivateKey("rsa/certificate.pem", "rsa/key.pem");
            var certificateString = sut.LoadCertificateWithPrivateKeyFromStrings(File.ReadAllText("rsa/certificate.pem"), File.ReadAllText("rsa/key.pem"));


            certificateString.Thumbprint.Should().Be(certificateFile.Thumbprint);
            certificateString.HasPrivateKey.Should().BeTrue();
            certificateString.PrivateKey.KeyExchangeAlgorithm.Should().Be("RSA");
        }
    }
}
