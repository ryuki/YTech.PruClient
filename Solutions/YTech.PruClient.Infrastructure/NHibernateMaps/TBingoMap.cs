using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.PruClient.Domain;

namespace YTech.PruClient.Infrastructure.NHibernateMaps
{
    public class TBingoMap : IAutoMappingOverride<TBingo>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TBingo> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.T_BINGO");
            mapping.Id(x => x.Id, "BINGO_ID")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.BingoDate, "BINGO_DATE");
            mapping.Map(x => x.BingoWeek, "BINGO_WEEK");
            mapping.Map(x => x.BingoMonth, "BINGO_MONTH");
            mapping.Map(x => x.BingoYear, "BINGO_YEAR");
            mapping.Map(x => x.BingoStatus, "BINGO_STATUS");
            mapping.Map(x => x.BingoWinner, "BINGO_WINNER");
            mapping.Map(x => x.BingoDesc, "BINGO_DESC");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
