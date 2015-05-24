using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;


namespace SprwMusic.Repository.Impl
{
    public class TrackRepository: ITrackRepository
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CreateViewModel CreateTrack(CreateTrackModel model)
        {
            var messages = new List<string>();
            var status = new CreateViewModel
            {
                Status = new StatusModel
                {
                     Success = true
                }
            };

            DateTime relaseDate;
            if (model.ReleaseDate == null)
            {
                relaseDate = DateTime.Now;
            }
            else
            {
                relaseDate = (DateTime)model.ReleaseDate;
            }
            try
            {
                log.Info("method : CreateTrack | action : starting db connections");
                using (var context = new SparrowMusicEntities11())
                {
                    var album = new SPRW_TRACK()
                    {
                        ARTIST_ID = model.ArtistId,
                        ALBUM_ID = model.AlbumId,
                        ACT_IND = true,
                        RELEASE_DATE = relaseDate,
                        NAME = model.TrackName,
                        DESCRP = model.Description,
                        LAST_MAINT_TIME = DateTime.Now,
                        LAST_MAINT_USER_ID = model.UserEmail
                    };
                    context.SPRW_TRACK.Add(album);
                    context.SaveChanges();

                    status.Id = album.TRACK_ID;
                    messages.Add("Successfully added track");
                }
            }
            catch (Exception e)
            {
                status.Status.Success = false;
                log.Error("method : CreateTrack | exception : " + e.Message);
            }
            return status;
        }

        public StatusModel DeleteTrack(int id)
        {
            var messages = new List<string>();
            var status = new StatusModel
            {
                Success = true
            };
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
                    var track = context.SPRW_TRACK.FirstOrDefault(i => i.TRACK_ID == id);
                    context.SPRW_TRACK.Remove(track);
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

        public TrackPathModel GetTrackPath(int trackId)
        {
            var model = new TrackPathModel();
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
                    var track = context.SPRW_TRACK.FirstOrDefault(i => i.TRACK_ID == trackId);
                    model.AlbumId = track.ALBUM_ID;
                    model.ArtistId = track.ARTIST_ID;
                    model.TrackId = trackId;
                }
            }
            catch (Exception e)
            {
                
            }

            return model;
        }
        public bool AddTrackPopularityLike(CreateTrackPopularModel model)
        {
            var success = true;
            try
            {
                log.Info("method : AddTrackPopularityLike | action : starting db connection | message : liking track " + model.TrackId);
                using (var context = new SparrowMusicEntities11())
                {

                    var like = new SPRW_TRACK_POPULAR_LIKES()
                    {
                        LIKE_DATE = DateTime.Now,
                        TRACK_ID = model.TrackId,
                        USER_ID = model.UserId
                    };
                    context.SPRW_TRACK_POPULAR_LIKES.Add(like);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                success = false;
                log.Error("method : AddTrackPopularityLike | exception : " + e.Message);
            }
            return success;
        }

        public bool AddTrackPopularityDislike(CreateTrackPopularModel model)
        {
            var success = true;
            try
            {
                using (var context = new SparrowMusicEntities11())
                {

                    var dislike = new SPRW_TRACK_POPULAR_DISLIKES()
                    {
                        DISLIKE_DATE = DateTime.Now,
                        TRACK_ID = model.TrackId,
                        USER_ID = model.UserId
                    };
                    context.SPRW_TRACK_POPULAR_DISLIKES.Add(dislike);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                success = false;
                log.Error("method : AddTrackPopularityDislike | exception : " + e.Message);
            }
            return success;
        }

        public bool AddTrackPopularityPlayThrough(CreateTrackPopularModel model)
        {
            var success = true;
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
                    var playThrough = new SPRW_TRACK_POPULAR_PLAY_THROUGH
                    {
                        PLAY_DATE = DateTime.Now,
                        TRACK_ID = model.TrackId
                    };
                    context.SPRW_TRACK_POPULAR_PLAY_THROUGH.Add(playThrough);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                success = false;
                log.Error("method : AddTrackPopularityPlayThrough | exception : " + e.Message);
            }
            return success;
        }
    }
}