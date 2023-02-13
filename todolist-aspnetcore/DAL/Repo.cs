using todolistaspnetcore.Models;

namespace todolistaspnetcore.DAL
{
    public class Repo : IRepo
    {
        public MyDbContext DbContext { get; private set; }

        public Repo(MyDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void AddPosition(IToDoPosition position)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }

            DbContext.Positions.Add((ToDoPosition)position);

            DbContext.SaveChanges();
        }
        public void EditPosition(IToDoPosition oldPosition, IToDoPosition newPosition)
        {
            if (oldPosition == null)
            {
                throw new ArgumentNullException(nameof(oldPosition));
            }

            if (newPosition == null)
            {
                throw new ArgumentNullException(nameof(newPosition));
            }

            IToDoPosition oldPositionDb = DbContext.Positions.Single(x => x.Id == oldPosition.Id);
            oldPositionDb.Description = newPosition.Description;

            DbContext.SaveChanges();
        }
        public void DeletePosition(IToDoPosition position)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }

            IToDoPosition positionDb = DbContext.Positions.Single(x => x.Id == position.Id);
            DbContext.Positions.Remove((ToDoPosition)positionDb);

            DbContext.SaveChanges();
        }
    }
}
