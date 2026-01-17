using ShoppingApp.Models;
using ShoppingApp.Repositories;

namespace ShoppingApp.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IRepository<DiscountSetting> _discountRepository;

        public DiscountService(IRepository<DiscountSetting> discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<DiscountSetting?> GetActiveDiscountSettingAsync()
        {
            return await _discountRepository.GetFirstOrDefaultAsync(d => d.IsActive);
        }

        public async Task<IEnumerable<DiscountSetting>> GetAllDiscountSettingsAsync()
        {
            return await _discountRepository.GetAllAsync();
        }

        public async Task<DiscountSetting?> GetDiscountSettingByIdAsync(int id)
        {
            return await _discountRepository.GetByIdAsync(id);
        }

        public async Task CreateDiscountSettingAsync(DiscountSetting setting)
        {
            setting.CreatedDate = DateTime.Now;
            await _discountRepository.AddAsync(setting);
        }

        public async Task UpdateDiscountSettingAsync(DiscountSetting setting)
        {
            setting.UpdatedDate = DateTime.Now;
            await _discountRepository.UpdateAsync(setting);
        }

        public async Task DeleteDiscountSettingAsync(int id)
        {
            var setting = await _discountRepository.GetByIdAsync(id);
            if (setting != null)
            {
                await _discountRepository.DeleteAsync(setting);
            }
        }

        public decimal CalculateDiscount(decimal productPrice, DiscountSetting? setting)
        {
            if (setting == null || !setting.IsActive)
                return 0;

            if (productPrice >= setting.MinimumAmountThreshold)
            {
                return productPrice * setting.DiscountPercentage / 100;
            }

            return 0;
        }
    }
}
