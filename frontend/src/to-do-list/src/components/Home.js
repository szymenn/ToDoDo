import Header from './Header';
import React from 'react';
import {withRouter} from 'react-router-dom';
import MainContent from './MainContent';

function Home(props){
    return(
        <div>
            <Header/>
            <MainContent/>
        </div>
    )
}

export default withRouter(Home)