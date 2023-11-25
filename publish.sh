#!/bin/bash
set -euxo pipefail

MODNAME=DefaultRations

dotnet publish -c Release -o "bin/$MODNAME"

VERSION=$(git tag --points-at HEAD | grep '^v[0-9]')
ZIPFILE="${MODNAME}-${VERSION}.zip"

cd bin
rm -f "$ZIPFILE"
zip -r "$ZIPFILE" "$MODNAME"
