using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetigentTest.Models.DBModels;

public class APIService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<APIService> _logger;

    public APIService(AppDbContext dbContext, ILogger<APIService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    protected void Log(string message, string methodName, string serviceName)
    {
        var logDirectory = @"C:\APILogs";
        var logFileName = $"{DateTime.UtcNow:dd-MM-yyyy}.txt";
        var logFilePath = Path.Combine(logDirectory, logFileName);

        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        var logMessage = $"{DateTime.UtcNow:dd-MM-yyyy HH:mm:ss} - Service: {serviceName}, Method: {methodName} - {message}";
        File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        _logger.LogError(logMessage);
    }

    protected AppDbContext DbContext => _dbContext;
}
