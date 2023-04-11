import React, { useState, useEffect } from "react";
import { statesList } from "../statesList.js";
import { cropsList } from "../cropsList.js";
import { yearsList } from "../yearsList.js";

export const DownloadPDF = (/*{ onChangeForm, downloadPDF }*/) => {
  const [selectedState, setSelectedState] = useState("");
  const [selectedCrop, setSelectedCrop] = useState("");
  const [selectedYear, setSelectedYear] = useState("");
  
  function getCommodities() {
    fetch('/api/Commodity')
      .then(response => {
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        return response.json();
      })
      .then(data => {
        console.log(data);
      })
      .catch(error => {
        console.error('There was a problem fetching the commodities:', error);
    });
}

  function getPDF() {
    fetch('/api/Ebook//Alabama_Forage Seeding_2023_PDF.pdf')
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
  return (
    <div style={styleSheet.contentStyle}>
      <div
        class="row"
        style={styleSheet.centerStyle}
      >
        <div class="col-md-2">
          <div style={styleSheet.dropStyle}>
            <label htmlFor="state">State<br/></label>
            <select
              id="states"
              value={selectedState}
              style={{textAlign: 'center'}}
              onChange={(e) => {
                //onChangeForm(e);
                setSelectedState(e.target.value);
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
            <label htmlFor="crop">Crop</label>
            <select
              id="crops"
              value={selectedCrop}
              style={{textAlign: 'center'}}
              onChange={(e) => {
                //onChangeForm(e);
                setSelectedCrop(e.target.value);
              }}
              name="selectedType"
            >
              <option value="">Please choose a crop</option>
              {cropsList.map((crop, index) => (
                <option key={index} value={crop}>
                  {crop}
                </option>
              ))}
            </select>
          </div>
          <div style={styleSheet.dropStyle}>
            <label htmlFor="year">Year</label>
            <select
              id="years"
              value={selectedYear}
              style={{textAlign: 'center'}}
              onChange={(e) => {
                //onChangeForm(e);
                setSelectedYear(e.target.value);
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
                console.log(selectedYear)
                getPDF();
                //setSelectedCrop(e.target.value);
              }}
              className="btn btn-danger"
            >
              <b>Download</b>
            </button>
            <button
            onClick={(e) => {
                console.log(selectedYear)
                getCommodities();
                //setSelectedCrop(e.target.value);
            }}>
                Log Commodities
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

const styleSheet = {
  dropStyle:{
      margin: 10,
      fontWeight: 'bold',
      display: 'grid'
  },
  centerStyle:{
      justifyContent: 'center',
      textAlign: "center"
  },
  contentStyle:{
    margin: "2%",
  },
}
