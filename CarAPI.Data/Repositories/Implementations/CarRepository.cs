using CarAPI.Data.Contexts;
using CarAPI.Domain.Dtos.Requests;
using CarAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarAPI.Data.Repositories.Implementations
{
    public class CarRepository
    {
        private readonly CarContext _context;

        public CarRepository(CarContext carContext)
        {
            _context = carContext;
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsycn()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetCar(Expression<Func<Car, bool>> predicate)
        {
            return await _context.Cars.AsQueryable().FirstOrDefaultAsync(predicate);
        }

        public async Task<Car> UpdateCarAsync(Car car)
        {
            var existingCar = await _context.Cars.FindAsync(car.Id);

            if (existingCar == null)
            {
                return null;
            }

            existingCar.CarName = car.CarName;
            existingCar.CarID = car.CarID;

            _context.Update(existingCar);
            var isSaved = await SaveChanges();

            if (isSaved)
            {
                return car;
            }

            return null;
        }

        public async Task<Car> AddCarAsync(CreateCarRequest request)
        {
            Car car = new Car
            {
                CarID = request.CarID,
                CarName = request.CarName
            };

            await _context.Cars.AddAsync(car);
            var isCarSaved = await SaveChanges();

            if (!isCarSaved)
            {
                return null;
            }

            return car;
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
