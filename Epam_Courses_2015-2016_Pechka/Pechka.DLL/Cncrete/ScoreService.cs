using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.Abstract;
using Pechka.DLL.Extends;
using Pechka.DLL.Models;

namespace Pechka.DLL.Cncrete
{
    public class ScoreService: IScoreService
    {
        public void AddNewScore(int id)
        {
            Score newScore=new Score();
            newScore.Date = DateTime.Now;
            newScore.Money = 0;
            newScore.UserId = id;
            using (PechkaContext work = new PechkaContext())
            {
                work.Scores.Add(newScore);
            }
            }
        public int? ChangeScoreByUserId(int userId, decimal newMoney)
        {
            var newScore = newMoney.ToScore(userId);
            using (PechkaContext work = new PechkaContext())
            {
                work.Scores.Add(newScore);
                work.SaveChanges();
                var temp = work.Scores.Where(u => u.UserId == userId).ToArray();
                return temp[temp.Length - 1].Id;
            }
        }

        public List<Score> HistoryOfScoreByUserId(int userId)
        {
            using (PechkaContext work = new PechkaContext())
            {
                return work.Scores.Where(u => u.UserId == userId).ToList();
            }
        }
    }
    
}
