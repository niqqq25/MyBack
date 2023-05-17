using Xunit;

namespace MyBack.IntegrationTests.Application;

[Collection(ApplicationIntegrationTestCollection.Name)]
public class ApplicationTestBase : TestBase
{
    public ApplicationTestBase(IntegrationTestsWebApplicationFactory factory) : base(factory)
    {
    }
}