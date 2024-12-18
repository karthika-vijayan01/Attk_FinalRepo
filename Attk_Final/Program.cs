using Attk_Final.Repository;

namespace Attk_Final
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
          
            builder.Services.AddScoped<IReceptionistRepository, ReceptionistRepository>();
            builder.Services.AddScoped<IBookAppointmentRepository, BookAppointmentRepository>();

            builder.Services.AddScoped<IStaffRepository, StaffRepository>();
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
           // builder.Services.AddScoped<IConsultationRepository, ConsultationRepository>();


            //Register service and repository
            builder.Services.AddScoped<IPharmacistRepository, PharmacistRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Pharma}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
