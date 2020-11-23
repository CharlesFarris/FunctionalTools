param($version = "0.0.1")
dotnet build -c Release /property:Company="Sleeping Bear Systems" /property:Version=$version /property:Product="FunctionalTools"