﻿namespace WeddingPlanner.RazorPages.Pages.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

}
