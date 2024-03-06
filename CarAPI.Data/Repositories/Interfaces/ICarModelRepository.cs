using CarAPI.Domain.Dtos.Requests;
using CarAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarAPI.Data.Repositories.Interfaces
{
    public interface ICarModelRepository
    {
        Task<IEnumerable<CarModel>> GetAllCarsAsycn();
        Task<CarModel> GetCar(Expression<Func<CarModel, bool>> predicate);
        Task<CarModel> UpdateCarAsync(CarModel carModel);
        Task<CarModel> AddCarAsync(CreateCarModelRequest request);
        Task<bool> DeleteCar(Guid carId);
    }
}
