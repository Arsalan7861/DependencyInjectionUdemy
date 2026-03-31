using System.Reflection;

namespace DependencyInjection.WebApplication
{
    public class Test : ITest
    {
        public string Name { get; set; } = "Arsalan Khroush";
        public int Age { get; set; }
        public void Method()
        {
            Console.WriteLine("Method called");
        }
    }

    public class AssemblyTest
    {
        public void Method()
        {
            var assembly = Assembly.GetExecutingAssembly(); // bu classin mevcut oldugu projenin assemblysini verir
            var types = assembly.GetTypes()
                .Where(i => i.IsClass
                && !i.IsAbstract
                && typeof(ITest).IsAssignableFrom(i)// i tipinin ITest interface'inden türemiş olup olmadığını kontrol eder, eğer i tipi ITest interface'inden türemişse true döner, aksi takdirde false döner
                ); // assemblydeki tum class
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type); // verilen type'dan bir instance oluşturur, bu instance'ı döndürür. Eğer type'ın parametresiz bir constructor'ı yoksa veya type soyut bir sınıf ise, bu method bir exception fırlatır
                PropertyInfo? propertyInfo = type.GetProperty("Name"); // `Name` adında bir property olup olmadığını kontrol eder, eğer varsa o property'nin bilgilerini döndürür, yoksa null döndürür
                if (propertyInfo is null) continue;

                var value = propertyInfo.GetValue(instance);
                Console.WriteLine(value);

                MethodInfo? methodInfo = type.GetMethod("Method"); // `Method` adında bir method olup olmadığını kontrol eder, eğer varsa o methodun bilgilerini döndürür, yoksa null döndürür
                if (methodInfo is null) continue;
                methodInfo.Invoke(instance, null); // instance üzerinde methodu çalıştırır, methodun parametreleri yoksa null verilir
            }
        }
    }
    public static class AAA
    {

    }
    public interface ITest
    {

    }
    public abstract class BBB
    {
    }
}
