# Models

We use some models in order to padronize how the entities should be in our app.

## User model

```csharp
      public class Users
      {
        public int Id { get; set; }
        public string SpotifyUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiresAt { get; set; }
        public List<Playlist> Playlists { get; set; }
        public List<string> LikedMusics { get; set; }
      }
```

The user model specifies that every user **must** have those properties. The priority should be: 

1. Id - Given automatically when user is authenticaded
2. AcessToken - We use this to authenticate the user to every endpoint in the app
3. RefreshToken - Assumes when the AcessToken expires
4. SpotifyUserId (this Id is used to identify the user in most endpoints)
5. Name
6. Email

> Playlists and LikedMusics can be null at first.

## Playlist model

```csharp
      public class Playlist
      {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public List<Music> Musics { get; set; }
      }
```

Here we have way less mandatory properties. The ones that should always be informed are:

1. Id - Given automatically when playlist is created
2. Name
3. Description - Can be null
4. IsPublic - Specifies if other users can acess your playlist
5. Musics - An empty array of the playlist's musics (it's initialized as null)

## Musics model

```csharp
      public class Music
      {
        public int MusicId { get; set; }
        public string MusicName { get; set; }
      }
```

And finally, the model that represents the songs. We only have 2 properties here, the most importante being the **MusicId**.