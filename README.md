# EntityFramework .NET
## Installation
    VsCode: MSBuild project tools package vscode: autocompletion des paquets .NET / C#
    SDK: https://dotnet.microsoft.com/en-us/download/visual-studio-sdks
## Création projet
Génération du projet:  dotnet new console -o appIsitech -f net7.0
    -o définit le dossier d'installation
    -f la version de dotnet
dotnet new list => retourne la liste des modèles pouvant être généré
abusé du dotnet build plutôt que le run
## Version
.NET Version < 6
    using System;
    namespace ISITECH
    {
        class Programme{
            static void Main(string[] args){
                Console.WriteLine("Hello World!");
            }
        }
    }
.NET Version >= 6: unsing inclus de base 
    Modifiable avec la config du fichier consoleApp.csproj:
        <ImplicitUsings>enable</ImplicitUsings>  en disable
    Gérer les Usings manuellement:
        <ItemGroup>
            <Using Remove="System" />
            <Using Include="System.Numerics" />
        </ItemGroup>
    
## Convention de nommage .NET 
Variable: CamelCase
Privée: PascalCase(underscore)

## Visualisé impact des imports: 
    using System.Reflection;
    Assembly? myApp = Assembly.GetEntryAssembly();
    if(myApp == null)
        return;
    foreach (AssemblyName name in myApp.GetReferencedAssemblies())
    {
        Assembly a = Assembly.Load(name);
        int methodCount = 0;
        foreach (TypeInfo t in a.DefinedTypes)
        {
            methodCount += t.GetMethods().Count();
        }
        Console.WriteLine(
            "{0:N0} types with {1:N0} methods in {2} assembly.",
            arg0: a.DefinedTypes.Count(),
            arg1: methodCount, 
            arg2: name.Name
        );
    }
## Verbatim strings
    Utilisation de @ pour retirer échappement
## Raw string literals
    string html = """
        <html>
            <body>
                <p>Hello</p>
            </body>
        </html>
        """;

## Interpolation de chaine de charactère
    var student = new { Name = "John", Age = 18 };
    string interpolatedtring = $"{student.Name}";
    string mixedInterpolatedtring = $@"{student.Name} is in ""Oxford University"".";
    string json = $$"""
        {
            "Name": "{{student.Name}}",
            "Age": {{student.Age}}
        }
        """;
    Résultat:
        {
            "Name": "John",
            "Age": 18
        }

TODO: 
northwind => DB
gérer base de donnée au sein d'une application avec entity avec l'approche code first

DOC: https://github.com/bendahmanem/ISITECH-ESI5-2223-EF/tree/main

## Création de projet pour chaque partie du projet
Lancement depuis le dossier source du projet
### Création de la migration
dotnet ef migrations add test --startup-project .\EFCore.WebApi\EFCore.WebApi.csproj --project .\EFCore.Common.Migrations\EFCore.Common.Migrations.csproj  --context AppDbContext

### Lancement des migrations
dotnet ef database update --project .\EFCore.Common.Migrations\ --startup-project .\EFCore.WebApi\EFCore.WebApi.csproj

### Lancement du projet
dotnet run --project .\EFCore.WebApi\
