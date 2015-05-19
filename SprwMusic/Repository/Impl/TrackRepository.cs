using System;
using System.Collections.Generic;
using System.Linq;
using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;


namespace SprwMusic.Repository.Impl
{
    public class TrackRepository: ITrackRepository
    {
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
                using (var context = new SparrowMusicEntities())
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
                messages.Add("Exception: " + e);
            }

            status.Status.Messages = messages;
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
                using (var context = new SparrowMusicEntities())
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

        public void PopularPlayThrough(int id)
        {
            
            try
            {
                using (var context = new SparrowMusicEntities())
                {
                    var model = new SPRW_TRACK_POPULAR_PLAY_THROUGH
                    {
                        PLAY_DATE = DateTime.Now,
                        TRACK_ID = id
                    };
                    context.SPRW_TRACK_POPULAR_PLAY_THROUGH.Add(model);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                
            }
        }
    }
}