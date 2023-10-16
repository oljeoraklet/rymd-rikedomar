public class FuelEfficiencyModule : SpaceshipModule
{
    public decimal EfficiencyIncreasePercentage { get; set; } // e.g., 0.10 for 10%

    public override void ApplyModuleEffect(Spaceship spaceship)
    {
        spaceship.Fuel *= (1 + EfficiencyIncreasePercentage); // Increases the fuel by the given percentage.
    }
}
