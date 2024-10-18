using System;

namespace Acc.Lib.Gallery.ViewModels.Pages;

public class AccDataProviderPageViewModel : ObservableObject
{
    public string GetCustomCarsSignature { get; } =
        @"public static IEnumerable<CustomCar> GetCustomCars();";
    public string GetCustomCarsUsage { get; } = @"var cars = AccDataProvider.GetCustomCars();
foreach(var car in cars) {
    // do something with each car
}";

}