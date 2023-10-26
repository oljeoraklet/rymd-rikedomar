public interface IProfile<T>
{
    List<T> GetItems();
    void ShowItems() { }
}