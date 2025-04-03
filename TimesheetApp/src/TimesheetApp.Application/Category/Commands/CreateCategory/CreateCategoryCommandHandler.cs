using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CategoryRepository;

namespace TimesheetApp.Application.Category.Commands.CreateCategory;

public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var exists = await _categoryRepository.ExistsByNameAsync(command.Name);
        if (exists)
        {
            throw new EntityAlreadyExistsException("Category with name '" + command.Name + "' already exists.");
        }
        
        var domainCategory = command.ToDomain();
        var createdCategory = await _categoryRepository.CreateAsync(domainCategory, cancellationToken);

        await _unitOfWork.Save(cancellationToken);
        
        return createdCategory.ToResponse();
    }
}