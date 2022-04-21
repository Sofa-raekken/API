using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> GetFeedback(int count, int skip, DateTime? startdate, DateTime? enddate);
        Task<Feedback> InsertFeedback(Feedback feedback);
        Task<Feedback> ResolvedFeedback(int id, bool isResolved);

    }
}
