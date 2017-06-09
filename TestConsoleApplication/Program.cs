using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
                        
        }

        public void DoSomeStaff()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
            });
            var mapper = config.CreateMapper();
            List<PersonDto> personDtoList = new List<PersonDto>();
            personDtoList.Add(new PersonDto { Id = 1, FirstName = null, LastName = "Smith", Age = 20 });
            personDtoList.Add(new PersonDto { Id = 2, FirstName = null, LastName = null, Age = 22 });
            personDtoList.Add(new PersonDto { Id = 3, FirstName = null, LastName = null });
            personDtoList.Add(new PersonDto { Id = 4, FirstName = "John", LastName = null, Age = 14 });
            personDtoList.Add(new PersonDto { Id = 5, FirstName = "Jane", LastName = "Ribbon", Age = 16 });
            personDtoList.Add(new PersonDto { Id = 6, FirstName = "Jack", LastName = "Jefferson", Age = 18 });


            Type type = typeof(PersonViewModel);
            var properties = type.GetProperties();
            var fields = type.GetFields();
            foreach (var personDto in personDtoList)
            {
                PersonViewModel person = mapper.Map<PersonViewModel>(personDto);
                foreach (var prop in properties)
                {
                    string name = prop.Name;
                    object temp = prop.GetValue(person);
                    Console.WriteLine($"{name}: {temp}");
                }
                Console.WriteLine();
            }
        }
    }
}
