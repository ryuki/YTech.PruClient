using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.PruClient.Domain.Contracts;
using YTech.PruClient.Domain.Contracts.Tasks;
using SharpArch.Domain;
using YTech.PruClient.Infrastructure.Repository;
using YTech.PruClient.Domain;

namespace YTech.PruClient.Tasks
{
    public class TBingoTasks : ITBingoTasks
    {
        private readonly ITBingoRepository _tBingoRepository;

        public TBingoTasks(ITBingoRepository tBingoRepository)
        {
            this._tBingoRepository = tBingoRepository;
        }

        public IEnumerable<TBingo> GetAllBingos()
        {
            var bingoomers = this._tBingoRepository.GetAll(); ;
            return bingoomers;
        }

        public TBingo Insert(Domain.TBingo bingo)
        {
            _tBingoRepository.DbContext.BeginTransaction();
            _tBingoRepository.Save(bingo);
            _tBingoRepository.DbContext.CommitTransaction();
            return bingo;
        }

        public TBingo Update(Domain.TBingo bingo)
        {
            _tBingoRepository.DbContext.BeginTransaction();
            _tBingoRepository.Update(bingo);
            _tBingoRepository.DbContext.CommitTransaction();
            return bingo;
        }

        public TBingo Delete(Domain.TBingo bingo)
        {
            _tBingoRepository.DbContext.BeginTransaction();
            _tBingoRepository.Delete(bingo);
            _tBingoRepository.DbContext.CommitTransaction();
            return bingo;
        }

        public TBingo One(string bingoId)
        {
            var bingo = this._tBingoRepository.Get(bingoId); ;
            return bingo;
        }

        public TBingo GetByDate(DateTime bingoDate)
        {
            TBingo bingo = this._tBingoRepository.GetByDate(bingoDate);
            return bingo;
        }


        public TBingo GetBingoByWeekStatus(int week, int year, string bingoStatus)
        {
            TBingo bingo = this._tBingoRepository.GetBingoByWeekStatus(week, year, bingoStatus);
            return bingo;
        }
    }
}
