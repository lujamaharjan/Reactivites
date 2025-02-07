import {Button, Header, Segment} from "semantic-ui-react";
import {useEffect, useState} from "react";
import {v4 as uuid} from 'uuid';
import { useStore } from "../../app/stores/store";
import {observer} from "mobx-react-lite";
import {Link, useParams, useNavigate} from "react-router-dom";
import { Activity } from "../../app/models/activity";
import LoadingComponent from "../../app/layout/LoadingComponent";
import {Formik, Form} from "formik";
import * as Yup from "yup";
import MyTextInput from "../../app/common/form/MyTextInput";
import MyTextArea from "../../app/common/form/MyTextArea";
import MySelect from "../../app/common/form/MySelect";
import { categoryOptions } from "../../app/common/options/categoryOptions";
import MyDateInput from "../../app/common/form/MyDateInput";

export default observer( function ActivityForm() {
    const navigate = useNavigate();
    const {activityStore} = useStore();
    const {
        loading,
        loadActivity,
        loadingInitial,
        updateActivity,
        createActivity,
    } = activityStore;
    const {id} = useParams();
    
    const [activity, setActivity] = useState<Activity>({
        id: '',
        title: '',
        description: '',
        category: '',
        date: null,
        city: '',
        venue: ''
    });

    const validationSchema = Yup.object({
        title: Yup.string().required('The activity title is required'),
        description: Yup.string().required('The activity description is required'),
        category: Yup.string().required(),
        date: Yup.string().required('The date is required').nullable(),
        venue: Yup.string().required(),
        city: Yup.string().required(),
    })
    useEffect(() => {
        if (id) {
            loadActivity(id).then(activity => setActivity(activity!));
        }
        else {
            setActivity({
                id: '',
                title: '',
                description: '',
                category: '',
                date: null,
                city: '',
                venue: ''
            });
        }
    }, [id, loadActivity]);
    
    function handleFormSubmit(activity: Activity) {
        console.log(activity)
        if(!activity.id)
        {
            activity.id = uuid();
            createActivity(activity).then(() => 
                navigate(`/activities/${activity.id}`)
            );
        }
        else
        {
            updateActivity(activity).then(() =>
                navigate(`/activities/${activity.id}`)
            );
        }
    }

  
    if(loadingInitial) return <LoadingComponent content="Loading Activity ..."/>
    return (
        <Segment clearing>
            <Header content="Activity Details" sub color='teal'/>
            <Formik validationSchema={validationSchema} 
                    enableReinitialize 
                    initialValues={activity} 
                    onSubmit={values =>handleFormSubmit(values)}>
                {
                    ({ handleSubmit, isValid, isSubmitting, dirty}) => (
                        <Form className={'ui form'} onSubmit={handleSubmit} autoComplete='off'>
                            <MyTextInput label="Title"  placeholder='Title' name='title'/>
                            
                            <MyTextArea label="Description" rows={3} placeholder='Description' name='description'/>
                            <MySelect options={categoryOptions} label="Category" placeholder='Category' name='category'/>
                            <MyDateInput 
                                placeholderText='Date' 
                                label="Date" 
                                placeholder='Date' 
                                name='date' 
                                showTimeSelect
                                timeCaption='time' 
                                dateFormat='MMMM d, yyyy h:mm aa'/>
                            <Header content="Location Details" sub color='teal'/>
                            <MyTextInput label="City" placeholder='City'  name='city'/>
                            <MyTextInput label="Venue" placeholder='Venue'  name='venue'/>
                            <Button disabled={isSubmitting || !dirty || !isValid} loading={loading} floated='right' positive type='submit' content='Submit'></Button>
                            <Button as={Link} to={"/activities"} floated='right' type='button' content='Cancel'></Button>
                        </Form>
                    )
                } 
            </Formik>
           
        </Segment>
    )
});