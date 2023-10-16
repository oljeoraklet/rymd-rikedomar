public abstract class SpaceshipModule
{
    public string ModuleName { get; set; }
    public string Description { get; set; }

    // This being abstract means you can have specific modules derive from this with unique properties or methods.
    // Example: FuelEfficiencyModule, CargoExpansionModule, DefenseShieldModule, etc.

    public abstract void ApplyModuleEffect(Spaceship spaceship);
}
