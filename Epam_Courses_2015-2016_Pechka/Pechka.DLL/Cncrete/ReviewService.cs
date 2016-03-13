using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.Abstract;
using Pechka.DLL.Models;

namespace Pechka.DLL.Cncrete
{
    public class ReviewService:IReviewService
    {
        public bool AddNewReview(int userId, int menuId, string body)
        {
            var reviewToAdd=new Review();
            reviewToAdd.UserId = userId;
            reviewToAdd.MenuId = menuId;
            reviewToAdd.Body = body;
            using (var work = new PechkaContext())
            {
                try
                {
                    work.Reviews.Add(reviewToAdd);
                    work.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
