using System;
using System.Collections.Generic;
using System.Linq;
using SprwMusic.Models;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.Repository.Impl
{
    public class SearchRepository: ISearchRepository
    {
        public IEnumerable<ArtistModel> SearchArtists(string name)
        {
            var artists = new List<ArtistModel>();
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
                    var artistList = context.SPRW_ARTIST.Where(i => i.NAME.StartsWith(name)).Take(3);
                    foreach(var artist in artistList)
                    {
                        var selectedArtist = new ArtistModel
                        {
                            AristName = artist.NAME,
                            ArtistId = artist.ARTIST_ID
                        };
                        artists.Add(selectedArtist);
                    }
                }
            }
            catch (Exception e)
            {

            }

            return artists;
        }

        public IEnumerable<AlbumModel> SearchAlbums(string name)
        {
            var albums = new List<AlbumModel>();
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
                    var albumList = context.SPRW_ALBUM.Where(i => i.NAME.StartsWith(name)).Take(3);
                    foreach (var album in albumList)
                    {
                        var selectedAlbum = new AlbumModel
                        {
//                            Artist = new ArtistModel
//                            {
//                                AristName = album.SPRW_ARTIST.NAME,
//                                ArtistId = album.ARTIST_ID,
//                                Description = album.SPRW_ARTIST.DESCRP
//                            },
                            AlbumName = album.NAME,
                            AlbumId = album.ARTIST_ID
                        };

                        albums.Add(selectedAlbum);
                    }
                }
            }
            catch (Exception e)
            {

            }

            return albums;
        }

        public IEnumerable<TrackModel> SearchTracks(string name)
        {
            var tracks = new List<TrackModel>();
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
                    var trackList = context.SPRW_TRACK.Where(i => i.NAME.StartsWith(name)).Take(3);
                    foreach (var track in trackList)
                    {
                        var selectedTrack = new TrackModel{
//                            Album = new AlbumModel
//                            {
//                                Artist = new ArtistModel
//                                {
//                                    AristName = track.SPRW_ARTIST.NAME,
//                                    ArtistId = track.ARTIST_ID,
//                                    Description = track.SPRW_ARTIST.DESCRP
//                                },
//                                AlbumName = track.SPRW_ALBUM.NAME,
//                                AlbumId = track.SPRW_ALBUM.ALBUM_ID
//                            },
                            TrackId = track.TRACK_ID,
                            TrackName = track.NAME
                        };
                        selectedTrack.TrackName = track.NAME;
                        selectedTrack.TrackId = track.TRACK_ID;
                        tracks.Add(selectedTrack);
                    }
                }
            }
            catch (Exception e)
            {

            }

            return tracks;
        }

        public SearchUserViewModel SearchUsers(string email)
        {
            var searchUserModel = new SearchUserViewModel();
            var userList = new List<UserModel>();
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
                    var users = context.SPRW_USER.Where(i => i.EMAIL.Contains(email));
                    foreach (var user in users)
                    {
                        var model = new UserModel()
                        {
                            UserEmail = user.EMAIL,
                            UserId = user.USER_ID
                        };
                        userList.Add(model);
                    }
                }
            }
            catch (Exception e)
            {
                
            }
            
            searchUserModel.Users = userList;
            return searchUserModel;
        }
    }
}