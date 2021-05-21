using AutoMapper;
using MolDavaBanking.Domain.BankViewModel;
using MolDavaBanking.Domain.CardViewModel;
using MolDavaBanking.Domain.SupportMessageViewModel;
using MolDavaBanking.Domain.TransactionViewModel;
using MolDavaBanking.Domain.UserViewModel;
using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Managers
{
    public class AutoMapper
    {
        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserViewModel_Register, User>()
                    .ForMember(dest => dest.IsLocked, act => act.MapFrom(src => 0));

                cfg.CreateMap<UserViewModel_Login, User>()
                    .ForMember(dest => dest.FirstName, act => act.MapFrom(src => ""))
                    .ForMember(dest => dest.LastName, act => act.MapFrom(src => ""))
                    .ForMember(dest => dest.IDNP, act => act.MapFrom(src => ""))
                    .ForMember(dest => dest.BirthDate, act => act.MapFrom(src => ""))
                    .ForMember(dest => dest.Adress, act => act.MapFrom(src => ""))
                    .ForMember(dest => dest.IsLocked, act => act.MapFrom(src => 0));

                cfg.CreateMap<User, UserViewModel_Register>();

                cfg.CreateMap<UserViewModel_GetUsers, User>()
                    .ForMember(dest => dest.Password, act => act.Ignore())
                    //.ForMember(dest => dest.Roles, act => act.Ignore())
                    .ForMember(dest => dest.UserId, act => act.Ignore());

                cfg.CreateMap<UserViewModel_Create, User>()
                    .ForMember(dest => dest.Roles, act => act.Ignore());

                cfg.CreateMap<User, UserViewModel_GetUsers>()
                    .ForMember(dest => dest.IsAdmin, act => act.Ignore())
                    .ForMember(dest => dest.IsAccountant, act => act.Ignore())
                    .ForMember(dest => dest.IsClient, act => act.Ignore());

                cfg.CreateMap<TransactionViewModel_GetTransactions, Transaction>();

                cfg.CreateMap<Transaction, TransactionViewModel_GetTransactions>();

                cfg.CreateMap<CardViewModel_CreateCard, Card>();

                cfg.CreateMap<BankViewModel, Bank>();

                cfg.CreateMap<SupportMessageViewModel, SupportMessage>()
                    .ForMember(dest => dest.CreationDate, act => act.MapFrom(src => DateTime.Now))
                    .ReverseMap();

                cfg.CreateMap<SupportMessageViewModel_Get, SupportMessage>()
                    .ReverseMap();
            });
        }
    }
}