#!/bin/bash
version=$1
hash=$(git rev-parse --short=7 HEAD)
buildNumber="v$version-$hash-$BUILD_BUILDNUMBER"

echo "##vso[task.setvariable variable=version]$version"
echo "##vso[task.setvariable variable=publish]true"
echo "##vso[task.setvariable variable=hash]$hash"
echo "##vso[build.updatebuildnumber]$buildNumber"
echo "##vso[build.addbuildtag]v$version"

projectFile=./Oocx.ReadX509CertificateFromPem/Oocx.ReadX509CertificateFromPem.csproj
changelog=$(cat CHANGELOG.md)
sed -i "s/0.0.0/${version}/g" $projectFile
sed -i "s/RELEASE_NOTES/${changelog}/g" $projectFile