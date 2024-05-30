using Users.Domain.Shared;

namespace Users.Domain.Users.Enumerations;

public class GenderType : Enumeration
{
    public static readonly GenderType Female = new GenderType(0, "Female");
    public static readonly GenderType Male = new GenderType(1, "Male");
    public static readonly GenderType None = new GenderType(2, "None");
    
    public GenderType(int id, string name) : base(id, name)
    {
    }
    
    public static GenderType? FromName(string? name)
    {
        var collection = GetAll<GenderType>();
        foreach (var gender in collection)
        {
            if (gender.Name == name)
                return gender;
        }

        return null;
    }
    
    public static GenderType? FromValue(int id)
    {
        var collection = GetAll<GenderType>();
        foreach (var gender in collection)
        {
            if (gender.Id == id)
                return gender;
        }

        return null;
    }
}