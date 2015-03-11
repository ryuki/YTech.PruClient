﻿using NHibernate;
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
            sql.AppendLine(@"   EXEC [dbo].[SP_GET_LIST_WO_READ]
                        		@User_Name = :UserName ");
            IQuery q = Session.CreateSQLQuery(sql.ToString()).AddEntity(typeof(TWOHaveRead));
            q.SetString("UserName", userName);

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
