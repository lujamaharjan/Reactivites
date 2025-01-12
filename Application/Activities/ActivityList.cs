using Application.Core;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Activities;

public class ActivityList
{
    public class Query: IRequest<Result<List<ActivityDto>>> {}

    public class Handler : IRequestHandler<Query, Result<List<ActivityDto>>>
    {
        private readonly DataContext _context;
        private readonly ILogger<EditActivity.Handler> _logger;
        private readonly IMapper _mapper;
        public Handler(DataContext context, ILogger<EditActivity.Handler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<Result<List<ActivityDto>>> Handle(Query request, CancellationToken ct)
        {
            var activities = await _context.Activities
                .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);
           return Result<List<ActivityDto>>.Success(activities);
        }
    }
}