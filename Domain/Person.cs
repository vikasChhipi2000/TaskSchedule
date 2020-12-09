using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

/// <Summary>A named individual with a set of skills.</Summary>
public class Person
{
  public int Id { get; set; }
  public string Name { get; set; }
  public HashSet<Skill> Skills { get; set; } = new HashSet<Skill>();

    // for sorting the person on the base of sum of all skillpoints
    public int sumOfAllSkill()
    {
        int sum = 0;
        foreach(Skill s in Skills)
        {
            sum += s.SkillLevel;
        }
        return sum;
    }

    public override string ToString()
    => $"[{Id}] {Name}, Skills: {SkillsToString()}";
  private string SkillsToString()
    => Skills.Select(i => i.Name).ToDelimitedString(",");
}
