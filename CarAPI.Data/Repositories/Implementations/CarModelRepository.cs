using CarAPI.Data.Contexts;
using CarAPI.Data.Repositories.Interfaces;
using CarAPI.Domain.Dtos.Requests;
using CarAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarAPI.Data.Repositories.Implementations
{
    public class CarModelRepository : ICarModelRepository
    {
        private readonly CarContext _context;

        public CarModelRepository(CarContext carContext)
        {
            _context = carContext;
        }

        public async Task<IEnumerable<CarModel>> GetAllCarsAsycn()
        {
            return await _context.CarModels.ToListAsync();
        }

        public async Task<CarModel> GetCar(Expression<Func<CarModel, bool>> predicate)
        {
            return await _context.CarModels.AsQueryable().FirstOrDefaultAsync(predicate);
        }

        public async Task<CarModel> UpdateCarAsync(CarModel carModel)
        {
            var existingCarModel = await _context.CarModels.FindAsync(carModel.Id);

            if (existingCarModel == null)
            {
                return null;
            }

            existingCarModel.CarID = carModel.CarID;
            existingCarModel.Model = carModel.Model;
            existingCarModel.CategoryId = carModel.CategoryId;

            _context.Update(existingCarModel);
            var isSaved = await SaveChanges();

            if (isSaved)
            {
                return existingCarModel;
            }

            return null;
        }

        public async Task<CarModel> AddCarAsync(CreateCarModelRequest request)
        {
            CarModel carModel = new CarModel
            {
                CarID = request.CarID,
                Model = request.Model,
                CategoryId = request.CategoryId
            };

            await _context.CarModels.AddAsync(carModel);
            var isCarSaved = await SaveChanges();

            if (!isCarSaved)
            {
                return null;
            }

            return carModel;
        }

        public async Task<bool> DeleteCar(Guid carId)
        {
            var carExists = await _context.Cars.FindAsync(carId);
            if (carExists == null)
            {
                return false;
            }

            _context.Cars.Remove(carExists);
            var isRemoved = await SaveChanges();

            if (!isRemoved)
                return false;

            return true;
        }

        private async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
