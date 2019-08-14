import React from 'react';
import {Form, FormGroup, Input, Container, Label} from 'reactstrap';
import Header from './Header';

export default function TaskForm(props){
    const { handleSubmit, handleChange, values } = props
    return(
     <div>
            <Header/>
                <Container>
                    <Form onSubmit={handleSubmit}>
                        <FormGroup>
                            <Label for="task">Task</Label>
                            <Input type="text" name="task" placeholder="Task" value={values.task} onChange={handleChange}/>
                        </FormGroup>
                        <FormGroup>
                            <Label for="date">Date</Label>
                            <Input type="datetime-local" name="date" value={values.date} onChange={handleChange}/>
                        </FormGroup>
                        <FormGroup>
                            <Input type="submit" value="Submit"/>
                        </FormGroup>
                    </Form>
                </Container>
            </div>
    )
}