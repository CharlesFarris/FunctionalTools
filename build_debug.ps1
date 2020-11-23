param($version = "0.0.1")
dotnet build -c Debug /property:Company="Sleeping Bear Systems" /property:Version=$version /property:Product="FunctionalTools"