# Testing ESRI API Project

This is a simple console application whose primary purpose is to test the REST API of ESRI's ArcGIS Services.

## To Setup and Run

- ESRIrequest.json

  - Needs URL updated (look for TODO).  Will look something like https://[your domain]/aghs/rest/services/[your map name]/AdminAll/MapServer/4 (4 is the index of the layer)
  - Correct column needs to be applied in file based on table data upload to your own map

- Program.cs
  
  - URL needs to be set for the execute GP Task. Will look something like https://[your domain]/aghs/rest/services/[your map name]/ExportWebMap/GPServer/Export%20Web%20Map/execute

Once URL's are set and column update you should be able to generate a map.

## How to Add this to your PDF

You will run similar logic but as you loop through each county you build up the uniqueValueInfos property with the county codes and the colors you want printed for the SVG. This will be done for every page. Once you have the SVG URL from the GP task you can download those images and apply them to the PDF.  Probably a good idea to cleanup the images after they are used to avoid eating up hard drive space.

### County Labels

Don't focus on this to much as we have a legend to the colors.  When you get here you can reach out to Brad if you have question on how we might be able to apply a label with the map SVG generated to get the numbers to show up.