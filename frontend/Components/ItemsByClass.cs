using Components.ApiService;

public class ItemsByClass {
    public List<string> Classes = new List<string> {"Light", "Medium", "Heavy"};

    public List<string> LSpecializations = new List<string>();
    public List<string> LWeapons = new List<string>();
    public List<string> LGadgets = new List<string>();

    public List<string> MSpecializations = new List<string>();
    public List<string> MWeapons = new List<string>();
    public List<string> MGadgets = new List<string>();

    public List<string> HSpecializations = new List<string>();
    public List<string> HWeapons = new List<string>();
    public List<string> HGadgets = new List<string>();

    private ApiService _api = new ApiService();

    public ItemsByClass()
    {
        _api.Initialize();
    }

    public async Task Initialize()
    {
        Dictionary<string, Dictionary<string, List<string>>> items = await _api.Get<Dictionary<string, Dictionary<string, List<string>>>>("items", null);


        LSpecializations = items["Light"]["Specializations"];
        LWeapons = items["Light"]["Weapons"];
        LGadgets = items["Light"]["Gadgets"];

        MSpecializations = items["Medium"]["Specializations"];
        MWeapons = items["Medium"]["Weapons"];
        MGadgets = items["Medium"]["Gadgets"];

        HSpecializations = items["Heavy"]["Specializations"];
        HWeapons = items["Heavy"]["Weapons"];
        HGadgets = items["Heavy"]["Gadgets"];
    }
}