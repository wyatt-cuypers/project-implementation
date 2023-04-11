import React, { Component } from 'react';

export class PDFGetter extends Component {

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }
  
getPDF() {
    fetch('/api/Ebook/TestPDF.pdf')
        .then(response => {
        // Check if the response was successful
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        // Return the response as a blob (binary data)
        return response.blob();
        })
        .then(data => {
        // Create a URL for the blob data
        const pdfUrl = URL.createObjectURL(data);
        // Open the PDF file in a new window
        window.open(pdfUrl);
        })
        .catch(error => {
        console.error('There was a problem downloading the PDF file:', error);
    });
    //const data = await response.json();
    //this.setState({ forecasts: data, loading: false });
  }

  render() {

    return (
      <div>
        <h1 id="tabelLabel" ></h1>
        <p>Click the button to download an example PDF</p>
        <button
            onClick={this.getPDF}
        >
            Open PDF
        </button>
      </div>
    );
  }
}