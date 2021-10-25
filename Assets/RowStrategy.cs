public interface IRowStrategy
{
    public void Update(Row row);
    public void AddBlock(Row row);
    public bool CanFill(Row row);
}
