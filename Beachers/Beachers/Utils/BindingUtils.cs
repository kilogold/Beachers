using Beachers.Models;
using System.Collections.Generic;

namespace Beachers.Utils
{
    public class DeploymentGroup : List<GearModel>
    {
        public string GroupName { get; }
        public DeploymentGroup(int capacity, int groupNumber) : base(capacity)
        {
            GroupName = $"Deployment {groupNumber+1}"; // UI shall not be zero-based.
        }
    }

}
