# CMS simplified
Headless (no UI, BE only) CMS for managing articles (CRUD).

### Description
Each article has its ID, title, body and timestamps (on creation and update). 

DB is seeded with some sample data. One user is added(Id = 1, Email = "admin@SharpIT.com") to show real-life usage of an authentication.

All DB entities derive from base ```Entity``` class that has common properties like ```Id```, date ```CreatedOn```, date ```ModifiedOn```. Dates are automatically preset when ```SaveChanges``` of the DB context is called.

```ApiControllerBase``` is added as a base controller and all others should derive from it. ```ApiControllerBase``` is decorated with ```AuthenticateApi``` attribute which is used to read headers and validate token.
```AllowAnonymous``` attribute is added only on top of the ```login``` API endpoint to skip the authorization process.
```AuthorizeApi(Activity.ViewArticles)``` attribute is added to some API endpoints to give example of pemission management.

For reading/search the articles, the endpoint ```search``` is used. It allows specifying a field to sort by including whether in an ascending or descending order + basic limit/offset pagination. It expects format like ```
{
  "itemsPerPage": 0,
  "pageNumber": 0,
  "order": [
    {
      "columnName": "string",
      "desc": true
    }
  ],
  "filter": {
    "title": "string",
    "body": "string"
  }
}```. Only ```itemsPerPage``` and ```pageNumber``` are required.

The whole client-server communication is in a JSON format. ```AddXmlDataContractSerializerFormatters``` is called in ```ConfigureServices``` to allow usage of XML format.

Swagger is integrated.

### Technical specifications:
* .NET Core 5
* ASP.NET Core
* Automated tests using nUnit and Autofac
* REST API
* MSSQL server
