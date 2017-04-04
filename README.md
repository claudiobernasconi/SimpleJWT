# SimpleJWT
The most simplistic .NET Json Web Token (JWT) Library.

# Usage
## Encode
```csharp
var jwtEncoder = new JwtEncoder(new NewtonsoftJsonSerializer(), new Base64Encoder());

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
