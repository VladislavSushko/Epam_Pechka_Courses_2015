
using System;
using Pechka.DLL.Models;

namespace Pechka.DLL.Extends
{
    static class ScoreExtends
    {
        public static Score ToScore(this decimal money, int userId)
        {
            var newScore=new Score();
            newScore.Money = money;
            newScore.Date = DateTime.Now;
            newScore.UserId = userId;
            return newScore;
        }
    }
}
