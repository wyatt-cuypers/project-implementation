import React, { Component } from "react";
import { statesList } from "../statesList.js";
import { yearsList } from "../yearsList.js";
import './DownloadPDF.css';
import loading from './image/loading.gif'
const today = new Date();

export class DownloadPDF extends Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedState: "",
      selectedAllYear: today.getUTCFullYear(),
      selectedGroupYear: today.getUTCFullYear(),
      loading: false,
      allGroupSelected: true,
      allSelected: true,
      stateOrAll: "",
    };
  }

  downloadSelectedPDFs() {
    fetch(`/api/DownloadPdf/${this.state.selectedState}/${this.state.selectedGroupYear}`, {
        method: "POST"})
    .then((response) => {
      console.log('this should execute');
        // Check if the response was successful
        if (!response.ok) {
            console.log(response)
          throw new Error("Network response was not ok");
        }
        this.setState({loading: false})
      })
      .catch((error) => {
        console.error("There was a problem generating the PDF files:", error);
      });
  }

  downloadAllPDFs() {
    fetch(`/api/DownloadPdf/${this.state.selectedAllYear}`, {
        method: "POST"})
    .then((response) => {
        // Check if the response was successful
        if (!response.ok) {
            console.log(response)
          throw new Error("Network response was not ok");
        }
        this.setState({loading: false})
      })
      .catch((error) => {
        console.error("There was a problem generating the PDF files:", error);
      });
  }

  render() {
    return (
      <div style={styleSheet.contentStyle}>
      {this.state.loading ? <div style={{fontWeight: 'bold', fontSize: 40, marginTop: 20}}><text>Generating </text> <img height="40px" width="40px" src={loading}/> </div>: <></>} 
      <div style={styleSheet.buttonDivStyle}>
        <button
        id="btnChoose"
          type="button"
          style={styleSheet.buttonStyle}
          onClick={(e) => {
            this.setState({stateOrAll: "State"})
          }}
          className="btn"
        >
          <b>Generate State</b>
        </button>
        <button
        id="btnChoose"
          type="button"
          //class="downloadButton"
          style={styleSheet.buttonStyle}
          onClick={(e) => {
            this.setState({stateOrAll: "All"});
            console.log(this.state.stateOrAll)
          }}
          className="btn"
        >
          <b>Generate All</b>
        </button>
      </div>
      {this.state.stateOrAll == "State" ? (
        <div class="row" style={styleSheet.centerStyle}>
          <div class="col-md-3">
            <div style={styleSheet.dropStyle}>
              <label htmlFor="state">
                State
                <br />
              </label>
              <select
                id="states"
                value={this.state.selectedState}
                style={{ textAlign: "center" }}
                onChange={(e) => {
                  this.setState({ selectedState: e.target.value });
                }}
                name="selectedState"
              >
                <option value="">Please choose a state</option>
                {statesList.map((state, index) => (
                  <option key={index} value={state}>
                    {state}
                  </option>
                ))}
              </select>
            </div>

            <div style={styleSheet.dropStyle}>
              <label htmlFor="year">Year</label>
              <select
                id="years"
                value={this.state.selectedGroupYear}
                style={{ textAlign: "center" }}
                onChange={(e) => {
                  this.setState({ selectedGroupYear: e.target.value });
                }}
                name="selectedGroupYear"
              >
                <option value="">Please choose a year</option>
                {yearsList.map((year, index) => (
                  <option key={index} value={year}>
                    {year}
                  </option>
                ))}
              </select>
            </div>
            <div style={styleSheet.centerStyle}>
              <button
                type="button"
                //class="downloadButton"
                style={{marginTop: 10}}
                onClick={(e) => {
                    if (
                      this.state.selectedState !== "" &&
                      this.state.selectedGroupYear !== ""
                    ) {
                      this.setState({ allGroupSelected: true, loading: true });
                      this.downloadSelectedPDFs();
                    } else {
                      this.setState({ allGroupSelected: false });
                    }
                }}
                className="btn"
              >
                <b>Download</b>
              </button>
            </div>
          </div>
          {!this.state.allGroupSelected ? (
            <div style={styleSheet.notFoundStyle}>
              <p>Please select all options before submitting</p>
            </div>
          ) : (
            <></>
          )}
        </div>
      ) : (<></>)}
      {this.state.stateOrAll == "All" ? (
            <div class="row" style={styleSheet.centerStyle}>
              <div class="col-md-3">
                <div style={styleSheet.dropStyle}>
                  <label htmlFor="year">Year</label>
                  <select
                    id="years"
                    value={this.state.selectedAllYear}
                    style={{ textAlign: "center" }}
                    onChange={(e) => {
                      this.setState({ selectedAllYear: e.target.value });
                    }}
                    name="selectedAllYear"
                  >
                    <option value="">Please choose a year</option>
                    {yearsList.map((year, index) => (
                      <option key={index} value={year}>
                        {year}
                      </option>
                    ))}
                  </select>
                </div>
                <div style={styleSheet.centerStyle}>
                  <button
                    type="button"
                    //class="downloadButton"
                    style={{marginTop: 10}}
                    onClick={(e) => {
                        if (
                          this.state.selectedAllYear !== ""
                        ) {
                          this.setState({ allSelected: true, loading: true });
                          this.downloadAllPDFs();
                        } else {
                          this.setState({ allSelected: false });
                        }
                    }}
                    className="btn"
                  >
                    <b>Download</b>
                  </button>
                </div>
              </div>
              {!this.state.allSelected ? (
                <div style={styleSheet.notFoundStyle}>
                  <p>Please select all options before submitting</p>
                </div>
          ) : (<></>)}
        </div>
      ) : (<></>)}
    </div>
    );
  }
}

const styleSheet = {
  dropStyle: {
    margin: 10,
    fontWeight: "bold",
    display: "grid",
  },
  centerStyle: {
    justifyContent: "center",
    textAlign: "center",
  },
  buttonDivStyle: {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
  },
  contentStyle: {
    margin: "2%",
    textAlign: "center",
  },
  notFoundStyle: {
    color: "red",
    fontWeight: "bold",
    fontSize: 25,
    margin: 25,
  },
  buttonStyle: {
    width: '25%', 
    margin: 30,
    //color: this.state.stateOrAll ==
  }
};
  
  