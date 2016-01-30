using NHibernate;
using NHibernate.Criterion;
using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.PruClient.Domain;
using YTech.PruClient.Domain.Contracts;

namespace YTech.PruClient.Infrastructure.Repository
{
    public class TBingoRepository : NHibernateRepositoryWithTypedId<TBingo, string>, ITBingoRepository
    {
        public TBingo GetByDate(DateTime bingoDate)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TBingo));
            criteria.Add(Expression.Eq("BingoDate", bingoDate));
            criteria.SetMaxResults(1);
            return criteria.UniqueResult<TBingo>();
        }


        public TBingo GetBingoByWeekStatus(int week, int year, string bingoStatus)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TBingo));
            criteria.Add(Expression.Eq("BingoWeek", week));
            criteria.Add(Expression.Eq("BingoYear", year));
            criteria.Add(Expression.Eq("BingoStatus", bingoStatus));
            criteria.SetMaxResults(1);
            return criteria.UniqueResult<TBingo>();
        }
    }
}
