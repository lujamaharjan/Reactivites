import {Button, Icon, Item, Segment} from "semantic-ui-react";
import {Link} from "react-router-dom";
import { Activity } from "../../app/models/activity";
import {format} from "date-fns";

interface Props {
    activity: Activity;
}
export default function ActivityComponent({activity}:Props) {

    return (
        <Segment.Group>
            <Segment>
                <Item.Group>
                    <Item.Image size='tiny' circular src='/assets/user.png'/>
                    <Item.Content>
                        <Item.Header as={Link} to={`/activities/${activity.id}`}>
                            {activity.title}
                        </Item.Header>
                        <Item.Description>Hosted by Bob</Item.Description>
                    </Item.Content>
                </Item.Group>
            </Segment>
            <Segment>
                <span>
                    <Icon name='clock'/> {format(activity.date!, 'dd MM yyyy h:mm aa')}
                    <Icon name='marker'/> {activity.venue}
                </span>
            </Segment>
            <Segment secondary>
                <span>
                    Attendees go here
                </span>
            </Segment>
            <Segment clearing>
                <span>{activity.description}</span>
                <Button
                    as={Link}
                    to={`/activities/${activity.id}`}
                    content='View'
                    floated='right'
                    color='blue'
                />
            </Segment>
        </Segment.Group>
    )
}