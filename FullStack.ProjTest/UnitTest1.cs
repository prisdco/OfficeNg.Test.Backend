using FullStack.ServiceModel;
using FuulStack_Test.Service;
using Moq;


namespace FullStack.ProjTest
{
    public class Tests
    {
        private readonly IApplicationClientFactory clientFactory;
        public Tests(IApplicationClientFactory clientFactory)
        {

            this.clientFactory = clientFactory;

        }
        [SetUp]
        public void Setup()
        {
        }

    }
}