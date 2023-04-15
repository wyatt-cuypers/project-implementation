import React, { Component } from "react";
import { statesList } from "../statesList.js";
import { yearsList } from "../yearsList.js";

export class DownloadPDF extends Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedState: "",
      selectedAllYear: "",
      selectedGroupYear: "",
      loading: true,
      allGroupSelected: true,
      allSelected: true,
    };
  }

  downloadSelectedPDFs() {
    fetch(`/api/DownloadPdf/${this.state.selectedState}/${this.state.selectedAllYear}`, {
        method: "POST"})
    .then((response) => {
        // Check if the response was successful
        if (!response.ok) {
            console.log(response)
          throw new Error("Network response was not ok");
        }
      })
      .catch((error) => {
        console.error("There was a problem generating the PDF files:", error);
      });
  }

  render() {
    return (
      <div style={styleSheet.contentStyle}>
        <div class="row" style={styleSheet.centerStyle}>
          {/* <div class="col-md-3"> */}
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
                name="selectedYear"
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
                style={styleSheet.buttonStyle}
                type="button"
                onClick={(e) => {
                  //console.log(this.state.selectedState, this.state.selectedCommodity, this.state.selectedYear)
                  if (
                    this.state.selectedState !== "" &&
                    this.state.selectedAllYear !== ""
                  ) {
                    this.setState({ allGroupSelected: true });
                    this.downloadSelectedPDFs();
                  } else {
                    this.setState({ allGroupSelected: false });
                  }
                }}
                className="btn btn-danger"
              >
                <b>Download Selected PDFs</b>
              </button>
            {/* </div> */}
          </div>
          {!this.state.allGroupSelected ? (
            <div style={styleSheet.notFoundStyle}>
              <p>Please select all options before submitting</p>
            </div>
          ) : (
            <></>
          )}
        </div>
        <div class="row" style={styleSheet.centerStyle}>
            <div style={styleSheet.dropStyle}>
              <label htmlFor="year">Year</label>
              <select
                id="years"
                value={this.state.selectedAllYear}
                style={{ textAlign: "center" }}
                onChange={(e) => {
                  this.setState({ selectedAllYear: e.target.value });
                }}
                name="selectedYear"
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
                style={styleSheet.buttonStyle}
                type="button"
                onClick={(e) => {
                  if (
                    this.state.selectedAllYear !== ""
                  ) {
                    this.setState({ allSelected: true });
                    this.downloadSelectedPDFs();
                  } else {
                    this.setState({ allSelected: false });
                  }
                }}
                className="btn btn-danger"
              >
                <b>Generate All PDFs</b>
              </button>
          </div>
          {!this.state.allSelected ? (
            <div style={styleSheet.notFoundStyle}>
              <p>Please select all options before submitting</p>
            </div>
          ) : (
            <></>
          )}
        </div>
      </div>
    );
  }
}

const styleSheet = {
    dropStyle: {
      margin: 10,
      width: '50%',
      fontWeight: "bold",
      display: "grid",
      verticalAlign: "top"
    },
    centerStyle: {
      justifyContent: "center",
      alignItems: 'center',
      textAlign: "center",
      display: "inline-block",
      width: '50%',
      verticalAlign: "top"
    },
    contentStyle: {
      display: 'flex',
      justifyContent: "center"
    },
    notFoundStyle: {
      color: "red",
      fontWeight: "bold",
      fontSize: 25,
      margin: 25,
    },
    buttonStyle: {
      marginTop: 10,
      padding: 10,
    },
  };
  
  