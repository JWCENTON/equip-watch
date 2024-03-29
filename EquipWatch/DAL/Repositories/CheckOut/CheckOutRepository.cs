﻿using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.CheckOut;

public class CheckOutRepository : ICheckOutRepository
{
    private readonly DatabaseContext _context;

    public CheckOutRepository(DatabaseContext context, IdentityContext identityContext)
    {
        _context = context;
        Seed.IfDbEmptyAddNewItems(context, identityContext);
    }

    public async Task<List<Domain.CheckOut.Models.CheckOut>> GetAllAsync()
    {
        return await _context.CheckOuts.ToListAsync();
    }

    public async Task<Domain.CheckOut.Models.CheckOut> GetAsync(Guid id)
    {
        var checkOut = await _context.CheckOuts.FirstOrDefaultAsync(c => c.Id == id);
        return checkOut ?? throw new KeyNotFoundException("CheckOut With given Id was not found");
    }

    public async Task CreateAsync(Domain.CheckOut.Models.CheckOut entity)
    {
        await _context.CheckOuts.AddAsync(entity);
    }

    public async Task UpdateAsync(Domain.CheckOut.Models.CheckOut entity)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}