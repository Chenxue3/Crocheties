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
    private readonly AuctionDbContext _context;
    private readonly IMapper _mapper;
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

}
