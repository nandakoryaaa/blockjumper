public interface IRowStrategy
{
    public void update(Row row);
    public void addBlock(Row row);
    public bool canFill(Row row);
}
