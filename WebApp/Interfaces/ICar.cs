using Microsoft.CodeAnalysis.CSharp;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface ICar
    {
        Task <Car> GetCarByIdAsync(int carId);
        Task <IEnumerable<Car>> GetAllCarsAsync();
        Task <IEnumerable<Car>> GetAllAvailableCarsAsync();
        Task AddCarAsync(Car car);
        Task UpdateCarAsync(Car car);
        Task DeleteCarAsync(int carId);
    }
}
