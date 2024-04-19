using System.ComponentModel;

namespace VideoNotesBackend.Enums
{
    public static class Rating
    {
        public enum Values
        {
            [Description("Highly Informative")]
            HighlyInformative,
            [Description("Partially Informative")]
            PartiallyInformative,
            [Description("Not Informative")]
            NotInformative
        }

        public static string GetDescription(this Values rating)
        {
            switch (rating)
            {
                case Values.HighlyInformative:
                    return "Highly Informative";
                case Values.PartiallyInformative:
                    return "Partially Informative";
                case Values.NotInformative:
                    return "Not Informative";
                default:
                    throw new ArgumentException("Invalid rating values");
            }
        }

        public static bool IsValidRating(Values rating)
        {
            return Enum.IsDefined(typeof(Values), rating);
        }
    }
}
