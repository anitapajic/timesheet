using Moq;
using TimesheetApp.Application.Category.Commands.CreateCategory;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CategoryRepository;
using Xunit;

namespace UnitTest.Application.Category.Commands.CreateCategory;

public class CreateCategoryCommandHandlerTest
{

    [Fact]
    public async Task Handle_ShouldCreateCategory()
    {
        var testContext = new TestContext();
        var command = testContext.GetCommand();
        
        testContext
            .WithCategoryRepositoryExistNameSetUp(command.Name, false)
            .WithCategoryRepositoryCreateAsyncSetUp();
        
        var handler = testContext.CreateHandler;
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(command.Name, result.Name);
        
        testContext.VerifyCreateAsync(command.Name, Times.Once);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Once);

    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCategoryAlreadyExists()
    {
        var testContext = new TestContext();
        var command = testContext.GetCommand();
        
        testContext
            .WithCategoryRepositoryExistNameSetUp(command.Name, true);
        
        var handler = testContext.CreateHandler;
        
        await Assert.ThrowsAsync<EntityAlreadyExistsException>(
            async () => await handler.Handle(command, CancellationToken.None));
        
        testContext.VerifyCreateAsync(command.Name, Times.Never);
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

        public CreateCategoryCommand GetCommand()
        {
            return new CreateCategoryCommand("TestCategory");
        }

        public TestContext WithCategoryRepositoryExistNameSetUp(string categoryName, bool exists)
        {
            _categoryRepository.Setup(repo => repo.ExistsByNameAsync(categoryName))
                .ReturnsAsync(exists);
            
            return this;
        }
        
        public TestContext WithCategoryRepositoryCreateAsyncSetUp()
        {
            _categoryRepository.Setup(repo => repo.CreateAsync(
                    It.IsAny<TimesheetApp.Domain.Models.Category>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((TimesheetApp.Domain.Models.Category input, CancellationToken _) => input);
            
            return this;
        }
        
        public void VerifyCreateAsync(string categoryName, Func<Times> times)
        {
            _categoryRepository.Verify(repo => repo.CreateAsync(
                It.Is<TimesheetApp.Domain.Models.Category>(c => c.Name == categoryName),
                It.IsAny<CancellationToken>()), times);
        }
        
        public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
        {
            _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
        }

        public CreateCategoryCommandHandler CreateHandler => new(_categoryRepository.Object, _unitOfWork.Object);
    }
}