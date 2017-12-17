set DAY=%1
set TOPIC=%2
set USER=Jeroen
set LANG=C#
set FOLDER="..\..\day %DAY%\Jeroen - C#\%TOPIC%"
mkdir %FOLDER%
xcopy /s template %FOLDER%
cd %FOLDER%
move template.csproj %TOPIC%.csproj

