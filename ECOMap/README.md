# ECO Map

This project is responsible for the loading, parsing, and generating the PDF as it pertains to ECO Maps.

## General Usage

- Entry into the data loading process is through ECODataService.cs
- Generating the PDF is through the EcoPdfGenerator.cs

## Code Review Feedback

This project just took your existing code and broke it into a separate assembly for a few reasons.

1. By breaking some of this out from the single Service.cs file you can begin to better understand the responsibility of each object and know what each does. (Readability)
2. Creating a separate project you can then have your API Service and any other project utilize the same code so nothing is lost.  Currently you have the Service assembly acting both as a client application and console application and you will lose some features when you convert back to an actual dot net core service. 
3. Parsing logic is tightly couple with DTO objects which will make it harder to transition to any other data source.  Comments in code "CODE REVIEW" 
4. Reading from CSV comes at a cost as you lose the benefits of any database...still OK to use, but I create some asynchronous logic to hopefully help with loading everything.  
5. Consider adding the PDF Output folder to the GIT Ignore file as when you start generating more files it will be more to upload through GIT. And this would not be done in real production code either. 