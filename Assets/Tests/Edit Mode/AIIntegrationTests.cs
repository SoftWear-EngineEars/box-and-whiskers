// Tests by Michael Polyakov
// I decided to focus on two major things: that the AIManager correctly delegates
// to the controller with the right prompt, and that EnvLoader parses the
// .env file. I didn't test the HTTP layer of GroqAIController directly, because
// it wouldn't be unit testing at that point (testing my code + the network + Groq API etc.)
// The tradeoff is that GroqAIController isn't tested here, but since it doesn't have
// any game logic (as it is meant to function like the real subject in proxy pattern), it isn't likely to really have bugs.
// The AIManager tests verify that the proxy is doing its job: mapping DialogueType
// to a prompt and forwarding it, which is an important behavior.
// EnvLoader tests cover the parsing edge cases (missing file, missing key, whitespace,
// quotes) since there's no other way to diagnose such issues.

using System.IO;
using System.Threading.Tasks;
using AI_Integration;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

public class AIIntegrationTests
{
    private GameObject _gameObject;
    private AIManager _aiManager;
    private IGroqAIController _mockController;

    [SetUp]
    public void Setup()
    {
        _gameObject = new GameObject();
        _aiManager = _gameObject.AddComponent<AIManager>();
        _mockController = Substitute.For<IGroqAIController>();
        _aiManager.SetController(_mockController);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_gameObject);
    }

    
    // AIManager tests

    [Test]
    public void AIManager_Implements_IAIManager()
    {
        Assert.IsInstanceOf<IAIManager>(_aiManager);
    }

    [Test]
    public async Task Request_DelegatesToController()
    {
        _mockController.Ask(Arg.Any<string>()).Returns(Task.FromResult("hello"));

        await _aiManager.Request(DialogueType.StartGame);

        await _mockController.Received().Ask(Arg.Any<string>());
    }

    [Test]
    public async Task Request_ReturnsControllerResponse()
    {
        _mockController.Ask(Arg.Any<string>()).Returns(Task.FromResult("hello"));

        var result = await _aiManager.Request(DialogueType.StartGame);

        Assert.AreEqual("hello", result);
    }

    [Test]
    public async Task Request_IncludesCorrectContext_ForStartGame()
    {
        _mockController.Ask(Arg.Any<string>()).Returns(Task.FromResult("hello"));

        await _aiManager.Request(DialogueType.StartGame);

        await _mockController.Received().Ask(Arg.Is<string>(s => s.Contains("level has just started"))); // From AIManager's prompt list
    }

    [Test]
    public async Task Request_IncludesCorrectContext_ForCollectKey()
    {
        _mockController.Ask(Arg.Any<string>()).Returns(Task.FromResult("hello"));

        await _aiManager.Request(DialogueType.CollectKey);

        await _mockController.Received().Ask(Arg.Is<string>(s => s.Contains("key"))); // From AIManager's prompt list
    }

    
    // EnvLoader tests

    [Test]
    public void EnvLoader_ParsesKey_FromValidFile()
    {
        var path = Path.Combine(Application.dataPath, "..", ".env");
        File.WriteAllText(path, "GROQ_API_KEY=test_key_teehee");

        var result = EnvLoader.GetApiKey();

        Assert.AreEqual("test_key_teehee", result);

        File.Delete(path);
    }

    [Test]
    public void EnvLoader_StripsQuotes_FromValue() // Not sure what the standards are for env files, but this seems like a reasonable thing to handle correctly
    {
        var path = Path.Combine(Application.dataPath, "..", ".env");
        File.WriteAllText(path, "GROQ_API_KEY=\"test_key_teehee\"");

        var result = EnvLoader.GetApiKey();

        Assert.AreEqual("test_key_teehee", result);

        File.Delete(path);
    }

    [Test]
    public void EnvLoader_StripsWhitespace_FromValue() // Not sure what the standards are for env files, but this seems like a reasonable thing to handle correctly
    {
        var path = Path.Combine(Application.dataPath, "..", ".env");
        File.WriteAllText(path, "GROQ_API_KEY=  test_key_teehee  ");

        var result = EnvLoader.GetApiKey();

        Assert.AreEqual("test_key_teehee", result);

        File.Delete(path);
    }

    [Test]
    public void EnvLoader_ReturnsNull_WhenKeyMissing()
    {
        var path = Path.Combine(Application.dataPath, "..", ".env");
        File.WriteAllText(path, "SOME_OTHER_KEY=value");

        var result = EnvLoader.GetApiKey();

        Assert.IsNull(result);

        File.Delete(path);
    }
}