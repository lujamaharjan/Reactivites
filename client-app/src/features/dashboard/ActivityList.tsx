
import { Header} from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import {observer} from "mobx-react-lite";
import ActivityComponent from "./ActivityComponent";
import {Fragment} from "react";

export default observer(function ActivityList()
{
    const { groupedActivities } = useStore().activityStore;
    
    return (
        <>
            {groupedActivities.map(([group, activities]) => (
                <Fragment key={group}>
                    <Header sub color='teal'>
                        {group}
                    </Header>
                    
                    {activities.map(activity => (
                        <ActivityComponent key={activity.id} activity={activity}/>
                    ))}
                        
                </Fragment >
            ))}
        </>

    )
});