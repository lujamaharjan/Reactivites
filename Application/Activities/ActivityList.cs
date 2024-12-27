using Domain;
using MediatR;

namespace Application.Activities;

public class ActivityList
{
    public class Query: IRequest<List<Activity>{}
    public class Handler : IRequestHandler<Query, List<Activity>>
}