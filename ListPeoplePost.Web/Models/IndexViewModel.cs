using ListPeoplePost.Data;

namespace ListPeoplePost.Web.Models
{
    public class IndexViewModel
    {
        public List<Person> People { get; set; }
        public string Message { get; set; }
    }
}
