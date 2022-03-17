using TruckControl.Business.Trucks.Domain;
using Xunit;

namespace TruckControl.Test.Business.Domain
{
    public class TruckTest
    {
        [Fact]
        public void Must_notify_if_id_is_not_valid()
        {
            var truck = Truck.Fabric.CreateTruckForUpdate(0, 1, 2000, 2001);

            Assert.False(truck.Valid);
            Assert.Contains(truck.Notifications, nf => nf == "Id must be greater than zero");
        }
        
        [Fact]
        public void Must_notify_if_model_is_not_valid()
        {
            var truck = Truck.Fabric.CreateTruckForUpdate(1, 99, 2000, 2001);

            Assert.False(truck.Valid);
            Assert.Contains(truck.Notifications, nf => nf == "Model is invalid");
        }
        
        [Fact]
        public void Must_notify_if_manufacturing_year_is_not_valid()
        {
            var truck = Truck.Fabric.CreateTruckForUpdate(1, 1, 0, 2001);

            Assert.False(truck.Valid);
            Assert.Contains(truck.Notifications, nf => nf == "Manufacturing year must be greater than zero");
        }
        
        [Fact]
        public void Must_notify_if_model_year_is_not_valid()
        {
            var truck = Truck.Fabric.CreateTruckForUpdate(1, 1, 2000, 0);

            Assert.False(truck.Valid);
            Assert.Contains(truck.Notifications, nf => nf == "Model year must be greater than zero");
        }
    }
}
