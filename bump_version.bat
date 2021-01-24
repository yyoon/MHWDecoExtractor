@ECHO OFF

if [%1]==[] GOTO NO_ARG_ERROR

git diff-index --quiet HEAD --
IF ERRORLEVEL 1 GOTO UNCOMMITTED_CHANGES_ERROR

ECHO %1> VERSION
git commit -am "Bump version number to v%1"
git tag v%1
git push origin master --tags
EXIT /b 0

:NO_ARG_ERROR
ECHO The version number must be specified.
EXIT /b 1

:UNCOMMITTED_CHANGES_ERROR
ECHO The repository currently has uncommitted changes.
EXIT /b 1
