<h1>Controllers</h1>

<p>We have some controllers in our project, some of them wich work by the SpotifyAPI standards. We'll explain how they
  funcion, and how their implementation is made, so stay with us!</p>

<ol>
  <ul>
    <h1>The Login</h1>

    <p>In order to authorize the user to use the SpotifyAPI endpoints, first we need to do a quick authorization (using
      to SpotifyAPI own authorize logic), so we can use the API itself. Here's how it works</p>

    <li> Users is requested to log with their spotify account</li>

    <pre><code class="language-csharp">

[HttpGet("authorize")]
    public IActionResult Authorize()
    {
        var request = new LoginRequest(new Uri(_redirectUri), _clientId, LoginRequest.ResponseType.Code)
        {
            Scope = [Scopes.UserReadPrivate, Scopes.UserReadEmail, Scopes.PlaylistModifyPublic, Scopes.PlaylistModifyPrivate]
        };
        var uri = request.ToUri();
        return Redirect(uri.ToString());
    } 

</code>
</pre>

    <li> After loging with their account, they will be redirected to the callback endpoint</li>

    <pre><code class="language-csharp">
 [HttpGet("callback")]
    public async Task<IActionResult> Callback(string code)
    {
        try
        {
            var tokenResponse = await new OAuthClient().RequestToken(
                new AuthorizationCodeTokenRequest(_clientId, _clientSecret, code, new Uri(_redirectUri))
            );

            var spotify = new SpotifyClient(tokenResponse.AccessToken);
            var spotifyProfile = await spotify.UserProfile.Current();

            var user = await _userService.FindOrCreateUserAsync(
                spotifyProfile.Id,
                spotifyProfile.DisplayName,
                spotifyProfile.Email,
                tokenResponse.AccessToken,
                tokenResponse.RefreshToken,
                tokenResponse.ExpiresIn
            );

            var claims = new List<Claim>
            {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.AuthorizationDecision, user.AccessToken)
            };

            var jwtToken = _token.GenerateAccessToken(claims, configuration);
            Console.WriteLine($"Logged in as: {spotifyProfile.DisplayName}");
            return Ok(new { Token = jwtToken });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

</code></pre>
    <li> The user will receive a authorization token, wich will be used to access all the other endpoints in the
      aplication, as well as his acess claims.</li>
  </ul>

  <ul>
    <h1>The Playlist Controller</h1>

    <p>The user can create, update and delete any of his personal playlists through these controllers</p>

    <h2>GetPlaylistAsyc Endpoint</h2>
    <li> Gets the user's playlist based on it's ID.</li>

    <pre><code class="language-csharp"></code></pre>
  </ul>
</ol>