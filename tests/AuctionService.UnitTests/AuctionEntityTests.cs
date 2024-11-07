using AuctionService.Entities;

namespace AuctionService.UnitTests;

public class AuctionEntityTests
{
    [Fact]
    public void HasReservePrice_ReservePriceGetZero_True()
    {
        var auction = new Auction{Id = new Guid(), ReservePrice = 10};

        var result = auction.HasReservePrice();

        Assert.True(result);
    }

    [Fact]
    public void HasReservePrice_ReservePriceIsZero_False()
    {
        var auction = new Auction{Id = new Guid(), ReservePrice = 0};

        var result = auction.HasReservePrice();

        Assert.False(result);
    }
}