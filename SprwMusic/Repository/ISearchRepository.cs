using System;
using System.Collections.Generic;
using SprwMusic.Models;

namespace SprwMusic.Repository
{
    public interface ISearchRepository
    {
        IEnumerable<ArtistModel> SearchArtists(string name);
        IEnumerable<AlbumModel> SearchAlbums(string name);
        IEnumerable<TrackModel> SearchTracks(string name);
    }
}