packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user -filter:" +[*]Restfulie.Server.* -[*]Restfulie.Server.Tests.*" -target:"packages\NUnit.ConsoleRunner.3.2.0\tools\nunit3-console.exe" -targetargs:"/domain:single src\Restfulie.Server.Tests\bin\release\Restfulie.Server.Tests.dll" -output:coverage.xml

packages\coveralls.io.1.3.4\tools\coveralls.net.exe --opencover coverage.xml