using System.Formats.Asn1;

namespace Quantum.ServiceDiscovery.Tests;

public class UrlTests
{
    [Fact]
    public async Task METHOD()
    {
        var url = "http://localhost:20";
        var uri = new Uri(url);

        Assert.Equal(20, uri.Port);
        Assert.Equal("http://localhost", uri.Host);
        
    }
}