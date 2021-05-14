using Xunit;

namespace Test.Grpc
{
    [CollectionDefinition("Servicestack")]
    public class BasicServiceStackCollection : ICollectionFixture<BasicServiceStackTestFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}