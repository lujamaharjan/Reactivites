
import {Button, Segment, Item, Label} from "semantic-ui-react";
import {SyntheticEvent, useState} from "react";
import { useStore } from "../../app/stores/store";
import {observer} from "mobx-react-lite";
import {Link} from "react-router-dom";

export default observer(function ActivityList()
{
    const [target, setTarget] = useState('');
    const {deleteActivity, activitiesByDate, loading} = useStore().activityStore;
    function handleActivityDelete(e: SyntheticEvent<HTMLButtonElement>, id: string) {
        setTarget(e.currentTarget.name)
        deleteActivity(id)
    }
    
    return (
        <Segment clearing>
            <Item.Group divided>
                {activitiesByDate.map(activity => (
                    <Item key={activity.id}>
                        <Item.Content>
                            <Item.Header as='a'>{activity.title}</Item.Header>
                            <Item.Meta>{activity.date}</Item.Meta>
                            <Item.Description>
                                <div>{activity.description}</div>
                                <div>{activity.city}, {activity.venue}</div>
                            </Item.Description>
                            <Item.Extra>
                                <Button floated='right' content='View' color='blue' as={Link} to={`/activities/${activity.id}`}></Button>
                                <Button 
                                    name={activity.id}
                                    loading={loading && target === activity.id} 
                                    floated='right' 
                                    content='Delete' 
                                    color='red' 
                                    onClick={(e) =>handleActivityDelete(e,activity.id)}>
                                </Button>
                                <Label basic content={activity.category}></Label>
                            </Item.Extra>
                        </Item.Content>
                    </Item>
                ))}
            </Item.Group>
        </Segment>
    )
});