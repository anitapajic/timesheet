using Moq;
using TimesheetApp.Application.Category.Commands.DeleteCategory;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CategoryRepository;
using Xunit;

namespace UnitTest.Application.Category.Commands.DeleteCategory;

public class DeleteCategoryCommandHandlerTest
{
    
    [Fact]
    public async Task Handle_ShouldDeleteCategory_WhenExists()
    {
        var id = Guid.NewGuid();
        var command = new DeleteCategoryCommand(id);
        var category = new TimesheetApp.Domain.Models.Category { Id = id, Name = "TestCategory" };
            
        var testContext = new TestContext()
            .WithCategoryRepositoryGetSetup(category)
            .WithCategoryRepositoryDeleteAsyncSetup(category.Id);

        var handler = testContext.CreateHandler;
        await handler.Handle(command, CancellationToken.None);
        
        testContext.VerifyDeleteAsync(category.Id, Times.Once);
     }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCategoryNotFoundForDeletion()
    {
        var id = Guid.NewGuid();
        var command = new DeleteCategoryCommand(id);
        
        var testContext = new TestContext()
            .WithCategoryRepositoryGetSetup(null!);
        
        var handler = testContext.CreateHandler;
        
        await Assert.ThrowsAsync<NoDataFoundException>(async () =>
            await handler.Handle(command, CancellationToken.None));
            
        testContext.VerifyDeleteAsync(command.Id, Times.Never);
        
    }
    
    internal class TestContext
    {
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public TestContext()
        {
            _categoryRepository = new Mock<ICategoryRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        public TestContext WithCategoryRepositoryGetSetup(TimesheetApp.Domain.Models.Category category)
        {
            _categoryRepository.Setup(repo => repo.Get(
                    It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(category);
            return this;
        }

        public TestContext WithCategoryRepositoryDeleteAsyncSetup(Guid id)
        {
            _categoryRepository.Setup(repo => repo.DeleteAsync(
                id, It.IsAny<CancellationToken>()));
            return this;
        }

        public void VerifyDeleteAsync(Guid id, Func<Times> times)
        {
            _categoryRepository.Verify(repo => repo.DeleteAsync(
                id,It.IsAny<CancellationToken>()), times);
        }
        public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
        {
            _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
        }
        
        public DeleteCategoryCommandHandler CreateHandler => new(_categoryRepository.Object, _unitOfWork.Object);
    }
    
}