using System;

namespace SmartGoals.Models;

public class ObjectiveAddModel
{
    public Guid GoalId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string TimeEstimated { get; set; }
    public bool IsFinished { get; set; }
    public string Specific { get; set; }
    public string Measurable { get; set; }
    public string Achievable { get; set; }
    public string Realistic { get; set; }
    public string Timely { get; set; }
}