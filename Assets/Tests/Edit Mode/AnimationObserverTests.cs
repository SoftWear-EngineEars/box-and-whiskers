// Tests by Pinak Unawane

/*
    I chose to make this test suite to test observer pattern implementation of frame animation.
    Without delving too deep into direct class uses, this test suite is meant to check that
    frame updates are broadcasted properly. For this, I added subscribe, unsubscribe, and 
    multi-observer tests. I also tested PlayerAnimationHandler to initialize and properly subscribe
    to the subject, so the mocks test both ends of the observer pattern.
    Note that I made test versions of abstract classes, because I expect the design pattern of
    the abstract classes to implement the observer pattern correctly, so subclasses can simply
    rely on the base class while implementing their own logic separately. In hindsight, the
    classes could have been interfaces, but abstract classes work for the tests with their
    own instantiable test versions.
*/

using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using UnityEngine.TestTools;

public class AnimationObserverTests
{
    private GameObject animationFrameManagerObject;
    private AnimationFrameManager animationFrameManager;

    private GameObject playerObject;
    private GameObject handlerObject;

    [SetUp]
    public void Setup()
    {
        animationFrameManagerObject = new GameObject();
        animationFrameManager = animationFrameManagerObject.AddComponent<AnimationFrameManager>();

        playerObject = null;
        handlerObject = null;
    }

    [Test]
    public void SendMessage_NotifiesObserver()
    {
        IAnimationFrameObserver observer = Substitute.For<IAnimationFrameObserver>();

        animationFrameManager.SubscribeToAnimationFrame(observer);

        animationFrameManager.SendMessage();

        observer.Received().OnAnimationFrame(0);
    }

    [Test]
    public void SendMessage_NotifiesAllObservers()
    {
        IAnimationFrameObserver observerOne = Substitute.For<IAnimationFrameObserver>();
        IAnimationFrameObserver observerTwo = Substitute.For<IAnimationFrameObserver>();

        animationFrameManager.SubscribeToAnimationFrame(observerOne);
        animationFrameManager.SubscribeToAnimationFrame(observerTwo);

        animationFrameManager.SendMessage();

        observerOne.Received().OnAnimationFrame(0);
        observerTwo.Received().OnAnimationFrame(0);
    }

    [Test]
    public void Unsubscribe_RemovesObserver()
    {
        IAnimationFrameObserver observer = Substitute.For<IAnimationFrameObserver>();

        animationFrameManager.SubscribeToAnimationFrame(observer);
        animationFrameManager.UnsubscribeFromAnimationFrame(observer);

        animationFrameManager.SendMessage();

        observer.DidNotReceive().OnAnimationFrame(Arg.Any<int>());
    }

    [Test]
    public void Initialize_SubscribesPlayerAnimationHandlerToSubject()
    {
        IAnimationFrameSubject subject = Substitute.For<IAnimationFrameSubject>();

        playerObject = new GameObject();
        playerObject.AddComponent<Rigidbody2D>();
        playerObject.AddComponent<SpriteRenderer>();
        TestPlayer player = playerObject.AddComponent<TestPlayer>();

        handlerObject = new GameObject();
        TestPlayerAnimationHandler handler = handlerObject.AddComponent<TestPlayerAnimationHandler>();

        handler.Initialize(player, subject);

        subject.Received().SubscribeToAnimationFrame(handler);
    }

    private class TestPlayer : Player
    {
    }

    private class TestPlayerAnimationHandler : PlayerAnimationHandler
    {
    }
}
