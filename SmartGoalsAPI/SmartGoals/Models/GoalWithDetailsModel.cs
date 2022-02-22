using System.Collections.Generic;

namespace SmartGoals.Models;

public class GoalWithDetailsModel : GoalModel
{
    public IEnumerable<ObjectiveModel> Objectives { get; set; }
}