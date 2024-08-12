﻿using System.ComponentModel.DataAnnotations.Schema;
using ToyStore.Core.Models;

namespace ToyStore.Api.DTOS
{
    public class UserAddressDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
