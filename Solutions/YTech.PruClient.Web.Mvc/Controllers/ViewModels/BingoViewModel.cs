using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace YTech.PruClient.Web.Mvc.Controllers.ViewModels
{
    public class BingoViewModel
    {
        public static string BingoImage = "~/Content/img/bingo_text.gif";
        public static string EmptyImage = "~/Content/img/noisegrain_notready.png";
        public static string NoBingoImage = "~/Content/img/Mr.-Bill-Oh-No.png";
        public BingoViewModel()
        {
            ImageFile = EmptyImage;
        }

        [ScaffoldColumn(true)]
        [DisplayName("No Bingo")]
        public string BingoId
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Tgl Bingo")]
        public DateTime BingoDate
        {
            get;
            set;
        }

        //[Required]
        [DisplayName("Status")]
        public string BingoStatus
        {
            get;
            set;
        }

        [DisplayName("DayOfWeek")]
        public int DayOfWeek
        {
            get;
            set;
        }

        [DisplayName("ImageFile")]
        public string ImageFile
        {
            get;
            set;
        }
    }
}