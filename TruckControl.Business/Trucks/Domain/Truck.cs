using System;
using TruckControl.Business.Shared;
using TruckControl.Business.Shared.Validator;

namespace TruckControl.Business.Trucks.Domain
{
    public class Truck : Notifiable
    {
        private Truck(long id, int model, int manufacturingYear, int modelYear)
        {
            if(id <= 0)
                AddNotification("Id must be greater than zero");

            if (!Enum.IsDefined(typeof(ModelEnum), model))
                AddNotification("Model is invalid");

            if (manufacturingYear <= 0)
                AddNotification("Manufacturing year must be greater than zero");

            if (modelYear <= 0)
                AddNotification("Model year must be greater than zero");

            if (!Valid)
                return;

            Id = id;
            Model = (ModelEnum)model;
            ManufacturingYear = modelYear;
            ModelYear = modelYear;
        }

        public long Id { get; private set; }
        public ModelEnum Model { get; private set; }
        public int ManufacturingYear { get; private set; }
        public int ModelYear { get; private set; }

        // Fabric

        public static class Fabric
        {
            public static Truck CreateTruckForUpdate(long id, int model, int manufacturingYear, int modelYear)
                => new Truck(id, model, manufacturingYear, modelYear);
        }
    }
}
