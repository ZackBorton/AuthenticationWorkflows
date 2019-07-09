# AuthenticationWorkflows 
Example OAuth2 authentication examples

### Resources
* [Spa Authentication Example](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-3.0)
* [Identity Server Documentation](https://github.com/IdentityServer/IdentityServer4) used to create tokens
* [Auth setup example](https://medium.com/@ozgurgul/asp-net-core-2-0-webapi-jwt-authentication-with-identity-mysql-3698eeba6ff8)
* [General Workflow](https://devblogs.microsoft.com/aspnet/bearer-token-authentication-in-asp-net-core/)

### Generating a Self signed cert
* ```openssl genrsa -des3 -passout pass:x -out IdentityServer4Auth.pass.key 2048 ```
* ```openssl rsa -passin pass:x -in IdentityServer4Auth.pass.key -out IdentityServer4Auth.key```
* ```rm IdentityServer4Auth.pass.key```
* ```openssl req -new -key IdentityServer4Auth.key -out IdentityServer4Auth.csr```
* ```openssl x509 -req -sha256 -days 365 -in IdentityServer4Auth.csr -signkey IdentityServer4Auth.key -out IdentityServer4Auth.crt```
* ```openssl pkcs12 -export -out IdentityServer4Auth.pfx -inkey IdentityServer4Auth.key -in IdentityServer4Auth.crt ```

// Appveyor or TravisCI