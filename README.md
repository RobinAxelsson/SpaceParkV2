# Space Park V2 - SpaceX

This is an Rest-API project that is based on the Star Wars Web API [swapi.dev/](https://swapi.dev/) and is used to for practicing Http-Requests, Entity Framework Core and ASP.NET web API.

Authors: [Albin Alm,](https://github.com/albinalm) [Robin Axelsson](https://github.com/RobinAxelsson)

![image from the console client](https://user-images.githubusercontent.com/63591629/117513008-0988a180-af91-11eb-8a78-a68ab4c97e91.png)

## .NET Projects overview

---

- [***SpacePark-API***](/Source/SpacePark-API) - main project ASP.NET web-api, contains:
  - Data Models
  - DbContext
  - Controllers (Rest)
  - Logic
- [***StarwarsConsoleClient***](https://github.com/PGBSNH20/spaceparkv2-buddygroup6-renegades/tree/main/Source/StarwarsConsoleClient) - A Console based frontend client that is migrated partly from V1
- [***SpaceParkTests***](https://github.com/PGBSNH20/spaceparkv2-buddygroup6-renegades/tree/main/Source/SpaceParkTests) - xunit test project, implements:
  - in-memory-sql database (dependent on nu-get EntityFrameworkCore.InMemory)
  -  mock web-host for testing the http-requests. See [MockWebHostFactory.cs](https://github.com/PGBSNH20/spaceparkv2-buddygroup6-renegades/blob/main/Source/SpaceParkTests/MockWebHostFactory.cs)

## Server side authorization, token and roles code example

---

```csharp
if (account.Password != model.Password || account.AccountName != model.Username) return Unauthorized();
            var identity = GetClaimsIdentity(account);
            var token = GetJwtToken(identity);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            var userToken = new UserToken()
            {
                Account = account,
                ExpiryDate = token.ValidTo,
                Token = tokenString
            };
            _repository.Add(userToken);
            _repository.SaveChanges();
            return Ok(new
            {
                token = tokenString,
                expiration = token.ValidTo
            });
```

## Client side login code example

---

```csharp
public async Task<bool> LoginAsync(string accountName, string password)
{
var client = new HttpClient();
var response = await JsonPostRequestAsync(EndPoints.Account.login, new
{
username = accountName,
password = password
});
var responseContentString = await response.Content.ReadAsStringAsync();
var token = GetResponseValueWithKey("token", responseContentString);
_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
 return response.IsSuccessStatusCode;
}
```

## Set-up instructions

---

***To Run the projects simultaneously in Visual Studio use following settings***

**![image](https://user-images.githubusercontent.com/63591629/117661026-20501380-b19e-11eb-931c-3e4d1da0f758.png)**
