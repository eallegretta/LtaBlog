using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Eaa.LtaBlog.Application.Models;
using Eaa.LtaBlog.Application.Core.Entities;
using Eaa.LtaBlog.Application.Core.Commands.Account;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Eaa.LtaBlog.Application.App_Start.AutoMappings), "Start")]
namespace Eaa.LtaBlog.Application.App_Start
{
	public static class AutoMappings
	{
		public static void Start()
		{
			MapModels();
			MapCommands();
		}

		private static void MapCommands()
		{
			
			Mapper.CreateMap<ProfileModel, RegisterUserCommand>();
			Mapper.CreateMap<RegisterUserCommand, User>()
				.AfterMap((ruc, u) => u.SetPassword(ruc.Password));
			Mapper.CreateMap<ProfileModel, EditProfileCommand>();
		}

		private static void MapModels()
		{
			Mapper.CreateMap<Post, PostsViewModel.PostModel>().ConvertUsing(p => new PostsViewModel.PostModel { Post = p });
			Mapper.CreateMap<ProfileModel, LoginModel>();
			Mapper.CreateMap<User, ProfileModel>();
		}
	}
}