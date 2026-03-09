using System.Threading.Tasks;

namespace AI_Integration
{
    public interface IGroqAIController
    {
        Task<string> Ask(string prompt);
    }
}