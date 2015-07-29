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
                using (var context = new SparrowMusicEntities11())
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

        public bool CreateArtistAssociation(string email, int artistId)
        {
            var success = true;
            try
            {
                using (var context = new SparrowMusicEntities11())
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
                success = false;
            }

            return success;
        }

        public bool CreateArtistAssociation(int artistId, int userId)
        {
            var success = true;
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
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
                success = false;
            }

            return success;
        }

        public CreateViewModel CreateEvent(CreateEventModel model)
        {
            var status = new CreateViewModel()
            {
                Status = new StatusModel()
                {
                    Success = true
                }
            };
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
                    var eventModel = new SPRW_ARTIST_EVENT()
                    {
                        ACT_IND = true,
                        ADDRESS = model.Address,
                        ARTIST_ID = model.ArtistId,
                        CITY = model.City,
                        DESCRP = model.Description,
                        EVENT_DATE = model.EventDate,
                        LAST_MAINT_TIME = DateTime.Now,
                        LAST_MAINT_USER_ID = model.UserEmail,
                        NAME = model.Name,
                        STATE_ABBR = model.State,
                        URL = model.Url
                    };
                    context.SPRW_ARTIST_EVENT.Add(eventModel);
                    context.SaveChanges();
                    status.Id = eventModel.EVENT_ID;
                }
            }
            catch (Exception e)
            {
                status.Status.Success = false;
            }

            return status;
        }

        public ArtistViewModel GetArtistById(int artistId)
        {
            ArtistViewModel artistModel = new ArtistViewModel();
            using (var context = new SparrowMusicEntities11())
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

                var events = new List<EventModel>();
                foreach (var e in artist.SPRW_ARTIST_EVENT)
                {
                    var model = new EventModel
                    {
                        Address = e.ADDRESS,
                        City = e.CITY,
                        State = e.STATE_ABBR,
                        Description = e.DESCRP,
                        EventDate = e.EVENT_DATE
                    };
                    events.Add(model);
                }
                artistModel.Artist.Events = events;
            }
            return artistModel;
        }

        public IEnumerable<ArtistListModel> GetArtists(string email)
        {
            var id = GetUserId(email);
            var artistList = new List<ArtistListModel>();
            try
            {
                using (var context = new SparrowMusicEntities11())
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

        public GenreViewModel GetGenres()
        {
            var model = new GenreViewModel {Genres = new List<GenreModel>()};
            using (var context = new SparrowMusicEntities11())
            {
                var genres = context.SPRW_GENRE;
                foreach (var genre in genres)
                {
                    model.Genres.Add(new GenreModel
                    {
                        Genre = genre.GENRE,
                        GenreId = genre.GENRE_ID
                    });
                }
            }
            return model;
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
                using (var context = new SparrowMusicEntities11())
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

        public StatusModel AddGenre(int artistId, int genreId)
        {
            var status = new StatusModel
            {
                Success = true
            };
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
                    var genre = context.SPRW_GENRE.FirstOrDefault(i => i.GENRE_ID == genreId);
                    var artist = context.SPRW_ARTIST.FirstOrDefault(i=>i.ARTIST_ID == artistId);
                    if (artist != null)
                        artist.SPRW_GENRE.Add(genre);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                status.Success = false;
            }
            return status;
        }

        public StatusModel RemoveGenre(int artistId, int genreId)
        {
            var status = new StatusModel
            {
                Success = true
            };
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
                    var genre = context.SPRW_GENRE.FirstOrDefault(i => i.GENRE_ID == genreId);
                    var artist = context.SPRW_ARTIST.FirstOrDefault(i => i.ARTIST_ID == artistId);
                    if (artist != null)
                        artist.SPRW_GENRE.Remove(genre);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                status.Success = false;
            }
            return status;
        }
        private int GetUserId(string email)
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