using Microsoft.EntityFrameworkCore;
using Moq;
using todolistaspnetcore.DAL;
using todolistaspnetcore.Models;

namespace todolistaspnetcore.Tests
{
    public class AddPosition
    {
        [Fact]
        public void CanAdd()
        {
            Mock<DbSet<ToDoPosition>> dbSetMock = Common.PrepareDbSetMock();
            Mock<MyDbContext> dbContextMock = Common.PrepareDbContextMock(dbSetMock.Object);

            Repo systemUnderTest = new Repo(dbContextMock.Object);

            int iPositionsCount = systemUnderTest.DbContext.Positions.Count();

            systemUnderTest.AddPosition(new ToDoPosition());

            Assert.Equal(iPositionsCount + 1, systemUnderTest.DbContext.Positions.Count());
        }
        [Fact]
        public void ThrowsArgumentNullExceptionOnNullPosition()
        {
            Mock<DbSet<ToDoPosition>> dbSetMock = Common.PrepareDbSetMock();
            Mock<MyDbContext> dbContextMock = Common.PrepareDbContextMock(dbSetMock.Object);

            Repo systemUnderTest = new Repo(dbContextMock.Object);

            Assert.Throws<ArgumentNullException>("position",
                () => systemUnderTest.AddPosition(null));
        }
        [Fact]
        public void ThrowsInvalidOperationExceptionOnNullList()
        {
            Mock<MyDbContext> dbContextMock = Common.PrepareDbContextNullSourceMock();

            Repo systemUnderTest = new Repo(dbContextMock.Object);

            Assert.Throws<NullReferenceException>(
                () => systemUnderTest.AddPosition(new ToDoPosition()));
        }
    }
}