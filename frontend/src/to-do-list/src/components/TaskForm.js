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
                            <Input type="text" id="task" placeholder="Task" onChange={e=>props.handleChange(e)} value={props.task} />
                        </FormGroup>
                        <FormGroup>
                            <Label for="date">Date</Label>
                            <Input type="datetime-local" id="date" placeholder="Date" onChange={e=>props.handleChange(e)} value={props.date} />
                        </FormGroup>
                        <FormGroup>
                            <Input type="submit" value="Submit"/>
                        </FormGroup>
                    </Form>
                </Container>
            </div>
    )
}