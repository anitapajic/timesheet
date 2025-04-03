using Moq;
using TimesheetApp.Application.Category.Commands.UpdateCategory;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CategoryRepository;
using Xunit;

namespace UnitTest.Application.Category.Commands.UpdateCategory;

public class UpdateCategoryCommandHandlerTest
{
    [Fact]
    public async Task Handle_ShouldUpdateCategory_WhenExists()
    {
        var testContext = new TestContext();
        var command = testContext.GetCommand();
        var category = new TimesheetApp.Domain.Models.Category {Id = command.Id, Name = "OldCategory"};
        var updatedCategory = new TimesheetApp.Domain.Models.Category { Id = command.Id, Name = command.Name };
        
        testContext
            .WithCategoryRepositoryGetSetup(command.Id, category)
            .WithCategoryRepositoryUpdateAsyncSetUp(updatedCategory);
        
        var handler = testContext.CreateHandler;

        var result = await handler.Handle(command, CancellationToken.None);
   
        Assert.NotNull(result);
        Assert.Equal(command.Name, result.Name);
        
        testContext.VerifyUpdateAsync(Times.Once);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Once);
    }
    
    [Fact]
    public async Task Handle_Should_ThrowException_WhenCategoryNotFound()
    {
        var testContext = new TestContext();
        var command = testContext.GetCommand();
        
        testContext
            .WithCategoryRepositoryGetSetup(command.Id, null!);
        
        var handler = testContext.CreateHandler;
        
        await Assert.ThrowsAsync<NoDataFoundException>(async () =>
            await handler.Handle(command, CancellationToken.None));
            
        testContext.VerifyUpdateAsync(Times.Never);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Never);
    }
    
    [Fact]
    public async Task Handle_ShouldThrowException_WhenCategoryAlreadyExists()
    {
        var testContext = new TestContext();
        var command = testContext.GetCommand();
        var category = new TimesheetApp.Domain.Models.Category {Id = command.Id, Name = "OldCategory"};
        testContext
            .WithCategoryRepositoryGetSetup(command.Id, category)
            .WithCategoryRepositoryExistNameSetUp(command.Name, true);
        
        var handler = testContext.CreateHandler;
        
        await Assert.ThrowsAsync<EntityAlreadyExistsException>(
            async () => await handler.Handle(command, CancellationToken.None));
        
        testContext.VerifyUpdateAsync(Times.Never);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Never);
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

        public UpdateCategoryCommand GetCommand()
        {
            return new UpdateCategoryCommand { Id = Guid.NewGuid(), Name = "UpdateCategory" };
        }
        public TestContext WithCategoryRepositoryGetSetup(Guid id, TimesheetApp.Domain.Models.Category category)
        {
            _categoryRepository.Setup(repo => repo.Get(
                    id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(category);
            return this;
        }
        
        public TestContext WithCategoryRepositoryExistNameSetUp(string categoryName, bool exists)
        {
            _categoryRepository.Setup(repo => repo.ExistsByNameAsync(categoryName))
                .ReturnsAsync(exists);
            
            return this;
        }
        
        public TestContext WithCategoryRepositoryUpdateAsyncSetUp(TimesheetApp.Domain.Models.Category updatedCategory)
        {
            _categoryRepository.Setup(repo => repo.UpdateAsync(
                    It.IsAny<TimesheetApp.Domain.Models.Category>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(updatedCategory);

            return this;
        }
        
        public void VerifyUpdateAsync(Func<Times> times)
        {
            _categoryRepository.Verify(repo => repo.UpdateAsync(
                It.IsAny<TimesheetApp.Domain.Models.Category>(), 
                It.IsAny<CancellationToken>()), times);
        }
        
        public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
        {
            _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
        }
        
        public UpdateCategoryCommandHandler CreateHandler => new(_categoryRepository.Object, _unitOfWork.Object);
    }
}