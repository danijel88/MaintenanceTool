using JetBrains.Annotations;
using MaintenanceTool.Core.MaintenanceAggregate;
using Xunit;

namespace MaintenanceTool.UnitTests.Core.MaintenanceAggregate;

[TestSubject(typeof(Group))]
public class GroupConstructor
{
  private Group? _testGroup;
  private string _testGroupName = "Cooling System";
  private int _testPlantId = 1;

  private Group CreateGroup()
  {
    return new Group(_testGroupName, _testPlantId);
  }
  [Fact]
  public void GroupInitializeName()
  {
    _testGroup = CreateGroup();
    Assert.Equal(_testGroupName,_testGroup.GroupName);
  }
  [Fact]
  public void GroupInitializePlantId()
  {
    _testGroup = CreateGroup();
    Assert.Equal(_testPlantId,_testGroup.PlantId);
  }

  
}
