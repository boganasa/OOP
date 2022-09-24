using Isu.Models;

namespace Isu.Entities;

public class ListOfGroups
{
    private List<GroupName> _groupList = new List<GroupName>();

    public ListOfGroups() { }
    public void AddGroup(GroupName groupName)
    {
        if (!_groupList.Contains(groupName))
        {
            _groupList.Add(groupName);
        }
    }

    public GroupName SearchGroup(GroupName groupName)
    {
        if (!_groupList.Contains(groupName))
        {
            _groupList.Add(groupName);
        }
    }
}