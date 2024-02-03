using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface ICar
    {
        Car GetCarById(int carId);
        IEnumerable<Car> GetAllCars();
        IEnumerable<Car> GetAllAvailableCars();
        Task AddCar(Car car);
        Task UpdateCar(Car car);
        Task DeleteCar(int carId);
    }
}
