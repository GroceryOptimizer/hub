namespace Core.Entities
{
    public class Coordinates
    {
        public int Id { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }


        public Coordinates(float longitude, float latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
