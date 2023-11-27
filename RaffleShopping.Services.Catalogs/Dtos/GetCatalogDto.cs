namespace RaffleShopping.Services.Catalogs.Dtos
{
    public class GetCatalogDto
    {
        public string _id { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

        public string PictureUrl { get; set; }
    }
}
