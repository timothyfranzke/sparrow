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

        public CreateViewModel CreateAlbumImage(CreateAlbumImageModel model)
        {
            var dbStat = _repository.CreateAlbumImage(model);
            if (dbStat.Status.Success)
            {
                var fileStat = CreateAlbumImage(model, dbStat.Id);
                if (!fileStat.Success)
                {
                    _repository.DeleteAlbumImage(dbStat.Id);
                }
            }
            return new CreateViewModel();
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
    }
}