using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.CategoryRepository;

namespace TimesheetApp.Application.Category.Queries.GetCategory;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryQueryResponse>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<GetCategoryQueryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(request.Id, cancellationToken);
        if (category == null) throw new NoDataFoundException("Category not found");
        return category.ToResponse();
    }
}