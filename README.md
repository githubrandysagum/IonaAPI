# IonaAPI
A combined API of thecatapi.com and the thedogapi.com

Prerequisite:
Download and Install Dotnet sdk
https://dotnet.microsoft.com/en-us/download/dotnet/6.0

### Run UnitTest
#### Commands:
…> cd IonaAPI.UnitTest
…> dotnet test

### Run the app.
#### Commands:
…> cd IonaAPI.API
…> dotnet run

### Limit and Paging
Zero is the first page.
Limit is  greater than 0 and not greater than 100.

### Endpoints:
#### /api/v1/Breeds
Example
Get: /api/v1/Breeds?page=0,limit=10
Returns lists of Breeds .

#### /api/v1/Breeds/id
Example
Get: /api/v1/Breeds/abys?page=0,limit=10
Returns lists of images by breed.

#### /api/v1/Images or /api/v1/list
Example
Get: /api/v1/Breeds?page=0,limit=10
Returns list of images.

#### /api/v1/Images/id or /api/v1/Image/id
Example
Get: /api/v1/Image/xyz
Returns the image by Id.

### Test with swagger.
Endpoints:
/swagger





