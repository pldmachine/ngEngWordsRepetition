"# ngEngWordsRepetition" 

#Project initialize

1. dotnet new sln -n App
2. dotnet new Angular -n Web
2. dotnet new classLib -n Data
2. dotnet new classLib -n Services
dotnet sln add .\Web\Web.csproj
dotnet sln add .\Data\Data.csproj
dotnet sln add .\Services\Services.csproj
dotnet add .\Web\Web.csproj reference .\Data\Data.csproj
dotnet add .\Web\Web.csproj reference .\Services\Services.csproj

cd .\Web\
npm install
cd ..
dotnet build
cd .\Web\
$Env:ASPNETCORE_ENVIRONMENT = "Development"
dotnet run

cd .\Data
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

cd .\Web
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

Project: Web
           services.AddDbContext<EFContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


Data>dotnet ef migrations add Words -s ../Web/Web.csproj -v

##Testing

http://xunit.github.io/docs/getting-started-dotnet-core

dotnet add package xunit
 <TargetFramework>netcoreapp2.0</TargetFramework>
 <DotNetCliToolReference Include="dotnet-xunit" Version="2.3.1" />
