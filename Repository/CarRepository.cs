using WebApp.Models;
using WebApp.Data;
using WebApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Repository
{
    public class CarRepository : ICar
    {
        private readonly ApplicationDbContext context;

        public CarRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task <IEnumerable<Car>> GetAllCarsAsync()
        {
            return await context.Cars.OrderBy(x => x.CarId).ToListAsync();
        }

        public async Task <IEnumerable<Car>> GetAllAvailableCarsAsync()
        {
            return await context.Cars.Where(c => c.IsAvailable).ToListAsync();
        }

        public async Task <Car> GetCarByIdAsync(int carId)
        {
            return await context.Cars.FirstOrDefaultAsync(car => car.CarId == carId);
        }

        public async Task AddCarAsync(Car car)
        {
            context.Cars.Add(car);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCarAsync(Car car)
        {
            context.Cars.Update(car);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int carId)
        {
            var car = context.Cars.Find(carId);

            if(car != null)
            {
                context.Cars.Remove(car);
                await context.SaveChangesAsync();
            }
        }
    }
}
