﻿namespace ToyStore.Api.DTOS
{
    public class OrderItemToReturnDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int quantity { get; set; }
    }
}