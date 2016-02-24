using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;
using YTech.PruClient.Domain.Contracts.Tasks;
using YTech.PruClient.Web.Mvc.Controllers.ViewModels;
using System.Linq;
using YTech.PruClient.Domain;
using System;
using System.Web;
using System.Globalization;

namespace YTech.PruClient.Web.Mvc.Controllers
{
    public class ContestController : Controller
    {
        private readonly ITBingoTasks bingoTasks;
        public ContestController(ITBingoTasks _bingoTasks)
        {
            this.bingoTasks = _bingoTasks;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Bingo(DateTime? bingoDate)
        {
            if (!bingoDate.HasValue)
            {
                bingoDate = DateTime.Today;
            }
            TBingo bingo = bingoTasks.GetByDate(bingoDate.Value);
            BingoViewModel vm = new BingoViewModel();
            vm.BingoDate = bingoDate.Value;
            vm.DayOfWeek = (int)vm.BingoDate.DayOfWeek;
            vm.ImageFile = BingoViewModel.EmptyImage;
            if (bingo != null)
            {
                vm.BingoId = bingo.Id;
                vm.BingoStatus = bingo.BingoStatus;
                if (bingo.BingoStatus == "YES")
                {
                    vm.ImageFile = BingoViewModel.BingoImage;
                }
                else
                {
                    vm.ImageFile = BingoViewModel.NoBingoImage;
                }
                //vm.Winner1 = "AUN";
                //vm.Winner2 = "DARWIN";
                //vm.Winner3 = "RINA";
                //vm.Winner4 = "EDI YANTO";
                //vm.Winner5 = "JOHN";

                vm.TheWinner = bingo.BingoWinner;
            }
            else
            {
                vm.BingoId = Guid.NewGuid().ToString();
            }
            return View(vm);
        }

        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Bingo(BingoViewModel model, string submitButton)
        {
            if (ModelState.IsValid)
            {
                if (submitButton == "Bingo")
                {
                    return FillBingo(model);
                }
                else if (submitButton == "SubmitWinner")
                {
                    return FillWinner(model);
                }
            }

            return View();
        }

        private ActionResult FillWinner(BingoViewModel model)
        {
            //save the winner
            TBingo bingo = bingoTasks.GetByDate(model.BingoDate);
            if (bingo != null)
            {
                bingo.BingoWinner = model.TheWinner;

                bingo.ModifiedDate = DateTime.Now;
                bingo.ModifiedBy = User.Identity.Name;
                bingo.DataStatus = "Updated";

                bingoTasks.Update(bingo);
            }

            return RedirectToAction("Bingo", "Contest", new { bingoDate = model.BingoDate });
        }

        private ActionResult FillBingo(BingoViewModel model)
        {
            if (model.BingoDate == DateTime.Today)
            {
                if (DateTime.Now.Hour < 10)
                {
                    return RedirectToAction("Bingo", "Contest", new { bingoDate = model.BingoDate });
                }
            }
            else if (model.BingoDate > DateTime.Today)
            {
                return RedirectToAction("Bingo", "Contest", new { bingoDate = model.BingoDate });
            }
            TBingo bingo = bingoTasks.GetByDate(model.BingoDate);
            if (bingo != null)
            {
                bingo.ModifiedDate = DateTime.Now;
                bingo.ModifiedBy = User.Identity.Name;
                bingo.DataStatus = "Updated";

                bingoTasks.Update(bingo);
            }
            else
            {
                //var bingo = bingoTasks.GetBingoByWeek(model.BingoDate.week);

                // Gets the Calendar instance associated with a CultureInfo.
                CultureInfo myCI = CultureInfo.CurrentCulture;
                System.Globalization.Calendar myCal = myCI.Calendar;
                // Gets the DTFI properties required by GetWeekOfYear.
                CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
                DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
                int week = myCal.GetWeekOfYear(model.BingoDate, myCWR, myFirstDOW);
                TBingo bingoWinThisWeek = bingoTasks.GetBingoByWeekStatus(week, model.BingoDate.Year, "YES");
                int dayOfWeek = (int)model.BingoDate.DayOfWeek;
                double param = 100;
                string stat = "";
                //no winner yet
                if (bingoWinThisWeek == null)
                {
                    param = (100d / 7d) * (dayOfWeek + 1d);
                    Random rand = new Random();
                    if (rand.NextDouble() * 100 <= param)
                    {
                        stat = "YES";
                    }
                    else
                    {
                        stat = "NO";
                    }
                }
                //we have a winner, set no winner anymore
                else
                {
                    stat = "NO";
                }

                bingo = new TBingo();
                bingo.SetAssignedIdTo(Guid.NewGuid().ToString());
                bingo.BingoDate = model.BingoDate;
                bingo.BingoStatus = stat;
                bingo.BingoWeek = week;
                bingo.BingoMonth = model.BingoDate.Month;
                bingo.BingoYear = model.BingoDate.Year;
                bingo.BingoWinner = string.Empty;

                bingo.CreatedDate = DateTime.Now;
                bingo.CreatedBy = User.Identity.Name;
                bingo.DataStatus = "New";
                bingoTasks.Insert(bingo);
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Bingo", "Contest", new { bingoDate = model.BingoDate });
        }
    }
}
