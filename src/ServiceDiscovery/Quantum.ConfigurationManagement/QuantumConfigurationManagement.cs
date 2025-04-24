using Microsoft.Extensions.Configuration;

namespace Quantum.ConfigurationManagement;

public class QuantumConfigurationManagement
{
    public QuantumConfigurationBuilder IWantToAddConfiguration()
        => new(new ConfigurationBuilder());
}

public class QuantumConfigurationBuilder(IConfigurationBuilder builder) : ConfigurationBuilder
{
    public IConfigurationBuilder Builder { get; } = builder;

    public QuantumConfigurationBuilder UseJsonFile(string path, bool optional, bool reloadOnChange)
    {
        Builder.AddJsonFile(path, optional, reloadOnChange);

        return this;
    }

    public QuantumConfigurationBuilder UseEnvironmentVariables()
    {
        Builder.AddEnvironmentVariables();

        return this;
    }

    public QuantumConfigurationBuilder UseCommandLine(string[] args)
    {
        Builder.AddCommandLine(args);
        return this;
    }

    public IConfigurationBuilder WithConfigurationRoot() 
        => Builder;

    public IConfigurationRoot Build()
    {
        return Builder.Build();
    }
}