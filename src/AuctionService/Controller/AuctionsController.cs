using System;
using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controller;

// ControllerBase is the base class for an MVC controller without view support.Only contain the API endposints, wont return views to the client

[ApiController]
[Route("api/auctions")]
public class AuctionsController : ControllerBase
{
    private readonly AuctionDbContext _context; // DbContext is used to interact with the database，这是一个数据库上下文
    private readonly IMapper _mapper; // AutoMapper is used to map between different types of objects, 这是一个对象映射器，用来映射不同类型的对象
    public AuctionsController(AuctionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/auctions
    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAuctions()
    {
        var auctions = await _context.Auctions
            .Include(x => x.Item)
            .OrderBy(x => x.Item.Make)
            .ToListAsync();

        return _mapper.Map<List<AuctionDto>>(auctions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        var auction = await _context.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (auction == null)
        {
            return NotFound();
        }

        return _mapper.Map<AuctionDto>(auction);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreatAuction(CreatAuctionDto auctionDto)
    {
        var auction = _mapper.Map<Auction>(auctionDto);
        //TODO: Add current user as seller

        auction.Seller = "TestSeller";
        _context.Auctions.Add(auction);

        var result = await _context.SaveChangesAsync() > 0;

        if(!result)
        {
            return BadRequest("Failed to create auction and save to database");
        }

        // CreatedAtAction is used to return a 201 status code with the location of the created resource
        return CreatedAtAction(nameof(GetAuctionById), new {auction.Id}, _mapper.Map<AuctionDto>(auction));

        
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
    {
        //这是一个更新操作，所以首先要找到要更新的对象
        var auction = await _context.Auctions.Include(x => x.Item).FirstOrDefaultAsync(x => x.Id == id);

        if (auction == null)
        {
            return NotFound(); //如果找不到要更新的对象，返回404
        }
        //TODO: check seller is the current user

        //将 updateAuctionDto 映射到 auction 对象, update the auction object with the updateAuctionDto

        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;//如果 updateAuctionDto.Make 为 null，则不更新 auction.Item.Make
        auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
        auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;
        auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
        auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;
        

        var result = await _context.SaveChangesAsync() > 0;

        if (!result)
        {
            return BadRequest("Failed to update auction and save to database");
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id){

        var auction = await _context.Auctions.FindAsync(id);

        if (auction == null)
        {
            return NotFound();
        }

        _context.Auctions.Remove(auction);

        var result = await _context.SaveChangesAsync() > 0;

        if (!result)
        {
            return BadRequest("Failed to delete auction and save to database");
        }

        return Ok();

    }

}
