namespace TruckControl.Business.Trucks.Commands
{
    public class UpdateTruckCommand
    {
        public long Id { get; set; }
        public int Model { get; set; }
        public int ManufacturingYear { get; set; }
        public int ModelYear { get; set; }
    }
}
