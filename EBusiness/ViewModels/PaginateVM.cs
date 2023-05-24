namespace EBusiness.ViewModels
{
    public class PaginateVM<T>
    {
        public List<T> Items { get; set; }
        public int CurentPage { get; set; }
        public int PageCount { get; set; }
    }
}
