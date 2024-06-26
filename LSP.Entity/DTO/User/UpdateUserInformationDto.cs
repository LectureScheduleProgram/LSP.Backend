﻿using System.ComponentModel.DataAnnotations;
using LSP.Core.Entities;

namespace LSP.Entity.DTO.User
{
    public class UpdateUserInformationDto : IDto
    {

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        public string Surname { get; set; }

        [Required]
        [MinLength(2)]
        public string PhoneNumber { get; set; }
    }
}
