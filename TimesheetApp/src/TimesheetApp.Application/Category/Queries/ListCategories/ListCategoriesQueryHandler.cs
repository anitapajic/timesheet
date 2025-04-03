using MediatR;
using TimesheetApp.Core.Repositories.CategoryRepository;

namespace TimesheetApp.Application.Category.Queries.ListCategories;

public class ListCategoriesQueryHandler : IRequestHandler<ListCategoriesQuery, List<ListCategoriesQueryResponse>>
{
    private readonly ICategoryRepository _categoryRepository;
    
    public ListCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<ListCategoriesQueryResponse>> Handle(ListCategoriesQuery query, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAll(cancellationToken);
        return categories.ToResponseList();
    }
}