using System;
using System.Diagnostics;
using System.Net;
using FinanceDashboard.Server.Controllers;
using FinanceDashboard.Server.Data;
using FinanceDashboard.Shared.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceDashboard.Tests;

public class HistoryControllerTests
{
	private readonly HistoryController _sut;
	private readonly ApplicationDbContext _context = new ApplicationDbContext(
        new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Database")
            .Options
        );

	public HistoryControllerTests()
	{
		_sut = new HistoryController(_context);
	}

    [Fact]
    public async Task GetHisotyByIdAsync_ShouldNotReturnHistory_WhenHistoryDoesNotExist()
    {
        // Arrange
        var id = 1;
        var history = new History() { Id = id, Username = "user1", CompanyName = "Apple", CompanySymbol = "appl", SearchTime = DateTime.Now };

        // Act
        var result = await _sut.GetHistory(id);
        var notFoundResult = (NotFoundResult)result;

        // Assert
        notFoundResult.StatusCode.Should().Be(404);
    }
}

