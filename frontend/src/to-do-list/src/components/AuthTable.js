import {Button, Table} from 'reactstrap';
import React from 'react';
import ToDo from './ToDo';

export default function AuthTable(props){
    const resultTodos = props.todos.map((entry, index) => {
        let date = new Date(entry.date)
        return(
            <ToDo index={index} task={entry.task} date={date.toDateString()} handleDelete={props.handleDelete} entry={entry}/>
        )
    })
    return(
            <Table>
                <thead>
                    <tr>
                        <th>Task</th>
                        <th>Date</th>
                        <th>
                            <Button color="success" onClick={props.handleAdd}>Add new Task</Button>
                        </th>
                    </tr>
                </thead>
                {resultTodos}
            </Table>
    )
}
