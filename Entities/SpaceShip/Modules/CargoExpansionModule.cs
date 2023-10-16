public class CargoExpansionModule : SpaceshipModule
{
    public int AdditionalCargoSpace { get; set; }

    public override void ApplyModuleEffect(Spaceship spaceship)
    {
        spaceship.CargoCapacity += AdditionalCargoSpace; // Increases the cargo capacity.
    }
}
