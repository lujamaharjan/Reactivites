using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities;

public class EditActivity
{
    public class Command : IRequest<Result<Unit>>
    {
        public Activity Activity { get; set; }   
    }
    
    public class Handler(DataContext context, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Activity.Id);
            if (activity is null) return null;
            _mapper.Map(request.Activity, activity);
            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
            if(!result) return Result<Unit>.Failure("Failed to update activity");
            return Result<Unit>.Success(Unit.Value);
        }
    }
}