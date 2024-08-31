using AutoFixture;
using Global.Delivery.Application.Features.Deliveryman.Commands.CreateDeliveryman;

namespace Global.Delivery.UnitTest.Application.Features.Deliveryman.Commands.CreateDeliveryman
{
    public class CreateDeliverymanCommandUnitTest
    {
        readonly Fixture _fixture;

        public CreateDeliverymanCommandUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            _fixture.Customize<CreateDeliverymanCommand>(c => c
                .With(p => p.Document, _fixture.Create<string>().Substring(0, 20))
                .With(p => p.LicenseNumber, _fixture.Create<string>().Substring(0, 20)));

            var command = _fixture.Create<CreateDeliverymanCommand>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid_When_License_Not_Informed()
        {
            var command = new CreateDeliverymanCommand(
                "José Santos",
                "358422222",
                new DateTime(1990, 3, 10),
                string.Empty,
                Domain.Entities.ELicenseType.A
            );

            var result = command.Validate();

            Assert.False(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid_When_Name_Not_Informed()
        {
            var command = new CreateDeliverymanCommand(
                string.Empty,
                "358422222",
                new DateTime(1990, 3, 10),
                "54545454",
                Domain.Entities.ELicenseType.A
            );

            var result = command.Validate();

            Assert.False(result);
        }


        [Fact]
        public void Command_Should_Be_Invalid_When_Document_Not_Informed()
        {
            var command = new CreateDeliverymanCommand(
                "José Silva",
                string.Empty,
                new DateTime(1990, 3, 10),
                "54545454",
                Domain.Entities.ELicenseType.A
            );

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
