namespace APS1laborotorna
{   
    public class Company
    {
        private string name;
        private string address;
        private int employeeCount;

        public Company(string name, string address, int employeeCount)
        {
            this.name = name;
            this.address = address;
            this.employeeCount = employeeCount;
        }

        public override string ToString()
        {
            return $"name Company - {this.name} address Company - {this.address}, employee count: {employeeCount}";
        }

    }
    class MiddlewareClass
    {
        private readonly RequestDelegate next;
        
        public MiddlewareClass(RequestDelegate next) { 
            this.next= next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;
            if (path != "/company")
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 101);
                await context.Response.WriteAsync($"Randomy number - {randomNumber}");
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseMiddleware<MiddlewareClass>();
            app.Run(async (context) =>
            {
                Company company = new Company("Company1", "Address1", 100);
                await context.Response.WriteAsync($"Info company:{company.ToString()}");
            });
            app.Run();
        }
    }
}
