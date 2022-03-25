namespace Web.ViewModels
{
    public class CatalogueViewModel
    {
        public int? TypesFilterApplied { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
        public List<CatalogItemViewModel> CatalogItems { get; set; }
    }
}
