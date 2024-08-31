using Global.Delivery.Application.Features.Deliveryman.Commands.UpdateDeliveryman;

namespace Global.Delivery.UnitTest.Application.Features.Deliveryman.Commands.UpdateDeliveryman
{
    public class UpdateDeliverymanCommandUnitTest
    {
        public UpdateDeliverymanCommandUnitTest()
        {
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = new UpdateDeliverymanCommand(Guid.NewGuid(), "teste.png", MemoryStreamTest.GetPng());

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid_When_Id_Is_Empty()
        {
            Guid id = Guid.Empty;

            var command = new UpdateDeliverymanCommand(
                id,
                "teste.png",
                MemoryStreamTest.GetPng()
            );

            var result = command.Validate();

            Assert.False(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid_When_File_Is_Empty()
        {
            Guid id = Guid.Empty;

            var command = new UpdateDeliverymanCommand(
                id,
                "teste.png",
                new MemoryStream()
            );

            var result = command.Validate();

            Assert.False(result);
        }


        [Fact]
        public void Command_Should_Be_Invalid_When_File_Extension_Is_Invalid()
        {
            Guid id = Guid.Empty;

            var command = new UpdateDeliverymanCommand(
                id,
                "teste.txt",
               MemoryStreamTest.GetPng()
            );

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
