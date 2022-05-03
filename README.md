# SimpleJWT
The most simplistic .NET Json Web Token (JWT) Library.
This repository demonstrates the basic functionality of encoding and decoding JWT tokens.

**Warning**: If you want to use authentication in a production environment, I highly suggest using a proper identity provider. Some of the recommended products are:
* [IdentityServer 4](https://github.com/IdentityServer/IdentityServer4) (open-source)
* [Duende IdentityServer](https://duendesoftware.com/) (paid)
* [Keycloak](https://github.com/keycloak/keycloak) (open-source)

## Target Frameworks
* .NET Standard 1.4

## NuGet
* Package available on [NuGet.org](https://www.nuget.org/packages/SimpleJWT/)

## Features
* Encodes and decodes Json Web Tokens (JWT)
* Verifies the signature while decoding
* Validates the exp-Claim if available
* Allows the usage of custom serializers and Base64 encoders
* Allows to provide standard claims which will always be added to the payload

# Usage
The use of dependency injection (if available) is recommended. However, the following code demonstrates the basic usage.
## Encode
```csharp
var jwtEncoder = new JwtEncoder(new NewtonsoftJsonSerializer(), new Base64Encoder(), new List<IStandardClaim>() { new ExpirationClaim() });

var payload = new Dictionary<string, object>
{
	{"sub", "1234567890"}, 
	{"name", "John Doe"}, 
	{"admin", true}
};

const string secret = "secret";
var jwt = jwtEncoder.Encode(payload, secret);
```

## Decode
```csharp
var jwtDecoder = new JwtDecoder(new NewtonsoftJsonSerializer(), new Base64Encoder(), new Base64Encoder());

const string secret = "secret";
const string jwt = @"eyJUeXAiOiJKV1QiLCJBbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibm
FtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9.du+/ve+/vUNj77+977+9Tu+/vdSmUzjvv71UBnFMbe+/vQTvv
71b77+9yZ7vv73vv73vv70Y77+9Uw==";

var payload = jwtDecoder.Decode(jwt, secret);
```

## Custom serializers and Base64 encoders
SimpleJWT allows you to use custom serializers and Base64 encoders. There are constructor overloads for ```JwtEncoder``` as well as ```JwtDecoder```:
Implement the ```IJsonSerializer``` and ```IJsonDeserializer``` interfaces or the ```IBase64Encoder``` and ```IBase64Decoder``` interface as needed.

## Standard claims
Implement the ```IStandardClaim``` interface to automatically add a claim to the payload while encoding. The only thing required to make it work is to pass the implementation to the constructor of the ```JwtEncoder``` or register it in your dependency injection container.

**Note**: If you want to have the *exp*-claim which will be verified during decoding you have to add the ExpirationClaim to the collection of ```IStandardClaim```'s you pass to the constructor or register it in your dependency injection container.

## Breaking changes from version 1.1 to 2.0
* Dropped explicit target framework ```net452``` (It's still supported because of the ```netstandard1.4``` target framework)
* Removed parameterless contructors for ```JwtDecoder``` and ```JwtEncoder``` types.
* Updated Newtonsoft.Json to the latest version.

## Known issues 2.0.0
* ```ExpirationClaim``` is internal (instead of public). Fixed in 2.0.1.
