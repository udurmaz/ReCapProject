﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTOs
{
    public class RentalDetailsDto : IDto
    {

        public int Id { get; set; }
        public int CarId { get; set; }
        public string BrandName { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }


    }
}
