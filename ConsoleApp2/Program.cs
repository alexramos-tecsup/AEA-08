using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();
        static void Main(string[] args)
        {
            FilteringLAMBDA();
            Console.Read();
        }
        static void IntroToLINQ()
        {
            //1.DataSource
            List<int> numbers = new List<int>(new int[7] { 0, 1, 2, 3, 4, 5, 6 });
            
            //2.QueryCreation
            //numQuery is an IEnumerable<int>
            var numQuery =
            from num in numbers
            where(num % 2) == 0
            select num;

            //3.Query execution
            foreach (int num in numQuery)
            {
                Console.Write("{0,1}", num);
            }
        }
        static void IntroToLINQLAMBDA()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
            var nums = numbers.Where(n => n % 2 == 0);

            foreach (int num in nums)
            {
                Console.Write("{0,1} ", num);
            }

        }
        static void DataSource()
        {
            var queryAllCostumers = from cust in context.clientes
                                    select cust;

            foreach (var item in queryAllCostumers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void DataSourceLAMBDA()
        {

            var Clientes = context.clientes;

            foreach (var Cliente in Clientes)
            {
                Console.WriteLine(Cliente.NombreCompañia);
            }

        }

        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;
            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }

        }
        static void FilteringLAMBDA()
        {
            var queryLondonCustomers = 
                context.clientes.Where(c => c.Ciudad == "Londres");

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }

        }
        static void Ordering()
        {
            var queryLondonCustomers3 =
                from cust in context.clientes
                where cust.Ciudad == "London"
                orderby cust.NombreCompañia ascending
                select cust;
            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void OrderingLAMBDA()
        {
            var queryLondonCustomers3 = context.clientes
                .Where(c => c.Ciudad == "Londres")
                .OrderBy(c => c.NombreCompañia);

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }
        static void Grouping()
        {
            var queryCustomersByCity =
                from cust in context.clientes
                group cust by cust.Ciudad;

            //customerGroup es a IGrouping<string> Custumer
            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("    {0}", customer.NombreCompañia);
                }
            }

        }
        static void GroupingLAMBDA()
        {
            var queryCustomersByCity = context.clientes.GroupBy(c => c.Ciudad);

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("     {0}", customer.NombreCompañia);
                }
            }

        }

        static void Grouping2()
        {
            var custQuery =
                from cust in context.clientes
                group cust by cust.Ciudad into custGroup
                where custGroup.Count() > 2
                orderby custGroup.Key
                select custGroup;
            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }
        static void Grouping2LAMBDA()
        {
            var custQuery = context.clientes
                .GroupBy(c => c.Ciudad).Where(c => c.Count() > 2)
                .OrderBy(c => c.Key);

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }

        }
        static void Joining()
        {
            var innerJoinQuery =
                from cust in context.clientes
                join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };
            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
        static void JoiningLAMBDA()
        {
            var queryJoin = context.clientes.Join(context.Pedidos,
              cust => cust.idCliente,
              dist => dist.IdCliente,
              (cust, dist) => new
              {
                  CustomerName = cust.NombreCompañia,
                  DistributorName = dist.PaisDestinatario

              });

            foreach (var item in queryJoin)
            {
                Console.WriteLine(item.CustomerName);
            }

        }
    }
}
