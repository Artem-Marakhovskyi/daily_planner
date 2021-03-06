﻿namespace DailyPlanner.Infrastructure
{
    public interface IPasswordHashManager
    {
        (string hash, byte[] salt) GenerateHashSaltPair(string password);

        bool IsPasswordValid(string password, string hash, byte[] salt);
    }
}
