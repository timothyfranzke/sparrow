using System;
using System.Collections.Generic;
using SprwMusic.Models;
using SprwMusic.Models.AuthModels;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;
using SprwMusic.Repository.Impl;
using SprwMusic.Utils;

namespace SprwMusic.BLL.Impl
{
    public class Artist : IArtist
    {
        private readonly ArtistRepository _repository;
        public Artist()
        {
            _repository = new ArtistRepository();
        }

        public Artist(ArtistRepository repo)
        {
            _repository = repo;
        }

        public CreateViewModel CreateArtist(CreateArtistModel model)
        {
            var messages = new List<string>();
            var status = new CreateViewModel();

            try
            {
                status = _repository.CreateArtist(model);
                
                if (status.Status.Success)
                {
                    var stat = _repository.CreateArtistAssociation(model.UserEmail, status.Id);
                    var dirStatus = CreateArtistDirectory(status.Id);
                    if (!dirStatus.Success)
                    {
                        status.Status.Success = false;
                        messages.Add("Failed to create artist directory");
                    }
                }
            }
            catch(Exception e)
            {
                status.Status.Success = false;
                status.Id = -1;
                messages.Add("Exception: " + e.Message);
                status.Status.Messages = messages;
            }

            return status;
        }

        public IEnumerable<ArtistListModel> GetArtists(string email)
        {
            var artists = _repository.GetArtists(email);

            return artists;
        }

        public ArtistViewModel GetArtistById(int artistId)
        {
            var artist = _repository.GetArtistById(artistId);

            return artist;
        }

        private StatusModel CreateArtistDirectory(int id)
        {
            var dir = "/artists/" + id;
            var dirAlbum = dir + "/albums";
            var dirImage = dir + "/imgs";
            var dirSingle = dirAlbum+ "/singles";
            
            var status = new StatusModel
            {
                Success = true,
                Messages = new List<string>()
            };
            try
            {
                FileUtil.CreateDirectory(dir);
                FileUtil.CreateDirectory(dirAlbum);
                FileUtil.CreateDirectory(dirImage);
                FileUtil.CreateDirectory(dirSingle);
            }
            catch(Exception e)
            {
                status.Success = false;
            }

            return status;
        }
    }
}