using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Entities;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IPromotionEngine
    {
        List<Promotion> GetPromotionsForProduct(Product product);
        PromotionResult CalculateFinalPrice(Product product, int quantity);
        List<Promotion> FilterPromotionsForProduct(Product product, List<Promotion> allPromos);

    }
}
