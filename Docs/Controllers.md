# Controllers

> We have some controllers in our project, some of which work by the SpotifyAPI standards. We'll explain how they function and how their implementation is made, so stay with us!

## The Login

> To authorize the user to use SpotifyAPI endpoints, we first need to perform a quick authorization (using SpotifyAPI's own logic). Here's how it works

* User is requested to log in with their Spotify account:

```csharp  
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
```

* After logging in, they will be redirected to the callback endpoint:

```csharp
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
```

> The user receives an authorization token to access other endpoints, as well as his acess claims.

## The Playlists

* Here the user is able to create, update and delete any of his personal playlists.

### Obtaining a playlist based on ID

> Most of the method used on these endpoints are being called from the PlaylistRepository, wich will be presented later on.

```csharp
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
```

> With this endpoint we're able to return the user's informed playlist (based on the ID that was given in the URL).

### Playlist Creation

* Users can also create their own playlist.

```csharp
[HttpPost("createPlaylist")]
        [Authorize]
        public async Task<ActionResult> CreatePlaylistAsync([FromBody] Playlist playlist)
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

            var userPlaylist = await _playlist.CreatePlaylistAsync(user.AccessToken, playlist);

            return Ok(userPlaylist);
        }
```

> Here we receive, from the body of the request, the name, description and songs (empty) to create the new playlist.

### Playlist update

* Users can add new songs to their playlist

```csharp
[HttpPost("{playlistId}")]
        [Authorize]
        public async Task<ActionResult<Playlist>> UpdatePlaylist([FromBody] List<string> uris, string playlistId)
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

            var playlist = await _playlist.AddSongToPlaylist(user.AccessToken, uris, playlistId);
            return Ok(playlist);
        }
```

> Here we receive the songs the user wants to add to the playlist (their spotify id, wich would be something like: "spotify:track:7MXVkk9YMctZqd1Srtv4MB"), and the playlist ID that they want to update.

#### Important

> _every endpoint check for any nullable value for both the user identification and their acess token. If one of them is null/invalid, a excpetion is throwed._