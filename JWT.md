resource: https://www.youtube.com/watch?v=DfqJUw-tWsM
json web tokens arised from the idea that apis end user is not the client but a web ui or mobile app that calls the api on behalf of the user
and so the api should not keep any state about the user session, so the token is self-contained and contains all the info needed to authenticate and authorize the user

what actually happens is that when user tries to login , he sends the credientals and the api server validates them and if they are valid, it generates a token that contains the user info and signs it with a secret key and sends it back to the client
after that every request the client make, he has to send the token in the authorization header and the api server validates the token and if it is valid, it allows the request to proceed
this means that in any endpoint, there must be a way to validate the token and extract the user info from it then proceed with the request

the token is usually a jwt (json web token) that contains three parts: header, payload, and signature
the header contains the type of the token and the signing algorithm
the payload contains the user info and the claims
the signature is used to verify that the token is not tampered with
the token is usually encoded in base64 and looks like this: xxxxx.yyyyy.zzzzz
where xxxxx is the header, yyyyy is the payload, and zzzzz is the signature

the signature is header + paylod + secret key
the secret key should be kept safe and not exposed to the client because the server uses it to veriify the token coming from the client
it does that by hashing the header and payload with the secret key and comparing it to the signature in the token
the token can also contain an expiration time after which it is no longer valid

#code:
in userService, generatetoken method: 

    private readonly string _jwtKey = "secretKeyforjwtauthenticationforPayWise"; // should come from config

private string GenerateToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtKey));
            var signingKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signingKey
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

and in program.cs: 
 //add authentication schema 
            builder.Services
                            .AddAuthentication(op =>op.DefaultAuthenticateScheme = "MySchema")
                            .AddJwtBearer("MySchema", option => {

                                string _jwtKey = "secretKeyforjwtauthenticationforPayWise";
                                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtKey));

                                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                                {
                                    IssuerSigningKey = key,
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                };
            }); 

//add authentication middleware

		app.UseAuthentication();
			app.UseAuthorization();


