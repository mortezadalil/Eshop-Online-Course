﻿using System.ComponentModel.DataAnnotations;

namespace Eshop.ViewModels.Account
{
    public class DetailUserVm
    {

        public string? Id { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string>? Roles { get; set; } = new List<string>();

    }
}
