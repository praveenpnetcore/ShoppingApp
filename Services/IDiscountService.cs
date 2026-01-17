using ShoppingApp.Models;

namespace ShoppingApp.Services
{
    public interface IDiscountService
    {
        Task<DiscountSetting?> GetActiveDiscountSettingAsync();
        Task<IEnumerable<DiscountSetting>> GetAllDiscountSettingsAsync();
        Task<DiscountSetting?> GetDiscountSettingByIdAsync(int id);
        Task CreateDiscountSettingAsync(DiscountSetting setting);
        Task UpdateDiscountSettingAsync(DiscountSetting setting);
        Task DeleteDiscountSettingAsync(int id);
        decimal CalculateDiscount(decimal productPrice, DiscountSetting? setting);
    }
}
