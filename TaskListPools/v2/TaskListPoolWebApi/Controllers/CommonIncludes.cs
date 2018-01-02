using System.Data.Entity;
using System.Linq;
using TaskListPoolLib;

namespace TaskListPoolWebApi.Controllers
{
    public static class CommonIncludes
    {
        public static IQueryable<Task> Include(this IQueryable<Task> source)
        {
            return source.Include(x => x.TaskGroupLinks.Select(y => y.TaskGroup));
        }
    }
}