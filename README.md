# IonaAPI
A combined API of thecatapi.com and the thedogapi.com

## Prerequisite:
Download and Install Dotnet sdk
https://dotnet.microsoft.com/en-us/download/dotnet/6.0

### Set secrets.
#### Commands:
…> cd IonaAPI.API
dotnet user-secrets set "CatApiSettings:Url" "your-CatApi-Url"
dotnet user-secrets set "CatApiSettings:ApiKey" "your-CatAPI-key"
dotnet user-secrets set "DogApiSettings:Url" "your-DogApi-Url"
dotnet user-secrets set "DogApiSettings:ApiKey" "your-DogAPI-key"


### Run IntegrationTest
#### Commands:
…> cd IonaAPI.IntegrationTest
…> dotnet test

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


## Endpoints:
#### /api/v1/Breeds
Example
Get: /api/v1/Breeds?page=0,limit=10.


**Returns:** Lists of Breeds.


--------------------------------------------


#### /api/v1/Breeds/id
Example
Get: /api/v1/Breeds/abys?page=0,limit=10.


**Returns:** Lists of images by breed.


--------------------------------------------


#### /api/v1/Images or /api/v1/list
Example
Get: /api/v1/Breeds?page=0,limit=10.


**Returns:** List of images.**


--------------------------------------------


#### /api/v1/Images/id or /api/v1/Image/id
Example
Get: /api/v1/Image/xyz


**Returns**: The image by Id.


--------------------------------------------


### Test with swagger.
Endpoints:
/swagger





