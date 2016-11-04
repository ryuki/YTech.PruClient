using NHibernate;
using NHibernate.Criterion;
using SharpArch.NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.PruClient.Domain;
using YTech.PruClient.Domain.Contracts;
using YTech.PruClient.Enums;

namespace YTech.PruClient.Infrastructure.Repository
{
    public class TWORepository : NHibernateRepositoryWithTypedId<TWO, string>, ITWORepository
    {
        public IEnumerable<TWO> GetWOByDate(DateTime? dateFrom, DateTime? dateTo)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TWO));
            criteria.Add(Expression.Between("WODate", dateFrom, dateTo));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            criteria.AddOrder(Order.Asc("WODate"));
            return criteria.List<TWO>();
        }

        public IEnumerable<TWOHaveRead> GetListNotDeleted(string userName)
        {
            //ICriteria criteria = Session.CreateCriteria(typeof(TWO));
            //criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            //return criteria.List<TWO>();

            StringBuilder sql = new StringBuilder();
            //            sql.AppendLine(@"   EXEC [dbo].[SP_GET_LIST_WO_READ]
            //                        		@User_Name = :UserName ");

            sql.AppendLine(@"   SELECT  WO.* , a.log_id, a.LOG_USER,a.log_status, a.log_date, 
b.log_id, b.LOG_USER, b.log_status, b.log_date ,
c.customer_name,c.customer_address, c.customer_phone,
case 
when a.LOG_USER = @User_Name then 'true' 
when a.log_date < b.log_date then 'true'
else 'false' end HaveBeenRead
FROM T_WO WO
LEFT JOIN (select l.*, 1 ranking
from t_wo_log l
where l.log_status <> 'Read'
) a
ON WO.WO_ID = a.WO_ID
and a.ranking = 1

LEFT JOIN (select l.*, 1 ranking
from t_wo_log l
where l.log_status = 'Read'
and l.log_user = @User_Name
) b
ON WO.WO_ID = b.WO_ID
and b.ranking = 1

left join m_customer c
on wo.customer_id = c.customer_id

where wo.data_status <> 'Deleted'
order by HaveBeenRead, wo.wo_priority desc, wo.wo_no desc ");

            sql.Replace("@User_Name", "'" + userName + "'");
            IQuery q = Session.CreateSQLQuery(sql.ToString()).AddEntity(typeof(TWOHaveRead));
            //q.SetString("UserName", userName);

            return q.List<TWOHaveRead>();
            //TWO
            //List<TWO> result = new List<TWO>(q.List());
            //return result;
        }

        public TWO GetWOByWONo(string woNo)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TWO));
            criteria.Add(Expression.Eq("WONo", woNo));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            return criteria.UniqueResult<TWO>();
        }
    }
}
