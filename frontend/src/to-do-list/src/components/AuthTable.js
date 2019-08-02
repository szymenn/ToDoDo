import {Button, Table} from 'reactstrap';
import React from 'react';

export default function AuthTable(props){
    const resultTodos = props.todos.map((entry, index) => {
        let date = new Date(entry.date)
        return(
            <tbody key={index}>
                    <tr>
                        <td >{entry.task}</td>
                        <td >{date.toDateString()}</td>
                        <th>
                            <Button color="secondary">Edit</Button>
                        </th>
                        <th>
                            <Button color="danger" onClick={(id)=>{props.handleDelete(entry.id)}}>Delete</Button>
                        </th>
                    </tr>
            </tbody>
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
