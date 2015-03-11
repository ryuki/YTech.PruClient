using SharpArch.NHibernate.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.PruClient.Enums;

namespace YTech.PruClient.Domain.Contracts
{
    public interface ITReferenceRepository : INHibernateRepositoryWithTypedId<TReference, string>
    {
        TReference GetByReferenceType(EnumReferenceType referenceType);

    }
}
