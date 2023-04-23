import React from 'react';
import logo from './image/NAULogo.png';

export const Header = () => {

    const headerStyle = {

        width: '100%',
        padding: '2%',
        //backgroundColor: "white",
        color: 'black',
        textAlign: 'center'
    }

    return (
        <div style={headerStyle}>
            <img src={logo} alt="NAU Country"/>
            {/* <h1>NAU Country PDF Generator</h1> */}
        </div>

    )
}
