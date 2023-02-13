using Microsoft.EntityFrameworkCore;
using Moq;
using todolistaspnetcore.DAL;
using todolistaspnetcore.Models;

namespace todolistaspnetcore.Tests
{
    public class EditPosition
    {
        [Fact]
        public void CanEdit()
        {
            Mock<DbSet<ToDoPosition>> dbSetMock = Common.PrepareDbSetMock();
            Mock<MyDbContext> dbContextMock = Common.PrepareDbContextMock(dbSetMock.Object);

            Repo systemUnderTest = new Repo(dbContextMock.Object);

            ToDoPosition oldPosition = new ToDoPosition();
            systemUnderTest.AddPosition(oldPosition);

            int iPositionsCount = systemUnderTest.DbContext.Positions.Count();

            ToDoPosition newPosition = new ToDoPosition();
            systemUnderTest.EditPosition(oldPosition, newPosition);

            Assert.Equal(iPositionsCount, systemUnderTest.DbContext.Positions.Count());
        }
        [Fact]
        public void ThrowsArgumentNullExceptionOnEditWhenListIsNull()
        {
            Mock<MyDbContext> dbContextMock = Common.PrepareDbContextNullSourceMock();

            Repo systemUnderTest = new Repo(dbContextMock.Object);

            Assert.Throws<ArgumentNullException>(
                () => systemUnderTest.EditPosition(new ToDoPosition(), new ToDoPosition()));
        }
        [Fact]
        public void ThrowsArgumentNullExceptionOnEditNullOldPosition()
        {
            Mock<DbSet<ToDoPosition>> dbSetMock = Common.PrepareDbSetMock();
            Mock<MyDbContext> dbContextMock = Common.PrepareDbContextMock(dbSetMock.Object);

            Repo systemUnderTest = new Repo(dbContextMock.Object);

            Assert.Throws<ArgumentNullException>("oldPosition",
                () => systemUnderTest.EditPosition(null, new ToDoPosition()));
        }
        [Fact]
        public void ThrowsArgumentNullExceptionOnEditNullNewPosition()
        {
            Mock<DbSet<ToDoPosition>> dbSetMock = Common.PrepareDbSetMock();
            Mock<MyDbContext> dbContextMock = Common.PrepareDbContextMock(dbSetMock.Object);

            Repo systemUnderTest = new Repo(dbContextMock.Object);

            Assert.Throws<ArgumentNullException>("newPosition",
                () => systemUnderTest.EditPosition(new ToDoPosition(), null));
        }
        [Fact]
        public void ThrowsInvalidOperationExceptionIfPositionNotFound()
        {
            Mock<DbSet<ToDoPosition>> dbSetMock = Common.PrepareDbSetMock();
            Mock<MyDbContext> dbContextMock = Common.PrepareDbContextMock(dbSetMock.Object);

            Repo systemUnderTest = new Repo(dbContextMock.Object);

            Assert.Throws<InvalidOperationException>(
                () => systemUnderTest.EditPosition(new ToDoPosition(), new ToDoPosition()));
        }
        [Fact]
        public void ThrowsInvalidOperationExceptionIfMoreThanOnePositionFound()
        {
            Mock<DbSet<ToDoPosition>> dbSetMock = Common.PrepareDbSetMock();
            Mock<MyDbContext> dbContextMock = Common.PrepareDbContextMock(dbSetMock.Object);

            Repo systemUnderTest = new Repo(dbContextMock.Object);

            ToDoPosition position = new ToDoPosition();
            systemUnderTest.AddPosition(position);
            systemUnderTest.AddPosition(position);

            Assert.Throws<InvalidOperationException>(
                () => systemUnderTest.EditPosition(position, position));
        }
    }
}