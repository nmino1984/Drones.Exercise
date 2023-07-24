using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Application.ViewModels.DroneMedication.Request
{
    public class DispatchRequestViewModel
    {
        public int droneId { get; set; }
        public List<int>? listMedications { get; set; }
    }
}
