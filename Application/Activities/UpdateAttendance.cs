using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities;

public class UpdateAttendance
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
    
    public class Handler(DataContext context , IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities
                .Include(a => a.Attendees)
                .ThenInclude(u => u.AppUser)
                .SingleOrDefaultAsync(m => m.Id == request.Id);
            
            if (activity == null) return null;
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername(), cancellationToken);
            if(user == null) return null;
            var hostUsername = activity.Attendees.FirstOrDefault(x => x.IsHost)?.AppUser?.UserName;
            var attendance = activity.Attendees.FirstOrDefault(x => x.AppUser.UserName == user.UserName);
            if (attendance != null && hostUsername != user.UserName)
            {
                activity.Attendees.Remove(attendance);
            }

            if (attendance is null)
            {
                attendance = new ActivityAttendee()
                {
                    AppUser = user,
                    Activity = activity,
                    IsHost = false
                };
                activity.Attendees.Add(attendance);
            }
            var result = await context.SaveChangesAsync(cancellationToken) > 0;
            return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem updating attendance");
        }
    }
}