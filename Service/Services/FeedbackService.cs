using Data.Models;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class FeedbackService : IFeedbackService
    {
        private Kbh_zooContext Context { get; }
        public FeedbackService(Kbh_zooContext context)
        {
            Context = context;
        }

        public async Task<List<Feedback>> GetFeedback(int count, int skip, DateTime? startdate, DateTime? enddate)
        {
            var exps = FeedbackExpression(startdate, enddate);
            var query = Context.Feedbacks.Include(x => x.CategoryIdCategoryNavigation).AsQueryable();
            foreach (var exp in exps)
            {
                query = query.Where(exp);
            }
            return await query.OrderByDescending(x => x.Date).Take(count).Skip(skip).ToListAsync();
        }

        private List<Expression<Func<Feedback, bool>>> FeedbackExpression(DateTime? startdate, DateTime? enddate)
        {

            List<Expression<Func<Feedback, bool>>> exps = new List<Expression<Func<Feedback, bool>>>();

            if (startdate is not null)
            {
                exps.Add(x => x.Date.Date <= startdate.Value.Date);
            }

            if (enddate is not null)
            {
                exps.Add(x => x.Date.Date >= enddate.Value.Date);
            }
            return exps;
        }

        public async Task<Feedback> InsertFeedback(Feedback feedback)
        {
            DateTime currentDateTime = DateTime.UtcNow;
            var denmark = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time");
            var currentDenmarkTime = TimeZoneInfo.ConvertTimeFromUtc(currentDateTime, denmark);

            feedback.Date = currentDenmarkTime;
            feedback.Resolved = 0;
            await Context.Feedbacks.AddAsync(feedback);

            if (await Context.SaveChangesAsync() > 0)
            {
                return feedback;
            }
            return null;
        }

        public async Task<Feedback> ResolvedFeedback(int id, bool isResolved)
        {
            var feedbackEntity = await Context.Feedbacks.SingleOrDefaultAsync(x => x.IdFeedback == id);

            if (feedbackEntity is null) { return null; }

            feedbackEntity.Resolved = (byte?)(isResolved == true ? 1 : 0);

            await Context.SaveChangesAsync();

            return feedbackEntity;
        }
    }
}
