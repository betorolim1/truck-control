using System;
using TruckControl.Business.Shared;
using TruckControl.Business.Shared.Validator;

namespace TruckControl.Business.Trucks.Domain
{
    public class TruckDomain : Notifiable
    {
        private TruckDomain(long id, int model, int manufacturingYear, int modelYear)
        {
            Id = id;
            Model = (ModelEnum)model;
            ManufacturingYear = manufacturingYear;
            ModelYear = modelYear;

            ValidateUpdate();
        }
        
        private TruckDomain(int model, int manufacturingYear, int modelYear)
        {
            Model = (ModelEnum)model;
            ManufacturingYear = manufacturingYear;
            ModelYear = modelYear;

            ValidateCreate();
        }

        public long Id { get; private set; }
        public ModelEnum Model { get; private set; }
        public int ManufacturingYear { get; private set; }
        public int ModelYear { get; private set; }

        private void ValidateCreate()
        {
            _validate();
        }
        
        private void ValidateUpdate()
        {
            if (Id <= 0)
                AddNotification("Id must be greater than zero");

            _validate();
        }

        private void _validate()
        {
            if (!Enum.IsDefined(typeof(ModelEnum), Model))
                AddNotification("Model is invalid");

            if (ManufacturingYear <= 0)
                AddNotification("Manufacturing year must be greater than zero");

            if (ModelYear <= 0)
                AddNotification("Model year must be greater than zero");
        }

        // Fabric

        public static class Fabric
        {
            public static TruckDomain CreateTruckForUpdate(long id, int model, int manufacturingYear, int modelYear)
                => new TruckDomain(id, model, manufacturingYear, modelYear);

            public static TruckDomain CreateTruckForInsert(int model, int manufacturingYear, int modelYear)
                => new TruckDomain(model, manufacturingYear, modelYear);
        }
    }
}
