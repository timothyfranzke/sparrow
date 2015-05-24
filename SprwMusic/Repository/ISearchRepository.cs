using System;
using System.Collections.Generic;
using SprwMusic.Models;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.Repository
{
    public interface ISearchRepository
    {
        IEnumerable<ArtistModel> SearchArtists(string name);
        IEnumerable<AlbumModel> SearchAlbums(string name);
        IEnumerable<TrackModel> SearchTracks(string name);
        SearchUserViewModel SearchUsers(string email);
    }
}