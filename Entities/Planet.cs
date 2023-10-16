public class Planet
{
    public string Name { get; set; }
    public List<BaseGood> AvailableGoods { get; private set; }

    public Planet(string name)
    {
        Name = name;
        AvailableGoods = new List<BaseGood>();
    }

    // Methods can include: AddGood, RemoveGood, ChangePrice, etc.
}