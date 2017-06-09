
namespace TestConsoleApplication
{
    using Autofac;
    using AutoMapper;
    using System;
    using System.Collections.Generic;

    class Program
    {
        private static IContainer _container;
        static void Main(string[] args)
        {
            containerInsctanceRegister();            
        }

        static private void containerInsctanceRegister()
        {
            var builder = new ContainerBuilder();
            var output = new Nothing("new name");
            builder.RegisterInstance(output).As<INothing>();

            _container = builder.Build();
            _container.BeginLifetimeScope().Resolve<INothing>().Showname();
        }

        static private void containerLambdaRegister()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => new Nothing("new name")).As<INothing>();

            _container = builder.Build();
            _container.BeginLifetimeScope().Resolve<INothing>().Showname();
        }

        static private void containerTypeRegister()
        {            
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Wizard>().As<IWizard>();
            _container = containerBuilder.Build();

            _container.BeginLifetimeScope().Resolve<IWizard>().DoSomeWeirdStuff();
        }

        static private void DoSomeStaff()
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

    public interface IWizard
    {
        void DoSomeWeirdStuff();
    }

    public class Wizard : IWizard
    {
        public void DoSomeWeirdStuff()
        {
            Console.WriteLine("That was weird!");
        }
    }

    public interface INothing
    {
        void Showname();
    }

    public class Nothing : INothing
    {
        public Nothing(string name)
        {
            this._name = name;
        }

        private string _name = "default name";

        public void Showname()
        {
            Console.WriteLine(_name);
        }
    }
}
