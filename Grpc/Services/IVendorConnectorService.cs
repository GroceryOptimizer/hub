﻿namespace Api.Services
{
    public interface IVendorConnectorService
    {
        Task<string> SendMessageAsync(string message);
        //Task<List<StockItemDTO>> GetInventoryAsync(ShoppingCartDTO cart);
    }
}
