using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;

namespace AuctionService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // 定义从 Auction 到 AuctionDto 的映射，并包含 Item 成员的映射
        CreateMap<Auction, AuctionDto>().IncludeMembers(x => x.Item); // Include Item mapping

        // 定义从 Item 到 AuctionDto 的映射
        CreateMap<Item, AuctionDto>();

        // 定义从 CreatAuctionDto 到 Auction 的映射
        CreateMap<CreatAuctionDto, Auction>()
            .ForMember(d => d.Item, o => o.MapFrom(s => s)); // 将 CreatAuctionDto 映射到 Auction 的 Item 属性

        // 定义从 CreatAuctionDto 到 Item 的映射
        CreateMap<CreatAuctionDto, Item>();
    }
}
