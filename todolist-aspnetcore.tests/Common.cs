using Microsoft.EntityFrameworkCore;
using Moq;
using todolistaspnetcore.DAL;
using todolistaspnetcore.Models;

namespace todolistaspnetcore.Tests
{
    internal static class Common
    {
        internal static Mock<DbSet<ToDoPosition>> PrepareDbSetMock()
        {
            List<ToDoPosition> list = new List<ToDoPosition>();

            Mock<DbSet<ToDoPosition>> dbSet = new Mock<DbSet<ToDoPosition>>();

            dbSet.As<IQueryable<ToDoPosition>>()
                 .Setup(c => c.GetEnumerator())
                 .Returns(list.GetEnumerator());

            dbSet.As<IQueryable<ToDoPosition>>()
                 .Setup(x => x.Provider)
                 .Returns(list.AsQueryable().Provider);

            dbSet.As<IQueryable<ToDoPosition>>()
                 .Setup(x => x.Expression)
                 .Returns(list.AsQueryable().Expression);

            dbSet.As<IQueryable<ToDoPosition>>()
                 .Setup(x => x.ElementType)
                 .Returns(list.AsQueryable().ElementType);

            dbSet.Setup(x => x.Add(It.IsAny<ToDoPosition>()))
                 .Callback(list.Add);

            dbSet.Setup(x => x.Remove(It.IsAny<ToDoPosition>()))
                 .Callback(new Action<ToDoPosition>((position) =>
                 {
                     list.Remove(position);
                 }));

            return dbSet;
        }
        internal static Mock<MyDbContext> PrepareDbContextMock(DbSet<ToDoPosition> dbSet)
        {
            Mock<DbContextOptions<MyDbContext>> contextOptions = new Mock<DbContextOptions<MyDbContext>>();
            contextOptions.Setup(c => c.ContextType)
                          .Returns(typeof(MyDbContext));

            Mock<MyDbContext> context = new Mock<MyDbContext>(contextOptions.Object);
            context.Setup(c => c.Positions)
                   .Returns(dbSet);

            return context;
        }
        internal static Mock<MyDbContext> PrepareDbContextNullSourceMock()
        {
            Mock<DbContextOptions<MyDbContext>> contextOptions = new Mock<DbContextOptions<MyDbContext>>();
            contextOptions.Setup(c => c.ContextType)
                          .Returns(typeof(MyDbContext));

            Mock<MyDbContext> context = new Mock<MyDbContext>(contextOptions.Object);
            context.Setup(c => c.Positions)
                   .Returns((DbSet<ToDoPosition>)null);

            return context;
        }
    }
}