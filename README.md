# MHWDecoExtractor

## Overview

This utility helps you read your `MONSTER HUNTER: WORLD` and
`MONSTER HUNTER WORLD: ICEBORNE` save files and extract the list of all the
decorations your character posseses. The list can then be exportedto be used on
http://summoned.me/ skill simulator.

This utility currently only supports Korean.

## Build Instructions

Clone the [*forked* version of `MHWSaveUtils` repo][saveutils-fork] as a sibling
of this repo. The resulting directory structure should look like the following:

```
<parent dir>
 |- MHWDecoExtractor (this project)
 +- MHWSaveUtils (fork)
```

The forked version of `MHWSaveUtils` project adds the capability of choosing the
language used when loading the master data.

After this, open the `MHWDecoExtractor.sln` solution with Visual Studio and
build the project.

## Publishing

From a clean repository state (i.e., no uncommitted changes), run the following
commands to release a new version.

```
.\bump_version.bat <new_version>
.\publish.bat
```

The new release executable will be produced under
`.\MHWDecoExtractorGUI\bin\Release\netcoreapp3.1\win-x64\publish\`.

Create a release page on GitHub and upload the executable.

## Credits

Thanks to https://github.com/TanukiSharp for providing the save utils library.

## License

MIT License

### Addendum

DO NOT use this code, in part or in totality, to cheat, or produce code that
would eventually lead to cheat.

## Disclaimer

This project is a personal hobby project and has no connection with the author's
employer. Use this tool at your own risk.


[saveutils-fork]: https://github.com/yyoon/MHWSaveUtils
