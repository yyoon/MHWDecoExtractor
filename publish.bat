@ECHO OFF
SET /p VERSION=<VERSION
dotnet publish ^
  -c Release ^
  -p:DebugType=None ^
  -p:Version=%VERSION% ^
  .\MHWDecoExtractorGUI\MHWDecoExtractorGUI.csproj

PowerShell -Command "& {Compress-Archive -Path .\MHWDecoExtractorGUI\bin\Release\netcoreapp3.1\publish\*.* -DestinationPath .\MHWDecoExtractorGUI-%VERSION%-win-x64.zip }"
