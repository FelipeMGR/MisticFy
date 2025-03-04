# Repositories

In this project, we are using repositories to make it simpler to call methods in our controllers. 

## PlaylistRepository

The repository that we use to control the methods for the PlaylistController implements these methods: 

### Creating playlists

```csharp
      public async Task<ActionResult<Playlist>> CreatePlaylistAsync(string token, [FromBody] Playlist playlist)
        {
          var acessToken = token.Replace("Bearer ", "");

          var config = SpotifyClientConfig.CreateDefault();
          var spotify = new SpotifyClient(config.WithToken(acessToken));

          var currentUser = await spotify.UserProfile.Current();
          string userId = currentUser.Id;

          var newPlayList = await spotify.Playlists.Create(userId, new PlaylistCreateRequest(playlist.Name)
          {

            Description = playlist.Description,
            Public = playlist.IsPublic
          });

          return new Playlist
          {
            Name = newPlayList.Name,
            Description = newPlayList.Description,
            Musics = []
          };
        }
```

Here we take two arguments, one being the token (AccessToken) and, on the body, we receive the info that should be present on the new playlist.

#### Step-by-step

1. Getting the token
```csharp 
 var acessToken = token.Replace("Bearer ", "");
  var config = SpotifyClientConfig.CreateDefault();
    var spotify = new SpotifyClient(config.WithToken(acessToken));
```
> This token is validated on the controller.

2. Getting the SpotifyClient

```csharp
  var config = SpotifyClientConfig.CreateDefault();
    var spotify = new SpotifyClient(config.WithToken(acessToken));
```
> The token is used to create a instance of SpotifyClient, wich is "stored" in the __spotify__ variable. This instance is used to manage playlist, and user related actions.

3. Getting the user ID

```csharp
  var currentUser = await spotify.UserProfile.Current();
   string userId = currentUser.Id;
```

> The current user id is stored in the __userId__ variable. This must be done in order to get the current user profile, so we don't create/update/delete the wrong playlist.

4. Creating and returning the new playlist

```csharp
var newPlayList = await spotify.Playlists.Create(userId, new PlaylistCreateRequest(playlist.Name)
          {
            Description = playlist.Description,
            Public = playlist.IsPublic
          });

          return new Playlist
          {
            Name = newPlayList.Name,
            Description = newPlayList.Description,
            Musics = []
          };
```

> Using the instance we created before for the SpotifyClient, we call the SpotifyWebAPI "Create" method, to create the playlist. This method uses 2 arguments to operate, one being the userId (that we got before), and a instance of the PlaylistCreateRequest, wich receives the playlist's name argument (that we get from the body request).

After using this method on the controller, you'll be able to create a new playlist.

### Adding a new song to a existing playlist

```csharp
      public async Task<ActionResult<Playlist>> AddSongToPlaylist(string token, [FromBody] List<string> uris, string playlistId)
        {
          var acessToken = token.Replace("Bearer ", "").Trim();

          var config = SpotifyClientConfig.CreateDefault().WithToken(acessToken);
          var spotify = new SpotifyClient(config);
          var updateRequest = new PlaylistAddItemsRequest(uris);

          var result = await spotify.Playlists.AddItems(playlistId, updateRequest);

          var updatedPlaylist = await spotify.Playlists.Get(playlistId);

          var musics = updatedPlaylist?.Tracks?.Items
                          .Where(item => item.Track is FullTrack)
                          .Select(m => new Music
                          {
                            MusicName = ((FullTrack)m.Track).Name
                          })
                          .ToList();


          return new Playlist
          {
            Name = updatedPlaylist.Name,
            Musics = musics
          };
        }
```

In this method, we take 3 arguments, wich are: token (AcessToken), uris (the song's URI's) and the playlistId.

#### Step-by-Step

1. Getting the token
```csharp 
 var acessToken = token.Replace("Bearer ", "");
  var config = SpotifyClientConfig.CreateDefault();
    var spotify = new SpotifyClient(config.WithToken(acessToken));
```

2. Getting the SpotifyClient

```csharp
  var config = SpotifyClientConfig.CreateDefault();
    var spotify = new SpotifyClient(config.WithToken(acessToken));
```
> The token is used to create a instance of SpotifyClient, wich is "stored" in the __spotify__ variable. This instance is used to manage playlist, and user related actions.

3. Getting a instance of __PlaylistAddItemsRequest__

```csharp
var updateRequest = new PlaylistAddItemsRequest(uris);
```

In order to update the playlist, we need to create a instance of this class and add the uris that we got from the body.

4. Getting the request result and the update playlist

```csharp
var result = await spotify.Playlists.AddItems(playlistId, updateRequest);
var updatedPlaylist = await spotify.Playlists.Get(playlistId);
```

After creating the instance of __PlaylistAddItemsRequest__, we call the SpotifyClient instance, then we call the Playlist extension followed by the __AddItems__ method, wich receives the playlistId (the Id of the playlist you want to update), and the instance of the update request. Then, after updating the playlist, we use the "Get" method to return the updated playlist, and store it on the updatedPlaylist variable.

5. Returning the updated playlist

Finally, after updating the playlist, we can return it. In order to return the playlists, we need to get the songs contained in it. For that, we use the following block of code: 

```csharp
var musics = updatedPlaylist?.Tracks?.Items
                          .Where(item => item.Track is FullTrack)
                          .Select(m => new Music
                          {
                            MusicName = ((FullTrack)m.Track).Name
                          })
                          .ToList();
```
Here, we're using the updatedPlaylist variable, then getting the tracks contained in it and finally the items. We filter it to just get the items that are __FullTrack__* type. For every __FullTrack__ type that we encounter, a new instance of the Music model will be created.

```csharp
return new Playlist
          {
            Name = updatedPlaylist.Name,
            Musics = musics
          };
```

After getting all the playlist items, we can now return it to the user. Here we're returning the playlist's name, and a list of Musics.

### Getting the user's playlist by it's Id

It's possible to retrieve the user's playlist using the playlist id. For that, we use the following code: 

```csharp
public async Task<ActionResult<Playlist>> GetUserPlaylistAsync(string token, string playlistId)
  {
    var acessToken = token.Replace("Bearer ", "").Trim();

    var config = SpotifyClientConfig.CreateDefault().WithToken(acessToken);
    var client = new SpotifyClient(config);

    var playlist = await client.Playlists.Get(playlistId);

    List<Music> playlistMusics = new List<Music>();

    if (playlist?.Tracks?.Items != null)
    {
      foreach (var item in playlist.Tracks.Items)
      {
        if (item.Track is FullTrack track)
        {
          playlistMusics.Add(new Music
          {
            MusicName = track.Name
          });
        }
      }
    }

    return new Playlist
    {
      Name = playlist?.Name,
      Description = playlist?.Description,
      Musics = playlistMusics
    };
  }
```
#### Step-by-Step
1. Getting the token
```csharp 
 var acessToken = token.Replace("Bearer ", "");
  var config = SpotifyClientConfig.CreateDefault();
    var spotify = new SpotifyClient(config.WithToken(acessToken));
```

2. Getting the SpotifyClient

```csharp
  var config = SpotifyClientConfig.CreateDefault();
    var spotify = new SpotifyClient(config.WithToken(acessToken));
```
> The token is used to create a instance of SpotifyClient, wich is "stored" in the __spotify__ variable. This instance is used to manage playlist, and user related actions.

3. Getting the playlist

In order to fetch the playlist, we use the following line of code: 
```csharp
var playlist = await spotify.Playlists.Get(playlistId);
```

We use the SpotifyClient instance, then call the Playlist extension followed by the Get method. As always, this method receives one argument, wich is the playlist Id.

4. Instanciating the playlist

To be able to return the playlist to the user, we need to store it somewhere first. To do that, we can instantiate a new List (Playlists, as the same suggests, is a List of songs) and add the playlist songs to it. The logic for that would be the following:

```csharp
List<Music> playlistMusics = new List<Music>();

```
First, we create the new List, wich is empty. After that, we iterate through the playlist we got with the Get method

```csharp
    if (playlist?.Tracks?.Items != null) // verifies if there's any null value in the array
    {
      foreach (var item in playlist.Tracks.Items)
      {
        if (item.Track is FullTrack track)
        {
          playlistMusics.Add(new Music
          {
            MusicName = track.Name // for every FullTrack type item, we'll add it to our playlistMusic variable
          });
        }
      }
    }
```
5. Returning the playlist

Finaly, after iterating through the array, we can now return the playlist for our user.

```csharp
return new Playlist
    {
      Name = playlist?.Name,
      Description = playlist?.Description,
      Musics = playlistMusics
    };
```
##### Important: we do not return the playlist with Musics = playlist.??? because there's not a method that returns all the playlist songs within a FullPlaylist type variable.

## MusicRepository

> Still undergoing...