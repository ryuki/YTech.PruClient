using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.PruClient.Domain.Contracts
{
    public interface ITBingoRepository : INHibernateRepositoryWithTypedId<TBingo, string>
    {
        TBingo GetByDate(DateTime bingoDate);

        TBingo GetBingoByWeekStatus(int week, int year, string bingoStatus);
    }
}
