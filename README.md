# SimpleJWT
The most simplistic .NET Json Web Token (JWT) Library.

## Target Frameworks
* .NET Standard 1.4
* .NET Framework 4.5.2

## NuGet
* Package available on [NuGet.org](https://www.nuget.org/packages/SimpleJWT/)

## Features
* Encodes and decodes Json Web Tokens (JWT)
* Verifies the signature while decoding
* Generates an exp-Claim per default which is validated while decoding
* Allows the usage of custom serializers and Base64 encoders
* Allows to provide standard claims which will always be added to the payload

# Usage
## Encode
```csharp
var jwtEncoder = new JwtEncoder();

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
var jwtDecoder = new JwtDecoder();

const string secret = "secret";
const string jwt = @"eyJUeXAiOiJKV1QiLCJBbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibm
FtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9.du+/ve+/vUNj77+977+9Tu+/vdSmUzjvv71UBnFMbe+/vQTvv
71b77+9yZ7vv73vv73vv70Y77+9Uw==";

var payload = jwtDecoder.Decode(jwt, secret);
```

## Custom serializers and Base64 encoders
SimpleJWT allows you to use custom serializers and Base64 encoders. There are constructor overloads for ```JwtEncoder``` as well as ```JwtDecoder```:

```csharp
var jwtEncoder = new JwtEncoder(new NewtonsoftJsonSerializer(), new Base64Encoder(), new List<IStandardClaim>());
var jwtDecoder = new JwtDecoder(new NewtonsoftJsonSerializer(), new Base64Encoder(), new Base64Encoder());
```
Implement the ```IJsonSerializer``` and ```IJsonDeserializer``` interfaces or the ```IBase64Encoder``` and ```IBase64Decoder``` interface as needed.

## Standard claims
Implement the ```IStandardClaim``` interface to automatically add a claim to the payload while encoding. The only thing required to make it work is to pass the implementation to the constructor of the ```JwtEncoder```.

**Note**: If you want to have the *exp*-claim which will be verified during decoding you have to add the ExpirationClaim to the collection of ```IStandardClaim```'s you pass to the constructor.