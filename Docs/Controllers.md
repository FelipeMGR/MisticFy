<h1>Controllers</h1>

<p>We have some controllers in our project, some of which work by the SpotifyAPI standards. We'll explain how they
  function and how their implementation is made, so stay with us!</p>

<ol>
  <!-- Login Section -->
  <li>
    <h2>The Login</h2>
    <p>To authorize the user to use SpotifyAPI endpoints, we first need to perform a quick authorization (using
      SpotifyAPI's own logic). Here's how it works:</p>

    <ol>
      <li>User is requested to log in with their Spotify account:</li>
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
        </code></pre>

      <li>After logging in, they will be redirected to the callback endpoint:</li>
      <pre>
          <code class="language-csharp">
            [HttpGet("callback")]
            public async Task<IActionResult> Callback(string code)
            {
                try
                {
                    var tokenResponse = await new OAuthClient().RequestToken(
                        new AuthorizationCodeTokenRequest(_clientId, _clientSecret, code, new Uri(_redirectUri))
                    );

                    // ... (rest of the code)
                    return Ok(new { Token = jwtToken });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Error = ex.Message });
                }
            }
          </code>
      </pre>
      <li>The user receives an authorization token to access other endpoints.</li>
  </li>
  <!-- Playlist Controller Section -->
  <li>
    <h2>The Playlist Controller</h2>
    <p>The user can create, update, and delete personal playlists through these controllers.</p>

    <h3>GetPlaylistAsync Endpoint</h3>
    <p>Gets the user's playlist based on its ID.</p>
    <pre><code class="language-csharp">
      [HttpGet("{userPlaylist}")]
        [Authorize]
        public async Task<ActionResult> GetPlaylistAsyc([FromRoute] string userPlaylist)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }

            var user = await _userService.GetUserByIdAsync(int.Parse(userId));
            if (user == null || string.IsNullOrEmpty(user.AccessToken))
            {
                return Unauthorized("User not found or access token missing.");
            }

            var playlist = await _playlist.GetUserPlaylistAsync(user.AccessToken, userPlaylist);
            return Ok(playlist);
        }
    </code></pre>
  </li>
</ol>