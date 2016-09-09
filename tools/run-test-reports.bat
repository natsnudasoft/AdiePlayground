@echo off
..\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user "-filter:+[*]* -[Mehdime.Entity]* -[*Tests]*" -target:..\packages\NUnit.ConsoleRunner.3.4.1\tools\nunit3-console.exe -targetargs:..\test\unit\unit-tests.nunit -excludebyattribute:*.ExcludeFromCodeCoverage* -excludebyfile:*Designer.cs -output:coverage.xml && ^
..\packages\ReportGenerator.2.4.5.0\tools\ReportGenerator.exe -reports:coverage.xml -targetdir:coverage && ^
echo Press any key to display report... && ^
pause >nul && ^
start coverage\index.htm