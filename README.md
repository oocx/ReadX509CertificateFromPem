[![Build Status](https://dev.azure.com/oocx/ReadX509CertificateFromPem/_apis/build/status/oocx.ReadX509CertificateFromPem?branchName=master)](https://dev.azure.com/oocx/ReadX509CertificateFromPem/_build/latest?definitionId=4&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=oocx_ReadX509CertificateFromPem&metric=alert_status)](https://sonarcloud.io/dashboard?id=oocx_ReadX509CertificateFromPem)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=oocx_ReadX509CertificateFromPem&metric=coverage)](https://sonarcloud.io/dashboard?id=oocx_ReadX509CertificateFromPem)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=oocx_ReadX509CertificateFromPem&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=oocx_ReadX509CertificateFromPem)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=oocx_ReadX509CertificateFromPem&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=oocx_ReadX509CertificateFromPem)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=oocx_ReadX509CertificateFromPem&metric=security_rating)](https://sonarcloud.io/dashboard?id=oocx_ReadX509CertificateFromPem)

# Read certificates and private keys from PEM files

X509 certificates and private keys can be stored in different formats. Not all of them are supported out of the box by the .NET class library.

Certificates with private keys can currently only be loaded from .pfx files (PKCS#12).


For RSA, DSA, and ECDsa, there is X509Certificate2.CopyWithPrivateKey() which could be used to add a private key to a certificate.
However, there is no overload that supports an ECDH private key. .NET Core 3 adds a Pkcs12Builder class. This class can be used to create a pfx file at runtime. This allows us to read certificate and
private key from a PEM file to create a new X509Certificate2 instance.

This library does exactly that. You can either copy the source to your own project or reference it as a NuGet package (TODO - this is work in progress, I did not publish a package yet).

So assuming you have two files, ``certificate.pem`` and ``key.pem``, you can now can then easily create a new X509Certificate2 from those files:

```
var reader = new CertificateFromPemReader();
X509Certificate2 myCertificate = reader.LoadCertificateWithPrivateKey("certificate.pem", "key.pem");
```

As .NET core uses different, platform specific implementations for cryptography, not all types of keys will work on all platforms.

I'm using this code in a .net core container that runs on kubernetes and gets its certificate from cert-manager, which proviedes certificaets as
Kubernetes secrets with key and certificate in PEM format.
