using Xunit;

namespace MyBack.IntegrationTests.Application;

[CollectionDefinition(Name)]
public class ApplicationIntegrationTestCollection : ICollectionFixture<IntegrationTestsWebApplicationFactory>
{
    public const string Name = "application-integration-tests";
    
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}