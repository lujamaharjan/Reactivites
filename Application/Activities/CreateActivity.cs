using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities;

public abstract class CreateActivity
{
    public class Command : IRequest<Result<Unit>>
    {
        public Activity Activity { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
        }
    }
    
    public class Handler(DataContext context, IUserAccessor accessor) : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context = context;
        private readonly IUserAccessor _accessor = accessor;
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _accessor.GetUsername(), cancellationToken);
            var attendee = new ActivityAttendee
            {
                AppUser = user,
                Activity = request.Activity,
                IsHost = true
            };
            request.Activity.Attendees.Add(attendee);
            _context.Activities.Add(request.Activity);
           var result = await _context.SaveChangesAsync(cancellationToken) > 0;
           return !result ? Result<Unit>.Failure("Failed to create activity") : Result<Unit>.Success(Unit.Value);
        }
    }
}