import React, { Component } from 'react';
import { Layout } from './components/Layout';
import './custom.css';
import { Header } from './components/Header';
import { DownloadPDF } from './components/DownloadPDF'


export default class App extends Component {
    static displayName = App.name;

    state = {
        selectedOptions: { },
    }

    downloadPDF = (e) => {
        this.downloadPDF(this.state.slectedOptions)
            /*need to use sections and http post to find pdf and return here*/
    }

    onChangeForm = (e) => {
        let selectedOptions = this.state.selectedOptions
        if (e.target.name === 'selectedState') {
            selectedOptions.state = e.target.value;
        } else if (e.target.name === 'selectedYear') {
            selectedOptions.year = e.target.value;
        } else if (e.target.name === 'selectedType') {
            selectedOptions = e.target.value;
        }
        this.setState({ selectedOptions })
    }

  render() {
    return (
        <div className="App">
            <Header></Header>
            <DownloadPDF
                onChangeForm={this.onChangeForm}
                downloadPDF={this.downloadPDF}
            >
            </DownloadPDF>
        </div>
      
    );
  }
}
