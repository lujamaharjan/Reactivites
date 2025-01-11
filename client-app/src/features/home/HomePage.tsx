import {Button, Container, Header, Segment, Image} from "semantic-ui-react";
import {Link} from "react-router-dom";
import { useStore } from "../../app/stores/store";
import LoginForm from "../users/LoginForm";
import RegisterForm from "../users/RegisterForm";

export default function HomePage(){
    const {userStore, modalStore} = useStore();
    return(
        
        
        <Segment inverted textAlign='center' vertical className='masthead'>
            <Container text>
                <Header as="h1" inverted>
                    <Image size="massive" src="/assets/logo.png" alt="logo" style={{marginBottom: 12}} />
                    Reactivities
                </Header>
                {userStore.isLoggedIn ? (
                    <>
                    <Header as="h2" inverted content="Welcome to Reactivities" />
                    <Button as={Link} to='/activities' size='huge' inverted>
                        Go to Activities!
                    </Button>
                    </>
                   
                ) : (
               
                    <>
                        <Button size='huge' onClick={() => modalStore.openModal(<LoginForm/>)} inverted>
                            Login!
                        </Button>

                        <Button  size='huge' onClick={() => modalStore.openModal(<RegisterForm/>)} inverted>
                            Register
                        </Button>
                    </>
                )}
            </Container>
        </Segment>
    )
}