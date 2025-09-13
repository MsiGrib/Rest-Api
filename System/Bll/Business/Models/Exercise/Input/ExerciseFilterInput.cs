using DataModel.Models;

namespace Business.Models.Exercise.Input
{
    public class ExerciseFilterInput : Pagination
    {
        public Guid? ExerciseId { get; init; } = null;
        public string? Name { get; init; } = null;
        public int? SetsFrom { get; init; } = null;
        public int? SetsTo { get; init; } = null;
        public int? RepsFrom { get; init; } = null;
        public int? RepsTo { get; init; } = null;
        public double? WeightFrom { get; init; } = null;
        public double? WeightTo { get; init; } = null;
    }
}