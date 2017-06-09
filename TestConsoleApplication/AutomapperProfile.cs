using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<PersonDto, PersonViewModel>()
                .ForMember("FirstName", opts => opts.NullSubstitute("Domyślne imie"))
                .ForMember("LastName", opts => opts.NullSubstitute("Domyślne nazwisko"));
        }
    }
}
