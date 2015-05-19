using System;
using System.Collections.Generic;
using System.Linq;
using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;
using SprwMusic.Repository;

namespace SprwMusic.Repository.Impl
{
    public class ArtistRepository: IArtistRepository
    {
        public CreateViewModel CreateArtist(CreateArtistModel model)
        {
            var messages = new List<string>();
            var status = new CreateViewModel()
            {
                Status = new StatusModel
                {
                    Success = true
                }
            };
            try
            {
                using (var context = new SparrowMusicEntities())
                {
                    var userId = GetUserId(model.UserEmail);
                    var artist = new SPRW_ARTIST()
                    {
                        ACT_IND = true,
                        NAME = model.Name,
                        DESCRP = model.Description,
                        LAST_MAINT_TIME = DateTime.Now,
                        LAST_MAINT_USER_ID = model.UserEmail
                    };

                    context.SPRW_ARTIST.Add(artist);
                    context.SaveChanges();
                    var artistId = artist.ARTIST_ID;
                    status.Id = artistId;
                }
            }
            catch(Exception e)
            {
                status.Status.Success = false;
                messages.Add("Exception: " + e);
            }

            status.Status.Messages = messages;
            return status;
        }

        public StatusModel CreateArtistAssociation(string email, int artistId)
        {
            var messages = new List<string>();
            var status = new StatusModel()
            {
                Success = true,
                Messages = new List<string>()
            };
            try
            {
                using (var context = new SparrowMusicEntities())
                {
                    var userId = GetUserId(email);
                    var artistMember = new SPRW_ARTIST_MEMBER
                    {
                        ROLE_ID = 1,
                        ACT_IND = true,
                        ARTIST_ID = artistId,
                        LAST_MAINT_TIME = DateTime.Now,
                        LAST_MAINT_USER_ID = userId,
                        USER_ID = userId
                    };
                    context.SPRW_ARTIST_MEMBER.Add(artistMember);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                status.Success = false;
                messages.Add("Exception: " + e);
            }

            status.Messages = messages;
            return status;
        }

        public ArtistViewModel GetArtistById(int artistId)
        {
            ArtistViewModel artistModel = new ArtistViewModel();
            using (var context = new SparrowMusicEntities())
            {
                var artist = context.SPRW_ARTIST.FirstOrDefault(i => i.ARTIST_ID == artistId);
                artistModel.Artist = new ArtistModel()
                {
                    ArtistId = artist.ARTIST_ID,
                    AristName = artist.NAME,
                    Description = artist.DESCRP,
                };
                var albums = new List<AlbumModel>();
                foreach (var album in artist.SPRW_ALBUM)
                {
                    var albumModel = new AlbumModel()
                    {
                        AlbumId = album.ALBUM_ID,
                        AlbumName = album.NAME,

                    };
                    var tracks = new List<TrackModel>();
                    foreach (var track in album.SPRW_TRACK)
                    {
                        var trackModel = new TrackModel()
                        {
                            TrackId = track.TRACK_ID,
                            TrackName = track.NAME
                        };
                        tracks.Add(trackModel);
                    }
                    albumModel.Tracks = tracks;
                    albums.Add(albumModel);
                }
                artistModel.Artist.Albums = albums;
            }
            return artistModel;
        }

        public IEnumerable<ArtistListModel> GetArtists(string email)
        {
            var id = GetUserId(email);
            var artistList = new List<ArtistListModel>();
            try
            {
                using (var context = new SparrowMusicEntities())
                {
                    var artists = context.SPRW_ARTIST_MEMBER.Where(i => i.USER_ID == id).Select(i => i.SPRW_ARTIST).ToList();
                    

                    foreach (var artist in artists)
                    {
                        var artistItem = new ArtistListModel();
                        artistItem.ArtistId = artist.ARTIST_ID;
                        artistItem.ArtistName = artist.NAME;
                        artistList.Add(artistItem);
                    }
                }
            }
            catch (Exception e)
            {

            }

            return artistList;
        }

        public IEnumerable<SPRW_ARTIST> GetArtistsByName(string name)
        {
            var messages = new List<string>();
            var status = new StatusModel()
            {
                Success = true
            };
            var artists = new List<SPRW_ARTIST>();
            try
            {
                using (var context = new SparrowMusicEntities())
                {
                    artists = context.SPRW_ARTIST.Where(i => i.NAME.StartsWith(name)).ToList();
                }
            }
            catch (Exception e)
            {
                status.Success = false;
                messages.Add("Exception: " + e);
            }
            status.Messages = messages;

            return artists;
        }
        private static int GetUserId(string email)
        {
            var id = -1;
            try
            {
                using (var context = new SparrowMusicEntities())
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