import React from 'react';
import {Form, FormGroup, Input, Container, Label} from 'reactstrap';
import Header from './Header';

export default function TaskForm(props){
    return(
     <div>
            <Header/>
                <Container>
                    <Form onSubmit={e=>props.handleSubmit(e)}>
                        <FormGroup>
                            <Label for="task">Task</Label>
                            <Input type="text" id="task" placeholder="Task"/>
                        </FormGroup>
                        <FormGroup>
                            <Label for="date">Date</Label>
                            <Input type="datetime-local" id="date" placeholder="Date"/>
                        </FormGroup>
                        <FormGroup>
                            <Input type="submit" value="Submit"/>
                        </FormGroup>
                    </Form>
                </Container>
            </div>
    )
}