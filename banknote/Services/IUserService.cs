﻿namespace banknote.Services
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}