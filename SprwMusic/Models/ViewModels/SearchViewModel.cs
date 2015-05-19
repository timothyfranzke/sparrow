using System.Collections.Generic;

namespace SprwMusic.Models.ViewModels
{
    public class SearchViewModel
    {
        public StatusModel Status { get; set; }
        public IEnumerable<ArtistModel> Artists { get; set; }
        public IEnumerable<AlbumModel> Albums { get; set; }
        public IEnumerable<TrackModel> Tracks { get; set; }
    }
}