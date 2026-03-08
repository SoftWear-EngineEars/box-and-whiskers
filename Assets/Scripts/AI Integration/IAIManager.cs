using System.Threading.Tasks;

public interface IAIManager
{
    public Task<string> Request(DialogueType type);
}