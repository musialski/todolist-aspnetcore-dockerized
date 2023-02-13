using todolistaspnetcore.Models;

namespace todolistaspnetcore.DAL
{
    public interface IRepo
    {
        MyDbContext DbContext { get; }

        void AddPosition(IToDoPosition position);
        void DeletePosition(IToDoPosition position);
        void EditPosition(IToDoPosition oldPosition, IToDoPosition newPosition);
    }
}