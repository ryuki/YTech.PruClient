using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.PruClient.Domain.Contracts.Tasks
{
    public interface ITWOStatusTasks
    {
        IEnumerable<TWOStatus> GetWOStatus(string woId);
    }
}
