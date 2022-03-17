using System;
using System.Collections.Generic;
using System.Text;

namespace TruckControl.Business.Shared.Validator.Interface
{
    public interface INotifiable
    {
        bool Valid { get; }
        List<string> Notifications { get; }
    }
}
