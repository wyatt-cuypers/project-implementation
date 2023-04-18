import React from 'react';

export const Header = () => {

    const headerStyle = {

        width: '100%',
        padding: '2%',
        backgroundColor: "lightblue",
        color: 'black',
        textAlign: 'center'
    }

    return (
        <div style={headerStyle}>
            <h1>NAU Country PDF Generator</h1>
        </div>

    )
}
