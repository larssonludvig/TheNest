namespace Domain;

public class Loadout
{
    public string Class { get; set; } = "";
    public string Weapon { get; set; } = "";
    public string Specialization { get; set; } = "";
    public List<string> Gadgets { get; set; } = new List<string>();
}