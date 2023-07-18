using ErrorOr;
using BuberBreakfast.Models;

namespace BuberBreakfast.Services.Breakfasts;

public interface IBreakfastService
{
    ErrorOr<Created> CreateBreakfast(Breakfast breakfast);
    ErrorOr<List<Breakfast>> ListBreakfasts();
    ErrorOr<Breakfast> GetBreakfast(Guid Id);
    ErrorOr<UpsertBreakfast> UpsertBreakfast(Breakfast breakfast);
    ErrorOr<Deleted> DeleteBrakFast(Guid Id);
}