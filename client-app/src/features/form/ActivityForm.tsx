import {Button, Form, Segment} from "semantic-ui-react";
import { Activity } from "../../app/models/activity";
import {ChangeEvent, useState} from "react";


interface Props{
    activity: Activity;
    closeForm: () => void;
    createOrEdit: (activity: Activity) => void;
}
export default function ActivityForm({activity: selectedActivity, closeForm, createOrEdit}:Props) {
    
    const initialState = selectedActivity ?? {
        id: '',
        title: '',
        description: '',
        category: '',
        date: '',
        city: '',
        venue: ''
    }
    
    const [activity, setActivity] = useState(initialState);
    
    function handleSubmit() {
        console.log(activity);
        createOrEdit(activity);
    }
    
    function handleInputChange(event: ChangeEvent<HTMLInputElement>) {
        const {name, value} = event.target;
        setActivity({...activity, [name]: value});
    }
    return (
        <Segment clearing>
            <Form onSubmit={handleSubmit} autoComplete='off'>
                <Form.Input placeholder='Title' value={activity.title} name='title' onChange={handleInputChange}></Form.Input>
                <Form.Input placeholder='Description' value={activity.description} name='description' onChange={handleInputChange}></Form.Input>
                <Form.Input placeholder='Category' value={activity.category} name='category' onChange={handleInputChange}></Form.Input>
                <Form.Input placeholder='Date' value={activity.date} name='date' onChange={handleInputChange}></Form.Input>
                <Form.Input placeholder='City' value={activity.city} name='city' onChange={handleInputChange}></Form.Input>
                <Form.Input placeholder='Venue' value={activity.venue} name='venue' onChange={handleInputChange}></Form.Input>
                <Button floated='right' positive type='submit' content='Submit'></Button>
                <Button floated='right' type='button' content='Cancel' onClick={closeForm}></Button>
            </Form>
        </Segment>
    )
}