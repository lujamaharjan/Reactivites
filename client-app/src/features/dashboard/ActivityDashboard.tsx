import {Grid} from "semantic-ui-react";
import { Activity } from "../../app/models/activity";
import ActivityList from "./ActivityList";
import ActivityDetails from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";

interface Props {
    activities: Activity[];
    selectedActivity: Activity | undefined;
    cancelSelectActivity: () => void;
    selectActivity: (id: string) => void;
    editMode: boolean;
    formOpen: (id?: string) => void;
    formClose: () => void;
    createOrEditActivity: (activity: Activity) => void;
    deleteActivity: (id: string) => void
}

export default function ActivityDashboard(
    {
        activities,
        selectedActivity,
        selectActivity,
        cancelSelectActivity,
        editMode,
        formClose,
        formOpen,
        createOrEditActivity,
        deleteActivity
    }: Props) {
    return (
        <>
            <Grid>
                <Grid.Column width={10}>
                    <ActivityList activities={activities} selectActivity={selectActivity} deleteActivity={deleteActivity}></ActivityList>
                </Grid.Column>
                   
                <Grid.Column width={6}>
                    {selectedActivity && !editMode &&
                    <ActivityDetails activity={selectedActivity} cancelSelectActivity={cancelSelectActivity}
                        openForm={formOpen}
                    />
                    }
                    {
                        editMode &&
                        <ActivityForm closeForm={formClose} activity={selectedActivity} createOrEdit={createOrEditActivity}/>
                    }
                   
                </Grid.Column>
            </Grid>
        </>
    );
};