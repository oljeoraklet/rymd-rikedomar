public class Planet
{
    public string Name { get; set; }
    public List<IGood> AvailableGoods { get; private set; }

    public Planet(string name)
    {
        Name = name;
        AvailableGoods = new List<IGood>();
    }

    // Methods can include: AddGood, RemoveGood, ChangePrice, etc.
}