using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagement.API.Policy
{
    public class UserIdRequirement : IAuthorizationRequirement
    {
        public int RequiredUserId { get; }

        public UserIdRequirement(int requiredUserId)
        {
            RequiredUserId = requiredUserId;
        }
    }
}