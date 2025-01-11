import {ErrorMessage, Form, Formik} from "formik";
import {Button, Header} from "semantic-ui-react";
import MyTextInput from "../../app/common/form/MyTextInput";
import { useStore } from "../../app/stores/store";
import {observer} from "mobx-react-lite"
import * as Yup from 'yup';
import ValidationError from "../errors/ValidationError";

export default observer(function RegisterForm()
{
    const {userStore} = useStore();
    return (
        <Formik
            initialValues ={{displayName: '', username: '', email: '', password: '', error: null}}
            onSubmit={(values,{setErrors}) =>
                userStore.register(values)
                    .catch(error => setErrors({error:error}))}
            validationSchema={Yup.object({
                displayName: Yup.string().required(),
                username: Yup.string().required(),
                email: Yup.string().required().email(),
                password: Yup.string().required()
            })}
            >
            {({handleSubmit, isSubmitting, errors, isValid, dirty}) => (
                <Form className='ui form error' onSubmit={handleSubmit} autoComplete='off'>
                    <Header as='h2' content='Register to Reactivities' color='teal' textAlign='center'/>
                    <MyTextInput
                        name='displayName'
                        placeholder='Display Name'
                    />
                    <MyTextInput
                        name='username'
                        placeholder='User Name'
                    />
                    <MyTextInput
                        name='email'
                        placeholder='Email'
                    />
                    <MyTextInput
                        name='password' placeholder='Password'
                    />
                    <ErrorMessage name='error' render={() => 
                    <ValidationError errors={errors.error as unknown as string[]}/>}/>
                    <Button loading={isSubmitting} disabled={!isValid || !dirty || isSubmitting} type='submit' content='Register' positive fluid primary  />
                </Form>
            )
            }

        </Formik>
    )
});