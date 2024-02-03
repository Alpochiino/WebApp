using WebApp.Models;
using WebApp.Data;
using WebApp.Interfaces;

namespace WebApp.Repository
{
    public class CarRepository : ICar
    {
        private readonly ApplicationDbContext context;

        public CarRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Car> GetAllCars()
        {
            return context.Cars.OrderBy(x => x.CarId);
        }

        public IEnumerable<Car> GetAllAvailableCars()
        {
            return context.Cars.Where(c => c.IsAvailable).ToList();
        }

        public Car GetCarById(int carId)
        {
            return context.Cars.FirstOrDefault(car => car.CarId == carId);
        }

        public async Task AddCar(Car car)
        {
            context.Cars.Add(car);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCar(Car car)
        {
            context.Cars.Update(car);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCar(int carId)
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
