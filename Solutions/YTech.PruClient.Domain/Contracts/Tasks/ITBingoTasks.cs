using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.PruClient.Enums;

namespace YTech.PruClient.Domain.Contracts.Tasks
{
    public interface ITBingoTasks
    {
        IEnumerable<TBingo> GetAllBingos();
        TBingo Insert(TBingo bingo);
        TBingo Update(TBingo bingo);
        TBingo Delete(TBingo bingo);
        TBingo One(string bingoId);
        TBingo GetByDate(DateTime bingoDate);

        TBingo GetBingoByWeekStatus(int week, int year, string bingoStatus);
    }
}
