﻿using AutoMapper;
using Tournament.Entities;
using Tournament.ViewModels;

namespace Tournament
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Team
            Mapper.CreateMap<Team, TeamViewModel>();
            Mapper.CreateMap<TeamViewModel, Team>();

            // Location
            Mapper.CreateMap<Location, LocationViewModel>();
            Mapper.CreateMap<LocationViewModel, Location>();

            // Player
            Mapper.CreateMap<Player, PlayerViewModel>();
            Mapper.CreateMap<PlayerViewModel, Player>();

            // Match
            Mapper.CreateMap<Match, MatchViewModel>();
            Mapper.CreateMap<MatchViewModel, Match>();

            // Comment
            Mapper.CreateMap<Comment, CommentViewModel>();
            Mapper.CreateMap<CommentViewModel, Comment>();
        }
    }
}