using System;
using System.Collections.Generic;
using System.Linq;
using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;


namespace SprwMusic.Repository.Impl
{
    public class AlbumRepository:IAlbumRepository
    {

        public AlbumRepository()
        {

        }
        public CreateViewModel CreateAlbum(CreateAlbumModel model)
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
                    var album = new SPRW_ALBUM()
                    {
                        ARTIST_ID = model.ArtistId,
                        RELEASE_DATE = model.ReleaseDate,
                        NAME = model.AlbumName,
                        DESCRP = model.Description,
                        LAST_MAINT_TIME = DateTime.Now,
                        LAST_MAINT_USER_ID = ""
                    };
                    context.SPRW_ALBUM.Add(album);
                    context.SaveChanges();

                    status.Id = album.ALBUM_ID;
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

        public CreateViewModel CreateAlbumImage(CreateAlbumImageModel model)
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
                    var albumImage = new SPRW_ALBUM_IMG
                    {
                        ACT_IND = true,
                        ALBUM_ID = model.AlbumId,
                        DESCRP = model.Description,
                        LAST_MAINT_TIME = DateTime.Now,
                        LAST_MAINT_USER_ID = model.UserId
                    };
                    context.SPRW_ALBUM_IMG.Add(albumImage);
                    context.SaveChanges();
                    status.Id = albumImage.IMG_ID;
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

        public int GetArtistId(int albumId)
        {
            int artistId = -1;
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
                    artistId = context.SPRW_ALBUM.FirstOrDefault(i => i.ALBUM_ID == albumId).ARTIST_ID;
                }
            }
            catch (Exception e)
            {
                
            }
            return artistId;
        }

        public StatusModel DeleteAlbum(int id)
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
                    var album = context.SPRW_ALBUM.FirstOrDefault(i => i.ALBUM_ID == id);
                    context.SPRW_ALBUM.Remove(album);
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

        public StatusModel DeleteAlbumImage(int id)
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
                    var album = context.SPRW_ALBUM_IMG.FirstOrDefault(i => i.IMG_ID == id);
                    context.SPRW_ALBUM_IMG.Remove(album);
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
    }
}