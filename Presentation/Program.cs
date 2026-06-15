using BLL;
using DAL;
using BLL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IClassroomRepository>(
    provider => new ClassroomRepository(connectionString!)
);

builder.Services.AddScoped<IReservationRepository>(
    provider => new ReservationRepository(connectionString!)
);
builder.Services.AddScoped<IEnrollmentRepository>(
    provider => new EnrollmentRepository(connectionString!)
);


builder.Services.AddScoped<EnrollmentService>();
builder.Services.AddScoped<ReservationService>();
builder.Services.AddScoped<ReservationApprovalService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();