using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SprwMusic.BLL;
using SprwMusic.Models;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.Repository.Impl
{
    public class DiscoverRepository:IDiscoverRepository
    {
        public DiscoverRepository()
        {
            
        }

        public PlaylistViewModel GetPlaylist(int page)
        {
            var playlist = new List<PlaylistModel>();
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
                    var tracks = context.SPRW_TRACK.OrderBy(i => i.SPRW_TRACK_POPULAR_LIKES.Count);
                    foreach (var track in tracks)
                    {
                        var model = new PlaylistModel();
                        model.AlbumId = track.ALBUM_ID;
                        model.AlbumName = track.SPRW_ALBUM != null ? track.SPRW_ALBUM.NAME : null;
                        model.ArtistId = track.ARTIST_ID;
                        model.ArtistName = track.SPRW_ARTIST.NAME;
                        model.TrackId = track.TRACK_ID;
                        model.PopCount = track.SPRW_TRACK_POPULAR_LIKES.Count(i => i.LIKE_DATE > DateTime.Now.AddMonths(-6)) -
                                         track.SPRW_TRACK_POPULAR_DISLIKES.Count(i => i.DISLIKE_DATE > DateTime.Now.AddMonths(-6));
                        //model.PrimGenre = track.SPRW_ARTIST.SPRW_GENRE != null ? track.SPRW_ARTIST.SPRW_GENRE.GENRE : null;

                        playlist.Add(model);
                    }
                }
            }
            catch (Exception e)
            {
                
            }
            

            return new PlaylistViewModel()
            {
                Playlist = playlist
            };
        }


        private static int GetUserId(string email)
        {
            var id = -1;
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
                    var firstOrDefault = context.SPRW_USER.FirstOrDefault(i => i.EMAIL.ToLower().Equals(email.ToLower()));
                    if (firstOrDefault != null)
                        id = firstOrDefault.USER_ID;
                }
            }
            catch (Exception e)
            {

            }

            return id;
        }
    }
}