namespace TruckControl.Model.EFModel
{
    public partial class Truck
    {
        public long Id { get; set; }
        public int Model { get; set; }
        public int ManufacturingYear { get; set; }
        public int ModelYear { get; set; }
    }
}
