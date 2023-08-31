namespace CPW219_eCommerceSite.Models
{
    public class PartCatalogViewModel
    {
        public PartCatalogViewModel(List<Part> parts, int lastPage, int currentPage)
        {
            Parts = parts;
            LastPage = lastPage;
            CurrentPage = currentPage;
        }

        public List<Part> Parts { get; private set; }
        /// <summary>
        /// Last page of catalog
        /// </summary>
        public int LastPage { get; private set; }

        public int CurrentPage { get; private set; }
    }
}
