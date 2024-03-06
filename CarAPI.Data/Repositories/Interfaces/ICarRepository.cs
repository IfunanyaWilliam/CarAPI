

using CarAPI.Domain.Dtos.Requests;
using CarAPI.Domain.Entities;
using System.Linq.Expressions;

namespace CarAPI.Data.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllCarsAsycn();
        Task<Car> GetCar(Expression<Func<Car, bool>> predicate);
        Task<Car> UpdateCarAsync(Car car);
        Task<Car> AddCarAsync(CreateCarRequest request);
        Task<bool> DeleteCar(Guid carId);
    }
}
