using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.Models;

namespace Pechka.DLL.Abstract
{
    public interface IScoreService
    {
        void AddNewScore(int id);
        int? ChangeScoreByUserId(int userId, decimal newMoney);
        List<Score> HistoryOfScoreByUserId(int userId);
    }
}
