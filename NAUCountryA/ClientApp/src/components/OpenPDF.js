import React, { Component } from "react";
import { statesList } from "../statesList.js";
import { yearsList } from "../yearsList.js";
const today = new Date();

export class OpenPDF extends Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedState: "",
      selectedCommodity: "",
      selectedYear: today.getUTCFullYear(),
      commoditiesList: [],
      loading: true,
      pdfNotFound: false,
      allSelected: true,
    };
  }

  // componentDidUpdate(prevState) {
  //   if(prevState.selectedState !== this.state.selectedState || prevState.selectedYear !== this.state.selectedYear) {
  //     if(this.state.selectedState !== "" && this.state.selectedYear !== "") {
        
  //       this.getCommodities();
  //     }
  //   }
  // }

  componentDidMount() {
    this.getCommodities();
  }

  async getCommodities() {
    const response = await fetch("/api/Commodity");
    const data = await response.json();
    this.setState({ commoditiesList: data, loading: false });
  }

  // getCommodities() {
  //   //this.setState({ loading: true });
  //   const response = fetch(`/api/Commodity/${this.state.selectedState}/${this.state.selectedYear}`).then(
  //   response => response.json())
  //   .then(data => {
  //     console.log(data);
  //     this.setState({ commoditiesList: data, loading: false });
  //   })
  //   .catch(err => {
  //     console.log(err);
  //   })
    
  //   //this.setState({ commoditiesList: data, loading: false });
  // }

  getPDF() {
    fetch(
      `/api/Ebook/${this.state.selectedState}/${this.state.selectedCommodity}/${this.state.selectedYear}`
    )
      .then((response) => {
        // Check if the response was successful
        if (!response.ok) {
          this.setState({ pdfNotFound: true });
          throw new Error("Network response was not ok");
        }
        // Return the response as a blob (binary data)
        this.setState({ pdfNotFound: false });
        return response.blob();
      })
      .then((data) => {
        // Create a URL for the blob data
        const pdfUrl = URL.createObjectURL(data);
        // Open the PDF file in a new window
        window.open(pdfUrl);
      })
      .catch((error) => {
        console.error("There was a problem downloading the PDF file:", error);
      });
  }
  render() {
    return (
      <div style={styleSheet.contentStyle}>
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
            {this.state.loading ? (
              <p>
                <em>Loading...</em>
              </p>
            ) : (
              <div style={styleSheet.dropStyle}>
                <label htmlFor="crop">Commodity</label>
                <select
                  id="crops"
                  value={this.state.selectedCommodity}
                  style={{ textAlign: "center" }}
                  onChange={(e) => {
                    this.setState({ selectedCommodity: e.target.value });
                  }}
                  name="selectedType"
                >
                  <option value="">Please choose a crop</option>
                  {this.state.commoditiesList.sort().map((crop, index) => (
                    <option key={index} value={crop}>
                      {crop}
                    </option>
                  ))}
                </select>
              </div>
            )}
            <div style={styleSheet.dropStyle}>
              <label htmlFor="year">Year</label>
              <select
                id="years"
                value={this.state.selectedYear}
                style={{ textAlign: "center" }}
                onChange={(e) => {
                  this.setState({ selectedYear: e.target.value });
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
                style={{ marginTop: 10 }}
                type="button"
                onClick={(e) => {
                  console.log(this.state.selectedState, this.state.selectedCommodity, this.state.selectedYear)
                  if (
                    this.state.selectedState !== "" &&
                    this.state.selectedCommodity !== "" &&
                    this.state.selectedYear !== ""
                  ) {
                    this.setState({ allSelected: true });
                    this.getPDF();
                  } else {
                    this.setState({ allSelected: false, pdfNotFound: false });
                  }
                }}
                className="btn btn-danger"
              >
                <b>Download</b>
              </button>
            </div>
          </div>
          {this.state.pdfNotFound ? (
            <div style={styleSheet.notFoundStyle}>
              <p>SELECTED PDF NOT FOUND</p>
            </div>
          ) : (
            <></>
          )}
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
    fontWeight: "bold",
    display: "grid",
    
  },
  centerStyle: {
    justifyContent: "center",
    textAlign: "center",
  },
  contentStyle: {
    margin: "2%",
    textAlign: "center"
  },
  notFoundStyle: {
    color: "red",
    fontWeight: "bold",
    fontSize: 25,
    margin: 25,
  },
};
