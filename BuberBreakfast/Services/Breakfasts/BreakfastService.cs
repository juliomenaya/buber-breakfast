using ErrorOr;

using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> _breakfasts = new();

    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        _breakfasts.Add(breakfast.Id, breakfast);
        return Result.Created;
    }

    public ErrorOr<List<Breakfast>> ListBreakfasts()
    {
        return _breakfasts.Values.ToList();
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if (_breakfasts.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }
        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertBreakfast> UpsertBreakfast(Breakfast breakfast)
    {
        bool isNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);
        _breakfasts[breakfast.Id] = breakfast;
        return new UpsertBreakfast(isNewlyCreated);
    }

    public ErrorOr<Deleted> DeleteBrakFast(Guid Id)
    {
        _breakfasts.Remove(Id);
        return Result.Deleted;
    }
}