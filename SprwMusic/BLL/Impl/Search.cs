using System;
using System.Collections.Generic;
using SprwMusic.Models;
using SprwMusic.Models.ViewModels;
using SprwMusic.Repository.Impl;

namespace SprwMusic.BLL.Impl
{
    public class Search:ISearch
    {
        private readonly SearchRepository _repository;
        public Search()
        {
            _repository = new SearchRepository();
        }

        public Search(SearchRepository repo)
        {
            _repository = repo;
        }
        public SearchViewModel SearchByName(string name)
        {
            var searchViewModel = new SearchViewModel{
                Status = new StatusModel{
                    Success = true,
                }
            };
            var messages = new List<string>();

            try
            {
                searchViewModel.Artists = _repository.SearchArtists(name);
                searchViewModel.Albums = _repository.SearchAlbums(name);
                searchViewModel.Tracks = _repository.SearchTracks(name);
            }
            catch (Exception e)
            {
                searchViewModel.Status.Success = false;
                messages.Add(e.Message);
            }

            searchViewModel.Status.Messages = messages;
            return searchViewModel;
        }
    }
}