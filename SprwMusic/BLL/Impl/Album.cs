using System;
using System.Collections.Generic;
using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;
using SprwMusic.Repository.Impl;
using SprwMusic.Utils;

namespace SprwMusic.BLL.Impl
{
    public class Album: IAlbum
    {
        private readonly AlbumRepository _repository;
        public Album()
        {
            _repository = new AlbumRepository();
        }

        public Album(AlbumRepository albumRepo)
        {
            _repository = albumRepo;
        }
        public CreateViewModel CreateAlbum(CreateAlbumModel model)
        {
            var status = _repository.CreateAlbum(model);
            if (status.Status.Success)
            {
                var fileStat = CreateAlbumDirectory(model.ArtistId, status.Id);
                if (!fileStat.Success)
                {
                    _repository.DeleteAlbum(status.Id);
                    status.Status.Success = false;
                    status.Id = 0;
                }
            }
            return status;
        }

        public bool CreateAlbumImg(CreateImgModel model)
        {
            return CreateAlbumImgDirectory(model);
        }
        
        private StatusModel CreateAlbumImage(CreateAlbumImageModel model, int imgId)
        {
            var status = new StatusModel
            {
                Success = true
            };
            var dir = "/artists/" + model.ArtistId + "/albums/" + model.AlbumId + "/img/" ;

            try
            {
                status.Success = FileUtil.CreateFile(dir, model.AlbumImage, imgId);
            }
            catch (Exception e)
            {
                status.Success = false;
            }

            return status;
        }

        public byte[] GetFile(int? artistId, int? albumId)
        {
            return FileUtil.GetImgFile(artistId, albumId, null);
        }

        public AlbumViewModel GetAlbums(int artistId)
        {
            return _repository.GetAlbum(artistId);
        }

        private StatusModel CreateAlbumDirectory(int artistId, int albumId)
        {
            var status = new StatusModel 
            {
                Success = true
            };
            var dir = "/artists/" + artistId + "/albums/" + albumId;
            var imgDir = dir + "/imgs";
            var trackDir = dir + "/tracks";

            try
            {
                status.Success = FileUtil.CreateDirectory(dir);
                status.Success = FileUtil.CreateDirectory(imgDir);
                status.Success = FileUtil.CreateDirectory(trackDir);
            }
            catch (Exception e)
            {
                status.Success = false;
            }

            return status;
        }

        private bool CreateAlbumImgDirectory(CreateImgModel model)
        {
            var success = true;
            var artistId = _repository.GetArtistId(model.TrackingId);
            var albums = String.Empty;
            if (model.TrackingId < 0)
            {
                albums = "/albums/singles";
            }
            else
            {
                albums = "/albums/" + model.TrackingId;
            }
            var dir = "/artists/" + artistId + albums + "/img/";

            var status = new StatusModel
            {
                Messages = new List<string>()
            };
            try
            {
                success = FileUtil.CreateImgFile(dir, model.AlbumImage, model.TrackingId);
            }
            catch (Exception e)
            {
                success = false;
            }

            return success;
        }
    }
}