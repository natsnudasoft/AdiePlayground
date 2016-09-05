..\..\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe^
 "-target:..\..\packages\NUnit.ConsoleRunner.3.4.1\tools\nunit3-console.exe"^
 "-targetargs:AdiePlayground.CommonTests\bin\Release\AdiePlayground.CommonTests.dll AdiePlayground.DataTests\bin\Release\AdiePlayground.DataTests.dll"^
 -register:user^
 "-filter:+[*]* -[Mehdime.Entity]* -[*Tests]*"^
 -excludebyattribute:*.ExcludeFromCodeCoverage*^
 -excludebyfile:*Designer.cs && ^
..\..\packages\ReportGenerator.2.4.5.0\tools\ReportGenerator.exe^
 -reports:results.xml^
 -targetdir:coverage && ^
start coverage\index.htm