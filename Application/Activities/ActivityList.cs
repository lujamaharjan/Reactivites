using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Activities;

public class ActivityList
{
    public class Query: IRequest<List<Activity>> {}

    public class Handler : IRequestHandler<Query, List<Activity>>
    {
        private readonly DataContext _context;
        private readonly ILogger<EditActivity.Handler> _logger;
        public Handler(DataContext context, ILogger<EditActivity.Handler> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task<List<Activity>> Handle(Query request, CancellationToken ct)
        {
            return await _context.Activities.ToListAsync();
        }
    }
}