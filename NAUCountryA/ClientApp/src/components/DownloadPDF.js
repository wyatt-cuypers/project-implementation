import React, { useState, useEffect } from 'react';

export const DownloadPDF = ({ onChangeForm, downloadPDF }) => {


    const contentStyle = {
        margin: '2%'
      
    }

 

    return (
        <div style={contentStyle}>
            <div class="row">
                <div class="col-md-2">
                    <label htmlFor="state">State:
                        <input list="states" onChange={(e) => onChangeForm(e)}  name="selectedState"/>
                    </label>

                    <datalist id="states">
                        <option value="Arizona" />
                        <option value="North Dakota" />
                    </datalist>
                    <label htmlFor="year">Year:
                        <input list="years" onChange={(e) => onChangeForm(e)} name="selectedYear" />
                    </label>

                    <datalist id="years">
                        <option value="2023" />
                    </datalist>
                    <label htmlFor="type">Type:
                        <input list="types" onChange={(e) => onChangeForm(e)} name="selectedType" />
                    </label>

                    <datalist id="types">
                        <option value="Corn" />
                        <option value="Wheat" />
                    </datalist>
                    <button type="button" onClick={(e) => downloadPDF()} className="btn btn-danger">Download</button>
               </div>

                <div class="col-md-10">
                    <div></div>
                </div>
            </div>
        </div>

    )
}
