using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pechka.DLL.Abstract
{
    public interface IReviewService
    {
        bool AddNewReview(int userId, int menuId, string body);
    }
}
