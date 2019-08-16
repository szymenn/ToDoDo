import React from 'react';
import {Form, FormGroup, Input, Container, Label, FormText } from 'reactstrap';
import Header from './Header';

export default function TaskForm(props){
    const { handleSubmit, handleChange, values, errors, touched } = props
    return(
     <div>
            <Header/>
                <Container>
                    <Form onSubmit={handleSubmit}>
                        <FormGroup>
                            <Label for="task">Task</Label>
                            <Input type="text" name="task" placeholder="Task" value={values.task} onChange={handleChange}/>
                            {errors.task && touched.task &&<FormText color="danger">{errors.task}</FormText>}
                        </FormGroup>
                        <FormGroup>
                            <Label for="date">Date</Label>
                            <Input type="datetime-local" name="date" value={values.date} onChange={handleChange}/>
                            {errors.date && touched.date &&<FormText color="danger">{errors.date}</FormText>}
                        </FormGroup>
                        <FormGroup>
                            <Input type="submit" value="Submit"/>
                        </FormGroup>
                    </Form>
                </Container>
            </div>
    )
}