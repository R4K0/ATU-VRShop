public interface IGrowableSpot
{
    PlantDefinition PlantDefinition { get; set; }
    bool CanGrow();
    void DoGrow();
    bool CanPlant(PlantDefinition plantDefinition);
    void Plant(PlantDefinition plantDefinition);
}