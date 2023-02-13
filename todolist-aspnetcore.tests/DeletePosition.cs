using Microsoft.EntityFrameworkCore;
using Moq;
using todolistaspnetcore.DAL;
using todolistaspnetcore.Models;

namespace todolistaspnetcore.Tests
{
    public class DeletePosition
    {
        [Fact]
        public void CanDelete()
        {
            Mock<DbSet<ToDoPosition>> dbSetMock = Common.PrepareDbSetMock();
            Mock<MyDbContext> dbContextMock = Common.PrepareDbContextMock(dbSetMock.Object);

            Repo systemUnderTest = new Repo(dbContextMock.Object);

            ToDoPosition position = new ToDoPosition();
            systemUnderTest.AddPosition(position);

            int iPositionsCount = systemUnderTest.DbContext.Positions.Count();

            systemUnderTest.DeletePosition(position);

            Assert.Equal(iPositionsCount - 1, systemUnderTest.DbContext.Positions.Count());
        }
        [Fact]
        public void ThrowsArgumentNullExceptionWhenListIsNull()
        {
            Mock<MyDbContext> dbContextMock = Common.PrepareDbContextNullSourceMock();

            Repo systemUnderTest = new Repo(dbContextMock.Object);

            Assert.Throws<ArgumentNullException>(
                () => systemUnderTest.DeletePosition(new ToDoPosition()));
        }
        [Fact]
        public void ThrowsArgumentNullExceptionOnNullPosition()
        {
            Mock<DbSet<ToDoPosition>> dbSetMock = Common.PrepareDbSetMock();
            Mock<MyDbContext> dbContextMock = Common.PrepareDbContextMock(dbSetMock.Object);

            Repo systemUnderTest = new Repo(dbContextMock.Object);

            Assert.Throws<ArgumentNullException>("position",
                () => systemUnderTest.DeletePosition(null));
        }
        [Fact]
        public void ThrowsInvalidOperationExceptionIfNotFound()
        {
            Mock<DbSet<ToDoPosition>> dbSetMock = Common.PrepareDbSetMock();
            Mock<MyDbContext> dbContextMock = Common.PrepareDbContextMock(dbSetMock.Object);

            Repo systemUnderTest = new Repo(dbContextMock.Object);

            Assert.Throws<InvalidOperationException>(
                () => systemUnderTest.DeletePosition(new ToDoPosition()));
        }
        [Fact]
        public void ThrowsInvalidOperationExceptionIfMoreThanOneFound()
        {
            Mock<DbSet<ToDoPosition>> dbSetMock = Common.PrepareDbSetMock();
            Mock<MyDbContext> dbContextMock = Common.PrepareDbContextMock(dbSetMock.Object);

            Repo systemUnderTest = new Repo(dbContextMock.Object);

            ToDoPosition position = new ToDoPosition();
            systemUnderTest.AddPosition(position);
            systemUnderTest.AddPosition(position);

            Assert.Throws<InvalidOperationException>(
                () => systemUnderTest.DeletePosition(position));
        }
    }
}