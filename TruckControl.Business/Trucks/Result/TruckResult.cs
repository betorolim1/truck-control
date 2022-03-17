using TruckControl.Business.Shared;

namespace TruckControl.Business.Trucks.Result
{
    public class TruckResult
    {
        public ModelEnum Model { get; set; }
        public int ManufacturingYear { get; set; }
        public int ModelYear { get; set; }
    }
}
