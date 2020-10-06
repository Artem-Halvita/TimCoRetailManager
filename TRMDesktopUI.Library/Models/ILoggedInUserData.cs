using System;

namespace TRMDesktopUI.Library.Models
{
    public interface ILoggedInUserData
    {
        DateTime CreatedDate { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string Id { get; set; }
        string LastName { get; set; }
        string Token { get; set; }
    }
}