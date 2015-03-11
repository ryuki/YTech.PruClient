using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.PruClient.Enums;

namespace YTech.PruClient.Domain.Contracts.Tasks
{
    public interface ITReferenceTasks
    {
        TReference GetByReferenceType(EnumReferenceType referenceType);
        TReference Update(Domain.TReference reference);
    }
}
