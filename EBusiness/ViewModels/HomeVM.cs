using EBusiness.Models;

namespace EBusiness.ViewModels
{
    public class HomeVM
    {
        public List<Blog> Blogs { get; set; }
        public List<Serv> Servs { get; set; }
        public Setting Settings { get; set; }

        public List<Slider> Sliders { get; set; }

        public List<Team> Teams { get; set; }

    }
}
