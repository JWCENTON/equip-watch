﻿namespace DAL;

public class EmailContext
{
    public string? UserName { get; init; }
    public string? Password { get; init; }
    public string? Address { get; init; }
    public string? Smtp { get; init; }
    public int Port { get; init; }
}