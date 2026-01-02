using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleLog.DAL.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public double DistanceKm { get; set; }
        public double AverageSpeed {  get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsIndoor { get; set; }
    }
}
