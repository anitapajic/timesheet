using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CategoryRepository;

namespace TimesheetApp.Application.Category.Commands.UpdateCategory;

public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(command.Id, cancellationToken);
        if (category == null) throw new NoDataFoundException("Category not found");
        var exists = await _categoryRepository.ExistsByNameAsync(command.Name);
        if (exists)
        {
            throw new EntityAlreadyExistsException("Category with name '" + command.Name + "' already exists.");
        }
        category.Name = command.Name;
        var updatedCategory = await _categoryRepository.UpdateAsync(category, cancellationToken);
        
        await _unitOfWork.Save(cancellationToken);
        
        return updatedCategory.ToResponse();
    }
}