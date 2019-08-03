import React from 'react';
import {Button} from 'reactstrap';

export default function ToDo(props){
    return( 
    <tbody key={props.index}>
        <tr>
            <td >{props.task}</td>
            <td >{props.date}</td>
            <th>
                <Button color="secondary" onClick={(id) => {props.handleEdit(props.id)}}>Edit</Button>
            </th>
            <th>
                <Button color="danger" onClick={(id)=>{props.handleDelete(props.id)}}>Delete</Button>
            </th>
        </tr>
    </tbody>)
}