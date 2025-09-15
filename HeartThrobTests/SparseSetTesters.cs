using HeartThrobFramework;
using HeartThrobFramework.Core;
using HeartThrobFramework.Components;

namespace HeartThrobTests;


public class SparseSetTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(30)]
    [InlineData(100)]
    public void Has_WhenAComponentIsPresent_ShouldReturnTrue(int entityId)
    {
        var set = TestHelpers.CreateSetFor<PlayerControlledComponent>();
        
        set.Add(entityId, new PlayerControlledComponent());
        
        Assert.True(set.Has(entityId));
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(30)]
    public void Has_WhenComponentDoesNotExist_ShouldReturnFalse(int entityId)
    {
        var set = TestHelpers.CreateSetFor<PlayerControlledComponent>();
        
        var result = set.Has(entityId);

        Assert.False(result);
    }
    
    [Theory]
    [ClassData(typeof(TestHelpers.SparseSetPlayerControlledComponentTestData))]
    public void SparseSet_RunAdd_AddsComponent(int entityId, PlayerControlledComponent component)
    {
        var set = TestHelpers.CreateSetFor<PlayerControlledComponent>();
        set.Add(entityId, component);
        
        Assert.True(set.Has(entityId));
        
        Assert.Equal(set.GetComponent(entityId), component);
    }

    [Theory]
    [ClassData(typeof(TestHelpers.SparseSetPlayerControlledComponentTestData))]
    public void Remove_RunRemove_RemovesComponentFromSet(int entityId, PlayerControlledComponent component)
    {
        var set = TestHelpers.CreateSetFor<PlayerControlledComponent>();
        
        set.Add(entityId, component);
        set.Remove(entityId);
        
        Assert.False(set.Has(entityId));
    }

//    [Theory]
//    [ClassData(typeof(TestHelpers.SparseSetPositionComponentTestData))]
//    public void Set_GivenUpdateComponent_UpdatesComponent(int entityId, Position component)
//    {
//        var testComponent = new Position(20, 20);
//        var set = TestHelpers.CreateSetFor<Position>();
//        
//        set.Add(entityId, component);
//        
//        set.Set(entityId, testComponent);
//        
//        Assert.Equal(set.GetComponent(entityId), testComponent);
//    }
}