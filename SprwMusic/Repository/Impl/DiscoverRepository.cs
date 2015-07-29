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
                    var tracks =
                        context.SPRW_TRACK.OrderByDescending(i => i.SPRW_TRACK_POPULAR_LIKES.Count).ThenBy(i => i.LAST_MAINT_TIME).Take(100);
                    foreach (var track in tracks)
                    {
                        if (playlist.Exists(i=>i.ArtistName == track.SPRW_ARTIST.NAME))
                        {
                            var playlistItem = new PlaylistTrack
                            {
                                AlbumId = track.ALBUM_ID,
                                AlbumName = track.SPRW_ALBUM != null ? track.SPRW_ALBUM.NAME : null,
                                TrackId = track.TRACK_ID,
                                PopCount =
                                    track.SPRW_TRACK_POPULAR_LIKES.Count(i => i.LIKE_DATE > DateTime.Now.AddMonths(-6)) -
                                    track.SPRW_TRACK_POPULAR_DISLIKES.Count(
                                        i => i.DISLIKE_DATE > DateTime.Now.AddMonths(-6))
                            };
                            var firstOrDefault = playlist.FirstOrDefault(i=>i.ArtistName == track.SPRW_ARTIST.NAME);
                            if (firstOrDefault != null)
                                firstOrDefault.Tracks.Add(playlistItem);
                        }
                        else
                        {
                            playlist.Add(new PlaylistModel
                            {
                                ArtistId = track.ARTIST_ID,
                                ArtistName = track.SPRW_ARTIST.NAME,
                                Tracks = new List<PlaylistTrack>
                                {
                                    new PlaylistTrack
                                    {
                                        AlbumId = track.ALBUM_ID,
                                        AlbumName = track.SPRW_ALBUM != null ? track.SPRW_ALBUM.NAME : null,
                                        TrackId = track.TRACK_ID,
                                        PopCount =
                                            track.SPRW_TRACK_POPULAR_LIKES.Count(i => i.LIKE_DATE > DateTime.Now.AddMonths(-6)) -
                                            track.SPRW_TRACK_POPULAR_DISLIKES.Count(
                                                i => i.DISLIKE_DATE > DateTime.Now.AddMonths(-6))
                                    }}
                                });
                                
                        }
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

        public EventViewModel GetEvents(string email)
        {
            return new EventViewModel();
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