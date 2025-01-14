using Application.Core;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities;

public class ActivityDetail
{
    public class Query : IRequest<Result<ActivityDto>>
    {
        public Guid Id { get; set; } 
        
    }
    
    public class Handler(DataContext context, IMapper mapper) : IRequestHandler<Query, Result<ActivityDto>>
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;
        
        public async Task<Result<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities
                .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            return Result<ActivityDto>.Success(activity);
        }
    }
}