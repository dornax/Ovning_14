namespace Garage3.Models.ViewModels
{
    public class SearchFilterSortViewModel
    {
        public IEnumerable<MemberShowViewModel> SearchFilterSortMembers { get; set; }
        //public string SortOrder { get; set; }
        //public bool Ascending { get; set; }
        //public string Filter { get; set; }
        public string NameSortParam { get; internal set; }
    }
}
