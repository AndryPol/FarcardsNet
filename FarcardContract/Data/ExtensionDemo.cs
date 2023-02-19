using FarcardContract.Data.Farcard6;

namespace FarcardContract.Data
{
    public static class ExtensionDemo
    {
        private static readonly string _holder = "Тестовый клиент";
        public static uint GetDemoAccount => 1234;

        public static long GetDemoCard => 1;

        public static CardInfoEx Demo(this CardInfoEx cardInfoEx)
        {
            cardInfoEx.Holy = YesNo.No;
            cardInfoEx.Grab = YesNo.No;
            cardInfoEx.Locked = YesNo.No;
            cardInfoEx.StopDate = YesNo.No;
            cardInfoEx.Deleted = YesNo.No;
            cardInfoEx.Account = GetDemoAccount;
            cardInfoEx.Holder = _holder;
            cardInfoEx.Summa = 1000.05m;
            cardInfoEx.Sum2 = 200.00m;
            cardInfoEx.Sum3 = 300.10m;
            cardInfoEx.Sum4 = 400.00m;
            cardInfoEx.Sum5 = 500.00m;
            cardInfoEx.Sum6 = 600.00m;
            cardInfoEx.Sum7 = 700.00m;
            cardInfoEx.Sum8 = 800.00m;
            cardInfoEx.DiscLimit = 900000.00m;
            cardInfoEx.Bonus = 5;
            cardInfoEx.Discount = 99;
            cardInfoEx.PersonID = 1234;
            cardInfoEx.Unpay = 0;
            cardInfoEx.ScrMessage = "Тестовая информация для экрана";
            cardInfoEx.DopInfo = "Тестовая информация";
            cardInfoEx.PrnMessage = "Тестовая информация для печати";

            return cardInfoEx;
        }

        public static HolderInfo Demo(this HolderInfo holderInfo)
        {
            holderInfo.Card = GetDemoCard;
            holderInfo.ClientId = GetDemoAccount;
            holderInfo.Owner = _holder;
            return holderInfo;
        }

        public static DiscLevelInfo Demo(this DiscLevelInfo discLevelInfo)
        {
            discLevelInfo.CurrentName = "Базовый уровень";
            discLevelInfo.NextName = "Расширенный уровень";
            discLevelInfo.SumToNextLevel = 0.99m;

            return discLevelInfo;
        }

    }
}
