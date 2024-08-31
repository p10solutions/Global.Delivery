using AutoFixture;
using Global.Delivery.Application.Features.Deliveryman.Queries.GetLicenseType;

namespace Global.LicenseType.UnitTest.Application.Features.LicenseTypes.Queries.GetById
{
    public class GetLicenseTypeQueryUnitTest
    {
        readonly Fixture _fixture;

        public GetLicenseTypeQueryUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<GetLicenseTypeQuery>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            var command = new GetLicenseTypeQuery(Guid.Empty);

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
