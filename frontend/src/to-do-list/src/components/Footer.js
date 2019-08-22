import React from 'react';
import { Card, Button, CardTitle } from 'reactstrap';

export default function Footer(props){
    return (
        <div>
            <Card body>
                <p className="lead">
                <Button href="https://github.com/szymenn/ToDoDo">View Source</Button>

                </p>
            </Card>
        </div>
    )
}
