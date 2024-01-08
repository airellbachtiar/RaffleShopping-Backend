namespace RaffleShopping.Services.Catalogs.Dtos
{
    public class GetCatalogDto
    {
        #pragma warning disable IDE1006 // Naming Styles
        public string _id { get; set; } = "";

        public string Title { get; set; } = "";

        public double Price { get; set; }

        public string PictureUrl { get; set; } = "";
    }
}
