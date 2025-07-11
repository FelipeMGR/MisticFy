# Repositories

In this project, we are using repositories to make it simpler to call methods in our controllers. 

## PlaylistRepository

The repository that we use to control the methods for the PlaylistController implements these methods: 

### Creating playlists

```csharp
      public async Task<ActionResult<SpotifyPlaylistDTO>> CreatePlaylistAsync(string token, [FromBody] Playlist playlist)
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

        return new SpotifyPlaylistDTO
        {
          Name = newPlayList.Name,
          Description = newPlayList.Description,
          Owner = new SpotifyUserDTO
          {
            DisplayName = newPlayList.Owner.DisplayName
          },
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

        return new SpotifyPlaylistDTO
        {
          Name = newPlayList.Name,
          Description = newPlayList.Description,
          Owner = new SpotifyUserDTO
          {
            DisplayName = newPlayList.Owner.DisplayName
          },
          Musics = []
        };
```

> Using the instance we created before for the SpotifyClient, we call the SpotifyWebAPI "Create" method, to create the playlist. This method uses 2 arguments to operate, one being the userId (that we got before), and a instance of the PlaylistCreateRequest, wich receives the playlist's name argument (that we get from the body request).

After using this method on the controller, you'll be able to create a new playlist. The return type will be a SpotifyPlaylistDTO, wich will be explained in the DTO section.

### Adding a new song to a existing playlist

```csharp
      public async Task<ActionResult<SpotifyPlaylistDTO>> AddSongToPlaylist(string token, [FromBody] List<string> uris, string playlistId)
  {
    var acessToken = token.Replace("Bearer ", "").Trim();

    var config = SpotifyClientConfig.CreateDefault().WithToken(acessToken);
    var spotify = new SpotifyClient(config);
    var updateRequest = new PlaylistAddItemsRequest(uris);

    var result = await spotify.Playlists.AddItems(playlistId, updateRequest);

    var updatedPlaylist = await spotify.Playlists.Get(playlistId);

    var musics = new List<MusicDTO>();

    if (updatedPlaylist?.Tracks?.Items != null)
    {
      foreach (var item in updatedPlaylist.Tracks.Items)
      {
        if (item.Track is FullTrack track)
        {
          musics.Add(new MusicDTO
          {
            Name = track.Name,
            Artists = track.Artists.Select(a => new SpotifyArtistDTO
            {
              Name = a.Name
            }).ToList(),
            Album = new SpotifyAlbumDTO
            {
              Name = track.Album.Name,
              Images = track.Album.Images.Select(i => new SpotifyImageDTO
              {
                Url = i.Url
              }).ToList()
            }
          });
        }
      }
    }
    return new SpotifyPlaylistDTO
    {
      Name = updatedPlaylist.Name,
      Owner = new SpotifyUserDTO
      {
        DisplayName = updatedPlaylist.Owner.DisplayName
      },
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
if (updatedPlaylist?.Tracks?.Items != null)
    {
      foreach (var item in updatedPlaylist.Tracks.Items)
      {
        if (item.Track is FullTrack track)
        {
          musics.Add(new MusicDTO
          {
            Name = track.Name,
            Artists = track.Artists.Select(a => new SpotifyArtistDTO
            {
              Name = a.Name
            }).ToList(),
            Album = new SpotifyAlbumDTO
            {
              Name = track.Album.Name,
              Images = track.Album.Images.Select(i => new SpotifyImageDTO
              {
                Url = i.Url
              }).ToList()
            }
          });
        }
      }
    }
```
Here, we're doing a validation before anything else, we are checking if there's any *null* value contained on the playlist that it's been returned. Then, if the Item is not null, we can procced to the iteration part, where we iterate through all the track itemns on the list, and add them to the __musics__ variable. In order to store songs only, we are using another validation to check if that item is a __FullTrack__ one. If it is, we add it to the list.

Every song that is been added has it's own info, such as name, artists and album. All those info are stored as well, but not everything, and that's why we use the DTO, to filter what will be visible for the user, and what will not.

```csharp
return new SpotifyPlaylistDTO
    {
      Name = updatedPlaylist.Name,
      Owner = new SpotifyUserDTO
      {
        DisplayName = updatedPlaylist.Owner.DisplayName
      },
      Musics = musics
    };
```

After getting all the playlist items, we can now return it to the user. The return type will be the SpotifyPlaylistDTO, wich will be explained in the DTO section.

### Getting the user's playlist by it's Id

It's possible to retrieve the user's playlist using the playlist id. For that, we use the following code: 

```csharp
public async Task<ActionResult<SpotifyPlaylistDetailsDTO>> GetUserPlaylistAsync(string token, string playlistId)
  {
    var accessToken = token.Replace("Bearer ", "").Trim();

    var config = SpotifyClientConfig.CreateDefault().WithToken(accessToken);
    var spotify = new SpotifyClient(config);

    var searchResult = await spotify.Playlists.Get(playlistId);

    var playlist = mapper.Map<SpotifyPlaylistDetailsDTO>(searchResult);

    return playlist;
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

var searchResult = await spotify.Playlists.Get(playlistId);

```
We use the SpotifyClient instance, then call the Playlist extension followed by the Get method. As always, this method receives one argument, wich is the playlist Id.

4. Returning the playlist

Now, after getting the user's playlist, we can return it. For that, we'll use the AutoMApper function, to map all the properties that we currently hold to a DTO, wich will filter the data that the user will be able to see.

```csharp

var playlist = mapper.Map<SpotifyPlaylistDetailsDTO>(searchResult);

return playlist;
```

## MusicRepository

> Still undergoing...